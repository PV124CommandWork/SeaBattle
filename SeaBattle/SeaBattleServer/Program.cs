using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeaBattleServer
{
    class Program
    {
        //TestServer
        static void Main(string[] args)
        {
            DAL.DataBaseAccess.ConnectToDatabase();
            ServerObj serverObj = new ServerObj(8008);
            Thread listen = new Thread(serverObj.Listener);
            listen.Start();
            listen.Join();
        }
    }
}
