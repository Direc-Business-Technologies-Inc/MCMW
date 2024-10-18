using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DirecLayer;
using MCWM.Helper;
using MCWM.Views.TravelOrder;

namespace MCWM.Views.Tools
{
    public partial class frmCrystalReport : Form
    {
        private string DocKey;
        private string dateFrom;
        private string dateTo;
        public string oDocKey { get { return DocKey; } set { DocKey = value; } }
        public string oDateFrom { get { return dateFrom; } set { dateFrom = value; } }
        public string oDateTo { get { return dateTo; } set { dateTo = value; } }
        public string tagMain = "";
        public string printed = "";
        public string trans = "";

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            { Close(); }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        public frmCrystalReport(string tag, string transtype)
        {
            InitializeComponent();
            tagMain = tag;
            trans = transtype;
        }

        private void frmCrystalReport_Load(object sender, EventArgs e)
        {
            using (ReportDocument cryRpt = new ReportDocument())
            {
                var oSettings = Properties.Settings.Default;


                string path = AppConfig.AppSettings("weightReportPath");

                //cryRpt.Load($"{path}\\Reports\\SLPI - LT Multiple-SO.rpt");   
                if (tagMain == "oneweigh")
                {
                    cryRpt.Load(path + $"\\ORDR - One Time Weigh Report.rpt");
                    cryRpt.SetParameterValue("DocKey@", oDocKey);
                }
                else if (tagMain == "external" || tagMain == "internalNotSAP" || tagMain == "localReport")
                {
                    cryRpt.Load(path + $"\\ORDR - Weight Report - LOCAL.rpt");
                    cryRpt.SetParameterValue("DocKey@", oDocKey);
                    cryRpt.SetParameterValue("PrintedBy@", PublicStatic._frmMain.lblUser.Text);
                }
                else if (tagMain == "List")
                {
                    cryRpt.Load(path + $"\\ORDR - List of Transaction Report.rpt");
                    cryRpt.SetParameterValue("dateFrom@", oDateFrom);
                    cryRpt.SetParameterValue("dateTo@", oDateTo);
                }
                else
                {
                    cryRpt.Load(path + $"\\ORDR - Weight Report.rpt");
                    cryRpt.SetParameterValue("DocKey@", oDocKey);
                    cryRpt.SetParameterValue("Tag@", trans);
                    cryRpt.SetParameterValue("PrintedBy@", PublicStatic._frmMain.lblUser.Text);
                }

                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                if (tagMain == "oneweigh" || tagMain == "external" || tagMain == "internalNotSAP" || tagMain == "localReport")
                {
                    //local data
                    crConnectionInfo.ServerName = AppConfig.AppSettings("SqlServer");
                    crConnectionInfo.DatabaseName = AppConfig.AppSettings("SqlDatabase");
                    crConnectionInfo.UserID = AppConfig.AppSettings("SqlUserId"); // "SYSTEM";//
                    crConnectionInfo.Password = AppConfig.AppSettings("SqlPassword");  //"Sb1@dbsi"; //
                    CrTables = cryRpt.Database.Tables;
                }
                else
                {
                    //SAP data
                    crConnectionInfo.ServerName = AppConfig.AppSettings("Server");
                    crConnectionInfo.DatabaseName = AppConfig.AppSettings("CompanyDB");
                    crConnectionInfo.UserID = AppConfig.AppSettings("DbUserName"); // "SYSTEM";//
                    crConnectionInfo.Password = AppConfig.AppSettings("DbPassword");  //"Sb1@dbsi"; //
                    CrTables = cryRpt.Database.Tables;
                }

                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                foreach (Table CrTable in CrTables)
                { crtableLogoninfo = CrTable.LogOnInfo; crtableLogoninfo.ConnectionInfo = crConnectionInfo; CrTable.ApplyLogOnInfo(crtableLogoninfo); }
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
                cryRpt.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                cryRpt.Dispose();
            }
        }

        private void frmCrystalReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tagMain == "external" || tagMain == "connected")
            {
                frmTravelOrderExternal frmTravelOrderExternal = new frmTravelOrderExternal();
                frmTravelOrderExternal.MdiParent = PublicStatic._frmMain;
                frmTravelOrderExternal.Show();
            }
            else if (tagMain == "internal")
            {
                frmTravelOrders frmTravelOrders = new frmTravelOrders();
                frmTravelOrders.MdiParent = PublicStatic._frmMain;
                frmTravelOrders.Show();
            }
            else if (tagMain == "oneweigh")
            {
                frmOneTimeWeigh frmOneTimeWeigh = new frmOneTimeWeigh();
                frmOneTimeWeigh.MdiParent = PublicStatic._frmMain;
                frmOneTimeWeigh.Show();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
