using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/references.html#Reference
    /// </summary>
    public class ReferencesType
    {
        public string? reference { get; set; }
        private string? _type;
        public string? type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == null)
                {
                    _type = null;
                }
                else
                {
                    _type = value.IsMatchUriRegex() ? value : null;
                }
            }
        }
        public IdentifierType? identifier { get; set; }
        public string? display { get; set; }
    }
}
