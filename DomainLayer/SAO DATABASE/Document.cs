using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainLayer.SAO_DATABASE
{

    [Table("ODOC")]
    public partial class Document
    {
        [Key]
        public string WBNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Transporter { get; set; }
        public string OriginOfWaste { get; set; }
        public string DRNo { get; set; }
        public string Driver { get; set; }
        public string PlateNo { get; set; }
        public string VehicleType { get; set; }
        public string YardPassNo { get; set; }
        public string TypeOfWaste { get; set; }
        public decimal Lengthh { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Container { get; set; }
        public decimal? Weight1 { get; set; }
        public decimal? Weight2 { get; set; }
        public decimal? NetWt { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string WgtInDate { get; set; }
        public string WgtOutDate { get; set; }
        public string WgtInTime { get; set; }
        public string WgtOutTime { get; set; }
        public string Operator1 { get; set; }
        public string Operator2 { get; set; }
        public string Remarks { get; set; }
        public string TransType { get; set; }
        public string BinNo1 { get; set; }
        public string BinNo2 { get; set; }
        public string Cont1 { get; set; }
        public string Cont2 { get; set; }
        public string TORNo { get; set; }
        public string Combination { get; set; }

        //public virtual ICollection<Document_Lines> DocumentLines { get; set; }
    }
}
