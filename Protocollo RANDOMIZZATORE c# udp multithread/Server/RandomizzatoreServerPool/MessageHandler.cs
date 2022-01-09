using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RandomizzatoreServerPool
{
    /// <summary>
    /// la classe gestisce un client o per meglio dire gestisce un socket che rappresenta il client
    /// </summary>
    public class MessageHandler
    {
        /// <summary>
        /// metodo statico che gestisce una singola connessione con un client
        /// il metodo inizia e finisce dal momento che il socket è attivo
        /// il parametro client è un Socket che rappresenta il client
        /// </summary>
        /// <param name="client"></param>
        public static void handle(Object clientEPAndMessageObj)
        {
            Encoding encoding = Encoding.ASCII;
            ClientEPAndMessage clientEPAndMessage = (ClientEPAndMessage)clientEPAndMessageObj;
            IPEndPoint clientEP = clientEPAndMessage.clientEP;

            byte[] byteData = clientEPAndMessage.messageData;
            double min = 0;
            double max = 1;

            String answer = "";
            String message = "";
            try
            {
                Random rnd = new Random();

                message = encoding.GetString(byteData);

                //pulizia del messaggio dagli a capo
                message = message.Replace(Environment.NewLine, "");

                Console.WriteLine("Message received: " + message + " from " + clientEP);

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
                else if (message.Length > 0) //messaggio non vuoto sconosciuto
                {
                    //preparazione della risposta
                    answer = "UNKNOWN COMMAND";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
            }
            Console.WriteLine("Answer: " + answer);
            clientEPAndMessage.RaiseAnswerReadyEvent(new AnswerReadyEventArgs(message, encoding.GetBytes(answer), clientEP));
        }
    }
}
