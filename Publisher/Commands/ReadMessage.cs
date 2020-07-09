using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publisher.Commands
{
    class ReadMessage
    {
        static byte[] netBuffer = new byte[1024];
        public static Command Read()
        {
            TCP_Connection.TCPNetworkStream.Read(netBuffer);

            Command command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

            Array.Clear(netBuffer, 0, 1024);

            return command;
        }
    }
}
