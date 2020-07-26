namespace Publisher.Commands
{
    class QueryTopics
    {
        public static void Query()
        {
            var command = new Command(CommandType.MessageTypeQuery);
            SendMessage.Send(command);
        }
    }
}
