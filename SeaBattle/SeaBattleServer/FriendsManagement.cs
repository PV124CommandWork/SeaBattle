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
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == UserLogin && u.Registration == null select u).FirstOrDefault();
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Login == FriendLogin && u.Registration == null select u).FirstOrDefault();
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            bool isRequested = (from fr in DataBaseAccess.DbContext.Friends
                                where fr.User1 == user
                                && fr.User2 == friend
                                select fr).FirstOrDefault() != null;
            if (isRequested)
            {
                throw new Exception();
            }
            Friend f = new Friend() { User1 = user, User2 = friend };
            DataBaseAccess.DbContext.Friends.Add(f);
            DataBaseAccess.DbContext.SaveChanges();
        }

        public static void RemoveFriend(string UserLogin, string FriendLogin)
        {
            if (DataBaseAccess.DbContext == null)
            {
                throw new Exception();
            }
            User user = (from u in DataBaseAccess.DbContext.Users where u.Login == UserLogin && u.Registration == null select u).FirstOrDefault();
            User friend = (from u in DataBaseAccess.DbContext.Users where u.Login == FriendLogin && u.Registration == null select u).FirstOrDefault();
            if (user == null || friend == null)
            {
                throw new Exception();
            }
            var f = (from fr in DataBaseAccess.DbContext.Friends
                     where fr.User1 == user && fr.User2 == friend
                     select fr).FirstOrDefault();

            if (f == null)
            {
                throw new Exception();
            }
            else
            {
                DataBaseAccess.DbContext.Friends.Remove(f);
                DataBaseAccess.DbContext.SaveChanges();
            }

        }
    }
}
