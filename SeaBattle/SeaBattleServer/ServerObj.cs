using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace SeaBattleServer
{
    public class ServerObj
    {
        static TcpListener server;
        private int portListening;
        private IPAddress ipListening = IPAddress.Parse("127.0.0.1");
        static List<ClientObj> clients = new List<ClientObj>();//all connections
        public ServerObj(int portListening)
        {
            this.portListening = portListening;
        }

        protected internal void AddConnection(ClientObj clientObj)
        {
            clients.Add(clientObj);
        }

        protected internal void RemoveConnection(string id)
        {
            //find connections by id
            ClientObj client = clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                clients.Remove(client);
            }
        }

        public void Listener()
        {
            while (true)
            {
                try
                {
                    server = new TcpListener(ipListening, this.portListening);
                    server.Start();

                    while (true)
                    {
                        TcpClient tcpClient = server.AcceptTcpClient();
                        ClientObj clientObj = new ClientObj(tcpClient, this);
                        Task clientTask = new Task(clientObj.Process);
                        clientTask.Start();
                    }
                }
                catch (Exception ex)
                {
                    Disconnect();
                }
            }
        }

        public static void SendToClientByLogin(string login, SeaBattleServerComunication.Request request)
        {
            foreach (var user in clients)
            {
                if (user.UserName == login)
                {
                    byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                    user.Stream.Write(data, 0, data.Length);
                    break;
                }
            }
        }

        public void Disconnect()
        {
            server.Stop();
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
        }
    }
}