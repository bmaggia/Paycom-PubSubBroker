using System;

namespace Publisher.Commands
{
    class ModifyTopics
    {

        public static void CreateTopic()
        {
            Console.WriteLine("Name of Topic:");

            var topic = Console.ReadLine();

            var command = new Command(CommandType.CreateTopic, topic);

            SendMessage.Send(command);
        }

        public static void DeleteTopic()
        {
            Console.WriteLine("Name of Topic:");

            var topic = Console.ReadLine();

            var command = new Command(CommandType.DeleteTopic, topic);

            SendMessage.Send(command);
        }
    }
}
