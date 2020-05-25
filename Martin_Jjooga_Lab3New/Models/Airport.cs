using Newtonsoft.Json;

namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Airport
    {
        [Key]
        [Column(Order = 0)]
        [JsonProperty(PropertyName = " Airport_Code")]
        public int Airport_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [JsonProperty(PropertyName = " City")]
        public string city { get; set; }

        [Key]
        [Column(Order = 2)]
        [JsonProperty(PropertyName = " latitude")]
        public double latitude { get; set; }

        [Key]
        [Column(Order = 3)]
        [JsonProperty(PropertyName = " logitude")]
        public double logitude { get; set; }
    }
}
