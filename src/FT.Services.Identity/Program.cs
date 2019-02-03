using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using FT.Common.Logging;
using FT.Common.Metrics;
using FT.Common.Mvc;
using FT.Common.Vault;

namespace FT.Services.Identity
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
