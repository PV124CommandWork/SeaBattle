using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GameDBContext
{
    public class CurrentBattles
    {
        private int _id;
        private int _firstPlayerId;
        private int _secondPlayerId;
        private bool _move;//true - first player move / false - second player move
        private string _firstFieldData;
        private string _secondFieldData;


        
        public int ID { get { return _id; } set { _id = value; } }
        public int FirstPlayerId { get { return _firstPlayerId; } set { _firstPlayerId = value; } }
        
        [ForeignKey("FirstPlayerId")]
        public virtual Account? firstPlayer { get; set; }

        public int SecondPlayerId { get { return _secondPlayerId; } set { _secondPlayerId = value; } }

        [ForeignKey("SecondPlayerId")]
        public virtual Account? secondPlayer { get; set; }
        public bool Move { get { return _move; } set { _move = value; } }
        public string FirstFieldData { get { return _firstFieldData; } set { _firstFieldData = value; } }
        public string SecondFieldData { get { return _secondFieldData; } set { _secondFieldData = value; } }
        public CurrentBattles(Account firstPlayer, Account secondPlayer, string firstFieldData = "", string secondFieldData = ""/*Add default value*/)
        {
            FirstPlayerId = firstPlayer.ID;
            this.firstPlayer = firstPlayer;
            SecondPlayerId = secondPlayer.ID;
            this.secondPlayer = secondPlayer;
            Random rnd = new Random();
            this.Move = rnd.NextSingle() > 0.5;
        }
        public CurrentBattles(int firstPlayerId, int secondPlayerId,bool Move, string firstFieldData = "", string secondFieldData = ""/*Add default value*/)
        {

            this.FirstPlayerId = firstPlayerId;
            this.SecondPlayerId = secondPlayerId;
            this.Move = Move;
            this.FirstFieldData = firstFieldData;
            this.SecondFieldData = secondFieldData;
        }
    }
}
