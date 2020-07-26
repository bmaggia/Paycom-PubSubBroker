using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PubSubBroker
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(TCP_Connection.StartTCPServer);

            var userInput = "";
            while(userInput != "Quit")
            {
                userInput = Console.ReadLine();
            }

            TCP_Connection.Listener.Stop();
        }
    }
}
