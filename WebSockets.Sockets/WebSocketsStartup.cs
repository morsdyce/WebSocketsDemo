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
            var todosController = new TodosController();

            IWampHost host = new DefaultWampHost(url);

            IWampHostedRealm realm = host.RealmContainer.GetRealmByName("realm1");

            Task<IAsyncDisposable> registrationTask = realm.Services.RegisterCallee(todosController);

            /*
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly currentAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<TypeInfo> types = currentAssembly.DefinedTypes.Where(
                    type => type != null &&
                    type.IsPublic &&
                    type.IsClass &&
                    !type.IsAbstract &&
                    typeof(Controller).IsAssignableFrom(type));

                foreach (Type type in types)
                {
                    Task<IAsyncDisposable> registrationTask = realm.Services.RegisterCallee(
                        Activator.CreateInstance(type)
                        );

                    registrationTask.Wait();
                }
            }*/

            host.Open();
        }
    }
}
