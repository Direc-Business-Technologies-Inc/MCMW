using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainLayer.SAO_DATABASE
{
    [Table("TRUCK_FLEET")]
    public class TruckFleet
    {
        public int Id { get; set; }
        public string PlateNo { get; set; } 


    }
}
