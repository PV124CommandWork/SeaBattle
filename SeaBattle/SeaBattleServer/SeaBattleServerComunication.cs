using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeaBattleServer;
using RegistrationNS;
using GameDBContext.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace SeaBattleServerComunication
{
    #region Request Class
    public class Request
    {
        public string Login = null;
        public string Password = null;
        public List<string> Data = null;
        public RequestType ReqType;

        public void Execute()
        {
            Request request = new Request() { Data = new List<string>() };
            switch (ReqType)
            {
                case RequestType.Login:
                    {
                        request.ReqType = RequestType.Login;
                        request.Data.Add(
                            ((from users in DAL.DataBaseAccess.DbContext.Users
                              where users.Login == Login
                              && users.Password == Password
                              && users.Registration == null
                              select users).FirstOrDefault() != null).ToString());
                        break;
                    }
                case RequestType.Register:
                    {
                        RegEmail regEmail = new RegEmail(DAL.DataBaseAccess.DbContext, Data[0]/*NickName*/, Data[1]/*Email*/, Login, Password);
                        if (Data.Count == 2) //2 це нікнейм і пошта, 3 це код
                        {
                            bool isLoginExist = (from u in DAL.DataBaseAccess.DbContext.Users
                                                 where u.Login == Login && u.Registration == null
                                                 select u).FirstOrDefault() == null;
                            if (isLoginExist)
                            {
                                request.Data.Add("This login already exists!");
                                break;
                            }
                            regEmail.sendCode();
                            request.Data.Add(true.ToString());
                        }
                        else
                        {
                            request.Data.Add(true.ToString());
                            if (regEmail.checkCode(Data[2]))//code
                            {
                                request.Data.Add(true.ToString());
                            }
                            else
                            {
                                request.Data.Add("Wrong code!");
                            }
                        }
                        break;
                    }
                case RequestType.GetRewards:
                    {
                        Reward rewards = (from u in DataBaseAccess.DbContext.Users
                                          join r in DataBaseAccess.DbContext.Rewards on u.Reward equals r
                                          where u.Registration == null
                                          && u.Login == Login
                                          && u.Password == Password
                                          select r).FirstOrDefault();
                        request.ReqType = RequestType.GetRewards;
                        if (rewards != null)
                        {
                            request.Data.Add(rewards.BattlesWon.ToString());
                            request.Data.Add(rewards.BattlesPlayed.ToString());
                        }
                        break;
                    }
                case RequestType.AddFriend:
                    {
                        request.ReqType = RequestType.AddFriend;
                        try
                        {
                            FriendsManagement.AddFriend(Login, Data[0]);
                            request.Data.Add("User added to friends!");
                        }
                        catch
                        {
                            request.Data.Add("User not found!");
                        }
                        break;
                    }
                case RequestType.RemoveFriend:
                    {
                        request.ReqType = RequestType.RemoveFriend;
                        try
                        {
                            FriendsManagement.RemoveFriend(Login, Data[0]);
                            request.Data.Add("User removed from friends!");
                        }
                        catch
                        {
                            request.Data.Add("Something went wrong!");
                        }
                        break;
                    }
                case RequestType.BattleRequest:
                    {
                        BattleManagement.createBattle(Login, Data[0], "", "");
                        break;
                    }
                case RequestType.BattleConfirm:
                    {
                        break;
                    }
                case RequestType.BattleEnded:
                    {
                        BattleManagement.deleteBattle((from u in DataBaseAccess.DbContext.Users
                                                       join b in DataBaseAccess.DbContext.CurrentBattles on u.CurrentBattle equals b
                                                       where u.Login == Login
                                                       select u.Id).FirstOrDefault());
                        break;
                    }
                case RequestType.Fire:
                    {
                        break;
                    }
                default: return;
            }
            ServerObj.SendToClientByLogin(Login, request);
        }
    }


    public enum RequestType
    {
        Register, Login, GetRewards, BattleRequest, BattleConfirm, BattleCanceled, BattleEnded, Fire, Exception, PlayerReady, AddFriend, RemoveFriend
    }
    #endregion
}