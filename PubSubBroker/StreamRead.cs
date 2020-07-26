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
            var connected = true;


            var pollTimer = new Timer(30000);
            pollTimer.AutoReset = true;
            pollTimer.Elapsed += (sender, e) => PollClient(ref connected, netStream, ref pollTimer);
            pollTimer.Start();

            byte[] netBuffer = new byte[1024];

            while (connected)
            {
                await netStream.ReadAsync(netBuffer);


                var command = JsonConvert.DeserializeObject<Command>(Encoding.ASCII.GetString(netBuffer));

                CommandProcessor.ProcessCommand(command, netStream);

                Array.Clear(netBuffer, 0, 1024);
            }
        }

        private static void PollClient(ref bool connected, NetworkStream netstream, ref Timer timer)
        {
            var command = new Command(CommandType.Poll);
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
