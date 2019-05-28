
using System.Security.Authentication;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace PropertyTaxPortal
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
                .UseKestrel(c =>
                {
                    c.ConfigureHttpsDefaults(opt =>
                    {
                        opt.SslProtocols = SslProtocols.Tls12;
                    });
                });
    }
}
