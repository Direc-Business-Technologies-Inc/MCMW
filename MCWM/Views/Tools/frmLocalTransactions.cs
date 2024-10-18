using DirecLayer;
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
    public partial class frmLocalTransactions : Form
    {
        public frmLocalTransactions()
        {
            InitializeComponent();
        }

        private void frmLocalTransactions_Load(object sender, EventArgs e)
        {
            loadData();

        }

        void loadReport()
        {

            int rowindex = dgvSummary.CurrentCell.RowIndex;
            frmCrystalReport frmCrystalReport = new frmCrystalReport("localReport", dgvSummary.Rows[rowindex].Cells[7].Value.ToString());
            frmCrystalReport.oDocKey = dgvSummary.Rows[rowindex].Cells[0].Value.ToString();
            frmCrystalReport.ShowDialog();
        }

        private void dgvSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loadReport();
        }

        void loadData()
        {
            //populate cmb
            string qry = "SELECT  x.WBNo, x.CustomerName, x.WgtInDate,x.WgtOutDate, x.DRNo, x.Driver, x.PlateNo, x.VehicleType,x.YardPassNo,x.TransType FROM ( " +
                       " SELECT A.* FROM ODOC A WHERE A.TransType <> 'FLEET' AND ISNULL(Weight2,0) <> 0 " +
                       "  UNION ALL " +
                       " SELECT A.* FROM ODOC A WHERE A.TransType ='FLEET' ) x ORDER BY CAST(x.WBNo AS INT)  ";
            DataTable dtSummary = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                    qry);
            dgvSummary.DataSource = dtSummary;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
