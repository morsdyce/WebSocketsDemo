using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebSockets.Wamp.Startup))]

namespace WebSockets.Wamp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string baseAddress = "http://127.0.0.1:9000";

            Console.WriteLine("Socket Server Started at port 9000");

            Console.ReadLine();
        }
    }
}
