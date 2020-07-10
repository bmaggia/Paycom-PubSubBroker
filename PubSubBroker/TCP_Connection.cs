using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PubSubBroker
{
    class TCP_Connection
    {

        public static List<TcpClient> TCPClients = new List<TcpClient>();

        public static TcpListener listener = new TcpListener(IPAddress.Any, 13);

        public static async Task StartTCPServer()
        {

            listener.Start();
            Console.WriteLine("Waiting for connection");


            while (true)
            {
                TcpClient newClient = await listener.AcceptTcpClientAsync();

                Console.WriteLine("New Client Connected");
                
                TCPClients.Add(newClient);
                Task newRead = Task.Run(() => StreamRead.BeginStreamRead(newClient.GetStream()));
            }
        }
    }  
}
