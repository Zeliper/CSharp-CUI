using System;
using System.Collections.Generic;
using System.Text;
using SimpleTcp;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Starting....");
            SimpleTcpServer server = new SimpleTcpServer("0.0.0.0:6974");

            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            // let's go!
            server.Start();

            // once a client has connected...
            server.Send("[ClientIp:Port]", "Hello, world!");
            char c = 'x';
            while (c != 'c')
            {
                c = Console.ReadKey().KeyChar;
                List<String> clients = (List<string>)server.GetClients();
                clients.ForEach( e =>
                {
                    server.Send(e,"Data Received Session : " + e);
                });
            }
        }

        static void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("[" + e.IpPort + "] client connected");
            SimpleTcpServer server = (SimpleTcpServer)sender;
            List<String> clients = (List<string>)server.GetClients();
            server.Send(e.IpPort, String.Format("[{1}]Current Session Count : {0}", clients.Count, e.IpPort));
        }

        static void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("[" + e.IpPort + "] client disconnected: " + e.Reason.ToString());
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("[" + e.IpPort + "]: " + Encoding.UTF8.GetString(e.Data));
        }
    }
}
