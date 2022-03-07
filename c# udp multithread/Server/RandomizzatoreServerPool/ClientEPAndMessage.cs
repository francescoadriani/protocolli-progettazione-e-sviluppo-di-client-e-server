using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RandomizzatoreServerPool
{
    public class AnswerReadyEventArgs
    {
        public AnswerReadyEventArgs(String message, byte[] answer, EndPoint clientEP) 
        { 
            this.answer = answer;
            this.message = message;
            this.clientEP = clientEP;
        }
        public String message { get; set; }
        public byte[] answer { get; set; }
        public EndPoint clientEP { get; set; }
    }
    public class ClientEPAndMessage
    {
        public delegate void AnswerReadyEventHandler(object sender, AnswerReadyEventArgs e);
        public event AnswerReadyEventHandler AnswerReadyEvent;
        public byte[] messageData{ get; set; }
        public IPEndPoint clientEP { get; set; }

        private ClientEPAndMessage()
        {
            //this def makes impossible call without arguments
        }
        public ClientEPAndMessage(byte[] messageData, int lenght, EndPoint clientEP)
        {
            this.clientEP = (IPEndPoint)clientEP;
            this.messageData = new byte[lenght];
            Array.Copy(messageData, this.messageData, lenght);
        }
        public virtual void RaiseAnswerReadyEvent(AnswerReadyEventArgs e)
        {
            // Raise the event in a thread-safe manner using the ?. operator.
            AnswerReadyEvent?.Invoke(this, e);
        }
    }
}
