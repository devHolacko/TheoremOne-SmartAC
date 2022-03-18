using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.DeviceRegisterations;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Services.Devices;

namespace SmartAC.Services
{
    public class ServicesStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            DataStartup.Configure(services, connectionString);

            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IValidator<RegisterDeviceRequest>, RegisterDeviceValidator>();
        }
    }
}
