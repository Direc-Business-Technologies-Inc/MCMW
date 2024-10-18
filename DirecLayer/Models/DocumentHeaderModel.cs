using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirecLayer.Models
{
    public class DocumentHeaderModel
    {
        public string DocType { get; set; }

        public DateTime? DocDate { get; set; }

        public DateTime? DocDueDate { get; set; }

        public string CardCode { get; set; }

        public string CardName { get; set; }

        public decimal? DiscPrcnt { get; set; }

        public decimal? DiscSum { get; set; }

        public string DocCur { get; set; }

        public decimal? DocRate { get; set; }

        public decimal? DocTotal { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public string Comments { get; set; }

        public int? SlpCode { get; set; }

        public int? Series { get; set; }

        public DateTime? TaxDate { get; set; }

        public string ShipToCode { get; set; }

        public string LicTradNum { get; set; }

        public string Project { get; set; }

        public string U_SINo { get; set; }

        public string U_PrepBy { get; set; }

        public string U_RevBy { get; set; }

        public string U_AppBy { get; set; }

        public string U_RetType { get; set; }

        public string U_CheckBy { get; set; }

        public string U_PONo { get; set; }

        public string U_Division { get; set; }

        public int? U_Attach { get; set; }

        public string U_EmpName { get; set; }

        public string U_Area { get; set; }

        public string U_TransferType { get; set; }

        public string U_TransferMode { get; set; }

        public string U_DRNo { get; set; }

        public string U_SONo { get; set; }

        public string U_2307rep { get; set; }

        public DateTime? U_2307repdate { get; set; }

        public string U_1601rep { get; set; }

        public DateTime? U_1601repdate { get; set; }

        public string U_DelBy { get; set; }

        public string U_RecBy { get; set; }

        public DateTime? U_DateDel { get; set; }

        public string U_CredType { get; set; }

        public string U_IssueType { get; set; }

        public string U_CustType { get; set; }

        public string U_Territory { get; set; }

        public string U_TruckPlateNo { get; set; }

        public string U_DelStat { get; set; }

        public string U_TransType { get; set; }

        public string U_Courier { get; set; }

        public string U_TrackingNo { get; set; }

        public DateTime? U_DateReceived { get; set; }

        public string U_Override { get; set; }

        public string U_DPRequired { get; set; }

        public string U_PreshipSampleReq { get; set; }

        public string U_PreshipSampleStat { get; set; }

        public string U_EDNo { get; set; }

        public string U_CONo { get; set; }

        public string U_Purpose { get; set; }

        public string U_TermsOfDel { get; set; }

        public string U_VendorRefNo { get; set; }

        public string U_ShippingDetails { get; set; }

        public int? U_SvcCallNo { get; set; }

        public string U_SvcCallDesc { get; set; }

        public int? U_GIGRNo { get; set; }

        public DateTime? U_ShipDateTo { get; set; }

        public string U_Instruction { get; set; }

        public string U_LogisticsRemarks { get; set; }

        public string U_ShippingRemarks { get; set; }

        public string U_ProductionRemarks { get; set; }

        public string U_ProductionStatus { get; set; }

        public string U_Remarks { get; set; }

        public string U_PortofLoading { get; set; }

        public string U_PortofDischarge { get; set; }

        public string U_PlaceofDelivery { get; set; }

        public DateTime? U_ETAPort { get; set; }

        public string U_ContainerNo { get; set; }

        public string U_ShippingLine { get; set; }

        public string U_BillofLadingNo { get; set; }

        public DateTime? U_BillofLadingDate { get; set; }

        public string U_VesselNo { get; set; }

        public string U_ShippingMarks { get; set; }

        public string U_BagMarkings { get; set; }

        public string U_GoodsOnPallets { get; set; }

        public string U_SpecialDocs { get; set; }

        public string U_EstTransitTime { get; set; }

        public int? U_ProdOrderNo { get; set; }

        public string U_OrigDoc { get; set; }

        public string U_Approval { get; set; }

        public string U_ETD_Manila { get; set; }

        public string U_PQStatus { get; set; }

        public string U_ExportDeclaration { get; set; }

        public string U_AWB_No { get; set; }

        public DateTime? U_AWB_Date { get; set; }

        public string U_PQLocation { get; set; }
    }
}
