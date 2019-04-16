using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Application.Gateway
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
            // Get jwt-secret
            var secret = _Configuration.GetValue<string>("Secret");

            // Get authentication provider
            var authenticationProvider = "AuthenticationProvider";

            // Create key
            var key = Encoding.ASCII.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(key);

            // Setup validation
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };

            // Configure authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(authenticationProvider, options =>
            {
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        var token = context.SecurityToken as JwtSecurityToken;
                        var payload = token.Payload;

                        // Create new header containing user-id
                        if (payload.TryGetValue("user_id", out object userIdObj))
                        {
                            var userId = userIdObj as string;
                            context.Request.Headers.Add("UserId", userId);
                        }

                        // create new header containing user-role
                        if (payload.TryGetValue("user_role", out object userRoleObj))
                        {
                            var userRole = userRoleObj as string;
                            context.Request.Headers.Add("UserRole", userRole);
                        }

                        return Task.CompletedTask;
                    }
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CrossOriginRequest",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CrossOriginRequest");
            app.UseOcelot().Wait();
        }
    }
}