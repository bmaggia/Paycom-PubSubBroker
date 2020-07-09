using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publisher.Commands
{
    class SendMessage
    {
        public static void Send(Command command)
        {
            string jsonString = JsonConvert.SerializeObject(command);

            TCP_Connection.TCPNetworkStream.Write(Encoding.ASCII.GetBytes(jsonString));
        }
    }
}
