using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Publisher.Commands;

namespace Publisher
{
    class StreamRead
    {
        public static List<byte[]> ReadBuffers = new List<byte[]>();
        static readonly byte[] netBuffer = new byte[1024];
        public static async Task BeginStreamRead()
        {
            while (true)
            {
                await TCP_Connection.TCPNetworkStream.ReadAsync(netBuffer);

                var command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                if (command.CommandType != CommandType.Poll)
                {
                    Console.WriteLine(command.MessageBody);
                }

                Array.Clear(netBuffer, 0, 1024);
            }
        }
    }
}
