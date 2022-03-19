using System;
using DotFramework.Infra.ExceptionHandling;
using DotFramework.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DotFramework.Infra.Web.API
{
    public class ApiExceptionHandler : ExceptionHandlerBase<ApiExceptionHandler>
    {
        private ApiExceptionHandler()
        {

        }

        public bool HandleException(ref Exception ex, ControllerActionDescriptor controllerActionDescriptor)
        {
            var controllerName = controllerActionDescriptor.ControllerTypeInfo.FullName;
            var actionName = controllerActionDescriptor.ActionName;

            return HandleException(ref ex, controllerName, actionName);
        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is UnauthorizedHttpException)
            {
                reThrow = true;
            }
            else if (ex is UnauthorizedAccessException)
            {
                ex = new UnauthorizedHttpException();
                reThrow = true;
            }
            else if (ex is HttpException)
            {
                reThrow = true;
            }
            else if (ex is ExceptionBase)
            {
                //Do Nothing
                //It was handled
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ApiPolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
