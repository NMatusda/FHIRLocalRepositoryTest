using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.DataTypes
{
    /// <summary>
    /// https://hl7.org/FHIR/datatypes.html#CodeableConcept
    /// </summary>
    public class CodeableConceptType
    {
        public CodingType[]? coding { get; set; }
        public string? text { get; set; }
    }
}
