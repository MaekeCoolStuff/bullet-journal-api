using System;
using System.Collections.Generic;
using System.Linq;
using BulletJournalApi.Models;
using BulletJournalApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json;

namespace BulletJournalApi
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
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };

            ConventionRegistry.Register("Camel Case", conventionPack, _ => true);

            //BsonClassMap.RegisterClassMap<Task>(cm => {
            //    cm.AutoMap();
            //});

            services.Configure<BulletJournalDatabaseSettings>(
                Configuration.GetSection(nameof(BulletJournalDatabaseSettings)));

            services.AddSingleton<IBulletJournalDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BulletJournalDatabaseSettings>>().Value);

            services.AddSingleton<TaskService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
