using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra
{
    public class InterceptionInvocationException : Exception
    {
        public InterceptionInvocationException() : this("Interception Invocation Error")
        {
        }

        public InterceptionInvocationException(string message) : base(message)
        {
        }

        public InterceptionInvocationException(Exception innerException) : this("Interception Invocation Error", innerException)
        {
        }

        public InterceptionInvocationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
