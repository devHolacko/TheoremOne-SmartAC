using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using SmartAC.Models.Common;
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
        }
    }
}
