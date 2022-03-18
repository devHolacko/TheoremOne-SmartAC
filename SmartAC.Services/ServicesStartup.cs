using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.Alerts;
using SmartAC.Models.Validations.DeviceRegisterations;
using SmartAC.Models.Validations.Devices;
using SmartAC.Models.Validations.Users;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Requests.Users;
using SmartAC.Services.Alerts;
using SmartAC.Services.Devices;
using SmartAC.Services.Others;

namespace SmartAC.Services
{
    public class ServicesStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            DataStartup.Configure(services, connectionString);

            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<ICacheManager, CacheManager>();

            services.AddTransient<IValidator<RegisterDeviceRequest>, RegisterDeviceValidator>();
            services.AddTransient<IValidator<ReportDeviceReadingsRequest>, ReportDeviceReadingsValidator>();
            services.AddTransient<IValidator<CreateAlertRequest>, CreateAlertValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
        }
    }
}
