using System;

namespace Subscriber.Commands
{
    class Subscription
    {
        public static void NewSubscription()
        {
            Console.WriteLine("Topic Name:");
            string topic = Console.ReadLine();

            Command command = new Command(CommandType.Subscribe, topic);

            SendMessage.Send(command);
        }

        public static void RemoveSubscription()
        {
            Console.WriteLine("Topic Name:");
            string topic = Console.ReadLine();

            Command command = new Command(CommandType.Unsubscribe, topic);

            SendMessage.Send(command);
        }
    }
}
