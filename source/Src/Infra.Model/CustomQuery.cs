using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class CustomQuery
    {
        [DataMember]
        public string ProcedureName { get; set; }

        private Dictionary<String, Object> _Parameters;
        [DataMember]
        public Dictionary<String, Object> Parameters
        {
            get
            {
                if (_Parameters == null)
                {
                    _Parameters = new Dictionary<String, Object>();
                }

                return _Parameters;
            }
            set
            {
                _Parameters = value;
            }
        }
    }
}
