using Newtonsoft.Json;

namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public int CustomerId { get; set; }
       

        [StringLength(14)]
        [JsonProperty(PropertyName = "Card_Number")]
        public string Card_Number { get; set; }
       
        [StringLength(14)]
        [JsonProperty(PropertyName = "Holder_Name")]
        public string Holder_Name { get; set; }
        [Column(TypeName = "date")]
        [JsonProperty(PropertyName = "Expiry_Date")]
        public DateTime? Expiry_Date { get; set; }
        [JsonProperty(PropertyName = "Balance")]
        public double? Balance { get; set; }
    }
}
