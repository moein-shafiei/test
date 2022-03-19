using System;

namespace DotFramework.Infra
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ApplicationNameAttribute : Attribute
    {
        public string Name { get; set; }

        public ApplicationNameAttribute(string name)
        {
            Name = name;
        }
    }
}
