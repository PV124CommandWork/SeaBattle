using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeaBattle;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media;
using System.Windows.Controls;
using SeaBattle.UserControls;
using System.Windows.Threading;

namespace SeaBattleServerComunication
{
    #region Server
    class ServerConnection
    {
        private const int port = 8008;
        private const string hostname = "127.0.0.1";

        public static TcpClient client = null;
        public static NetworkStream NetStream = null;

        public static void Connect()
        {
            try
            {
                client = new TcpClient(hostname, port);
                NetStream = client.GetStream();
                Task.Run(() =>
                {
                    ReceiveFromServer.Listen();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Server is down...");
            }
        }
    }
    #endregion
    #region ServerComunication
    class ReceiveFromServer
    {
        public static void Listen()
        {
            //try
            //{
            while (true)
            {
                StringBuilder builder = new StringBuilder();

                byte[] data = new byte[256];
                do
                {
                    int bytes = ServerConnection.NetStream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (ServerConnection.NetStream.DataAvailable);

                Request request = JsonConvert.DeserializeObject<Request>(builder.ToString());
                if (request == null)
                {
                    continue;
                }
                switch (request.ReqType)
                {
                    case RequestType.Login:
                        {
                            if (request.Data[0] == true.ToString())
                            {
                                MessageBox.Show("Congratulation! Successfully logined!");
                            }
                            else
                            {
                                UC_LoginPage.showException.Invoke("Wrong login or password!");
                            }
                            break;
                        }
                    case RequestType.Register:
                        {
                            MessageBox.Show(request.Data[0]);
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
                    default: throw new Exception();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    ServerConnection.NetStream?.Close();
            //    ServerConnection.client?.Close();
            //    Environment.Exit(0);
            //}
        }
    }

    class SendToServer
    {
        public static void SendRegisterData(string userName, string login, string password, string email, string registerCode = "")
        {
            Request request = new Request()
            {
                Login = login, 
                Password = password,
                Data = new List<string>() { userName, email },
                ReqType = RequestType.Register
            };
            if(registerCode != "")
            {
                request.Data.Add(registerCode);
            }

            SendRequestToServer(request);
        }
        public static void SendLoginData(string login, string password)
        {
            Request request = new Request()
            {
                Login = login,
                Password = password,
                ReqType = RequestType.Login
            };

            SendRequestToServer(request);
        }
        public static void SendRequestToServer(Request request)
        {
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
            ServerConnection.NetStream.Write(data, 0, data.Length);
        }
    }
    #endregion
    #region Request Class
    public class Request
    {
        public string Login = null;
        public string Password = null;
        public List<string> Data = null;
        public RequestType ReqType;
    }

    public enum RequestType
    {
        Register, Login, GetRewards, BattleRequest, BattleConfirm, BattleEnded, Fire, Exception
    }
    #endregion
}