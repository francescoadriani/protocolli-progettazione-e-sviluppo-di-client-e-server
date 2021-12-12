using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RandomizzatoreServerPool
{
    /// <summary>
    /// il server rappresenta una istanza di un server tcp che gestisce le connessioni ai client
    /// </summary>
    public class Server
    {
        /// <summary>
        /// è il socket server che attende connessioni in ingresso
        /// </summary>
        Socket server;

        /// <summary>
        /// rappresenta l'indirizzo ip alfanumerico su cui il server si mette in ascolto
        /// </summary>
        String ipAddressString;

        /// <summary>
        /// rappresenta la porta su cui il server si mette in ascolto
        /// </summary>
        int port;


        /// <summary>
        /// Costruttore del server, memorizza ip e porta passati come parametri
        /// </summary>
        /// <param name="ipAddressString"></param>
        /// <param name="port"></param>
        public Server(String ipAddressString, int port)
        {
            this.ipAddressString = ipAddressString;
            this.port = port;
        }

        /// <summary>
        /// è il metodo che avvia il server e si mette in ascolto di client
        /// termina solo se ci sono eccezioni spengendo il socket server
        /// nel ciclo che cattura connessioni in ingresso vengono lanciati thread di clientHandler che gestiscono singolarmente un socket che rappresenta un client
        /// </summary>
        public void start()
        {
            IPAddress localAddr = IPAddress.Parse(ipAddressString);
            IPEndPoint iPEndPoint = new IPEndPoint(localAddr, port);
            server = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iPEndPoint);
            server.Listen(10);
            try
            {
                while (true)
                {
                    ThreadPool.SetMinThreads(10, 0);
                    ThreadPool.SetMaxThreads(10, 0);
                    Console.WriteLine("Waiting for a new connection...");
                    Socket client = server.Accept();
                    Console.WriteLine("Connection incoming with " + client.RemoteEndPoint.ToString());
                    ThreadPool.QueueUserWorkItem(ClientHandler.handle, client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
        }
    }
}
