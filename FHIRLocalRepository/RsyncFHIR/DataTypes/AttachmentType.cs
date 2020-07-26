using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#Attachment
    /// </summary>
    public class AttachmentType
    {
        private string? _contentType;
        public string? contentType
        {
            get
            {
                return _contentType;
            }
            set
            {
                if (value == null)
                {

                }
                else
                {
                    _contentType = value.IsMatchCodeRegex() ? value : null;
                }
            }
        }
        private string? _language;
        public string? language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value == null)
                {
                    _language = null;
                }
                else
                {
                    _language = value.IsMatchCodeRegex() ? value : null;
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
        public string? url { get; set; }
        public uint? size { get; set; }
        private string? _hash;
        public string? hash
        {
            get
            {
                return _hash;
            }
            set
            {
                if (value == null)
                {
                    _hash = null;
                }
                else
                {
                    _hash = value.IsMatchBase64BinaryRegex() ? value : null;
                }
            }
        }
        public string? title { get; set; }
        private string? _creation;
        public string? creation
        {
            get
            {
                return _creation;
            }
            set
            {
                if (value == null)
                {
                    _creation = null;
                }
                else
                {
                    _creation = value.IsMatchDateTimeRegex() ? value : null;
                }
            }
        }
    }
}
