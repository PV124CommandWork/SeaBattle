using System.Net.Mail;
using System.Net;
using GameDBContext.Data;
using GameDBContext.Entities;

namespace RegistrationNS
{
    public struct UserData
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class RegEmail
    {
        public GameDbContext Db { get; set; }
        public UserData UserData { get; set; }
        public RegEmail(GameDbContext db, UserData userData)
        {
            this.Db = db;
            this.UserData = userData;
        }
        public RegEmail(GameDbContext db, string nickname, string email, string login, string password)
        {
            this.UserData = new UserData { Email = email, Login = login, Password = password, Nickname = nickname };
            this.Db = db;
        }

        public string getText(string code)
        {
            return "Your registration code for Sea battle game: " + code;
        }

        public void sendCode()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("battlesea427@gmail.com", "oqsrdwibrohabilf"),
                EnableSsl = true
            };
            Random rnd = new Random();
            string code = "";
            while (code.Length < 6)
            {
                code += rnd.Next(0, 10);
            }
            string text = getText(code);
            client.Send("battlesea427@gmail.com", UserData.Email, "Sea battle registration", text);
            User player = new User(UserData.Nickname, UserData.Login, UserData.Password, UserData.Email);
            Registration reg = new Registration(DateTime.Now.AddMinutes(15), code);
            Db.Registrations.Add(reg);
            Db.SaveChanges();
            player.RegistrationId = reg.Id;
            Db.Users.Add(player);
            Db.SaveChanges();
        }

        public bool checkCode(string userInput)
        {
            User? Player = (from u in Db.Users
                            join r in Db.Registrations on u.Registration equals r
                            where r.RegCode == userInput && r.Deadline > DateTime.Now
                            select u).FirstOrDefault();

            if (Player != null)
            {
                Player.RegistrationId = null;
                Player.Friends = new List<User>();
                Reward rewards = new Reward(0, 0);
                Db.Add(rewards);
                Db.SaveChanges();
                Player.RewardId = rewards.Id;
                Db.Users.Update(Player);
                Db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}