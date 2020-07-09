using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Newtonsoft.Json;
using PubSubBroker.Commands;

namespace PubSubBroker
{
    class StreamRead
    {
        static byte[] netBuffer = new byte[1024];
        public static async Task BeginStreamRead(NetworkStream netStream)
        {
            while (true)
            {
                await netStream.ReadAsync(netBuffer);

                Command command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                CommandProcessor.ProcessCommand(command, netStream);

                Array.Clear(netBuffer, 0, 1024);
            }
        }
    }
}
