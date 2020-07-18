using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Period
    /// </summary>
    public class PeriodType
    {
        private string? _start;
        public string? start
        {
            get
            {
                return _start;
            }
            set
            {
                if (value == null)
                {
                    _start = null;
                }
                else
                {
                    _start = value.IsMatchDateTimeRegex() ? value : null;
                }
            }
        }
        private string? _end;
        public string? end
        {
            get
            {
                return _end;
            }
            set
            {
                if (value == null)
                {
                    _end = null;
                }
                else
                {
                    _end = value.IsMatchDateTimeRegex() ? value : null;
                }
            }
        }
    }
}
