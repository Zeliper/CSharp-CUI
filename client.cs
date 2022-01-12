using System;
using System.Text;
using SimpleTcp;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Starting....");
            // instantiate
            SimpleTcpClient client = new SimpleTcpClient("ec.zeliper.com:6974");

            // set events
            client.Events.Connected += Connected;
            client.Events.Disconnected += Disconnected;
            client.Events.DataReceived += DataReceived;

            // let's go! 
            client.Connect();

            // once connected to the server...
            client.Send("Hello, world!");
            Console.ReadKey();
        }

        static void Connected(object sender, EventArgs e)
        {
            Console.WriteLine("*** Server connected");
        }

        static void Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("*** Server disconnected");
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("[" + e.IpPort + "] " + Encoding.UTF8.GetString(e.Data));
        }
    }
}
