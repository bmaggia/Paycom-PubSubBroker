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

        public static TcpListener Listener = new TcpListener(IPAddress.Any, 13);

        public static async Task StartTCPServer()
        {

            Listener.Start();
            Console.WriteLine("Waiting for connection");


            while (true)
            {
                TcpClient newClient = await Listener.AcceptTcpClientAsync();

                Console.WriteLine("New Client Connected");
                
                TCPClients.Add(newClient);
                _ = Task.Run(() => StreamRead.BeginStreamRead(newClient.GetStream())); // not having a variable leads to a warning. Is this the correct way to get rid of that warning?
            }
        }
    }  
}
