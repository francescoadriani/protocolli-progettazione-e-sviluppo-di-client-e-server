using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RandomizzatoreServer
{
    public class ClientHandler
    {
        Socket server;
        String ipAddressString;
        int port;
        public ClientHandler(String ipAddressString, int port)
        {
            this.ipAddressString = ipAddressString;
            this.port = port;
        }

        public void start()
        {
            IPAddress localAddr = IPAddress.Parse(ipAddressString);
            IPEndPoint iPEndPoint = new IPEndPoint(localAddr, port);
            server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iPEndPoint);
            server.Listen(10);
            StartListener();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a new connection...");
                    Socket client = server.Accept();
                    Console.WriteLine("Connection incoming with " + client.RemoteEndPoint.ToString());
                    Thread t = new Thread(new ParameterizedThreadStart(handleClient));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
        }
        public void handleClient(Object obj)
        {
            Socket client = (Socket)obj;
            client.Receive(new byte[512]);
            double min = 0;
            double max = 1;
            try 
            {
                bool disconnect = false;
                Random rnd = new Random();
                Encoding encoding = Encoding.ASCII;
                while (!disconnect)
                {
                    byte[] byteData = new byte[512];
                    int bytesReceived = 0;
                    String message = "";
                    do
                    {
                        bytesReceived = client.Receive(byteData);
                        message += encoding.GetString(byteData, 0, bytesReceived);
                    }
                    while (client.Available > 0);
                    message = message.Replace(Environment.NewLine, "");
                    if (message.ToLower()==("get") || (message.ToLower().StartsWith("get") && (message.Split('[').Length > 1 && message.Contains("]"))))
                    {
                        if (message.Split('[').Length > 1 && message.Contains("]"))
                        {
                            double minTemp = 0;
                            double maxTemp = 0;
                            String param = message.Split('[')[1];
                            if (double.TryParse(param.Split(';')[0].Substring(0), out minTemp))
                            {
                                if (double.TryParse(param.Split(';')[1].Substring(0, param.Split(';')[1].IndexOf(']')), out maxTemp))
                                {
                                    min = minTemp;
                                    max = maxTemp;
                                }
                            }
                        }
                        double res = min + rnd.NextDouble() * (max - min);
                        client.Send(encoding.GetBytes(res.ToString("0.00") + "\n\r"));
                    }
                    else if (message.ToLower() == "close")
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        return;
                    }
                    else if (message.Length > 0)
                    {
                        client.Send(encoding.GetBytes("UNKNOWN COMMAND\n\r"));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}
