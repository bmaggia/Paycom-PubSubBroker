using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace PubSubBroker.Commands
{
    class SendMessage
    {
        public static bool Send(Command command, NetworkStream netstream)
        {
            // good Json serialization
            string jsonString = JsonConvert.SerializeObject(command);

            try
            {
                netstream.Write(Encoding.ASCII.GetBytes(jsonString));
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
