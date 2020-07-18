using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#HumanName
    /// </summary>
    public class HumanNameType
    {
        private CommonEnum.NameUseCode? _use;
        public string? use
        {
            get
            {
                return _use == null ? null : _use.GetStringValue();
            }
            set
            {
                CommonEnum.NameUseCode prs;
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
        public string? text { get; set; }
        public string? family { get; set; }
        public string[]? given { get; set; }
        public string[]? prefix { get; set; }
        public string[]? suffix { get; set; }
        public PeriodType? period { get; set; }
    }
}
