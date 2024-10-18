using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.SAO_DATABASE
{
    [Table("OCRD")]
    public class Customers
    {
        public int Id { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string frozenFor { get; set; }
        public string U_Dim1 { get; set; }
        public string U_Dim2 { get; set; }
        public string U_Dim3 { get; set; }
        public string GroupType { get; set; }
        public string U_Municipality { get; set; }
        public string U_Zone { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
