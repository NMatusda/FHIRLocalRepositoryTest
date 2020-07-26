using RsyncFHIR.DataTypes;
using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.Resource
{
    /// <summary>
    /// https://hl7.org/FHIR/resource.html
    /// </summary>
    public abstract class BaseResource
    {
        public abstract string? resourceType { get; set; }
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
        public MetaType? meta { get; set; }
        private string? _implicitRules;
        public string? implicitRules
        {
            get
            {
                return _implicitRules;
            }
            set
            {
                if (value == null)
                {
                    _implicitRules = null;
                }
                else
                {
                    _implicitRules = value.IsMatchUriRegex() ? value : null;
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
    }
}
