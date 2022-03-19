using DotFramework.Core;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DotFramework.Infra.ExceptionHandling
{
    public abstract class ExceptionHandlerBase<TExceptionHandler> : SingletonProvider<TExceptionHandler>, IExceptionHandler
        where TExceptionHandler : class
    {
        public bool HandleException(ref Exception ex, [CallerMemberName]string methodName = "")
        {
            MethodBase methodBase = ReflectionHelper.GetCallerMethod(methodName);
            return HandleException(ref ex, methodBase);
        }

        public bool HandleException(ref Exception ex, object caller, [CallerMemberName]string methodName = "")
        {
            return HandleException(ref ex, caller.GetType().FullName, methodName);
        }

        public bool HandleException(ref Exception ex, Type callerType, [CallerMemberName]string methodName = "")
        {
            return HandleException(ref ex, callerType.FullName, methodName);
        }

        public bool HandleException(ref Exception ex, MethodBase methodBase)
        {
            return HandleException(ref ex, methodBase.DeclaringType.FullName, methodBase.Name);
        }

        public abstract bool HandleException(ref Exception ex, string className, string methodName);
    }

    public interface IExceptionHandler
    {
        bool HandleException(ref Exception ex, [CallerMemberName]string methodName = "");
        bool HandleException(ref Exception ex, object caller, [CallerMemberName]string methodName = "");
        bool HandleException(ref Exception ex, Type callerType, [CallerMemberName]string methodName = "");
        bool HandleException(ref Exception ex, MethodBase methodBase);
        bool HandleException(ref Exception ex, string className, string methodName);
    }
}
