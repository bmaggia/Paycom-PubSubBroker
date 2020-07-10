namespace PubSubBroker.Commands
{
    // Command used for client/server communcation.
    class Command
    {
        public CommandType CommandType;

        // Topic of command is not used MessageTypeQuery
        public string Topic;

        public string MessageBody;

        public Command (CommandType commandtype, string topic = "", string messagebody = "")
        {
            CommandType = commandtype;
            Topic = topic;
            MessageBody = messagebody;
        }
    }

    public enum CommandType
    {
        NewMessage,
        Subscribe,
        Unsubscribe,
        MessageTypeQuery,
        CreateTopic,
        DeleteTopic,
        Poll
    }
}
