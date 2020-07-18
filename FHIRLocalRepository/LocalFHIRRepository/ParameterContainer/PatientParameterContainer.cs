using LocalFHIRRepository.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.ParameterContainer
{
    public class PatientParameterContainer : BaseParameterContainer
    {
        public JObject? JsonParameter { get; set; }

        /// <summary>
        /// Parameterの整合性を確認する
        /// </summary>
        /// <returns></returns>
        public override bool IsValid()
        {
            if (JsonParameter == null) return false;

            var resourceType = JsonParameter.GetValue("resourceType");
            if (resourceType == null) return false;

            CommonEnum.FHIRResourceType rtype;
            if (!resourceType.ToString().TryParseToEnum(out rtype)) return false;

            // Patientか、EntryがすべてPatientのBundleのみ許容する
            switch (rtype)
            {
                case CommonEnum.FHIRResourceType.Patient:
                    break;
                case CommonEnum.FHIRResourceType.Bundle:
                    var entries = JsonParameter.GetValue("entry");
                    if (entries == null) return false;
                    if (!IsAllPatientEntries(entries)) return false;

                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// EntryのすべてのresourceがPatientか確認する
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private bool IsAllPatientEntries(JToken entries) => entries.IsMatchAllEntriesType(CommonEnum.FHIRResourceType.Patient);

        /// <summary>
        /// JSONをString型で返す
        /// </summary>
        /// <returns></returns>
        public override string OutputJson()
        {
            if (JsonParameter is null) throw new NullReferenceException("Null Patient Parameter Refarence.");
            return JsonConvert.SerializeObject(JsonParameter);
        }

        /// <summary>
        /// ファイル名を取得する
        /// </summary>
        /// <returns></returns>
        public override string SynchronizeFileName()
        {
            if (JsonParameter is null) throw new NullReferenceException("Null Patient Parameter Refarence.");
            return $"Patient_{DateTime.Now.ToString("yyyyMMddhhmmssfff")}.json";
        }
    }
}
