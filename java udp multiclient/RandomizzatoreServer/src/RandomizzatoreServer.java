
import java.io.*;
import java.net.*;

public class RandomizzatoreServer
    {
        /// <summary>
        /// è il socket server che attende connessioni in ingresso
        /// </summary>
        DatagramSocket server;

        /// <summary>
        /// rappresenta la porta su cui il server si mette in ascolto
        /// </summary>
        int port;


        /// <summary>
        /// Costruttore del server, memorizza ip e porta passati come parametri
        /// </summary>
        /// <param name="ipAddressString"></param>
        /// <param name="port"></param>
        public RandomizzatoreServer(int port)
        {
            this.port = port;
        }

        /// <summary>
        /// è il metodo che avvia il server e si mette in ascolto di datagrammi di client
        /// termina solo se ci sono eccezioni spengendo il socket server
        /// nel ciclo che cattura i datagrammi in ingresso vengono lanciati thread di messageHandler che gestiscono singolarmente un messaggio
        /// </summary>
        public void start()
        {
            try
            {
                //1. creating a server socket, parameter is local port number
                server = new DatagramSocket(port);
                
                //buffer to receive incoming data
                byte[] buffer = new byte[65536];
                DatagramPacket incoming = new DatagramPacket(buffer, buffer.length);
                
                //2. Wait for an incoming data
                System.out.println("Server socket created. Waiting for incoming data...");
                
                //communication loop
                while(true)
                {
                    server.receive(incoming);
                    System.out.println(incoming.getAddress().getHostAddress() + " : " + incoming.getPort() + " - " + 
                        new String(incoming.getData(), 0, incoming.getLength()));
                    byte[] a = MessageHandler.handle(incoming.getData(),incoming.getLength(), incoming.getAddress(), incoming.getPort());
                    DatagramPacket dp = new DatagramPacket(a , a.length, incoming.getAddress() , incoming.getPort());
                    System.out.println("Answer to: " + incoming.getAddress().getHostAddress() + " : " + incoming.getPort() + " - " + new String(a));
                    server.send(dp);
                }
            }
            
            catch(IOException e)
            {
                System.err.println("IOException " + e);
            }
        }
    }
