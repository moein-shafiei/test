using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotFramework.Infra.Security.Model
{
    public class ApplicationHierarchiesResponseModel
    {
        [JsonProperty("HID")]
        public String HIDString { get; set; }

        [JsonProperty("Code")]
        public String Code { get; set; }

        [JsonProperty("Name")]
        public String Name { get; set; }

        [JsonProperty("Description")]
        public String Description { get; set; }

        [JsonProperty("Type")]
        public Byte Type { get; set; }

        [JsonProperty("Path")]
        public String Path { get; set; }

        [JsonProperty("Controller Name")]
        public String ControllerName { get; set; }

        [JsonProperty("Action Name")]
        public String ActionName { get; set; }

        [JsonProperty("Route Values")]
        public String RouteValues { get; set; }

        [JsonProperty("Icon Type")]
        public Byte? IconType { get; set; }

        [JsonProperty("Icon")]
        public String Icon { get; set; }

        [JsonProperty("Need Authorization")]
        public Boolean NeedAuthorization { get; set; }

        public Dictionary<String, Object> RouteValueDictionary
        {
            get
            {
                if (String.IsNullOrEmpty(RouteValues))
                {
                    return null;
                }

                var keyValuePairs = RouteValues.Split(';').Select(x => x.Split('='))
                                                               .Where(x => x.Length == 2)
                                                               .ToDictionary(x => x.First(), x => (object)x.Last());

                return keyValuePairs;
            }
        }
    }
}
