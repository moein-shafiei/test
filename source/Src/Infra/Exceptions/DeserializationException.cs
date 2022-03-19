using System;

namespace DotFramework.Infra
{
    public class DeserializationException : ExceptionBase
    {
        public string Content { get; private set; }

        public DeserializationException(string content) : base("Error in deserialization")
        {
            Content = content;
        }

        public DeserializationException(string content, Exception inner) : base("Error in deserialization", inner)
        {
            Content = content;
        }
    }
}
