using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using GameDBContext.Entities;
namespace SeaBattleServer
{
    public static class FriendsManagement
    {
        public static void AddFriend(string UserLogin, string FriendLogin)
        {
            if (DataBaseAccess.DbContext == null)
            {
                throw new Exception();
            }
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == UserLogin select u) as User;
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Login == FriendLogin select u) as User;
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            user.Friends.Add(friend);
            DataBaseAccess.DbContext.Update(user);
            DataBaseAccess.DbContext.SaveChanges();
        }

        public static void AddFriend(string userLogin, int FriendId)
        {
            if (DataBaseAccess.DbContext == null)
            {
                throw new Exception();
            }
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == userLogin select u) as User;
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Id == FriendId select u) as User;
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            user.Friends.Add(friend);
            DataBaseAccess.DbContext.Update(user);
            DataBaseAccess.DbContext.SaveChanges();
        }

        public static void RemoveFriend(string UserLogin, string FriendLogin)
        {
            if (DataBaseAccess.DbContext == null)
            {
                throw new Exception();
            }
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == UserLogin select u) as User;
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Login == FriendLogin select u) as User;
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            bool check = user.Friends.Remove(friend);
            if (check == false)
            {
                throw new Exception();
            }
            else
            {
                DataBaseAccess.DbContext.Update(user);
                DataBaseAccess.DbContext.SaveChanges();
            }

        }

        public static void RemoveFriend(string UserLogin, int FriendId)
        {
            if (DataBaseAccess.DbContext == null)
            {
                throw new Exception();
            }
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == UserLogin select u) as User;
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Id == FriendId select u) as User;
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            bool check = user.Friends.Remove(friend);
            if (check == false)
            {
                throw new Exception();
            }
            else
            {
                DataBaseAccess.DbContext.Update(user);
                DataBaseAccess.DbContext.SaveChanges();
            }
        }
    }
}
