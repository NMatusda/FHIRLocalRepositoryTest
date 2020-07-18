using LocalFHIRRepository.Util;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/patient-definitions.html#Patient.contact
    /// </summary>
    public class ContactType
    {
        public CodeableConceptType[]? relationship { get; set; }
        public HumanNameType? name { get; set; }
        public ContactPointType[]? telecom { get; set; }
        public AddressType? address { get; set; }
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
        public ReferencesType? organization { get; set; }
        public PeriodType? period { get; set; }
    }
}
