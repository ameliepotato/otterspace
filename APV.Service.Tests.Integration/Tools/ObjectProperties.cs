using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APV.Service.Tests.Integration.Tools
{
    internal class ObjectProperties
    {
        public static T? GetPropertyValue<T>(JsonDocument obj, string propertyName)
        {
            return obj.RootElement.GetProperty(propertyName).Deserialize<T>();
        }

        public static bool IsPropertyValueEqual<T>(JsonDocument obj, string propertyName, T propertyValue)
        {
            T? val = obj.RootElement.GetProperty(propertyName).Deserialize<T>();
            if(val != null)
            {
                return val.Equals(propertyValue);
            }
            return false;
        }
    }
}
