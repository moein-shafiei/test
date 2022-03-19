using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.ExceptionHandling
{
    public class ExceptionHandlerInterceptionBehavior : IInterceptionBehavior
    {
        public virtual IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result = null;

            try
            {
                result = getNext()(input, getNext);

                if (result.Exception != null)
                {
                    ExceptionDispatchInfo.Capture(result.Exception).Throw();
                }

                return result;
            }
            catch (Exception ex)
            {
                if (HandleException(ref ex, input))
                {
                    throw ex;
                }

                return null;
            }
        }

        public virtual IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public virtual bool WillExecute
        {
            get
            {
                return true;
            }
        }

        public virtual bool HandleException(ref Exception ex, IMethodInvocation input)
        {
            return false;
        }
    }
}
