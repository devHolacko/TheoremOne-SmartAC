using Microsoft.Extensions.DependencyInjection;
using SmartAC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services
{
    public class ServicesStartup
    {
        public static void Configure(IServiceCollection services)
        {
            DataStartup.Configure(services);
        }
    }
}
