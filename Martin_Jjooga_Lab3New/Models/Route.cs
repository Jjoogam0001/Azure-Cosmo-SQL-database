using Newtonsoft.Json;

namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Route
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "flightNumber")]
        public int? flightNber { get; set; }

        [StringLength(10)]
        [JsonProperty(PropertyName = "carrier")]
        public string carrier { get; set; }

        [StringLength(10)]
        [JsonProperty(PropertyName = "departure_airport")]
        public string departure_airport { get; set; }

        [StringLength(10)]
        [JsonProperty(PropertyName = "rrival_Airport")]

        public string arrival_airport { get; set; }
    }
}
