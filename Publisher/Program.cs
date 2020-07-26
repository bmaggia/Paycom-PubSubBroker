using System;
using Publisher.Commands;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInput = "";

            TCP_Connection tcpConnection = new TCP_Connection();
            bool connected = tcpConnection.ConnectTCP();

            if (connected)
            {
                Console.WriteLine("Connected to server. Type \"Help\" for a list of commands.");
            }

            while (userInput != "quit" && connected)
            {
                userInput = Console.ReadLine().ToLower();

                if (userInput == "help")
                {
                    Console.WriteLine("Commands: \n" +
                        "Compose Message \n" +
                        "Query Topics \n" +
                        "Delete Topic \n");
                }
                else if (userInput == "compose message")
                {
                    ComposeMessage.Compose();
                }
                else if (userInput == "query topics")
                {
                    QueryTopics.Query();

                }
                else if (userInput == "create topic")
                {
                    ModifyTopics.CreateTopic();
                }
                else if (userInput == "delete topic")
                {
                    ModifyTopics.DeleteTopic();
                }
            }
        }
    }
}
