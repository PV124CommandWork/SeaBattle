using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using GameDBContext.Entities;
namespace SeaBattleServer
{
    public static class BattleManagement
    {
        public static void createBattle(int firstUserId, int secondUserId, string firstFieldData, string secondFieldData)
        {
            CurrentBattle temp = new CurrentBattle(firstFieldData, secondFieldData);
            DataBaseAccess.DbContext.CurrentBattles.Add(temp);
            User firstUser = (from u in DataBaseAccess.DbContext.Users where u.Id == firstUserId select u) as User;
            User secondUser = (from u in DataBaseAccess.DbContext.Users where u.Id == secondUserId select u) as User;
            if(firstUser == null || secondUser == null)
            {
                throw new Exception();
            }
            firstUser.CurrentBattleId = temp.Id;
            secondUser.CurrentBattleId = temp.Id;
            DataBaseAccess.DbContext.Update(firstUser);
            DataBaseAccess.DbContext.Update(secondUser);
            DataBaseAccess.DbContext.SaveChanges();
        }
        public static void createBattle(string firstUserLogin, string secondUserLogin, string firstFieldData, string secondFieldData)
        {
            CurrentBattle temp = new CurrentBattle(firstFieldData, secondFieldData);
            DataBaseAccess.DbContext.CurrentBattles.Add(temp);
            User firstUser = (from u in DataBaseAccess.DbContext.Users where u.Login == firstUserLogin select u) as User;
            User secondUser = (from u in DataBaseAccess.DbContext.Users where u.Login == secondUserLogin select u) as User;
            if (firstUser == null || secondUser == null)
            {
                throw new Exception();
            }
            firstUser.CurrentBattleId = temp.Id;
            secondUser.CurrentBattleId = temp.Id;
            DataBaseAccess.DbContext.Update(firstUser);
            DataBaseAccess.DbContext.Update(secondUser);
            DataBaseAccess.DbContext.SaveChanges();
        }
    }
}
