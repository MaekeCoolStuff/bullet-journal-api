using System;
using BulletJournalApi.Models;
using BulletJournalApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BulletJournal.Data;
using BulletJournalApi.Helpers;

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
            services.AddCors();

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };

            ConventionRegistry.Register("Camel Case", conventionPack, _ => true);

            services.Configure<ConfigSettings>(
                Configuration.GetSection(nameof(ConfigSettings)));

            services.AddSingleton<ConfigSettings>(sp =>
                sp.GetRequiredService<IOptions<ConfigSettings>>().Value);

            var settings = Configuration.GetSection(nameof(ConfigSettings)).Get<ConfigSettings>();
            var key = Encoding.ASCII.GetBytes(settings.AuthSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = context.Principal.Identity.Name;
                        var user = userService.GetById(int.Parse(userId));
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<ItemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddControllers();
            services.AddDbContext<BulletJournalContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("BulletJournalConnection"));
                }
                );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddApplicationInsightsTelemetry();
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
