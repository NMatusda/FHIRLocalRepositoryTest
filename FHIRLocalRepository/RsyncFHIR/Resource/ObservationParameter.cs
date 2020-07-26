using RsyncFHIR.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsyncFHIR.Resource
{
    /// <summary>
    /// http://hl7.org/fhir/observation.html
    /// </summary>
    public class ObservationResource : BaseResource
    {
        public override string? resourceType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //    public string? resourceType { get; set; }
        //    public string? id { get; set; }
        //    public Meta? meta { get; set; }
        //    public string? implicitRules { get; set; }
        //    public string? language { get; set; }
        //    public NarrativeType? text { get; set; }
        //    public IdentifierType[]? identifier { get; set; }
        //    public Partof[]? partOf { get; set; }
        //    public string? status { get; set; }
        //    public Category[]? category { get; set; }
        //    public ObservationCode? code { get; set; }
        //    public Subject? subject { get; set; }
        //    public DateTime? effectiveDateTime { get; set; }
        //    public Valuequantity? valueQuantity { get; set; }
        //    public Interpretation[]? interpretation { get; set; }
        //    public Device? device { get; set; }
        //    public Referencerange[]? referenceRange { get; set; }
    }
}
