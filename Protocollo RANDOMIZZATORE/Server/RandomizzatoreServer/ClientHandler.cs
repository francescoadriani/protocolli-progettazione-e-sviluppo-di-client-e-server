using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace RandomizzatoreServer
{
    /// <summary>
    /// la classe gestisce un client o per meglio dire gestisce un socket che rappresenta il client
    /// </summary>
    public class ClientHandler
    {
        /// <summary>
        /// metodo statico che gestisce una singola connessione con un client
        /// il metodo inizia e finisce dal momento che il socket è attivo
        /// il parametro obj è un Socket che rappresenta il client
        /// </summary>
        /// <param name="obj"></param>
        public static void handle(Object obj)
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

                //fin a quando non ci sono motivi per disconnettersi
                while (!disconnect)
                {
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

                    //se c'è un messaggio get oppure get[min;max]
                    if (message.ToLower() == ("get") || (message.ToLower().StartsWith("get") && (message.Split('[').Length > 1 && message.Contains("]"))))
                    {
                        //se c'è il parametro min e max impostali nelle variabili di stato
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

                        //generazione del numero random dal min al max
                        double res = min + rnd.NextDouble() * (max - min);

                        //invio della risposta
                        client.Send(encoding.GetBytes(res.ToString("0.00") + "\n\r"));
                    }
                    else if (message.ToLower() == "close") //comando close da parte del client
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        return;
                    }
                    else if (message.Length > 0) //messaggio non vuoto sconosciuto
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
