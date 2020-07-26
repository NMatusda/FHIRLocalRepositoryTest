using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Address
    /// </summary>
    public class AddressType
    {
        private CommonEnum.AddressUseCode? _use;
        public string? use
        {
            get
            {
                return _use == null ? null : _use.GetStringValue();
            }
            set
            {
                CommonEnum.AddressUseCode prs;
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
        private CommonEnum.AddressTypeCode? _type;
        public string? type
        {
            get
            {
                return _type == null ? null : _type.GetStringValue();
            }
            set
            {
                CommonEnum.AddressTypeCode prs;
                if (Enum.TryParse(value, out prs))
                {
                    _type = prs;
                }
                else
                {
                    _type = null;
                }
            }
        }
        public string? text { get; set; }
        public string[]? line { get; set; }
        public string? city { get; set; }
        public string? district { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }
        public PeriodType? period { get; set; }
    }
}
