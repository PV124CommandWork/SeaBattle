using System.ComponentModel.DataAnnotations.Schema;

namespace GameDBContext.Entities
{
    public class User
    {
        public User(string nickname, string login, string password, string email)
        {
            Nickname = nickname;
            Login = login;
            Email = email;
            Password = password;
        }

        public User(int id, string nickname, string login, string password, string email, int registerId, int rewardsId)
        {
            Id = id;
            Nickname = nickname;
            Login = login;
            Email = email;
            Password = password;
            RegistrationId = registerId;
            RewardId = rewardsId;
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        // Navigation Properties

        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }
        public virtual Registration Registration { get; set; }

        [ForeignKey("Reward")]
        public int RewardId { get; set; }
        public virtual Reward Reward { get; set; }

        public virtual CurrentBattle CurrentBattle { get; set; }

        public virtual IList<User> Friends { get; set; } // TODO : check not to add yourself
    }
}
