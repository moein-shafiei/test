using System;

namespace DotFramework.Infra.ServiceFactory
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public OperationTypeAttribute(string name)
        {
            Name = name;
        }
    }
}
