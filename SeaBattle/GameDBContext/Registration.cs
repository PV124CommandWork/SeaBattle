using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GameDBContext
{
    public class Registration
    {
        private int _id;
        private DateTime _deadline;
        private string _regCode;
        private int _playerId;


        public int ID { get { return _id; } set { _id = value; } }
        public DateTime Deadline { get { return _deadline; } set { _deadline = value; } }
        public string RegCode
        {
            get { return _regCode; }
            set
            {
                if (value.Length <= 6)
                {
                    _regCode = value;
                }
            }
        }
        public int PlayerId { get { return _playerId; } set { _playerId = value; } }
        [ForeignKey("PlayerId")]
        public virtual Account player { get; set; }
        public Registration(DateTime deadline, string regCode, int playerId)
        {
            this.Deadline = deadline;
            this.RegCode = regCode;
            this.PlayerId = playerId;
        }
    }
}
