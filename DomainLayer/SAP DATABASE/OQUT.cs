namespace DomainLayer.SAP_DATABASE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OQUT")]
    public partial class OQUT
    {
        [Key]
        public int DocEntry { get; set; }

        public int DocNum { get; set; }

        [StringLength(1)]
        public string DocType { get; set; }

        [StringLength(1)]
        public string CANCELED { get; set; }

        [StringLength(1)]
        public string Handwrtten { get; set; }

        [StringLength(1)]
        public string Printed { get; set; }

        [StringLength(1)]
        public string DocStatus { get; set; }

        [StringLength(1)]
        public string InvntSttus { get; set; }

        [StringLength(1)]
        public string Transfered { get; set; }

        [StringLength(20)]
        public string ObjType { get; set; }

        public DateTime? DocDate { get; set; }

        public DateTime? DocDueDate { get; set; }

        [StringLength(15)]
        public string CardCode { get; set; }

        [StringLength(100)]
        public string CardName { get; set; }

        [StringLength(254)]
        public string Address { get; set; }

        [StringLength(100)]
        public string NumAtCard { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatPercent { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSumFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscPrcnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscSumFC { get; set; }

        [StringLength(3)]
        public string DocCur { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocTotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocTotalFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidToDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrosProfit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrosProfFC { get; set; }

        [StringLength(11)]
        public string Ref1 { get; set; }

        [StringLength(11)]
        public string Ref2 { get; set; }

        [StringLength(254)]
        public string Comments { get; set; }

        [StringLength(50)]
        public string JrnlMemo { get; set; }

        public int? TransId { get; set; }

        public int? ReceiptNum { get; set; }

        public short? GroupNum { get; set; }

        public short? DocTime { get; set; }

        public int? SlpCode { get; set; }

        public short? TrnspCode { get; set; }

        [StringLength(1)]
        public string PartSupply { get; set; }

        [StringLength(1)]
        public string Confirmed { get; set; }

        public short? GrossBase { get; set; }

        public int? ImportEnt { get; set; }

        [StringLength(1)]
        public string CreateTran { get; set; }

        [StringLength(1)]
        public string SummryType { get; set; }

        [StringLength(1)]
        public string UpdInvnt { get; set; }

        [StringLength(1)]
        public string UpdCardBal { get; set; }

        public short Instance { get; set; }

        public int? Flags { get; set; }

        [StringLength(1)]
        public string InvntDirec { get; set; }

        public int? CntctCode { get; set; }

        [StringLength(1)]
        public string ShowSCN { get; set; }

        [StringLength(15)]
        public string FatherCard { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SysRate { get; set; }

        [StringLength(1)]
        public string CurSource { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatSumSy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscSumSy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DocTotalSy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidSys { get; set; }

        [StringLength(1)]
        public string FatherType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GrosProfSy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(1)]
        public string IsICT { get; set; }

        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Volume { get; set; }

        public short? VolUnit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight { get; set; }

        public short? WeightUnit { get; set; }

        public int? Series { get; set; }

        public DateTime? TaxDate { get; set; }

        [StringLength(8)]
        public string Filler { get; set; }

        [StringLength(1)]
        public string DataSource { get; set; }

        [StringLength(16)]
        public string StampNum { get; set; }

        [StringLength(1)]
        public string isCrin { get; set; }

        public int? FinncPriod { get; set; }

        public short? UserSign { get; set; }

        [StringLength(1)]
        public string selfInv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatPaid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatPaidFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VatPaidSys { get; set; }

        public short? UserSign2 { get; set; }

        [StringLength(1)]
        public string WddStatus { get; set; }

        public int? draftKey { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalExpns { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalExpFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalExpSC { get; set; }

        public int? DunnLevel { get; set; }

        [StringLength(254)]
        public string Address2 { get; set; }

        public int? LogInstanc { get; set; }

        [StringLength(1)]
        public string Exported { get; set; }

        public int? StationID { get; set; }

        [StringLength(2)]
        public string Indicator { get; set; }

        [StringLength(1)]
        public string NetProc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AqcsTax { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AqcsTaxFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AqcsTaxSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CashDiscPr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CashDiscnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CashDiscFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CashDiscSC { get; set; }

        [StringLength(50)]
        public string ShipToCode { get; set; }

        [StringLength(32)]
        public string LicTradNum { get; set; }

        [StringLength(27)]
        public string PaymentRef { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTSumFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTSumSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RoundDif { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RoundDifFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RoundDifSy { get; set; }

        [StringLength(1)]
        public string CheckDigit { get; set; }

        public int? Form1099 { get; set; }

        [StringLength(20)]
        public string Box1099 { get; set; }

        [StringLength(1)]
        public string submitted { get; set; }

        [StringLength(1)]
        public string PoPrss { get; set; }

        [StringLength(1)]
        public string Rounding { get; set; }

        [StringLength(1)]
        public string RevisionPo { get; set; }

        public short Segment { get; set; }

        public DateTime? ReqDate { get; set; }

        public DateTime? CancelDate { get; set; }

        [StringLength(1)]
        public string PickStatus { get; set; }

        [StringLength(1)]
        public string Pick { get; set; }

        [StringLength(1)]
        public string BlockDunn { get; set; }

        [StringLength(15)]
        public string PeyMethod { get; set; }

        [StringLength(1)]
        public string PayBlock { get; set; }

        public int? PayBlckRef { get; set; }

        [StringLength(1)]
        public string MaxDscn { get; set; }

        [StringLength(1)]
        public string Reserve { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Max1099 { get; set; }

        [StringLength(15)]
        public string CntrlBnk { get; set; }

        [StringLength(254)]
        public string PickRmrk { get; set; }

        [StringLength(53)]
        public string ISRCodLine { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpAppl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpApplFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpApplSC { get; set; }

        [StringLength(20)]
        public string Project { get; set; }

        [StringLength(1)]
        public string DeferrTax { get; set; }

        [StringLength(20)]
        public string LetterNum { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTApplied { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTAppliedF { get; set; }

        [StringLength(1)]
        public string BoeReserev { get; set; }

        [StringLength(32)]
        public string AgentCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WTAppliedS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSumF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EquVatSumS { get; set; }

        public short? Installmnt { get; set; }

        [StringLength(1)]
        public string VATFirst { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NnSbAmnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NnSbAmntSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NbSbAmntFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExepAmnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExepAmntSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExepAmntFC { get; set; }

        public DateTime? VatDate { get; set; }

        [StringLength(25)]
        public string CorrExt { get; set; }

        public int? CorrInv { get; set; }

        public int? NCorrInv { get; set; }

        [StringLength(1)]
        public string CEECFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseAmnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseAmntSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseAmntFC { get; set; }

        [StringLength(15)]
        public string CtlAccount { get; set; }

        public int? BPLId { get; set; }

        [StringLength(100)]
        public string BPLName { get; set; }

        [StringLength(32)]
        public string VATRegNum { get; set; }

        [StringLength(10)]
        public string TxInvRptNo { get; set; }

        public DateTime? TxInvRptDt { get; set; }

        [Column(TypeName = "ntext")]
        public string KVVATCode { get; set; }

        [StringLength(100)]
        public string WTDetails { get; set; }

        public int? SumAbsId { get; set; }

        public DateTime? SumRptDate { get; set; }

        [Required]
        [StringLength(10)]
        public string PIndicator { get; set; }

        [StringLength(20)]
        public string ManualNum { get; set; }

        [StringLength(1)]
        public string UseShpdGd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseVtAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseVtAtSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseVtAtFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NnSbVAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NnSbVAtSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NbSbVAtFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExptVAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExptVAtSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExptVAtFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LYPmtAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LYPmtAtSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LYPmtAtFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpAnSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpAnSys { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpAnFrgn { get; set; }

        [Required]
        [StringLength(2)]
        public string DocSubType { get; set; }

        [StringLength(1)]
        public string DpmStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAmnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAmntSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAmntFC { get; set; }

        [StringLength(1)]
        public string DpmDrawn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmPrcnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidSum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidSumFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidSumSc { get; set; }

        [StringLength(4)]
        public string FolioPref { get; set; }

        public int? FolioNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAppl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmApplFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmApplSc { get; set; }

        public int? LPgFolioN { get; set; }

        [Column(TypeName = "ntext")]
        public string Header { get; set; }

        [Column(TypeName = "ntext")]
        public string Footer { get; set; }

        [StringLength(1)]
        public string Posted { get; set; }

        public int? OwnerCode { get; set; }

        [StringLength(15)]
        public string BPChCode { get; set; }

        public int? BPChCntc { get; set; }

        [StringLength(50)]
        public string PayToCode { get; set; }

        [StringLength(1)]
        public string IsPaytoBnk { get; set; }

        [StringLength(3)]
        public string BnkCntry { get; set; }

        [StringLength(30)]
        public string BankCode { get; set; }

        [StringLength(50)]
        public string BnkAccount { get; set; }

        [StringLength(50)]
        public string BnkBranch { get; set; }

        [StringLength(1)]
        public string isIns { get; set; }

        [StringLength(30)]
        public string TrackNo { get; set; }

        [StringLength(11)]
        public string VersionNum { get; set; }

        public int? LangCode { get; set; }

        [StringLength(1)]
        public string BPNameOW { get; set; }

        [StringLength(1)]
        public string BillToOW { get; set; }

        [StringLength(1)]
        public string ShipToOW { get; set; }

        [StringLength(1)]
        public string RetInvoice { get; set; }

        public DateTime? ClsDate { get; set; }

        public int? MInvNum { get; set; }

        public DateTime? MInvDate { get; set; }

        public short? SeqCode { get; set; }

        public int? Serial { get; set; }

        [StringLength(3)]
        public string SeriesStr { get; set; }

        [StringLength(3)]
        public string SubStr { get; set; }

        [StringLength(6)]
        public string Model { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExpFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExpSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExAp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExApF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxOnExApS { get; set; }

        [StringLength(1)]
        public string LastPmnTyp { get; set; }

        public int? LndCstNum { get; set; }

        [StringLength(1)]
        public string UseCorrVat { get; set; }

        [StringLength(1)]
        public string BlkCredMmo { get; set; }

        [StringLength(1)]
        public string OpenForLaC { get; set; }

        [StringLength(1)]
        public string Excised { get; set; }

        public DateTime? ExcRefDate { get; set; }

        [StringLength(8)]
        public string ExcRmvTime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SrvGpPrcnt { get; set; }

        public int? DepositNum { get; set; }

        [StringLength(31)]
        public string CertNum { get; set; }

        [StringLength(1)]
        public string DutyStatus { get; set; }

        [StringLength(1)]
        public string AutoCrtFlw { get; set; }

        public DateTime? FlwRefDate { get; set; }

        [StringLength(100)]
        public string FlwRefNum { get; set; }

        public int? VatJENum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmVat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmVatFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmVatSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAppVat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAppVatF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DpmAppVatS { get; set; }

        [StringLength(1)]
        public string InsurOp347 { get; set; }

        [StringLength(1)]
        public string IgnRelDoc { get; set; }

        [StringLength(50)]
        public string BuildDesc { get; set; }

        [StringLength(1)]
        public string ResidenNum { get; set; }

        public int? Checker { get; set; }

        public int? Payee { get; set; }

        public int? CopyNumber { get; set; }

        [StringLength(1)]
        public string SSIExmpt { get; set; }

        public int? PQTGrpSer { get; set; }

        public int? PQTGrpNum { get; set; }

        [StringLength(1)]
        public string PQTGrpHW { get; set; }

        [StringLength(1)]
        public string ReopOriDoc { get; set; }

        [StringLength(1)]
        public string ReopManCls { get; set; }

        [StringLength(1)]
        public string DocManClsd { get; set; }

        public short? ClosingOpt { get; set; }

        public DateTime? SpecDate { get; set; }

        [StringLength(1)]
        public string Ordered { get; set; }

        [StringLength(1)]
        public string NTSApprov { get; set; }

        public short? NTSWebSite { get; set; }

        [StringLength(50)]
        public string NTSeTaxNo { get; set; }

        [StringLength(50)]
        public string NTSApprNo { get; set; }

        [StringLength(1)]
        public string PayDuMonth { get; set; }

        public short? ExtraMonth { get; set; }

        public short? ExtraDays { get; set; }

        public short? CdcOffset { get; set; }

        [Column(TypeName = "ntext")]
        public string SignMsg { get; set; }

        [Column(TypeName = "ntext")]
        public string SignDigest { get; set; }

        [StringLength(50)]
        public string CertifNum { get; set; }

        public int? KeyVersion { get; set; }

        [StringLength(1)]
        public string EDocGenTyp { get; set; }

        public short? ESeries { get; set; }

        [StringLength(50)]
        public string EDocNum { get; set; }

        public int? EDocExpFrm { get; set; }

        [StringLength(1)]
        public string OnlineQuo { get; set; }

        [StringLength(20)]
        public string POSEqNum { get; set; }

        [StringLength(20)]
        public string POSManufSN { get; set; }

        public int? POSCashN { get; set; }

        [StringLength(1)]
        public string EDocStatus { get; set; }

        [Column(TypeName = "ntext")]
        public string EDocCntnt { get; set; }

        [StringLength(1)]
        public string EDocProces { get; set; }

        [StringLength(50)]
        public string EDocErrCod { get; set; }

        [Column(TypeName = "ntext")]
        public string EDocErrMsg { get; set; }

        [StringLength(1)]
        public string EDocCancel { get; set; }

        [StringLength(1)]
        public string EDocTest { get; set; }

        [StringLength(10)]
        public string EDocPrefix { get; set; }

        public int? CUP { get; set; }

        public int? CIG { get; set; }

        [StringLength(1)]
        public string DpmAsDscnt { get; set; }

        [Column(TypeName = "ntext")]
        public string Attachment { get; set; }

        public int? AtcEntry { get; set; }

        [StringLength(254)]
        public string SupplCode { get; set; }

        [StringLength(1)]
        public string GTSRlvnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseDisc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseDiscSc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseDiscFc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BaseDiscPr { get; set; }

        public int? CreateTS { get; set; }

        public int? UpdateTS { get; set; }

        [StringLength(1)]
        public string SrvTaxRule { get; set; }

        public int? AnnInvDecR { get; set; }

        [StringLength(15)]
        public string Supplier { get; set; }

        public int? Releaser { get; set; }

        public int? Receiver { get; set; }

        [StringLength(8)]
        public string ToWhsCode { get; set; }

        public DateTime? AssetDate { get; set; }

        [StringLength(25)]
        public string Requester { get; set; }

        [StringLength(155)]
        public string ReqName { get; set; }

        public short? Branch { get; set; }

        public short? Department { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1)]
        public string Notify { get; set; }

        public int? ReqType { get; set; }

        [StringLength(1)]
        public string OriginType { get; set; }

        [StringLength(1)]
        public string IsReuseNum { get; set; }

        [StringLength(1)]
        public string IsReuseNFN { get; set; }

        [StringLength(1)]
        public string DocDlvry { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidDpm { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidDpmF { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaidDpmS { get; set; }

        public int? EnvTypeNFe { get; set; }

        public int? AgrNo { get; set; }

        [StringLength(1)]
        public string IsAlt { get; set; }

        public int? AltBaseTyp { get; set; }

        public int? AltBaseEnt { get; set; }

        [StringLength(250)]
        public string AuthCode { get; set; }

        public DateTime? StDlvDate { get; set; }

        public int? StDlvTime { get; set; }

        public DateTime? EndDlvDate { get; set; }

        public int? EndDlvTime { get; set; }

        [StringLength(20)]
        public string VclPlate { get; set; }

        [StringLength(10)]
        public string ElCoStatus { get; set; }

        [StringLength(2)]
        public string AtDocType { get; set; }

        [StringLength(254)]
        public string ElCoMsg { get; set; }

        [StringLength(1)]
        public string PrintSEPA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FreeChrg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FreeChrgFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FreeChrgSC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NfeValue { get; set; }

        [StringLength(100)]
        public string FiscDocNum { get; set; }

        public int? RelatedTyp { get; set; }

        public int? RelatedEnt { get; set; }

        public int? CCDEntry { get; set; }

        public int? NfePrntFo { get; set; }

        public int? ZrdAbs { get; set; }

        public int? POSRcptNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCTax { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCTaxFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCTaxSC { get; set; }

        public int? TpCusPres { get; set; }

        public DateTime? ExcDocDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCFrght { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCFrghtFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FoCFrghtSC { get; set; }

        public short? InterimTyp { get; set; }

        [StringLength(5)]
        public string PTICode { get; set; }

        [StringLength(1)]
        public string Letter { get; set; }

        public int? FolNumFrom { get; set; }

        public int? FolNumTo { get; set; }

        public int? FolSeries { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SplitTax { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SplitTaxFC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SplitTaxSC { get; set; }

        [StringLength(228)]
        public string ToBinCode { get; set; }

        [StringLength(1)]
        public string PriceMode { get; set; }

        [StringLength(1)]
        public string PoDropPrss { get; set; }

        [StringLength(20)]
        public string PermitNo { get; set; }

        [StringLength(2)]
        public string MYFtype { get; set; }

        [StringLength(32)]
        public string DocTaxID { get; set; }

        public DateTime? DateReport { get; set; }

        [StringLength(3)]
        public string RepSection { get; set; }

        [StringLength(1)]
        public string ExclTaxRep { get; set; }

        public int? PosCashReg { get; set; }

        [StringLength(20)]
        public string DmpTransID { get; set; }

        [StringLength(15)]
        public string ECommerBP { get; set; }

        [StringLength(15)]
        public string EComerGSTN { get; set; }

        [StringLength(1)]
        public string Revision { get; set; }

        [StringLength(100)]
        public string RevRefNo { get; set; }

        public DateTime? RevRefDate { get; set; }

        [StringLength(100)]
        public string RevCreRefN { get; set; }

        public DateTime? RevCreRefD { get; set; }

        [StringLength(100)]
        public string TaxInvNo { get; set; }

        public DateTime? FrmBpDate { get; set; }

        [StringLength(2)]
        public string GSTTranTyp { get; set; }

        public int? BaseType { get; set; }

        public int? BaseEntry { get; set; }

        [StringLength(1)]
        public string ComTrade { get; set; }

        [StringLength(1)]
        public string UseBilAddr { get; set; }

        public short? IssReason { get; set; }

        [StringLength(1)]
        public string ComTradeRt { get; set; }

        [StringLength(1)]
        public string SplitPmnt { get; set; }

        public int? SOIWizId { get; set; }

        [StringLength(1)]
        public string SelfPosted { get; set; }

        [Column(TypeName = "ntext")]
        public string EnBnkAcct { get; set; }

        [StringLength(100)]
        public string EncryptIV { get; set; }

        [StringLength(20)]
        public string U_SINo { get; set; }

        [StringLength(20)]
        public string U_PrepBy { get; set; }

        [StringLength(20)]
        public string U_RevBy { get; set; }

        [StringLength(20)]
        public string U_AppBy { get; set; }

        [StringLength(15)]
        public string U_RetType { get; set; }

        [StringLength(254)]
        public string U_Remarks { get; set; }

        [StringLength(20)]
        public string U_CheckBy { get; set; }

        [StringLength(50)]
        public string U_PONo { get; set; }

        [StringLength(50)]
        public string U_Division { get; set; }

        public int? U_Attach { get; set; }

        [StringLength(30)]
        public string U_EmpName { get; set; }

        [StringLength(50)]
        public string U_Area { get; set; }

        [StringLength(20)]
        public string U_ReqBy { get; set; }

        [StringLength(10)]
        public string U_TransferType { get; set; }

        [StringLength(10)]
        public string U_TransferMode { get; set; }

        [StringLength(20)]
        public string U_TransCat { get; set; }

        [StringLength(50)]
        public string U_DRNo { get; set; }

        [StringLength(15)]
        public string U_SONo { get; set; }

        [StringLength(5)]
        public string U_2307rep { get; set; }

        public DateTime? U_2307repdate { get; set; }

        [StringLength(5)]
        public string U_1601rep { get; set; }

        public DateTime? U_1601repdate { get; set; }

        [StringLength(30)]
        public string U_CustName { get; set; }

        [StringLength(10)]
        public string U_CustCode { get; set; }

        [StringLength(10)]
        public string U_Load { get; set; }

        [StringLength(20)]
        public string U_ShippingType { get; set; }

        [StringLength(50)]
        public string U_Origin { get; set; }

        [StringLength(50)]
        public string U_Destination { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_TotalWt { get; set; }

        [StringLength(20)]
        public string U_Packaging { get; set; }

        [StringLength(20)]
        public string U_Border { get; set; }

        public DateTime? U_DelDate { get; set; }

        [StringLength(10)]
        public string U_ShipStat { get; set; }

        public int? U_CABalance { get; set; }

        [StringLength(10)]
        public string U_PRNo { get; set; }

        [StringLength(20)]
        public string U_RecBy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DecValue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_TotalFreight { get; set; }

        [StringLength(254)]
        public string U_ToAdd { get; set; }

        [StringLength(254)]
        public string U_FromAdd { get; set; }

        [StringLength(20)]
        public string U_GRNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DecValFee { get; set; }

        [StringLength(20)]
        public string U_JONo { get; set; }

        [StringLength(20)]
        public string U_PRONo { get; set; }

        [StringLength(50)]
        public string U_DiscID { get; set; }

        public DateTime? U_InvoiceDate { get; set; }

        [StringLength(50)]
        public string U_PriorityLevel { get; set; }

        [StringLength(50)]
        public string U_JobType { get; set; }

        [StringLength(20)]
        public string U_EmpCode { get; set; }

        [StringLength(20)]
        public string U_ShipStatus { get; set; }

        public DateTime? U_Date { get; set; }

        [StringLength(50)]
        public string U_TIN { get; set; }

        [StringLength(254)]
        public string U_Address { get; set; }

        [StringLength(50)]
        public string U_Vendor { get; set; }

        [StringLength(50)]
        public string U_Department { get; set; }

        [StringLength(50)]
        public string U_PurchType { get; set; }

        [StringLength(20)]
        public string U_ICSOSRefNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_DiscBase { get; set; }

        [StringLength(10)]
        public string U_YPNo { get; set; }

        [StringLength(10)]
        public string U_SWMFNo { get; set; }

        [StringLength(50)]
        public string U_DelBy { get; set; }

        [StringLength(20)]
        public string U_PlateNo { get; set; }

        [StringLength(50)]
        public string U_Driver { get; set; }

        [StringLength(30)]
        public string U_Purpose { get; set; }

        [StringLength(50)]
        public string U_Dispatcher { get; set; }

        [StringLength(50)]
        public string U_Helper1 { get; set; }

        [StringLength(50)]
        public string U_Helper2 { get; set; }

        [StringLength(50)]
        public string U_Helper3 { get; set; }

        public short? U_TripStart { get; set; }

        public short? U_TripEnd { get; set; }

        public short? U_GateOut { get; set; }

        public short? U_GateIn { get; set; }

        public short? U_LoadTime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_MileStart { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_MileEnd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Fuel { get; set; }

        [StringLength(10)]
        public string U_Cont1 { get; set; }

        [StringLength(10)]
        public string U_Cont2 { get; set; }

        [StringLength(30)]
        public string U_Rem1 { get; set; }

        [StringLength(30)]
        public string U_Rem2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Weight1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Weight2 { get; set; }

        [StringLength(50)]
        public string U_WasteType { get; set; }

        [StringLength(20)]
        public string U_Disposal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_NetWt { get; set; }

        [StringLength(10)]
        public string U_BinNo2 { get; set; }

        [StringLength(10)]
        public string U_BinNo1 { get; set; }

        public DateTime? U_WgtInDate { get; set; }

        public DateTime? U_WgtOutDate { get; set; }

        public string U_WgtInTime { get; set; }

        public string U_WgtOutTime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Length { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Width { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? U_Height { get; set; }

        [StringLength(10)]
        public string U_TORNo { get; set; }

        [StringLength(30)]
        public string U_IssueType { get; set; }

        [StringLength(50)]
        public string U_Operator { get; set; }

        [StringLength(100)]
        public string U_Transporter { get; set; }

        [StringLength(10)]
        public int U_WBNo { get; set; }

        [StringLength(50)]
        public string U_Company { get; set; }

        [StringLength(10)]
        public string U_Source { get; set; }

        [StringLength(25)]
        public string U_MISNo { get; set; }

        [StringLength(10)]
        public string U_Combination { get; set; }

        [StringLength(50)]
        public string U_Operator1 { get; set; }

        [StringLength(50)]
        public string U_Operator2 { get; set; }

        public virtual ICollection<QUT1> DocumentLines { get; set; }
    }
}
