using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using DotNetCoreAngularCrudDataService.Framework.Helpers.File;
using DotNetCoreAngularCrudDataService.Framework.Models;
using DotNetCoreAngularCrudDataService.Framework.Services;

namespace DotNetCoreAngularCrudDataService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration;
        private ILogger _logger;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            //Cors is added for access the API from "heroku" to "localhost"
            services.AddCors();
            services.AddCors(o => o.AddPolicy("AllowCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc();
            services.AddOptions();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPresentationService, PresentationService>();

            var appSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtModel>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JwtModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
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
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton(typeof(IFileReader<PresentationModel>), typeof(FileReader<PresentationModel>));
            services.AddSingleton(typeof(IFileReader<User>), typeof(FileReader<User>));
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            _logger = loggerFactory.CreateLogger("StartupLogger");
        }
    }
}
