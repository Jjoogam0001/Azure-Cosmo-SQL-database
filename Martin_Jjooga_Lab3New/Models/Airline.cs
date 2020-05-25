using Newtonsoft.Json;

namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Airline")]
    public partial class Airline
    {
        public int id { get; set; }

        [JsonProperty(PropertyName = "Code")]

        [StringLength(3)]
        public string code { get; set; }


        [StringLength(10)]
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
    }
}
