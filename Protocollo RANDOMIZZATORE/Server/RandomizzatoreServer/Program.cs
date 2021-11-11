using System;

namespace RandomizzatoreServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server opened");
            ClientHandler clientHandler = new ClientHandler("127.0.0.1", 10144);
            clientHandler.start();
        }
    }
}
