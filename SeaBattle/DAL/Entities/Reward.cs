using System.ComponentModel.DataAnnotations;

namespace GameDBContext.Entities
{
    public class Reward
    {
        public Reward(int battlesPlayed, int battlesWon)
        {
            BattlesPlayed = battlesPlayed;
            BattlesWon = battlesWon;
        }

        [Key]
        public int Id { get; set; }
        public int BattlesPlayed { get; set; }
        public int BattlesWon { get; set; }

        // Navigation Properties

        public virtual IList<User> Users { get; set; }
    }
}
