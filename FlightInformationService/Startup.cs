using FlightInformationService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace FlightInformationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Create instance, and begin loading of data from file so the first request isn't slow.
            // Need to find a better approach to this, this feels dirty. Ideally load from a database, extra work though.
            var dataService = new DataService();
            dataService.LoadDataFromFile();

            services
                .AddSingleton<IDataService>(dataService)
                .AddTransient<IFlightService, FlightService>()
                .AddSwaggerGen(c =>
                {

                    try
                    {
                        // Get XML Comments file
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                    }
                    catch (Exception)
                    {
                        // Do nothing, comments won't appear in Swagger.
                    }

                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flight Information API", Version = "v1" });
                })
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Normall would only be exposed in dev mode.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight Information API");
            });
        }
    }
}
