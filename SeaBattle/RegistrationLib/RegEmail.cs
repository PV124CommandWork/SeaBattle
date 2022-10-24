using GameDBContext;
using System.Net.Mail;
using System.Net;
namespace RegistrationLib
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
        private GameDbContext _db;
        private UserData _userData;
        private Account _player;
        public GameDbContext db { get { return _db; } set { _db = value; } }
        public UserData userData { get { return _userData; } set { _userData = value; } }
        public Account Player { get { return _player; } set { _player = value; } }
        public RegEmail(GameDbContext db, UserData userData)
        {
            this.db = db;
            this.userData = userData;
        }
        public RegEmail(GameDbContext db, string nickname, string email, string login, string password)
        {
            this.userData = new UserData { Email = email, Login = login, Password = password, Nickname = nickname };
            this.db = db;
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
            while(code.Length < 6)
            {
                code += rnd.Next(0,10);
            }
            string text = getText(code);
            client.Send("battlesea427@gmail.com", userData.Email, "Sea battle registration", text);
            Account player = new Account()
            {
                Nickname = userData.Nickname,
                Login = userData.Login,
                Password = userData.Password,
                Email = userData.Email,
                FriendList = String.Empty
            };
            Registration reg = new Registration(DateTime.Now.AddMinutes(15), code, player.ID);
            player.RegisterId = reg.ID;
            db.Accounts.Add(player);
            db.Registrations.Add(reg);
            db.SaveChanges();
        }

        public bool checkCode(string userInput)
        {
            bool result = (userInput == ( from r in db.Registrations where r.PlayerId == Player.ID select r.RegCode).First()) && (DateTime.Now < ( from r in db.Registrations where r.PlayerId == Player.ID select r.Deadline).First());
            if (result)
            {
                Player.RegisterId = 0;
                Player.Friends = new List<Friend>();
                Player.FriendList = "";
                Rewards rewards = new Rewards(0, 0);
                Player.RewardsId = rewards.ID;
                db.Accounts.Update(Player);
            }
            return result;
        
        }
    }
}