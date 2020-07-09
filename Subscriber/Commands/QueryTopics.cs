using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Commands
{
    class QueryTopics
    {
        public static void Query()
        {
            Command command = new Command(CommandType.MessageTypeQuery, "", "");
            SendMessage.Send(command);
        }
    }
}
