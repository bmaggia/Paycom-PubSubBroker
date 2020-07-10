using System;

namespace Subscriber.Commands
{
    static class CommandProcessor
    {

        public static void ProcessCommand(Command command)
        {
            if (command.CommandType == CommandType.NewMessage)
            {
                Console.WriteLine("New Message; Topic: " + command.Topic + "   Message: " + command.MessageBody);
            }
            else if (command.CommandType == CommandType.Poll)
            {
                Command poll = new Command(CommandType.Poll);
                SendMessage.Send(poll);
            }
            else
            {
                Console.WriteLine(command.MessageBody);
            }
        }
    }
}
