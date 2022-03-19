using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra.Web
{
    public class ApplicationManager : SingletonProvider<ApplicationManager>
    {
        private ApplicationManager()
        {

        }

        private String _ApplicationCode;
        public String ApplicationCode
        {
            get
            {
                if (String.IsNullOrEmpty(_ApplicationCode))
                {
                    //_ApplicationCode = BuildManager.GetGlobalAsaxType().BaseType.Assembly.GetApplicationName();
                    if(Assembly.GetEntryAssembly() != null)
                    {
                        _ApplicationCode = Assembly.GetEntryAssembly().GetName().Name;
                    }
                    else
                    {
                        _ApplicationCode = "IIS Express";
                    }
                }

                return _ApplicationCode;
            }
        }
    }
}
