using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#ContactPoint
    /// </summary>
    public class ContactPointType
    {
        private CommonEnum.ContactPointSystemCode? _system;
        public string? system
        {
            get
            {
                return _system == null ? null : _system.GetStringValue();
            }
            set
            {
                CommonEnum.ContactPointSystemCode prs;
                if (Enum.TryParse(value, out prs))
                {
                    _system = prs;
                }
                else
                {
                    _system = null;
                }
            }
        }
        public string? value { get; set; }
        private CommonEnum.ContactPointUseCode? _use;
        public string? use
        {
            get
            {
                return _use == null ? null : _use.GetStringValue();
            }
            set
            {
                CommonEnum.ContactPointUseCode prs;
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
        private string? _rank;
        public string? rank
        {
            get
            {
                return _rank;
            }
            set
            {
                if (value == null)
                {
                    _rank = null;
                }
                else
                {
                    _rank = value.IsMatchPositiveIntRegex() ? value : null;
                }
            }
        }
        public PeriodType? period { get; set; }
    }
}
