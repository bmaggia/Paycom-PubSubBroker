using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

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
            if (command.CommandType == CommandType.Subscribe)
            {
                Console.WriteLine(command.MessageBody);
            }
            else if (command.CommandType == CommandType.Unsubscribe)
            {
                Console.WriteLine(command.MessageBody);
            }
            else if (command.CommandType == CommandType.MessageTypeQuery)
            {
                Console.WriteLine(command.MessageBody);
            }
        }
    }
}
