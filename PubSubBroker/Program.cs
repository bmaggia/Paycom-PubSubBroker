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
            // I'm fairly certain the TCPServer variable is not doing anything. You can also use a method group here. So the line would look as follows:
            // Task.Run(TCP_Connection.StartTCPServer);
            Task TCPServer = Task.Run(() => TCP_Connection.StartTCPServer());

            string userInput = "";
            while(userInput != "Quit")
            {
                userInput = Console.ReadLine();
            }

            TCP_Connection.listener.Stop();
        }
    }
}
