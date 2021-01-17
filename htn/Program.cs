using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using htn.Azure;
using htn.ML;
using htn.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace htn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var c = new CompositeAnalyzer(new TextAnalyzer(), new PyBridge());
            //Console.WriteLine(c.AnalyzeComposite(Console.ReadLine()));
            //Console.WriteLine(Assembly.GetCallingAssembly().FullName);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(o => o.ListenAnyIP(5000));
                });
    }
}