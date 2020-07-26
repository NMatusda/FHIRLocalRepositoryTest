using RsyncFHIR.DataTypes;
using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.Resource
{
    /// <summary>
    /// https://hl7.org/FHIR/bundle.html
    /// </summary>
    public class PatientBundleResource : BaseResource
    {
        private const string BundleResourceType = "Bundle";
        public override string? resourceType
        {
            get
            {
                return BundleResourceType;
            }
            set
            {
                if (value == null) throw new NullReferenceException();
                if (!value.Equals(BundleResourceType)) throw new InvalidCastException();
            }
        }
        public IdentifierType? identifier { get; set; }
        private CommonEnum.BundleTypeCode? _type;
        public string? type
        {
            get
            {
                return _type == null ? null : _type.GetStringValue();
            }
            set
            {
                CommonEnum.BundleTypeCode prs;
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
        private string? _timestamp;
        public string? timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                if (value == null)
                {
                    _timestamp = null;
                }
                else
                {
                    _timestamp = value.IsMatchInstantRegex() ? value : null;
                }
            }
        }
        public uint? total { get; set; }
        public BundleLinkType? link { get; set; }
        public PatientEntryType[]? entry { get; set; }
        public SignatureType? signature { get; set; }

        /// <summary>
        /// https://hl7.org/FHIR/patient-definitions.html#Patient.link.type
        /// </summary>
        public class BundleLinkType
        {
            public string? relation { get; set; }
            private string? _url;
            public string? url
            {
                get
                {
                    return _url;
                }
                set
                {
                    if (value == null)
                    {
                        _url = null;
                    }
                    else
                    {
                        _url = value.IsMatchUriRegex() ? value : null;
                    }
                }
            }
        }

        public class PatientEntryType
        {
            public BundleLinkType[]? link { get; set; }
            private string? _fullUrl;
            public string? fullUrl
            {
                get
                {
                    return _fullUrl;
                }
                set
                {
                    if (value == null)
                    {
                        _fullUrl = null;
                    }
                    else
                    {
                        _fullUrl = value.IsMatchUriRegex() ? value : null;
                    }
                }
            }
            public PatientResource? resource { get; set; }
            public PatientEntrySearchType? search { get; set; }
            public PatientEntryRequest? request { get; set; }
            public PatientEntryResponse? response { get; set; }

            public class PatientEntrySearchType
            {
                private CommonEnum.SearchEntryModeCode? _mode;
                public string? mode
                {
                    get
                    {
                        return _mode == null ? null : _mode.GetStringValue();
                    }
                    set
                    {
                        CommonEnum.SearchEntryModeCode prs;
                        if (Enum.TryParse(value, out prs))
                        {
                            _mode = prs;
                        }
                        else
                        {
                            _mode = null;
                        }
                    }
                }
                private string? _score;
                public string? score
                {
                    get
                    {
                        return _score;
                    }
                    set
                    {
                        if (value == null)
                        {
                            _score = null;
                        }
                        else
                        {
                            _score = value.IsMatchDecimalRegex() ? value : null;
                        }
                    }
                }
            }

            public class PatientEntryRequest
            {
                private CommonEnum.HTTPVerbCode? _method;
                public string? method
                {
                    get
                    {
                        return _method == null ? null : _method.GetStringValue();
                    }
                    set
                    {
                        CommonEnum.HTTPVerbCode prs;
                        if (Enum.TryParse(value, out prs))
                        {
                            _method = prs;
                        }
                        else
                        {
                            _method = null;
                        }
                    }
                }
                private string? _url;
                public string? url
                {
                    get
                    {
                        return _url;
                    }
                    set
                    {
                        if (value == null)
                        {
                            _url = null;
                        }
                        else
                        {
                            _url = value.IsMatchUriRegex() ? value : null;
                        }
                    }
                }
                public string? ifNoneMatch { get; set; }
                private string? _ifModifiedSince;
                public string? ifModifiedSince
                {
                    get
                    {
                        return _ifModifiedSince;
                    }
                    set
                    {
                        if (value == null)
                        {
                            _ifModifiedSince = null;
                        }
                        else
                        {
                            _ifModifiedSince = value.IsMatchInstantRegex() ? value : null;
                        }
                    }
                }
                public string? ifMatch { get; set; }
                public string? ifNoneExist { get; set; }
            }

            public class PatientEntryResponse
            {
                public string? status { get; set; }
                private string? _location;
                public string? location
                {
                    get
                    {
                        return _location;
                    }
                    set
                    {
                        if (value == null)
                        {
                            _location = null;
                        }
                        else
                        {
                            _location = value.IsMatchUriRegex() ? value : null;
                        }
                    }
                }
                public string? etag { get; set; }
                private string? _lastModified;
                public string? lastModified
                {
                    get
                    {
                        return _lastModified;
                    }
                    set
                    {
                        if (value == null)
                        {
                            _lastModified = null;
                        }
                        else
                        {
                            _lastModified = value.IsMatchInstantRegex() ? value : null;
                        }
                    }
                }
                public BaseResource? outcome { get; set; }
            }
        }
    }
}
