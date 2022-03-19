using System;

namespace DotFramework.Infra
{
    public class ErrorEventArgs : EventArgs
    {
        public Exception Error { get; set; }

        public ErrorEventArgs(Exception ex) : base()
        {
            Error = ex;
        }
    }
}
