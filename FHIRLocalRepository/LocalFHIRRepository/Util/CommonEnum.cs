using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Util
{
    public static class CommonEnum
    {
        /// <summary>
        /// stringをEnumに変換する
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="s"></param>
        /// <param name="enm"></param>
        /// <returns></returns>
        public static bool TryParseToEnum<TEnum>(this string s, out TEnum enm) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (typeof(TEnum).IsEnum)
            {
                return Enum.TryParse(s, out enm) && Enum.IsDefined(typeof(TEnum), enm);
            }
            else
            {
                enm = default(TEnum);
                return false;
            }
        }

        public enum FHIRResourceType
        {
            [StringValue("Bundle")]
            Bundle = 236,
            [StringValue("Patient")]
            Patient = 801,
            [StringValue("Observation")]
            Observation = 1001,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-identifier-use.html
        /// </summary>
        public enum IdentifierUseCode
        {
            [StringValue("usual")]
            usual = 0,
            [StringValue("official")]
            official = 1,
            [StringValue("temp")]
            temp = 2,
            [StringValue("secondary")]
            secondary = 3,
            [StringValue("old")]
            old = 4,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-administrative-gender.html
        /// </summary>
        public enum AdministrativeGenderCode
        {
            [StringValue("male")]
            male = 0,
            [StringValue("female")]
            female = 1,
            [StringValue("other")]
            other = 2,
            [StringValue("unknown")]
            unknown = 3,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-link-type.html
        /// </summary>
        public enum LinkTypeCode
        {
            [StringValue("replaced-by")]
            replacedby = 0,
            [StringValue("replaces")]
            replaces = 1,
            [StringValue("refer")]
            refer = 2,
            [StringValue("seealso")]
            seealso = 3,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-name-use.html
        /// </summary>
        public enum NameUseCode
        {
            [StringValue("usual")]
            usual = 0,
            [StringValue("official")]
            official = 1,
            [StringValue("temp")]
            temp = 2,
            [StringValue("nickname")]
            nickname = 3,
            [StringValue("anonymous")]
            anonymous = 4,
            [StringValue("old")]
            old = 5,
            [StringValue("maiden")]
            maiden = 6,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-contact-point-system.html
        /// </summary>
        public enum ContactPointSystemCode
        {
            [StringValue("phone")]
            phone = 0,
            [StringValue("fax")]
            fax = 1,
            [StringValue("email")]
            email = 2,
            [StringValue("pager")]
            pager = 3,
            [StringValue("url")]
            url = 4,
            [StringValue("sms")]
            sms = 5,
            [StringValue("other")]
            other = 6,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-contact-point-use.html
        /// </summary>
        public enum ContactPointUseCode
        {
            [StringValue("home")]
            home = 0,
            [StringValue("work")]
            work = 1,
            [StringValue("temp")]
            temp = 2,
            [StringValue("old")]
            old = 3,
            [StringValue("mobile")]
            mobile = 4,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-address-use.html
        /// </summary>
        public enum AddressUseCode
        {
            [StringValue("home")]
            home = 0,
            [StringValue("work")]
            work = 1,
            [StringValue("temp")]
            temp = 2,
            [StringValue("old")]
            old = 3,
            [StringValue("billing")]
            billing = 4,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-address-type.html
        /// </summary>
        public enum AddressTypeCode
        {
            [StringValue("postal")]
            postal = 0,
            [StringValue("physical")]
            physical = 1,
            [StringValue("both")]
            both = 2,
        }

        /// <summary>
        /// https://hl7.org/FHIR/narrative.html#Narrative
        /// </summary>
        public enum NarrativeStatusCode
        {
            [StringValue("generated")]
            generated = 0,
            [StringValue("extensions")]
            extensions = 1,
            [StringValue("additional")]
            additional = 2,
            [StringValue("empty")]
            empty = 3,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-bundle-type.html
        /// </summary>
        public enum BundleTypeCode
        {
            [StringValue("document")]
            document = 0,
            [StringValue("message")]
            message = 1,
            [StringValue("transaction")]
            transaction = 2,
            [StringValue("transaction-response")]
            transactionResponse = 3,
            [StringValue("batch")]
            batch = 4,
            [StringValue("batch-response")]
            batchResponse = 5,
            [StringValue("history")]
            history = 6,
            [StringValue("searchset")]
            searchset = 7,
            [StringValue("collection")]
            collection = 8,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-search-entry-mode.html
        /// </summary>
        public enum SearchEntryModeCode
        {
            [StringValue("match")]
            match = 0,
            [StringValue("include")]
            include = 1,
            [StringValue("outcome")]
            outcome = 2,
        }

        /// <summary>
        /// https://hl7.org/FHIR/valueset-http-verb.html
        /// </summary>
        public enum HTTPVerbCode
        {
            [StringValue("GET")]
            GET = 0,
            [StringValue("HEAD")]
            HEAD = 1,
            [StringValue("POST")]
            POST = 2,
            [StringValue("PUT")]
            PUT = 3,
            [StringValue("DELETE")]
            DELETE = 4,
            [StringValue("PATCH")]
            PATCH = 5,
        }
    }
}
