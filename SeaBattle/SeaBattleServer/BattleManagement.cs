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
        public static void createBattle(string firstUserLogin, string secondUserLogin, string firstFieldData, string secondFieldData)
        {
            User firstUser = (from u in DataBaseAccess.DbContext.Users where u.Login == firstUserLogin && u.Registration == null select u).FirstOrDefault();
            User secondUser = (from u in DataBaseAccess.DbContext.Users where u.Login == secondUserLogin && u.Registration == null select u).FirstOrDefault();
            if (firstUser == null || secondUser == null)
            {
                throw new Exception("User not found!");
            }
            if (firstUser.CurrentBattleId != null || secondUser.CurrentBattleId != null)
            {
                throw new Exception("User already have a battle request!");
            }
            CurrentBattle temp = new CurrentBattle(firstFieldData, secondFieldData);
            DataBaseAccess.DbContext.CurrentBattles.Add(temp);
            firstUser.CurrentBattle = temp;
            secondUser.CurrentBattle = temp;
            DataBaseAccess.DbContext.Update(firstUser);
            DataBaseAccess.DbContext.Update(secondUser);
            DataBaseAccess.DbContext.SaveChanges();
        }

        public static void deleteBattle(int? battleId)
        {
            if (battleId != null)
            {
                CurrentBattle temp = (from b in DataBaseAccess.DbContext.CurrentBattles where b.Id == battleId select b).FirstOrDefault();
                var users = (from u in DataBaseAccess.DbContext.Users where u.CurrentBattleId == battleId select u);
                if (temp == null)
                {
                    throw new Exception();
                }
                foreach (var item in users)
                {
                    item.CurrentBattle = null;
                    item.CurrentBattleId = null;
                }
                DataBaseAccess.DbContext.Users.UpdateRange(users);
                DataBaseAccess.DbContext.CurrentBattles.Remove(temp);
                DataBaseAccess.DbContext.SaveChanges();
            }
        }
        public static void completeBattle(string winnerLogin)
        {
            CurrentBattle battle = (from b in DataBaseAccess.DbContext.CurrentBattles where b.Users[0].Login == winnerLogin || b.Users[1].Login == winnerLogin select b).FirstOrDefault();
            if(battle == null)
            {
                throw new Exception();
            }
            User winner = (from u in battle.Users where u.Login == winnerLogin select u).FirstOrDefault();
            User loser = (from u in battle.Users where u.Login != winnerLogin select u).FirstOrDefault();
            if (winner == null || loser == null) {
                throw new Exception();
            }
            winner.Reward.BattlesPlayed++;
            loser.Reward.BattlesPlayed++;
            winner.Reward.BattlesWon++;
            deleteBattle(battle.Id);
            DataBaseAccess.DbContext.Rewards.Update(winner.Reward);
            DataBaseAccess.DbContext.Rewards.Update(loser.Reward);
            DataBaseAccess.DbContext.Users.Update(winner);
            DataBaseAccess.DbContext.Users.Update(loser);
            DataBaseAccess.DbContext.SaveChanges();
        }
    }
}
 