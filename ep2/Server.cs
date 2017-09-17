using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HTTPServerTutorial
{
    class Server
    {
        public bool Running = false;
        private int Port;
        private TcpListener Listener;

        public Server(int port)
        {
            Port = port;
        }

        public Server()
        {
            Port = 8080;
        }

        public void Start()
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Running = true;
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            Listener.Start();
            Console.WriteLine("Server started at port {0}", Port);

            while (Running)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = Listener.AcceptTcpClient();
                ManageClient(client);
                client.Close();
            }
        }

        private void ManageClient(TcpClient client)
        {
            Console.WriteLine("Client connected");
        }
    }
}
