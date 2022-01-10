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
        /// è il metodo che avvia il server e si mette in ascolto di datagrammi di client
        /// termina solo se ci sono eccezioni spengendo il socket server
        /// nel ciclo che cattura i datagrammi in ingresso vengono lanciati thread di messageHandler che gestiscono singolarmente un messaggio
        /// </summary>
        public void start()
        {
            IPAddress localAddr = IPAddress.Parse(ipAddressString);
            IPEndPoint iPEndPoint = new IPEndPoint(localAddr, port);
            server = new Socket(iPEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(iPEndPoint);
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a new connection...");
                    byte[] byteData = new byte[2048];
                    EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                    int bytesReceived = server.ReceiveFrom(byteData, ref clientEP);
                    Console.WriteLine("Message incoming from " + clientEP.ToString());
                    server.SendTo(MessageHandler.handle(byteData, bytesReceived, clientEP), clientEP);
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
