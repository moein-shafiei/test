using DotFramework.Core.Serialization;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class SerializedResult
    {
        public SerializedResult(dynamic dynamicObject)
        {
            DynamicObject = dynamicObject;
        }

        [DataMember]
        public string SerializedObject { get; set; }

        public dynamic DynamicObject
        {
            get
            {
                if (SerializedObject == null)
                {
                    return null;
                }

                return JsonSerializerHelper.Deserialize<dynamic>(SerializedObject);
            }
            set
            {
                SerializedObject = JsonSerializerHelper.SimpleSerialize(value);
            }
        }
    }
}
