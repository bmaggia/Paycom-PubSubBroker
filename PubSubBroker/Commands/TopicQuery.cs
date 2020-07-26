using System.Linq;
using System.Net.Sockets;

namespace PubSubBroker.Commands
{
    class TopicQuery
    {
        public static void Query(NetworkStream netstream)
        {
            // Get all topics in messages
            string[] topics = Messages.BrokerMessages.Select(m => m.Topic).ToArray();

            bool[] subscribed = Messages.BrokerMessages.Select(s => s.Subscribers.Contains(netstream)).ToArray();

            // Format string for clients
            var output = "";
            for(int i = 0; i < topics.Length; i++)
            {
                if (subscribed[i])
                {
                    output += "*";
                }
                output += topics[i];
                output += "\n";
            }

            output += "*Currently subscribed to";

            var command = new Command(CommandType.MessageTypeQuery, "", output);

            SendMessage.Send(command, netstream);
        }
    }
}
