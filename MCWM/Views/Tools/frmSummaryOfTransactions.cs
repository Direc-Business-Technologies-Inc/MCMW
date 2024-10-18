using DirecLayer;
using MCWM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCWM.Views.Tools
{
    public partial class frmSummaryOfTransactions : Form
    {
        public frmSummaryOfTransactions()
        {
            InitializeComponent();
        }

        private void dgvSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loadReport();
        }

        void loadReport()
        {

            int rowindex = dgvSummary.CurrentCell.RowIndex;
            frmCrystalReport frmCrystalReport = new frmCrystalReport("", dgvSummary.Rows[rowindex].Cells[7].Value.ToString());
            frmCrystalReport.oDocKey = dgvSummary.Rows[rowindex].Cells[0].Value.ToString();
            frmCrystalReport.ShowDialog();
        }

        private void frmSummaryOfTransactions_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            if (PublicStatic.IsConnectedToServer)
            {
                //populate cmb
                string qry = $" SELECT x.* FROM (SELECT U_WBNo [Transaction No.], U_TORNo [TOR No.], DocNum, U_WgtInDate [Weight In], CardName [Customer Name], U_Driver,  U_DRNo [DR No.], 'SO' [Transtype] " +
                $"  FROM ORDR WHERE ISNULL(U_WBNo,'') <> '' AND ISNULL(U_NetWt,0) <> 0  AND U_WgtInDate BETWEEN '{dateTimePicker1.Text}' AND '{dateTimePicker2.Text}'" +
                $" UNION ALL " +
                $"  SELECT U_WBNo [Transaction No.], U_TORNo [TOR No.], DocNum, U_WgtInDate [Weight In], CardName [Customer Name], U_Driver,  U_DRNo [DR No.], 'SQ' [Transtype] " +
                $"  FROM OQUT WHERE ISNULL(U_WBNo,'') <> '' AND ISNULL(U_NetWt,0) <> 0  AND U_WgtInDate BETWEEN '{dateTimePicker1.Text}' AND '{dateTimePicker2.Text}') x " +
                $"  ORDER BY CAST(x.[Transaction No.] as int) DESC ";
                DataTable dtSummary = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                        qry);
                dgvSummary.DataSource = dtSummary;
            }
            else
            {
                MessageBox.Show("No connection to SAP.");

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            loadData();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvSummary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
