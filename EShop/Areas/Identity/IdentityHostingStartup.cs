using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EShop.Areas.Identity.IdentityHostingStartup))]
namespace EShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}