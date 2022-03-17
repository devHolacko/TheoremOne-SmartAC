using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartAC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Context.Sql
{
    public static class ContextStartup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SmartACDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
