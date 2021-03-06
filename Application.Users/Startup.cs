﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Users
{
    public class Startup
    {

        public IConfiguration _Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get connection-string from config
            var connectionString = _Configuration
                .GetConnectionString("DatabaseConnection");

            var usersSettings = _Configuration.GetSection("UsersSettings");
            services.Configure<UsersSettings>(usersSettings);

            // Create connection to postgress
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<UsersContext>(options =>
                    options.UseNpgsql(connectionString));

            // Add service
            services.AddScoped<Services.IBaseService, Services.BaseService>();
            services.AddScoped<Services.IGroupService, Services.GroupService>();
            services.AddScoped<Services.IAuthService, Services.AuthService>();
            services.AddScoped<Services.IUserService, Services.UserService>();

            // Setup mvc
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
