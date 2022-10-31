using System.ComponentModel.DataAnnotations;

namespace GameDBContext.Entities
{
    public class CurrentBattle
    {
        public CurrentBattle(string firstFieldData, string secondFieldData)
        {
            FirstFieldData = firstFieldData;
            SecondFieldData = secondFieldData;

            Random rnd = new Random();
            Move = rnd.NextSingle() > 0.5;
        }

        public CurrentBattle(string firstFieldData, string secondFieldData, bool move)
        {
            FirstFieldData = firstFieldData;
            SecondFieldData = secondFieldData;
            Move = move;
        }

        [Key]
        public int Id { get; set; }
        public bool Move { get; set; } // true - first player move / false - second player move
        public string FirstFieldData { get; set; }
        public string SecondFieldData { get; set; }

        // Navigation Properties

        public virtual IList<User> Users { get; set; }
    }
}
