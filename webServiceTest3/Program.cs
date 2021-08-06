using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webServiceTest3
{
    public class Program
    {
        public static IConfiguration Configuration { get;  }
        private const long AmountOfLogs = 100000;
        public static void Main(string[] args)
        {            
                                

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {  
            return Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((context, conf) =>
                    {
                        conf.MinimumLevel.Information()
                        .WriteTo.PostgreSQL(
                            connectionString: "User ID =admin1;Password=admin1;Server=localhost;Port=5432;Database=patients;",
                            tableName: "public.mylogs",
                            needAutoCreateTable: true,
                            useCopy: false);
                    }).UseStartup<Startup>();
                });
        }
            

    }
}
