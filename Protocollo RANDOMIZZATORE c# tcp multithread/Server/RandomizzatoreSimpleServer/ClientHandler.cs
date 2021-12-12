using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace RandomizzatoreSimpleServer
{
    /// <summary>
    /// la classe gestisce un client o per meglio dire gestisce un socket che rappresenta il client
    /// </summary>
    public class ClientHandler
    {
        /// <summary>
        /// metodo statico che gestisce una singola connessione con un client
        /// il metodo inizia e finisce dal momento che il socket è attivo
        /// il parametro client è un Socket che rappresenta il client
        /// </summary>
        /// <param name="client"></param>
        public static void handle(Socket client)
        {
            double min = 0;
            double max = 1;
            try
            {
                bool disconnect = false;
                Random rnd = new Random();
                Encoding encoding = Encoding.ASCII;

                //fin a quando non ci sono motivi per disconnettersi
                while (!disconnect)
                {
                    String answer = "";
                    byte[] byteData = new byte[512];
                    int bytesReceived = 0;
                    String message = "";

                    //recupero del messaggio fino a che ci sono byte disponibili
                    do
                    {
                        bytesReceived = client.Receive(byteData);
                        message += encoding.GetString(byteData, 0, bytesReceived);
                    }
                    while (client.Available > 0);

                    //pulizia del messaggio dagli a capo
                    message = message.Replace(Environment.NewLine, "");

                    Console.WriteLine("Message received: " + message + " from " + client.RemoteEndPoint);

                    //se c'è un messaggio get oppure get[min;max]
                    if (message.ToLower() == ("get") || (message.ToLower().StartsWith("get") && (message.Split('[').Length > 1 && message.Contains("]"))))
                    {
                        //se c'è il parametro min e max impostali nelle variabili di stato
                        if (message.Split('[').Length > 1 && message.Contains("]"))
                        {
                            double minTemp = 0;
                            double maxTemp = 0;
                            String param = message.Split('[')[1];
                            if (double.TryParse(param.Split(';')[0].Substring(0), NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out minTemp))
                            {
                                if (double.TryParse(param.Split(';')[1].Substring(0, param.Split(';')[1].IndexOf(']')), NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out maxTemp))
                                {
                                    min = minTemp;
                                    max = maxTemp;
                                }
                            }
                        }

                        //generazione del numero random dal min al max
                        double res = min + rnd.NextDouble() * (max - min);

                        //preparazione della risposta
                        answer = res.ToString("N", new CultureInfo("it-IT", false).NumberFormat);
                    }
                    else if (message.ToLower() == "close") //comando close da parte del client
                    {
                        //chiusura del socket su richiesta del client
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        return;
                    }
                    else if (message.Length > 0) //messaggio non vuoto sconosciuto
                    {
                        //preparazione della risposta
                        answer = "UNKNOWN COMMAND";
                    }

                    //invio della risposta
                    client.Send(encoding.GetBytes(answer + "\n\r"));
                    Console.WriteLine("Message sent: " + answer + " to " + client.RemoteEndPoint);
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
