using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainLayer.SAO_DATABASE
{
    [Table("DOC1")]
    public partial class Document_Lines
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocEntry { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineNum { get; set; }

        public int? TargetType { get; set; }

        public int? TrgetEntry { get; set; }

        [StringLength(16)]
        public string BaseRef { get; set; }

        public int? BaseType { get; set; }

        public int? BaseEntry { get; set; }

        public int? BaseLine { get; set; }

        [StringLength(1)]
        public string LineStatus { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(100)]
        public string Dscription { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quantity { get; set; }

        public DateTime? ShipDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscPrcnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineTotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalFrgn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenSumFC { get; set; }

        [StringLength(50)]
        public string VendorNum { get; set; }

        [StringLength(17)]
        public string SerialNum { get; set; }

        [StringLength(8)]
        public string WhsCode { get; set; }

        public int? SlpCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Commission { get; set; }

        [StringLength(1)]
        public string TreeType { get; set; }

        [StringLength(15)]
        public string AcctCode { get; set; }

        [StringLength(1)]
        public string TaxStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrossBuyPr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PriceBefDi { get; set; }

        public DateTime? DocDate { get; set; }

        public int? Flags { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenCreQty { get; set; }

        [StringLength(1)]
        public string UseBaseUn { get; set; }

        [StringLength(50)]
        public string SubCatNum { get; set; }

        [StringLength(15)]
        public string BaseCard { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalSumSy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenSumSys { get; set; }

        [StringLength(1)]
        public string InvntSttus { get; set; }

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
        public decimal? Height1 { get; set; }

        public short? Hght1Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Height2 { get; set; }

        public short? Hght2Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Width1 { get; set; }

        public short? Wdth1Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Width2 { get; set; }

        public short? Wdth2Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Length1 { get; set; }

        public short? Len1Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? length2 { get; set; }

        public short? Len2Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Volume { get; set; }

        public short? VolUnit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight1 { get; set; }

        public short? Wght1Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight2 { get; set; }

        public short? Wght2Unit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Factor1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Factor2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Factor3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Factor4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PackQty { get; set; }

        [StringLength(1)]
        public string UpdInvntry { get; set; }

        public int? BaseDocNum { get; set; }

        [StringLength(100)]
        public string BaseAtCard { get; set; }

        [StringLength(16)]
        public string SWW { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSumFrgn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSumSy { get; set; }

        public int? FinncPriod { get; set; }

        [StringLength(20)]
        public string ObjType { get; set; }

        public int? LogInstanc { get; set; }

        [StringLength(100)]
        public string BlockNum { get; set; }

        [StringLength(20)]
        public string ImportLog { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DedVatSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DedVatSumF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DedVatSumS { get; set; }

        [StringLength(1)]
        public string IsAqcuistn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DistribSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DstrbSumFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DstrbSumSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrssProfit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrssProfSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrssProfFC { get; set; }

        public int? VisOrder { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? INMPrice { get; set; }

        public int? PoTrgNum { get; set; }

        [StringLength(11)]
        public string PoTrgEntry { get; set; }

        [StringLength(1)]
        public string DropShip { get; set; }

        public int? PoLineNum { get; set; }

        [StringLength(254)]
        public string Address { get; set; }

        [StringLength(8)]
        public string TaxCode { get; set; }

        [StringLength(1)]
        public string TaxType { get; set; }

        [StringLength(50)]
        public string OrigItem { get; set; }

        [StringLength(1)]
        public string BackOrdr { get; set; }

        [StringLength(100)]
        public string FreeTxt { get; set; }

        [StringLength(1)]
        public string PickStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PickOty { get; set; }

        public int? PickIdNo { get; set; }

        public short? TrnsCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatAppld { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatAppldFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatAppldSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseOpnQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatDscntPr { get; set; }

        [StringLength(1)]
        public string WtLiable { get; set; }

        [StringLength(1)]
        public string DeferrTax { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatPer { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSumF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSumS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineVat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineVatlF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineVatS { get; set; }

        [StringLength(100)]
        public string unitMsr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NumPerMsr { get; set; }

        [StringLength(1)]
        public string CEECFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ToStock { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ToDiff { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExciseAmt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPerUnit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotInclTax { get; set; }

        [StringLength(3)]
        public string CountryOrg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckDstSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ReleasQtty { get; set; }

        [StringLength(1)]
        public string LineType { get; set; }

        [StringLength(1)]
        public string TranType { get; set; }

        [Column(TypeName = "ntext")]
        public string Text { get; set; }

        public int? OwnerCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StockPrice { get; set; }

        [StringLength(1)]
        public string ConsumeFCT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LstByDsSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckINMPr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LstBINMPr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckDstFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckDstSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LstByDsFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LstByDsSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StockSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StockSumFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StockSumSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckSumApp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckAppFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckAppSc { get; set; }

        [StringLength(50)]
        public string ShipToCode { get; set; }

        [StringLength(254)]
        public string ShipToDesc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckAppD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckAppDFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StckAppDSC { get; set; }

        [StringLength(1)]
        public string BasePrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GTotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GTotalFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GTotalSC { get; set; }

        [StringLength(1)]
        public string DistribExp { get; set; }

        [StringLength(1)]
        public string DescOW { get; set; }

        [StringLength(1)]
        public string DetailsOW { get; set; }

        public short? GrossBase { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatWoDpm { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatWoDpmFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatWoDpmSc { get; set; }

        [StringLength(6)]
        public string CFOPCode { get; set; }

        [StringLength(6)]
        public string CSTCode { get; set; }

        public int? Usage { get; set; }

        [StringLength(1)]
        public string TaxOnly { get; set; }

        [StringLength(1)]
        public string WtCalced { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? QtyToShip { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DelivrdQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OrderedQty { get; set; }

        [StringLength(8)]
        public string CogsOcrCod { get; set; }

        public int? CiOppLineN { get; set; }

        [StringLength(15)]
        public string CogsAcct { get; set; }

        [StringLength(1)]
        public string ChgAsmBoMW { get; set; }

        public DateTime? ActDelDate { get; set; }

        [StringLength(8)]
        public string OcrCode2 { get; set; }

        [StringLength(8)]
        public string OcrCode3 { get; set; }

        [StringLength(8)]
        public string OcrCode4 { get; set; }

        [StringLength(8)]
        public string OcrCode5 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxDistSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxDistSFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxDistSSC { get; set; }

        [StringLength(1)]
        public string PostTax { get; set; }

        [StringLength(1)]
        public string Excisable { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AssblValue { get; set; }

        public int? RG23APart1 { get; set; }

        public int? RG23APart2 { get; set; }

        public int? RG23CPart1 { get; set; }

        public int? RG23CPart2 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo2 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo3 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo4 { get; set; }

        [StringLength(8)]
        public string CogsOcrCo5 { get; set; }

        [StringLength(1)]
        public string LnExcised { get; set; }

        public int? LocCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StockValue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GPTtlBasPr { get; set; }

        [StringLength(100)]
        public string unitMsr2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NumPerMsr2 { get; set; }

        [StringLength(1)]
        public string SpecPrice { get; set; }

        [StringLength(2)]
        public string CSTfIPI { get; set; }

        [StringLength(2)]
        public string CSTfPIS { get; set; }

        [StringLength(2)]
        public string CSTfCOFINS { get; set; }

        [StringLength(10)]
        public string ExLineNo { get; set; }

        [StringLength(1)]
        public string isSrvCall { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PQTReqQty { get; set; }

        public DateTime? PQTReqDate { get; set; }

        public int? PcDocType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PcQuantity { get; set; }

        [StringLength(1)]
        public string LinManClsd { get; set; }

        [StringLength(1)]
        public string VatGrpSrc { get; set; }

        [StringLength(1)]
        public string NoInvtryMv { get; set; }

        public int? ActBaseEnt { get; set; }

        public int? ActBaseLn { get; set; }

        public int? ActBaseNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenRtnQty { get; set; }

        public int? AgrNo { get; set; }

        public int? AgrLnNum { get; set; }

        [StringLength(1)]
        public string CredOrigin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Surpluses { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DefBreak { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Shortages { get; set; }

        public int? UomEntry { get; set; }

        public int? UomEntry2 { get; set; }

        [StringLength(20)]
        public string UomCode { get; set; }

        [StringLength(20)]
        public string UomCode2 { get; set; }

        [StringLength(8)]
        public string FromWhsCod { get; set; }

        [StringLength(1)]
        public string NeedQty { get; set; }

        [StringLength(1)]
        public string PartRetire { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RetireQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RetireAPC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RetirAPCFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RetirAPCSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InvQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpenInvQty { get; set; }

        [StringLength(1)]
        public string EnSetCost { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RetCost { get; set; }

        public int? Incoterms { get; set; }

        public int? TransMod { get; set; }

        [StringLength(15)]
        public string LineVendor { get; set; }

        [StringLength(1)]
        public string DistribIS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ISDistrb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ISDistrbFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ISDistrbSC { get; set; }

        [StringLength(1)]
        public string IsByPrdct { get; set; }

        public int? ItemType { get; set; }

        [StringLength(1)]
        public string PriceEdit { get; set; }

        public int? PrntLnNum { get; set; }

        [StringLength(1)]
        public string LinePoPrss { get; set; }

        [StringLength(1)]
        public string FreeChrgBP { get; set; }

        [StringLength(1)]
        public string TaxRelev { get; set; }

        [StringLength(254)]
        public string LegalText { get; set; }

        [StringLength(1)]
        public string ThirdParty { get; set; }

        [StringLength(32)]
        public string LicTradNum { get; set; }

        [StringLength(1)]
        public string InvQtyOnly { get; set; }

        public int? UnencReasn { get; set; }

        [StringLength(50)]
        public string ShipFromCo { get; set; }

        [StringLength(254)]
        public string ShipFromDe { get; set; }

        [StringLength(228)]
        public string FisrtBin { get; set; }

        [StringLength(11)]
        public string AllocBinC { get; set; }

        [StringLength(4)]
        public string ExpType { get; set; }

        [StringLength(50)]
        public string ExpUUID { get; set; }

        [StringLength(1)]
        public string ExpOpType { get; set; }

        [StringLength(3)]
        public string DIOTNat { get; set; }

        [StringLength(2)]
        public string MYFtype { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GPBefDisc { get; set; }

        public short? ReturnRsn { get; set; }

        public short? ReturnAct { get; set; }

        public int? StgSeqNum { get; set; }

        public int? StgEntry { get; set; }

        [StringLength(100)]
        public string StgDesc { get; set; }

        [StringLength(2)]
        public string ItmTaxType { get; set; }

        public int? SacEntry { get; set; }

        public int? NCMCode { get; set; }

        public int? HsnEntry { get; set; }

        public int? OriBAbsEnt { get; set; }

        public int? OriBLinNum { get; set; }

        public int? OriBDocTyp { get; set; }

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

        [StringLength(25)]
        public string U_PartNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_VarPrice { get; set; }

        [StringLength(5)]
        public string U_WTCode { get; set; }
    }
}
