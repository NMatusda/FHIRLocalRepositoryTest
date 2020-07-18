using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Util
{
    public static class CommonRegex
    {
        private const string DateRegex = @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1]))?)?";
        public static bool IsMatchDateRegex(this string value) => Regex.IsMatch(value, DateRegex);

        private const string DateTimeRegex = @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00)))?)?)?";
        public static bool IsMatchDateTimeRegex(this string value) => Regex.IsMatch(value, DateTimeRegex);

        private const string Base64BinaryRegex = @"(\s*([0-9a-zA-Z\+\=]){4}\s*)+";
        public static bool IsMatchBase64BinaryRegex(this string value) => Regex.IsMatch(value, Base64BinaryRegex);

        private const string IdRegex = @"[A-Za-z0-9\-\.]{1,64}";
        public static bool IsMatchIdRegex(this string value) => Regex.IsMatch(value, IdRegex);

        private const string InstantRegex = @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))";
        public static bool IsMatchInstantRegex(this string value) => Regex.IsMatch(value, InstantRegex);

        private const string UriRegex = @"\S*";
        public static bool IsMatchUriRegex(this string value) => Regex.IsMatch(value, UriRegex);

        private const string CodeRegex = @"[^\s]+(\s[^\s]+)*";
        public static bool IsMatchCodeRegex(this string value) => Regex.IsMatch(value, CodeRegex);

        private const string PositiveIntRegex = @"\+?[1-9][0-9]*";  // positiveIntの正規表現については記載間違い? https://hl7.org/FHIR/datatypes.html#positiveInt
        public static bool IsMatchPositiveIntRegex(this string value) => Regex.IsMatch(value, PositiveIntRegex);

        private const string DecimalRegex = @"-?(0|[1-9][0-9]*)(\.[0-9]+)?([eE][+-]?[0-9]+)?";
        public static bool IsMatchDecimalRegex(this string value) => Regex.IsMatch(value, DecimalRegex);
    }
}
