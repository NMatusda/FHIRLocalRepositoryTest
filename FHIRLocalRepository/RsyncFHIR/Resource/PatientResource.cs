using Newtonsoft.Json;
using RsyncFHIR.DataTypes;
using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RsyncFHIR.Resource
{
    /// <summary>
    /// https://hl7.org/FHIR/patient.html
    /// </summary>
    [JsonObject]
    public class PatientResource : BaseResource
    {
        private const string PatientResourceType = "Patient";
        [JsonProperty("resourceType")]
        public override string? resourceType
        {
            get
            {
                return PatientResourceType;
            }
            set
            {
                if (value == null) throw new NullReferenceException();
                if (!value.Equals(PatientResourceType)) throw new InvalidCastException();
            }
        }
        [JsonProperty("text")]
        public NarrativeType? text { get; set; }
        public IdentifierType[]? identifier { get; set; }
        public bool? active { get; set; }
        public HumanNameType[]? name { get; set; }
        public ContactPointType[]? telecom { get; set; }
        private CommonEnum.AdministrativeGenderCode? _gender;
        public string? gender
        {
            get
            {
                return _gender == null ? null : _gender.GetStringValue();
            }
            set
            {
                CommonEnum.AdministrativeGenderCode prs;
                if (Enum.TryParse(value, out prs))
                {
                    _gender = prs;
                }
                else
                {
                    _gender = null;
                }
            }
        }
        private string? _birthDate;
        public string? birthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value == null)
                {
                    _birthDate = null;
                }
                else
                {
                    _birthDate = value.IsMatchDateRegex() ? value : null;
                }
            }
        }
        public bool? deceasedBoolean { get; set; }
        private string? _deceasedDateTime;
        public string? deceasedDateTime
        {
            get
            {
                return _deceasedDateTime;
            }
            set
            {
                if (value == null)
                {
                    _deceasedDateTime = null;
                }
                else
                {
                    _deceasedDateTime = value.IsMatchDateTimeRegex() ? value : null;
                }
            }
        }
        public AddressType[]? address { get; set; }
        public CodeableConceptType? maritalStatus { get; set; }
        public bool? multipleBirthBoolean { get; set; }
        public int? multipleBirthInteger { get; set; }
        public AttachmentType[]? photo { get; set; }
        public ContactType[]? contact { get; set; }
        public CommunicationType[]? communication { get; set; }
        public ReferencesType? generalPractitioner { get; set; }
        public ReferencesType? managingOrganization { get; set; }
        public PatientLinkType[]? link { get; set; }

        /// <summary>
        /// https://hl7.org/FHIR/patient-definitions.html#Patient.link.type
        /// </summary>
        public class PatientLinkType
        {
            public ReferencesType? other { get; set; }
            private CommonEnum.LinkTypeCode? _type;
            public string? type
            {
                get
                {
                    return _type == null ? null : _type.GetStringValue();
                }
                set
                {
                    CommonEnum.LinkTypeCode prs;
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
        }
    }
}
