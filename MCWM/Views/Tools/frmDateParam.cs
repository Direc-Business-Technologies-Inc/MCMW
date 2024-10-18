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
    public partial class frmDateParam : Form
    {
        public frmDateParam()
        {
            InitializeComponent();
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDateParam_Load(object sender, EventArgs e)
        {
            txtDateFrom.Text = DateTime.Now.ToShortDateString();
            txtDateTo.Text = DateTime.Now.ToShortDateString();

            this.Focus();
            txtDateFrom.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PublicStatic.IsConnectedToServer)
                { 
                    if (!string.IsNullOrWhiteSpace(txtDateFrom.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtDateTo.Text))
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport("List", "");
                            frmCrystalReport.oDateFrom = txtDateFrom.Text;
                            frmCrystalReport.oDateTo = txtDateTo.Text;
                            frmCrystalReport.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Supply 'Date To' properly.");
                            txtDateTo.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Supply 'Date From' properly.");
                        txtDateFrom.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("No connection to SAP."); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
