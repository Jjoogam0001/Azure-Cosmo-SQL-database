using Newtonsoft.Json;

namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Flight
    {
        [StringLength(10)]
        [JsonProperty(PropertyName = "PsgrName")]
        public string PsgrName { get; set; }

        [StringLength(10)]
        [JsonProperty(PropertyName = "Passport_Number")]
        public string Passport_Number { get; set; }
        [JsonProperty(PropertyName = "Departure_Date")]
        public DateTime? Departure_Date { get; set; }
        [JsonProperty(PropertyName = "AirFare")]
        public double? Airfare { get; set; }

        public int id { get; set; }
    }
}
