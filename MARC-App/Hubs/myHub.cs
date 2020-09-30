using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaSecondApp.Hubs
{
    public class myHub:Hub
    {



        public void sendmessage(string x)
        {
            Clients.All.SendAsync("display", x);
        }






    }
}
