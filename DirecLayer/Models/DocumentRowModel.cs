using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirecLayer.Models
{
    public class DocumentRowModel
    {
        public int LineNum { get; set; }

        public string ItemCode { get; set; }

        public string Dscription { get; set; }

        public decimal? Quantity { get; set; }

        public DateTime? ShipDate { get; set; }

        public decimal? Price { get; set; }

        public string Currency { get; set; }

        public decimal? Rate { get; set; }

        public decimal? DiscPrcnt { get; set; }

        public decimal? LineTotal { get; set; }

        public string SerialNum { get; set; }

        public string WhsCode { get; set; }

        public int? SlpCode { get; set; }

        public string AcctCode { get; set; }

        public string TaxStatus { get; set; }

        public decimal? GrossBuyPr { get; set; }

        public decimal? PriceBefDi { get; set; }

        public DateTime? DocDate { get; set; }

        public string OcrCode { get; set; }

        public string Project { get; set; }

        public string CodeBars { get; set; }

        public decimal? VatPrcnt { get; set; }

        public string VatGroup { get; set; }

        public decimal? PriceAfVAT { get; set; }

        public decimal? VatSum { get; set; }

        public string ObjType { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string unitMsr { get; set; }

        public string ShipToCode { get; set; }

        public string ShipToDesc { get; set; }

        public string BasePrice { get; set; }

        public decimal? GTotal { get; set; }

        public string CogsAcct { get; set; }

        public string OcrCode2 { get; set; }

        public string OcrCode3 { get; set; }

        public string OcrCode4 { get; set; }

        public string OcrCode5 { get; set; }

        public string CogsOcrCo2 { get; set; }

        public string CogsOcrCo3 { get; set; }

        public string CogsOcrCo4 { get; set; }

        public string CogsOcrCo5 { get; set; }

        public int? UomEntry { get; set; }

        public string UomCode { get; set; }

        public string FromWhsCod { get; set; }

        public int? ItemType { get; set; }

        public decimal? U_VarianceQty { get; set; }

        public string U_Vendor { get; set; }

        public string U_TIN { get; set; }

        public string U_Address { get; set; }

        public decimal? U_Disc1 { get; set; }

        public decimal? U_Disc2 { get; set; }

        public decimal? U_Disc3 { get; set; }

        public decimal? U_Disc4 { get; set; }

        public decimal? U_Disc5 { get; set; }

        public decimal? U_NetDisc { get; set; }

        public string U_API_Vendor { get; set; }

        public string U_API_TIN { get; set; }

        public string U_bir_tax_grp { get; set; }

        public string U_birvalid { get; set; }

        public string U_Bir_validated { get; set; }

        public decimal? U_ACTUAL_QTY { get; set; }

        public decimal? U_VARIANCE_QTY { get; set; }

        public decimal? U_GrossPrice { get; set; }

        public decimal? U_Qty { get; set; }

        public string U_UOM { get; set; }

        public decimal? U_ActualQty { get; set; }

        public DateTime? U_DateValid { get; set; }

        public string U_TransType { get; set; }

        public string U_InvyUoM { get; set; }

        public string U_ExpenseType { get; set; }

        public string U_Origin { get; set; }

        public string U_Destination { get; set; }

        public decimal? U_Weight { get; set; }

        public decimal? U_Dimension { get; set; }

        public string U_API_Address { get; set; }

        public string U_FreightBasis { get; set; }

        public decimal? U_StdFreight { get; set; }

        public decimal? U_TotalStdFreight { get; set; }

        public decimal? U_FinalFreight { get; set; }

        public string U_TripType { get; set; }

        public string U_TripCount { get; set; }

        public string U_SKU { get; set; }

        public decimal? U_DiscBaseAmt { get; set; }

        public decimal? U_DiscBase { get; set; }

        public decimal? U_DiscTotal { get; set; }

        public decimal? U_CABal { get; set; }

        public decimal? U_PurchUOM { get; set; }

        public string U_SWMFNo { get; set; }

        public string U_YPNo { get; set; }

        public string U_SONo { get; set; }

        public string U_Remarks { get; set; }
    }
}
