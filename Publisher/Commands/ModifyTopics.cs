using System;
using System.Collections.Generic;
using System.Text;

namespace Publisher.Commands
{
    class ModifyTopics
    {

        public static void CreateTopic()
        {
            Console.WriteLine("Name of Topic:");

            string topic = Console.ReadLine();

            Command command = new Command(CommandType.CreateTopic, topic, "");

            SendMessage.Send(command);
        }

        public static void DeleteTopic()
        {
            Console.WriteLine("Name of Topic:");

            string topic = Console.ReadLine();

            Command command = new Command(CommandType.DeleteTopic, topic, "");

            SendMessage.Send(command);
        }
    }
}
