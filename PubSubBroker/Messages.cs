using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using PubSubBroker.Commands;


namespace PubSubBroker
{
    class Messages
    {
        public static List<Message> BrokerMessages = new List<Message>();

        public static void AddMessage(string topic, string messagebody)
        {
            // index = -1 when topic does not exist.
            int index = BrokerMessages.FindIndex(m => m.Topic == topic);

            if (index >= 0) // Add message to existing topic
            {
                BrokerMessages[index].TopicMessages.Add(messagebody);
                PublishToSubscribers(index);
            }
            else // Create topic if it does not exist
            {
                BrokerMessages.Add(new Message(topic, messagebody));
            }
        }

        public static string CreateTopic(string topic)
        {
            int index = BrokerMessages.FindIndex(m => m.Topic == topic);

            if (index >= 0) // Topic exists
            {
                return topic + " already exists";
            }
            else // Topic does not exist -- the else here could also be removed
            { 
                BrokerMessages.Add(new Message(topic));
                return topic + " successfully created";
            }
        }

        public static string DeleteTopic(string topic)
        {
            int index = BrokerMessages.FindIndex(m => m.Topic == topic);

            if (index >= 0) // Topic exists
            {
                BrokerMessages.RemoveAt(index);
                return topic + " successfully deleted";
            }
            else // Topic does not exist
            {
                return topic + " does not exist";
            }
        }

        static void PublishToSubscribers(int index)
        {
            Message message = BrokerMessages[index];
            // Get last (newest) message from list
            Command command = new Command(CommandType.NewMessage, message.Topic, message.TopicMessages.Last());

            // Send to all subscribers
            foreach(NetworkStream netstream in message.Subscribers)
            {
                SendMessage.Send(command, netstream);
            }
        }

        public static string AddSubscriber(string topic, NetworkStream netstream)
        {
            // Check if topic exists
            int index = BrokerMessages.FindIndex(m => m.Topic == topic);
            if (index >= 0)
            {
                int subscriberIndex = BrokerMessages[index].Subscribers.IndexOf(netstream);
                if (subscriberIndex >= 0) // If user is subscribed
                {
                    return "Already subscribed to: " + topic;
                }
                else
                {
                    BrokerMessages[index].Subscribers.Add(netstream);
                    return "Successfully subscribed to: " + topic;
                }
            }
            else
            {
                return topic + " does not exist";
            }
        }

        public static string RemoveSubscriber(string topic, NetworkStream netstream)
        {
            // Check if topic exists
            int index = BrokerMessages.FindIndex(m => m.Topic == topic);
            if (index >= 0)
            {
                int subscriberIndex = BrokerMessages[index].Subscribers.IndexOf(netstream);
                if (subscriberIndex >= 0) // If user is subscribed
                {
                    BrokerMessages[index].Subscribers.Remove(netstream);
                    return "Successfully unsubscribed to: " + topic;
                }
                else
                {
                    return "Not subscribed to: " + topic;
                }
            }
            else
            {
                return topic + " does not exist";
            }
        }
    }

    class Message
    {
        public string Topic;

        public List<string> TopicMessages = new List<string>();

        public List<NetworkStream> Subscribers = new List<NetworkStream>();

        public Message(string topic, string message)
        {
            Topic = topic;
            TopicMessages.Add(message);
        }

        public Message(string topic)
        {
            Topic = topic;
        }
    }
}
