using DotFramework.Core.Configuration;
using DotFramework.Infra.Security;
using DotFramework.Infra.Web;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using DotFramework.Infra.Web.API.Auth;
using DotFramework.Infra.Web.API.Auth.Middleware;
using DotFramework.Infra.Web.API.Auth.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DotFramework.Infra.Web.API;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DotFramework.Infra.ExceptionHandling;
using System.Collections.Generic;
using DotFramework.Infra;
using DotFramework.Infra.Web.API.Controller;
using WebApplication1.Middleware;

namespace WebApplication1
{
    public class Startup : SecureWebAPIStartup
    {
        public override string AplicationCode => AppSettingsManager.Instance.Get("ApplicationCode");

        public Startup(IConfiguration configuration) : base(configuration)
        {
            AccessTokenExpireTimeSpan = new TimeSpan(0, 30, 0);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services
                .AddControllers(options =>
                {
                    options.AllowEmptyInputInBodyModelBinding = true;
                    options.Filters.Add<ExceptionActionFilter>();
                })
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = null;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);

                        // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSwaggerGen(c=>c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "My API"
            }));

            //services.AddSingleton<IExceptionMetadataService>(new ExceptionMetadataService("test.com"));

            InitializeServices(services);
            InitializeAuthServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
            });

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            InitializeApplication(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeAuthApplication(app, env);
        }

        public override void InitializeApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.InitializeApplication(app, env);
            //app.UseMiddleware<TestMiddleware>();
        }

        protected override void InitializeAuthApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.InitializeAuthApplication(app, env);
            AuthService.Instance.Initialize(AppSettingsManager.Instance.Get("AuthEndpointPath"), AppSettingsManager.Instance.Get("ClientID"), AppSettingsManager.Instance.Get("ClientSecret"));
        }
    }
}
