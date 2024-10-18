using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainLayer.SAP_DATABASE
{
    [Table("ORDR")]
    public partial class ORDR
    {

        [StringLength(1)]
        public string DocType { get; set; }

        public DateTime? DocDate { get; set; }

        public DateTime? DocDueDate { get; set; }

        [StringLength(15)]
        public string CardCode { get; set; }

        [StringLength(100)]
        public string CardName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscPrcnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscSum { get; set; }

        [StringLength(3)]
        public string DocCur { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocTotal { get; set; }

        [StringLength(11)]
        public string Ref1 { get; set; }

        [StringLength(11)]
        public string Ref2 { get; set; }

        [StringLength(254)]
        public string Comments { get; set; }

        public int? SlpCode { get; set; }

        public int? Series { get; set; }

        public DateTime? TaxDate { get; set; }
        [StringLength(50)]
        public string ShipToCode { get; set; }

        [StringLength(32)]
        public string LicTradNum { get; set; }

        [StringLength(20)]
        public string Project { get; set; }

        [StringLength(20)]
        public string U_SINo { get; set; }

        [StringLength(30)]
        public string U_PrepBy { get; set; }

        [StringLength(20)]
        public string U_RevBy { get; set; }

        [StringLength(50)]
        public string U_AppBy { get; set; }

        [StringLength(15)]
        public string U_RetType { get; set; }

        [StringLength(20)]
        public string U_CheckBy { get; set; }

        [StringLength(50)]
        public string U_PONo { get; set; }

        [StringLength(50)]
        public string U_Division { get; set; }

        public int? U_Attach { get; set; }

        [StringLength(30)]
        public string U_EmpName { get; set; }
        [StringLength(20)]
        public string ObjType { get; set; }

        [StringLength(20)]
        public string U_Area { get; set; }

        [StringLength(10)]
        public string U_TransferType { get; set; }

        [StringLength(10)]
        public string U_TransferMode { get; set; }

        [StringLength(50)]
        public string U_DRNo { get; set; }

        [StringLength(20)]
        public string U_SONo { get; set; }

        [StringLength(5)]
        public string U_2307rep { get; set; }

        public DateTime? U_2307repdate { get; set; }

        [StringLength(5)]
        public string U_1601rep { get; set; }

        public DateTime? U_1601repdate { get; set; }

        [StringLength(50)]
        public string U_DelBy { get; set; }

        [StringLength(30)]
        public string U_RecBy { get; set; }

        public DateTime? U_DateDel { get; set; }

        [StringLength(20)]
        public string U_CredType { get; set; }

        [StringLength(50)]
        public string U_IssueType { get; set; }

        [StringLength(30)]
        public string U_CustType { get; set; }

        [StringLength(30)]
        public string U_Territory { get; set; }

        [StringLength(50)]
        public string U_TruckPlateNo { get; set; }

        [StringLength(10)]
        public string U_DelStat { get; set; }

        [StringLength(50)]
        public string U_TransType { get; set; }

        [StringLength(30)]
        public string U_Courier { get; set; }

        [StringLength(30)]
        public string U_TrackingNo { get; set; }

        public DateTime? U_DateReceived { get; set; }

        [StringLength(10)]
        public string U_Override { get; set; }

        [StringLength(10)]
        public string U_DPRequired { get; set; }

        [StringLength(20)]
        public string U_PreshipSampleReq { get; set; }

        [StringLength(20)]
        public string U_PreshipSampleStat { get; set; }

        [StringLength(20)]
        public string U_EDNo { get; set; }

        [StringLength(20)]
        public string U_CONo { get; set; }

        [Column(TypeName = "ntext")]
        public string U_Purpose { get; set; }

        [StringLength(50)]
        public string U_TermsOfDel { get; set; }

        [StringLength(50)]
        public string U_VendorRefNo { get; set; }

        [StringLength(50)]
        public string U_ShippingDetails { get; set; }

        public int? U_SvcCallNo { get; set; }

        [StringLength(254)]
        public string U_SvcCallDesc { get; set; }

        public int? U_GIGRNo { get; set; }

        public DateTime? U_ShipDateTo { get; set; }

        [StringLength(254)]
        public string U_Instruction { get; set; }

        [StringLength(254)]
        public string U_LogisticsRemarks { get; set; }

        [StringLength(254)]
        public string U_ShippingRemarks { get; set; }

        [StringLength(254)]
        public string U_ProductionRemarks { get; set; }

        [StringLength(10)]
        public string U_ProductionStatus { get; set; }

        [Column(TypeName = "ntext")]
        public string U_Remarks { get; set; }

        [StringLength(254)]
        public string U_PortofLoading { get; set; }

        [StringLength(254)]
        public string U_PortofDischarge { get; set; }

        [StringLength(254)]
        public string U_PlaceofDelivery { get; set; }

        public DateTime? U_ETAPort { get; set; }

        [StringLength(20)]
        public string U_ContainerNo { get; set; }

        [StringLength(50)]
        public string U_ShippingLine { get; set; }

        [StringLength(50)]
        public string U_BillofLadingNo { get; set; }

        public DateTime? U_BillofLadingDate { get; set; }

        [StringLength(20)]
        public string U_VesselNo { get; set; }

        [StringLength(50)]
        public string U_ShippingMarks { get; set; }

        [StringLength(254)]
        public string U_BagMarkings { get; set; }

        [StringLength(1)]
        public string U_GoodsOnPallets { get; set; }

        [StringLength(254)]
        public string U_SpecialDocs { get; set; }

        [StringLength(254)]
        public string U_EstTransitTime { get; set; }

        public int? U_ProdOrderNo { get; set; }

        [StringLength(254)]
        public string U_OrigDoc { get; set; }

        [StringLength(10)]
        public string U_Approval { get; set; }

        [StringLength(254)]
        public string U_ETD_Manila { get; set; }

        [StringLength(10)]
        public string U_PQStatus { get; set; }

        [StringLength(30)]
        public string U_ExportDeclaration { get; set; }

        [StringLength(50)]
        public string U_AWB_No { get; set; }

        public DateTime? U_AWB_Date { get; set; }

        [StringLength(10)]
        public string U_PQLocation { get; set; }

        [StringLength(10)]
        public decimal U_Weight1 { get; set; }

        [StringLength(10)]
        public decimal U_Weight2 { get; set; }

        [StringLength(10)]
        public decimal U_NetWt { get; set; }

        [StringLength(10)]
        public string U_BinNo1 { get; set; }

        [StringLength(10)]
        public string U_BinNo2 { get; set; }

        [StringLength(10)]
        public DateTime U_WgtInDate { get; set; }

        [StringLength(10)]
        public DateTime U_WgtOutDate { get; set; }

        [StringLength(10)]
        public string U_WgtInTime { get; set; }

        [StringLength(10)]
        public string U_WgtOutTime { get; set; }

        [StringLength(254)]
        public string U_Driver { get; set; }

        [StringLength(20)]
        public string U_PlateNo { get; set; }

        [StringLength(20)]
        public string U_YPNo { get; set; }

        [StringLength(20)]
        public string U_WasteType { get; set; }

        [StringLength(20)]
        public string U_Disposal { get; set; }

        [StringLength(20)]
        public string U_Transporter { get; set; }

        [StringLength(20)]
        public decimal U_Length { get; set; }

        [StringLength(20)]
        public decimal U_Width { get; set; }

        [StringLength(20)]
        public decimal U_Height { get; set; }

        [StringLength(10)]
        public string U_Cont1 { get; set; }

        [StringLength(10)]
        public string U_Cont2 { get; set; }

        [StringLength(10)]
        public string U_Combination { get; set; }

        [StringLength(254)]
        public string U_Rem1 { get; set; }

        [StringLength(10)]
        public string U_Origin { get; set; }

        [StringLength(10)]
        public string U_TripStart { get; set; }

        [StringLength(10)]
        public string U_GateIn { get; set; }

        [StringLength(10)]
        public string U_TripEnd { get; set; }

        [StringLength(10)]
        public string U_GateOut { get; set; }

        [StringLength(10)]
        public int U_WBNo { get; set; }

        [StringLength(10)]
        public int U_TORNo { get; set; }

        [StringLength(100)]
        public string U_Destination { get; set; }

        [StringLength(50)]
        public string U_Operator1 { get; set; }

        [StringLength(50)]
        public string U_Operator2 { get; set; }
        public virtual ICollection<RDR1> DocumentLines { get; set; }

    }
}
