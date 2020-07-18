using LocalFHIRRepository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/resource.html#Meta
    /// </summary>
    public class MetaType
    {
        private string? _id;
        public string? id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == null)
                {
                    _id = null;
                }
                else
                {
                    _id = value.IsMatchIdRegex() ? value : null;
                }
            }
        }
        private string? _lastUpdated;
        public string? lastUpdated
        {
            get
            {
                return _lastUpdated;
            }
            set
            {
                if (value == null)
                {
                    _lastUpdated = null;
                }
                else
                {
                    _lastUpdated = value.IsMatchInstantRegex() ? value : null;
                }
            }
        }
        private string? _source;
        public string? source
        {
            get
            {
                return _source;
            }
            set
            {
                if (value == null)
                {
                    _source = null;
                }
                else
                {
                    _source = value.IsMatchUriRegex() ? value : null;
                }
            }
        }
        public string[]? profile { get; set; }
        public CodingType[]? security { get; set; }
        public CodingType[]? tag { get; set; }
    }
}
