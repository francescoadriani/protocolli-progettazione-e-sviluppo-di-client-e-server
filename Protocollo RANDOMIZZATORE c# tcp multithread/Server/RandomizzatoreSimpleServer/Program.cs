using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizzatoreSimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server opened");
            Server server = new Server("127.0.0.1", 10144);
            server.start();
        }
    }
}
