using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartAC.DevicesAPI.Attributes.SensorReading;
using SmartAC.Models.Mappings;
using SmartAC.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.DevicesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;
                var profiles = MappingsConfig.GetMappingsProfiles();
                foreach (var profile in profiles)
                {
                    mc.AddProfile(profile);
                }
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddControllers(config =>
            {
                //config.Filters.Add(new SafeReadingActionFilter());
            }).AddFluentValidation();

            services.AddSingleton(mapper);

            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartAC.DevicesAPI", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SmartAC.DevicesAPI.xml"));

                c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = string.Empty
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Token"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });

            ServicesStartup.Configure(services, Configuration.GetSection("AppSettings")["DbConnectionString"]);

            services.AddScoped<SafeReadingActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartAC.DevicesAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
