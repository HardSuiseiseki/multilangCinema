using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Cinema.Hubs.SignalRChat))]

namespace Cinema.Hubs
{
    public class SignalRChat
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
