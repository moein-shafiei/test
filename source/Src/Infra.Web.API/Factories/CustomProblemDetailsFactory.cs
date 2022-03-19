using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExceptionMetadataService _exceptionMetadataService;

        /// <inheritdoc />
        public CustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options, IWebHostEnvironment webHostEnvironment, IExceptionMetadataService exceptionMetadataService = null)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _exceptionMetadataService = exceptionMetadataService;
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            statusCode ??= 500; // <-- Microsoft hard codes the value? Why aren't they using StatusCodes.Status500InternalServerError?

            ProblemDetails problemDetails = null;

            var context = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (context?.Error != null)
            {
                Exception exception = context.Error;

                if (exception is ExceptionBase baseException)
                {
                    problemDetails = CreateHandledProblem(httpContext, instance, baseException);
                }
                else
                {
                    try
                    {
                        if (context is IExceptionHandlerPathFeature pathContext)
                        {
                            ApiExceptionHandler.Instance.HandleException(ref exception, this.GetType().FullName, pathContext.Path);
                        }
                        else
                        {
                            ApiExceptionHandler.Instance.HandleException(ref exception);
                        }
                    }
                    catch(Exception ex)
                    {
                        exception = ex;
                    }

                    problemDetails = CreateHandledProblem(httpContext, instance, exception as ExceptionBase);
                }
            }

            if (problemDetails == null)
            {
                //	default exception handler
                problemDetails = CreateDefaultProblem(statusCode, title, type, detail, instance);
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            statusCode = problemDetails.Status;
            // <-- The result serializer doesn't use the status from the 
            //	ProblemDetails object to set this code. You have to set
            //	it by hand.
            httpContext.Response.StatusCode = statusCode.Value;

            return problemDetails;
        }

        private ProblemDetails CreateDefaultProblem(int? statusCode, string title, string type, string detail, string instance)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Type = type,
                Title = title
            };

            if (_webHostEnvironment.IsDevelopment())
            {
                problemDetails.Detail = detail;
                problemDetails.Instance = instance;
            }

            return problemDetails;
        }

        private ProblemDetails CreateHandledProblem(HttpContext httpContext, string instance, ExceptionBase exception)
        {
            int statusCode;

            if (exception is UnauthorizedHttpException)
            {
                statusCode = 401;
            }
            else
            {
                statusCode = 400;
            }

            ProblemDetails problemDetails;

            ExceptionBase innerMostException = exception;

            while (innerMostException.InnerException is ExceptionBase && !(innerMostException.InnerException is InitialException))
            {
                innerMostException = (ExceptionBase)innerMostException.InnerException;
            }

            problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Type = _exceptionMetadataService?.GetMetadata(innerMostException)?.RFC ?? innerMostException.RFC,
                Title = exception.Message,
                Extensions =
                {
                    { "traceId", exception.TraceID }
                }
            };

            if (_webHostEnvironment.IsDevelopment())
            {

                problemDetails.Detail = CreateFormattedMessage(innerMostException);
                problemDetails.Instance = instance;
            }

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status = problemDetails.Status ?? statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }
        }

        private string CreateFormattedMessage(Exception exception)
        {
            StringWriter writer = null;
            StringBuilder stringBuilder = null;
            try
            {
                writer = new StringWriter(CultureInfo.InvariantCulture);
                TextExceptionFormatter formatter = new TextExceptionFormatter(writer, exception);
                formatter.Format();
                stringBuilder = writer.GetStringBuilder();

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
