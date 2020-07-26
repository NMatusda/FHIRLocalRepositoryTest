using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/narrative.html#Narrative
    /// </summary>
    public class NarrativeType
    {
        private CommonEnum.NarrativeStatusCode? _status;
        public string? status
        {
            get
            {
                return _status == null ? null : _status.GetStringValue();
            }
            set
            {
                CommonEnum.NarrativeStatusCode prs;
                if (Enum.TryParse(value, out prs))
                {
                    _status = prs;
                }
                else
                {
                    _status = null;
                }
            }
        }
        public string? div { get; set; }
    }
}
