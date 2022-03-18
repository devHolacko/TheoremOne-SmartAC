using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Services.Devices;

namespace SmartAC.Services
{
    public class ServicesStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            DataStartup.Configure(services, connectionString);

            services.AddTransient<IDeviceService, DeviceService>();
        }
    }
}
