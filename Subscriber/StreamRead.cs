using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Newtonsoft.Json;
using Subscriber.Commands;

namespace Subscriber
{
    class StreamRead
    {
        public static List<byte[]> ReadBuffers = new List<byte[]>();
        static byte[] netBuffer = new byte[1024];
        public static async Task BeginStreamRead()
        {
            while (true)
            {
                await TCP_Connection.TCPNetworkStream.ReadAsync(netBuffer);

                Command command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                CommandProcessor.ProcessCommand(command);

                Array.Clear(netBuffer, 0, 1024);
            }
        }
    }
}
