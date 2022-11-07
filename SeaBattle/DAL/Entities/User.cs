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

        public int? RegistrationId { get; set; }
        [ForeignKey("RegistrationId")]
        public virtual Registration? Registration { get; set; }

        public int? RewardId { get; set; }
        [ForeignKey("RewardId")]
        public virtual Reward? Reward { get; set; }

        public int? CurrentBattleId { get; set; }
        [ForeignKey("CurrentBattleId")]
        public virtual CurrentBattle? CurrentBattle { get; set; }
    }
}
