using Microsoft.Extensions.DependencyInjection;
using SmartAC.Context.Sql;
using SmartAC.Models.Common;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Data
{
    public static class DataStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            ContextStartup.Configure(services, connectionString);

            services.AddTransient<IAlertDataService, AlertDataService>();
            services.AddTransient<IDeviceDataService, DeviceDataService>();
            services.AddTransient<IDeviceRegisterationDataService, DeviceRegisterationDataService>();
            services.AddTransient<ISensorsReadingDataService, SensorsReadingDataService>();
            services.AddTransient<IInvalidSensorsReadingDataService, InvalidSensorsReadingDataService>();

        }
    }
}
