using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            
            
            
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingcontext,config) =>
            {
                config.AddJsonFile($"configuration.{hostingcontext.HostingEnvironment.EnvironmentName.ToLower()}.json")
                .AddEnvironmentVariables();

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
