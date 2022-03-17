using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using SmartAC.Models.Common;
using SmartAC.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services
{
    public class ServicesStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            DataStartup.Configure(services, connectionString);

            services.AddTransient<IAlertDataService, AlertDataService>();
            services.AddTransient<IDeviceDataService, DeviceDataService>();
            services.AddTransient<IDeviceRegisterationDataService, DeviceRegisterationDataService>();
            services.AddTransient<ISensorsReadingDataService, SensorsReadingDataService>();
            services.AddTransient<IInvalidSensorsReadingDataService, InvalidSensorsReadingDataService>();
        }
    }
}
