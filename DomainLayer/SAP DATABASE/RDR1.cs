using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainLayer.SAP_DATABASE
{
    public partial class RDR1
    {

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineNum { get; set; }


        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(100)]
        public string Dscription { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quantity { get; set; }

        public DateTime? ShipDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }
        public decimal? UnitPrice { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscPrcnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineTotal { get; set; }

        [StringLength(17)]
        public string SerialNum { get; set; }

        [StringLength(8)]
        public string WhsCode { get; set; }

        public int? SlpCode { get; set; }

        [StringLength(15)]
        public string AcctCode { get; set; }

        [StringLength(1)]
        public string TaxStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrossBuyPr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PriceBefDi { get; set; }

        public DateTime? DocDate { get; set; }

        [StringLength(8)]
        public string OcrCode { get; set; }

        [StringLength(20)]
        public string Project { get; set; }

        [StringLength(254)]
        public string CodeBars { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatPrcnt { get; set; }

        [StringLength(8)]
        public string VatGroup { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PriceAfVAT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSum { get; set; }

        [StringLength(20)]
        public string ObjType { get; set; }

        [StringLength(254)]
        public string Address { get; set; }

        [StringLength(8)]
        public string TaxCode { get; set; }

        [StringLength(100)]
        public string unitMsr { get; set; }

        [StringLength(50)]
        public string ShipToCode { get; set; }

        [StringLength(254)]
        public string ShipToDesc { get; set; }

        [StringLength(1)]
        public string BasePrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GTotal { get; set; }

        [StringLength(15)]
        public string CogsAcct { get; set; }

        [StringLength(8)]
        public string OcrCode2 { get; set; }

        [StringLength(8)]
        public string OcrCode3 { get; set; }

        [StringLength(8)]
        public string OcrCode4 { get; set; }

        [StringLength(8)]
        public string OcrCode5 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo2 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo3 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo4 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo5 { get; set; }

        public int? UoMEntry { get; set; }

        [StringLength(20)]
        public string UomCode { get; set; }

        [StringLength(8)]
        public string FromWhsCod { get; set; }

        public int? ItemType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_VarianceQty { get; set; }

        [StringLength(100)]
        public string U_Vendor { get; set; }

        [StringLength(15)]
        public string U_TIN { get; set; }

        [StringLength(45)]
        public string U_Address { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Disc1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Disc2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Disc3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Disc4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Disc5 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_NetDisc { get; set; }

        [StringLength(30)]
        public string U_API_Vendor { get; set; }

        [StringLength(30)]
        public string U_API_TIN { get; set; }

        [StringLength(10)]
        public string U_bir_tax_grp { get; set; }

        [StringLength(10)]
        public string U_birvalid { get; set; }

        [StringLength(10)]
        public string U_Bir_validated { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_ACTUAL_QTY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_VARIANCE_QTY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_GrossPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Qty { get; set; }

        [StringLength(10)]
        public string U_UOM { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_ActualQty { get; set; }

        public DateTime? U_DateValid { get; set; }

        [StringLength(20)]
        public string U_TransType { get; set; }

        [StringLength(10)]
        public string U_InvyUoM { get; set; }

        [StringLength(20)]
        public string U_ExpenseType { get; set; }

        [StringLength(30)]
        public string U_Origin { get; set; }

        [StringLength(30)]
        public string U_Destination { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Dimension { get; set; }

        [StringLength(254)]
        public string U_API_Address { get; set; }

        [StringLength(10)]
        public string U_FreightBasis { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_StdFreight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_TotalStdFreight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_FinalFreight { get; set; }

        [StringLength(10)]
        public string U_TripType { get; set; }

        [StringLength(10)]
        public string U_TripCount { get; set; }

        [StringLength(20)]
        public string U_SKU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DiscBaseAmt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DiscBase { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DiscTotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_CABal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_PurchUOM { get; set; }

        [StringLength(10)]
        public string U_SWMFNo { get; set; }

        [StringLength(10)]
        public string U_YPNo { get; set; }

        [StringLength(10)]
        public string U_SONo { get; set; }

        [StringLength(254)]
        public string U_Remarks { get; set; }

        [StringLength(15)]
        public string CostingCode { get; set; }
        [StringLength(15)]
        public string CostingCode2 { get; set; }
        [StringLength(15)]
        public string CostingCode3 { get; set; }

    }
}
