using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeaBattleServer;
using RegistrationNS;

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
                             select users).FirstOrDefault() != null).ToString());
                        break;
                    }
                case RequestType.Register:
                    {
                        RegEmail regEmail = new RegEmail(DAL.DataBaseAccess.DbContext, Data[0]/*NickName*/, Data[1]/*Email*/, Login, Password);
                        if (Data.Count == 2) //2 це нікнейм і пошта, 3 це код
                        {
                            regEmail.sendCode();
                            request.Data.Add("Code was sended!");
                        }
                        else
                        {
                            if (regEmail.checkCode(Data[2]))//code
                            {
                                request.Data.Add("Successfully registered!");
                            }
                            else
                            {
                                request.Data.Add("Something wet wrong!");
                            }
                        }
                        break;
                    }
                case RequestType.GetRewards:
                    {
                        break;
                    }
                case RequestType.BattleRequest:
                    {
                        break;
                    }
                case RequestType.BattleConfirm:
                    {
                        break;
                    }
                case RequestType.BattleEnded:
                    {
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
        Register, Login, GetRewards, BattleRequest, BattleConfirm, BattleEnded, Fire, Exception
    }
    #endregion
}