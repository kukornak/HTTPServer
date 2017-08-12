using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            Console.WriteLine("Server started at port 8080");

            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = listener.AcceptTcpClient();

                StreamReader request = new StreamReader(client.GetStream());
                StreamWriter response = new StreamWriter(client.GetStream());

                //Console.WriteLine(request.ReadLine());

                String[] tokens = request.ReadLine().Split(' ');
                Console.WriteLine(tokens[1]);
                String path = tokens[1];
                if(path == "/")
                {
                    path = "index.html";
                }
                try
                {
                    StreamReader fileReader = new StreamReader("../../web/" + path);
                    response.WriteLine("HTTP/1.1 200 OK\n");
                    while (!fileReader.EndOfStream)
                    {
                        response.WriteLine(fileReader.ReadLine());
                    }
                    response.Flush();
                }
                catch (Exception e)
                {
                    response.WriteLine("HTTP/1.1 404 File not found!\n");
                    response.WriteLine("<h1>404 Not found</h1>");
                    response.Flush();
                }

                client.Close();
            }
        }
    }
}
