using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainLayer.SAO_DATABASE
{

    [Table("TRUCK_WEIGHT")]

    public class TruckWeight
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? NetWt { get; set; }
        public string PlateNo { get; set; }
        public string Combination { get; set; }
        public string Remarks { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime? UpdateDate { get; set; }
    }
}
