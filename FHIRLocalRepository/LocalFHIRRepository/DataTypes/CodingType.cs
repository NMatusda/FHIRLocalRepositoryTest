using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Coding
    /// </summary>
    public class CodingType
    {
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
        public string? version { get; set; }
        private string? _code;
        public string? code
        {
            get
            {
                return _code;
            }
            set
            {
                if (value == null)
                {
                    _code = null;
                }
                else
                {
                    _code = value.IsMatchCodeRegex() ? value : null;
                }
            }
        }
        public string? display { get; set; }
        public bool? userSelected { get; set; }
    }
}