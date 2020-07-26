using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Identifier
    /// </summary>
    public class IdentifierType
    {
        private CommonEnum.IdentifierUseCode? _use;
        public string? use
        {
            get
            {
                return _use == null ? null : _use.GetStringValue();
            }
            set
            {
                CommonEnum.IdentifierUseCode prs;
                if (Enum.TryParse(value, out prs))
                {
                    _use = prs;
                }
                else
                {
                    _use = null;
                }
            }
        }
        public CodeableConceptType? type { get; set; }
        private string? _system;
        public string? system
        {
            get
            {
                return _system;
            }
            set
            {
                if (value == null)
                {
                    _system = null;
                }
                else
                {
                    _system = value.IsMatchUriRegex() ? value : null;
                }
            }
        }
        public string? value { get; set; }
        public PeriodType? period { get; set; }
        public ReferencesType? assigner { get; set; }
    }
}
