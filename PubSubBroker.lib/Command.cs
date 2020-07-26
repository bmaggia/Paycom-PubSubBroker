// Command used for client/server communcation.
public class Command
{
    public CommandType CommandType;

    // Topic of command is not used MessageTypeQuery
    public string Topic;

    public string MessageBody;

    public Command(CommandType commandType, string topic = "", string messageBody = "")
    {
        CommandType = commandType;
        Topic = topic;
        MessageBody = messageBody;
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