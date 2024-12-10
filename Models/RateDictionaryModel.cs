using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDHL_API_Implementation.Models
{
    public class RateDictionaryModel
    {
        public string? AccountNumber { get; set; }
        public string? OriginCountryCode { get; set; }
        public string? OriginCityName { get; set; }
        public string? DestinationCountryCode { get; set; }
        public string? DestinationCityName { get; set; }
        public string? Weight { get; set; }
        public string? Length { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? PlannedShippingDate { get; set; }
        public string? IsCustomsDeclarable { get; set; }
        public string? UnitOfMeasurement { get; set; }
        public string? NextBusinessDay { get; set; }
        public string? GetAllValueAddedServices { get; set; }
        public string? DestinationPostalCode { get; set; }
        public string? RequestEstimatedDeliveryDate { get; set; }

        public string? EstimatedDeliveryDateType = "QDDF";
    }
}
