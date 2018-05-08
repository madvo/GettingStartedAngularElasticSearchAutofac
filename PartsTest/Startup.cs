using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using PartsTest.Autofac;

[assembly: OwinStartup(typeof(PartsTest.Startup))]

namespace PartsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
       
    }
}
