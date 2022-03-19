using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using DotFramework.Infra.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DotFramework.Infra.Web.API
{
    public abstract class WebAPIStartup
    {
        public abstract string AplicationCode { get; }

        public IConfiguration Configuration { get; }

        public WebAPIStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
        }

        public virtual void InitializeServices(IServiceCollection services)
        {
            services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public virtual void InitializeApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeTraceLogger();
            HttpContextProvider.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }

        public virtual void InitializeTraceLogger()
        {
            TraceLogManager.Instance.Initialize(AplicationCode);
        }
    }
}
