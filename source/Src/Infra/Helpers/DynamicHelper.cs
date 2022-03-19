using Newtonsoft.Json.Linq;

namespace DotFramework.Infra
{
    public static class DynamicHelper
    {
        public static T GetTypedDefinition<T>(dynamic obj) where T : class
        {
            if (obj != null)
            {
                string json = null;

                if (obj.GetType() == typeof(JObject))
                {
                    json = obj.ToString();
                }
                else
                {
                    json = JSONSerializerHelper.SimpleSerialize(obj);
                }

                return JSONSerializerHelper.Deserialize<T>(json);
            }
            else
            {
                return null;
            }
        }
    }
}
