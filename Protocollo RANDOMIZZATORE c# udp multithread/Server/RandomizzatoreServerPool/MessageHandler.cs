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
    /// la classe gestisce un messaggio di un client
    /// </summary>
    public class MessageHandler
    { 
        /// <summary>
        /// dizionario che memorizza lo stato della connessione relativa ad ogni ip
        /// </summary>
        public static Dictionary<String, State> ipStringState = new Dictionary<string, State>();

        /// <summary>
        /// metodo statico che gestisce un messaggio di un client
        /// il parametro clientEPAndMessageObj è una coppia: endpoint del client e messaggio ricevuto
        /// </summary>
        /// <param name="clientEPAndMessageObj"></param>
        public static void handle(Object clientEPAndMessageObj)
        {
            Encoding encoding = Encoding.ASCII;
            ClientEPAndMessage clientEPAndMessage = (ClientEPAndMessage)clientEPAndMessageObj;
            IPEndPoint iPEndPoint = clientEPAndMessage.clientEP;

            byte[] byteData = clientEPAndMessage.messageData;
            double min = 0;
            double max = 1;

            if (ipStringState.ContainsKey(iPEndPoint.Address.ToString()))
            {
                min = ipStringState[iPEndPoint.Address.ToString()].Min;
                max = ipStringState[iPEndPoint.Address.ToString()].Max;
            }

            String answer = "";
            String message = "";
            try
            {
                Random rnd = new Random();

                message = encoding.GetString(byteData);

                //pulizia del messaggio dagli a capo
                message = message.Replace(Environment.NewLine, "");

                Console.WriteLine("Message received: " + message + " from " + iPEndPoint);

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
                                if (ipStringState.ContainsKey(iPEndPoint.Address.ToString()))
                                {
                                    ipStringState[iPEndPoint.Address.ToString()].Min = min;
                                    ipStringState[iPEndPoint.Address.ToString()].Max = max;
                                }
                                else
                                {
                                    ipStringState.Add(iPEndPoint.Address.ToString(), new State(min, max));
                                }
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
            clientEPAndMessage.RaiseAnswerReadyEvent(
                new AnswerReadyEventArgs(message, encoding.GetBytes(answer), iPEndPoint));
        }
    }
}
