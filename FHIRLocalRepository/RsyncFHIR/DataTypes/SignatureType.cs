using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Signature
    /// </summary>
    public class SignatureType
    {
        public CodingType? _type { get; set; }
        private string? _when;
        public string? when
        {
            get
            {
                return _when;
            }
            set
            {
                if (value == null)
                {
                    _when = null;
                }
                else
                {
                    _when = value.IsMatchInstantRegex() ? value : null;
                }
            }
        }
        public ReferencesType? who { get; set; }
        public ReferencesType? onBehalfOf { get; set; }
        private string? _targetFormat;
        public string? targetFormat
        {
            get
            {
                return _targetFormat;
            }
            set
            {
                if (value == null)
                {
                    _targetFormat = null;
                }
                else
                {
                    _targetFormat = value.IsMatchCodeRegex() ? value : null;
                }
            }
        }
        private string? _sigFormat;
        public string? sigFormat
        {
            get
            {
                return _sigFormat;
            }
            set
            {
                if (value == null)
                {
                    _sigFormat = null;
                }
                else
                {
                    _sigFormat = value.IsMatchCodeRegex() ? value : null;
                }
            }
        }
        private string? _data;
        public string? data
        {
            get
            {
                return _data;
            }
            set
            {
                if (value == null)
                {
                    _data = null;
                }
                else
                {
                    _data = value.IsMatchBase64BinaryRegex() ? value : null;
                }
            }
        }
    }
}
