using Microsoft.Extensions.DependencyInjection;
using SmartAC.Context.Sql;
using SmartAC.Models.Common;
using SmartAC.Models.Interfaces.Common;
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
