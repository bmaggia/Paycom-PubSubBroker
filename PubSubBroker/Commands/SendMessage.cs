using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PubSubBroker.Commands
{
    class SendMessage
    {
        public static void Send(Command command, NetworkStream netstream)
        {
            string jsonString = JsonConvert.SerializeObject(command);

            netstream.Write(Encoding.ASCII.GetBytes(jsonString));
        }
    }
}
