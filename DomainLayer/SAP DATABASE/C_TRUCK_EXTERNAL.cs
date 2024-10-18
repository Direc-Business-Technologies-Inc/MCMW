namespace DomainLayer.SAP_DATABASE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("@TRUCK_EXTERNAL")]
    public partial class C_TRUCK_EXTERNAL
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string U_VehicleType { get; set; } 


    }
}
