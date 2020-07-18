using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Util
{
    public static class CommonJson
    {
        /// <summary>
        /// Entryのすべての要素のresourceが指定のResourceTypeか確認する
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static bool IsMatchAllEntriesType(this JToken entries, CommonEnum.FHIRResourceType resourceType)
        {
            try
            {
                foreach (JObject child in (JArray)entries)
                {
                    var resource = child.GetValue("resource");
                    if (resource == null) return false;

                    var rtype = ((JObject)resource).GetValue("resourceType");
                    if (rtype == null) return false;

                    CommonEnum.FHIRResourceType frt;
                    if (!rtype.ToString().TryParseToEnum(out frt)) return false;

                    if (frt != resourceType) return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
