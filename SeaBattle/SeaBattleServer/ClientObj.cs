using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleServer
{
    public class ClientObj
    {
        protected internal string Id { get; private set; }
        protected internal string UserName { get; private set; } = "NN";
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        ServerObj server;
        public ClientObj(TcpClient tcpClient, ServerObj serverObj)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            this.server = serverObj;
            this.server.AddConnection(this);
        }
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                while (true)
                {
                    string message = GetMessage();
                    Request request = JsonConvert.DeserializeObject<Request>(message);
                    if (request.ReqType == RequestType.Login || request.ReqType == RequestType.Register)
                    {
                        UserName = request.Login;
                    }
                    request.Execute();
                    //Console.WriteLine($"{UserName}: {message}");
                    //byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Request() { ReqType = RequestType.Login, Data = new List<string>() { "Succed" } }));
                    //Stream.Write(data, 0, data.Length);
                }
            }
            catch
            {

            }
            finally
            {
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[100];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);
            return builder.ToString();
        }

        public void Close()
        {
            Stream?.Close();
            client?.Close();
        }
    }
}
