using System;

namespace Publisher.Commands
{
    public static class ComposeMessage
    {
        public static void Compose()
        {
            Console.WriteLine("Message Topic:");
            string messageSubject = Console.ReadLine();
            Console.WriteLine("Message Body:");
            string messageBody = Console.ReadLine();

            Console.WriteLine("Send? (y/n)");
            string send = Console.ReadLine();

            if (send == "y")
            {
                Command command = new Command(CommandType.NewMessage, messageSubject, messageBody);

                SendMessage.Send(command);
            }
        }
    }
}
