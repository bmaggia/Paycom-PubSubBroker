
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Publisher
{
    class TCP_Connection
    {
        private const int portNum = 13;
        private const string hostName = "127.0.0.1";

        public static TcpClient ClientConnection;
        public static NetworkStream TCPNetworkStream;

        public bool ConnectTCP()
        {
            try
            {
                ClientConnection = new TcpClient(hostName, portNum);

                TCPNetworkStream = ClientConnection.GetStream();

                Task newRead = Task.Run(() => StreamRead.BeginStreamRead());

                return true;
            }
            catch
            {
                Console.WriteLine("Can't connect to broker, Try again? (y/n)");

                string input = Console.ReadLine();
                if (input == "y")
                {
                    ConnectTCP();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
