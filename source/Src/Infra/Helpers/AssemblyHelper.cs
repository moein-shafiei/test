using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public static class AssemblyHelper
    {
        public static string GetAssemblyVersion(Assembly assembly)
        {
            return AssemblyHelper.ParseVersionNumber(assembly).ToString();
        }

        private static Version ParseVersionNumber(Assembly assembly)
        {
            AssemblyName assemblyName = new AssemblyName(assembly.FullName);
            return assemblyName.Version;
        }
    }
}
