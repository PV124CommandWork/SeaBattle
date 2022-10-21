using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        }
    }

    public enum RequestType
    {
        Register, Login, GetRewards, BattleRequest, BattleConfirm, BattleEnded, Fire, Exception
    }
    #endregion
}