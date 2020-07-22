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
            string output = "";
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

            Command command = new Command(CommandType.MessageTypeQuery, "", output);

            SendMessage.Send(command, netstream);

            // I think there is a way you could utilize your existing Message class to make this a little simpler. 
            // That is to say, I think the logic of the Query function could be encapsulated in the Message class itself.
            // E.g. the topic & subscribers lists could be a dictionary that would give you a hash lookup. Then you could reduce
            // lines 11-25 to a couple of lines with a linq query. But yours is good approach as well.
        }
    }
}
