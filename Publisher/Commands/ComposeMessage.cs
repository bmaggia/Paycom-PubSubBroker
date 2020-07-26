using System;

namespace Publisher.Commands
{
    public static class ComposeMessage
    {
        public static void Compose()
        {
            Console.WriteLine("Message Topic:");
            var messageSubject = Console.ReadLine();
            Console.WriteLine("Message Body:");
            var messageBody = Console.ReadLine();

            Console.WriteLine("Send? (y/n)");
            var send = Console.ReadLine();

            if (send == "y")
            {
                var command = new Command(CommandType.NewMessage, messageSubject, messageBody);

                SendMessage.Send(command);
            }
        }
    }
}
