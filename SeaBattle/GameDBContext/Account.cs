using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
namespace GameDBContext
{
    public class Account
    {
        private int _id;
        private string _nickname;
        private string _login;
        private string _password;
        private string _email;
        private int? _registerId;
        private string _friendList;
        private int? _rewardsId;


        public int ID { get { return _id; } set { _id = value; } }
        public string Nickname { get { return _nickname; } set { _nickname = value; } }
        public string Login { get { return _login; } set { _login = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public int? RegisterId { get { return _registerId; } set { _registerId = value; } }
        [ForeignKey("RegisterId")]
        public virtual Registration? Registration { get; set; }
        public string? FriendList { get { return _friendList; } set { _friendList = value; } }
        public int? RewardsId { get { return _rewardsId; } set { _rewardsId = value; } }
        [ForeignKey("RewardsId")]
        public virtual Rewards Rewards { get; set; }

        public List<Friend> Friends;
        public Account(string nickname, string login, string password, string email, string friendsJSON = "")
        {
            Nickname = nickname;
            Login = login;
            Email = email;
            Password = password;
            if (friendsJSON != "")
            {
                Friends = JsonSerializer.Deserialize<List<Friend>>(friendsJSON);
            }
            else
            {
                Friends = new List<Friend>();
            }
        }
        public Account()
        {
            Friends = new List<Friend>();
        }
        public Account(int Id, string nickname, string login, string email, string password, int? registerId, int? rewardsId, string friendsJSON = "")
        {
            this.ID = Id;
            this.Nickname = nickname;
            this.Login = login;
            this.Email = email;
            this.Password = password;
            this.RegisterId = registerId;
            this.RewardsId = rewardsId;
            if(friendsJSON != "")
            {
                FriendList = friendsJSON;
                Friends = JsonSerializer.Deserialize<List<Friend>>(friendsJSON);
            }
            else
            {
                Friends = new List<Friend>();
                FriendList = JsonSerializer.Serialize(Friends);
            }
        }
    }



    public class Friend
    {
        public string Nickname;
    }
}