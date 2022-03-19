using System;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class SecureDomainModelBase : DomainModelBase
    {
        private Int64 _SessionID;
        [DataMember]
        public Int64 SessionID
        {
            get
            {
                return _SessionID;
            }
            set
            {
                SetProperty(ref _SessionID, value);
            }
        }
    }
}
