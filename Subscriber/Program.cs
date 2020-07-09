using System;
using System.Text;
using Subscriber.Commands;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "";

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
                    Console.WriteLine("Commands:");
                    Console.WriteLine("Subscribe");
                    Console.WriteLine("Unsubscribe");
                    Console.WriteLine("Query Topics");

                }
                else if (userInput == "subscribe")
                {
                    Subscription.NewSubscription();
                }
                else if (userInput == "unsubscribe")
                {
                    Subscription.RemoveSubscription();
                }
                else if (userInput == "query topics")
                {
                    QueryTopics.Query();
                }

                switch (userInput)
                {
                    case "Help":
                        Console.WriteLine("1) Subscribe");
                        Console.WriteLine("2) Unsubscribe");
                        Console.WriteLine("3) Query Topics");
                        break;
                    case "Subscribe":
                        Subscription.NewSubscription();
                        break;
                    case "Unsubscribe":
                        Subscription.RemoveSubscription();
                        break;
                    case "Query Topics":
                        QueryTopics.Query();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
