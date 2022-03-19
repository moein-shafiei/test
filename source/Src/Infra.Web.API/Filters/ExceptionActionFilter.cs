using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotFramework.Infra.Web.API
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExceptionActionFilter(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        #region Overrides of ExceptionFilterAttribute

        public override void OnException(ExceptionContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            Type controllerType = controllerActionDescriptor.ControllerTypeInfo;

            var controllerBase = typeof(ControllerBase);

            // Api's implements ControllerBase but not Controller
            if (controllerType.IsSubclassOf(controllerBase))
            {
                var exception = context.Exception;

                try
                {
                    ApiExceptionHandler.Instance.HandleException(ref exception, controllerActionDescriptor);
                    context.Exception = exception;
                }
                catch (Exception ex)
                {
                    context.Exception = ex;
                }
            }

            base.OnException(context);
        }

        #endregion
    }
}
