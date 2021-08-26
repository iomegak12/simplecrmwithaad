using CRMAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                    options.Audience = "13eb3f81-7fea-439c-9333-3ee14a2df267";
                    options.Authority = "https://login.microsoftonline.com/ea7e4f72-59a3-4e64-9fec-7902d023ed63";

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddControllers();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("DefaultPolicy",
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowAnyOrigin();
                    });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.Authority = "https://login.microsoftonline.com/ea7e4f72-59a3-4e64-9fec-7902d023ed63";
            //            options.Audience = "8dd398d3-069d-452f-a3e8-f4f7b312ab54";

            //            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //            {
            //                ValidateAudience = true
            //            };
            //        });



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

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
