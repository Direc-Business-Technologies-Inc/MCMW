using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.SAO_DATABASE
{

    [Table("Price")]
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public decimal? Pricee { get; set; }
        public string UOM { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Limit { get; set; }
    }
}
