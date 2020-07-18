using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/patient-definitions.html#Patient.communication
    /// </summary>
    public class CommunicationType
    {
        public CodeableConceptType? language { get; set; }
        public bool? preferred { get; set; }
    }
}
