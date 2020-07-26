using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.Util
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }
        public StringValueAttribute(string value) => this.StringValue = value;
    }

    public static class CommonAttribute
    {
        public static string? GetStringValue(this Enum value)
        {
            var type = value.GetType();
            var fieldinfo = type.GetField(value.ToString());

            if (fieldinfo == null) return null;

            StringValueAttribute[]? attribs = fieldinfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attribs == null) return null;

            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}
