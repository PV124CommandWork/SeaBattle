using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GameDBContext
{
    public class Rewards
    {
        private int _id;
        private int _battlesPlayed;
        private int _battlesWon;


        public int ID { get { return _id; } set { _id = value; } }
        public int BattlesPlayed { get { return _battlesPlayed; } set { _battlesPlayed = value; } }
        public int BattlesWon { get { return _battlesWon; } set { _battlesWon = value; } }
        public Rewards(int BattlesPlayed, int BattlesWon)
        {
            this.BattlesPlayed = BattlesPlayed;
            this.BattlesWon = BattlesWon;
        }
    }
}
