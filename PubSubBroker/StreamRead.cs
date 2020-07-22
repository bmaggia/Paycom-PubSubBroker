using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using PubSubBroker.Commands;
using System.Timers;
using System.Threading.Tasks;

namespace PubSubBroker
{
    class StreamRead
    {
        public static async Task BeginStreamRead(NetworkStream netStream)
        {
            bool connected = true;


            Timer pollTimer = new Timer(30000);
            pollTimer.AutoReset = true;
            pollTimer.Elapsed += (sender, e) => PollClient(sender, e, ref connected, netStream, ref pollTimer);
            pollTimer.Start();

            byte[] netBuffer = new byte[1024];

            while (connected)
            {
                await netStream.ReadAsync(netBuffer);

                Command command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                CommandProcessor.ProcessCommand(command, netStream);

                Array.Clear(netBuffer, 0, 1024);
            }
        }

        // If source and e are not used, I am not certain that you need to include them as variables.
        private static void PollClient(Object source, ElapsedEventArgs e, ref bool connected, NetworkStream netstream, ref Timer timer)
        {
            Command command = new Command(CommandType.Poll);
            connected = SendMessage.Send(command, netstream);

            if (!connected)
            {
                Console.WriteLine("Disconnecting Client");
                Messages.BrokerMessages.ForEach(c => c.Subscribers.Remove(netstream));

                connected = false;
                netstream.Close();
                netstream.Dispose();
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}
