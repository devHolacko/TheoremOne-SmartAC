using Microsoft.Extensions.DependencyInjection;
using SmartAC.Context.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Data
{
    public static class DataStartup
    {
        public static void Configure(IServiceCollection services)
        {
            ContextStartup.Configure(services);
        }
    }
}
