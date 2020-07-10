using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber
{
    class Command
    {
        public CommandType CommandType;

        public string Topic;

        public string MessageBody;

        public Command(CommandType commandtype, string topic = "", string messagebody = "")
        {
            CommandType = commandtype;
            Topic = topic;
            MessageBody = messagebody;
        }
    }

    enum CommandType
    {
        NewMessage,
        Subscribe,
        Unsubscribe,
        MessageTypeQuery,
        Poll
    }
}
