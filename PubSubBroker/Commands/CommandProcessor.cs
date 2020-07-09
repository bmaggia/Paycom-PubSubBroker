using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

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

                Command returnCommand = new Command(CommandType.NewMessage, command.Topic, "Message received");

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.Subscribe)
            {
                Console.WriteLine("Subscribe: " + command.Topic);
                string result = Messages.AddSubscriber(command.Topic, netstream);

                Command returnCommand = new Command(CommandType.Subscribe, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);

            }
            else if (command.CommandType == CommandType.Unsubscribe)
            {
                Console.WriteLine("Unsubscribe: " + command.Topic);
                string result = Messages.RemoveSubscriber(command.Topic, netstream);

                Command returnCommand = new Command(CommandType.Unsubscribe, command.Topic, result);

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

                string result = Messages.CreateTopic(command.Topic);

                Command returnCommand = new Command(CommandType.CreateTopic, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
            else if (command.CommandType == CommandType.DeleteTopic)
            {
                Console.WriteLine("Deleting Topic: " + command.Topic);

                string result = Messages.DeleteTopic(command.Topic);

                Command returnCommand = new Command(CommandType.DeleteTopic, command.Topic, result);

                SendMessage.Send(returnCommand, netstream);
            }
        }
    }
}
