using System;
using System.Web.Http.Controllers;
using System.ServiceModel;
using System.Web.Http;
using System.Net;
using DotFramework.Infra.ExceptionHandling;
using DotFramework.Core;

namespace DotFramework.Infra.Web.API
{
    public class ApiExceptionHandler : ExceptionHandlerBase<ApiExceptionHandler>
    {
        private ApiExceptionHandler()
        {

        }

        public bool HandleException(ref Exception ex, HttpControllerContext controllercontext, HttpActionContext actionContext)
        {
            var controllerName = controllercontext.Controller.ToString();
            var actionName = (actionContext.ActionDescriptor as ReflectedHttpActionDescriptor).MethodInfo.Name;

            return HandleException(ref ex, controllerName, actionName);
        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            try
            {
                if (ex is ApiCustomException)
                {
                    reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ApiCustomPolicy, className, methodName);
                    ex = new ApiCustomException(ex.Message, ex);
                }
                if (ex is UnauthorizedHttpException || ex is UnauthorizedAccessException)
                {
                    ex = new HttpResponseException(HttpStatusCode.Unauthorized);
                    reThrow = true;
                }
                else if (ex is HttpResponseException)
                {
                    reThrow = true;
                }
                else if (ex is ExceptionBase)
                {
                    //Do Nothing
                    //It was handled
                }
                else if (ex is FaultException<ProcessExecutionFault>)
                {
                    ex = (ex as FaultException<ProcessExecutionFault>).Detail.InnerExceptionDetail.Parse();
                }
                else
                {
                    reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ApiPolicy, className, methodName);
                }
            }
            catch (Exception exp)
            {
                ex = exp;
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
