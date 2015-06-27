using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSockets.Api.Controllers;

namespace WebSockets.Sockets
{
    public class Program
    {
        public void Main(string[] args)
        {

            string baseAddress = "http://127.0.0.1:9000";

            WebSocketsStartup.Start(url: baseAddress);
        
            Console.WriteLine("Started WebSockets server on port 9000");
            Console.ReadLine();
          
        }
    }
}
