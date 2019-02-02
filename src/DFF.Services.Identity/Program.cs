using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DFF.Common.Logging;
using DFF.Common.Metrics;
using DFF.Common.Mvc;
using DFF.Common.Vault;

namespace DFF.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLogging()
                .UseVault()
                .UseLockbox()
                .UseAppMetrics();
    }
}
