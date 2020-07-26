using System;
using System.Net.Sockets;

namespace PubSubBroker.Commands
{
    static class CommandProcessor
    {

        public static void ProcessCommand(Command command, NetworkStream netstream)
        {
            if (command.CommandType == CommandType.NewMessage)
            {
                Console.WriteLine("New Message: " + command.Topic + ": " + command.MessageBody);
                Messages.AddMessage(command.Topic, command.MessageBody);

                var returnCommand = new Command(CommandType.NewMessage, command.Topic, "Message received");

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.Subscribe)
            {
                Console.WriteLine("Subscribe: " + command.Topic);
                var result = Messages.AddSubscriber(command.Topic, netstream);

                var returnCommand = new Command(CommandType.Subscribe, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.Unsubscribe)
            {
                Console.WriteLine("Unsubscribe: " + command.Topic);
                var result = Messages.RemoveSubscriber(command.Topic, netstream);

                var returnCommand = new Command(CommandType.Unsubscribe, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.MessageTypeQuery)
            {
                Console.WriteLine("Message query");
                TopicQuery.Query(netstream);
            }
            else if (command.CommandType == CommandType.CreateTopic)
            {
                Console.WriteLine("Creating Topic: " + command.Topic);

                var result = Messages.CreateTopic(command.Topic);

                var returnCommand = new Command(CommandType.CreateTopic, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.DeleteTopic)
            {
                Console.WriteLine("Deleting Topic: " + command.Topic);

                var result = Messages.DeleteTopic(command.Topic);

                var returnCommand = new Command(CommandType.DeleteTopic, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
        }
    }
}
