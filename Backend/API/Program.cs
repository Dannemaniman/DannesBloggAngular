using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();//när ett dotnet run command executas.. så letar den efter denna metoden.. och sen kommer den köra koden inuti
            //.ContentRootPath.. är var den hittar filerna av vårt projekt!
            //IConfiguration.. är configen för vår app.. och det finns flera ställen där den kan hämta denna configen ifrån! en av ställena är våra environment variables,
            //men även command line och appsettings filen!
            //den kommer hämta från alla ställen ifall det finns info där!
            
            //Slutligen, så sätter den upp logging och returnar HostBuilder instancen!
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => //Inuti denna.. så säger den också till ifall vi ska använda något annat när vi startar vår app.. dvs Startup.cs 
                { 
                    webBuilder.UseStartup<Startup>();
                });
    }
}
