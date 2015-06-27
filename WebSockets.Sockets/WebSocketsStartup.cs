using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using SystemEx;
using WampSharp.V2;
using WampSharp.V2.Realm;
using WebSockets.Api.Controllers;

namespace WebSockets.Sockets
{
    public class WebSocketsStartup
    {
        public static void Start(string url)
        {
            IWampHost host = new DefaultWampHost(url);

            IWampHostedRealm realm = host.RealmContainer.GetRealmByName("realm1");

            var todosController = new TodosController();
            Task<IAsyncDisposable> registrationTask = realm.Services.RegisterCallee(todosController);

            host.Open();
        }
    }
}
