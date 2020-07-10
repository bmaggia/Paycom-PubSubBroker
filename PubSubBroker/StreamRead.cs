using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Newtonsoft.Json;
using PubSubBroker.Commands;
using System.Timers;

namespace PubSubBroker
{
    class StreamRead
    {
        public static async Task BeginStreamRead(NetworkStream netStream)
        {
            bool connected = true;

            bool polling = false;

            Timer pollTimer = new Timer(30000);
            pollTimer.AutoReset = true;
            pollTimer.Elapsed += (sender, e) => PollClient(sender, e, ref polling, ref connected, netStream);
            pollTimer.Start();

            byte[] netBuffer = new byte[1024];

            while (connected)
            {
                await netStream.ReadAsync(netBuffer);

                pollTimer.Interval = 30000;
                polling = false;

                Command command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                CommandProcessor.ProcessCommand(command, netStream);

                Array.Clear(netBuffer, 0, 1024);
            }
        }

        private static void PollClient(Object source, ElapsedEventArgs e, ref bool polling, ref bool connected, NetworkStream netstream)
        {
            if (polling)
            {
                Console.WriteLine("Disconnecting Client");
                connected = false;
            }
            else
            {
                Command command = new Command(CommandType.Poll);
                SendMessage.Send(command, netstream);
                polling = true;
            }
        }
    }
}
