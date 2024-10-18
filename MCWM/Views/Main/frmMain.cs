using DirecLayer;
using MCWM.Views.Tools;
using MCWM.Views.Security;
using MCWM.Views.TravelOrder;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using MCWM.Helper;

namespace MCWM.Views.Main
{
    //public partial class frmMain : Form, IfrmMain
    public partial class frmMain : Form
    {
        public string userCode { get; set; }
        public frmMain(string user)
        {
            InitializeComponent();
            userCode = user;
            lblVersion.Text = $"v{SystemSettings.Info.AssemblyVersion}";

            string query = "SELECT " +
                            $"{(PublicStatic.IsConnectedToServer ? $"Name FROM [@USERS] WHERE U_AUserId = " : $"Name FROM OUSR WHERE Username = ")}'{userCode}'";

            //string qry = $" SELECT U_OperatorName FROM [@WBUSERS] WHERE Code  = '{userCode}'";
            DataTable dtUserName = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                               query);
            lblUser.Text = DataHelper.ReadDataRow(dtUserName, "Name", "", 0);
        }

        #region Event Registration
        //public event EventHandler _Main_newTravelOrderToolStripMenuItem_Click;
        #endregion

        #region Variables
        //public Form mainForm
        //{
        //    get => this;
        //}
        #endregion

        #region Private Declarations

        #endregion

        #region Public Declarations
        //public void ShowForm()
        //{
        //    Show();
        //}

        //public void CloseForm()
        //{
        //    Close();
        //} 

        //public void HideForm()
        //{
        //    Hide();
        //}

        //public void DisposeForm()
        //{
        //    Dispose();
        //}

        //public void NotiMsg()
        //{
        //    MessageBox.Show("Operation Successful!");
        //}

        //public void NotiMsg(string message)
        //{
        //    MessageBox.Show(message);
        //}
        #endregion

        #region Events
        public void newTravelOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            frmUserConfig frmUserConfig = new frmUserConfig();
            frmUserConfig.MdiParent = this;
            frmUserConfig.Show();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            CloseForm();
            //EventHelper.RaisedEvent(sender, _Main_newTravelOrderToolStripMenuItem_Click, e); 
            frmTravelOrderExternal frmTravelOrderExternal = new frmTravelOrderExternal();
            frmTravelOrderExternal.MdiParent = this;
            frmTravelOrderExternal.Show();
        }


        #endregion


        public void CloseForm([Optional]Form form)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm == form)
                { frm.Close(); return; }
                else { frm.Close(); }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void OneTimeWeighMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            frmOneTimeWeigh frmOneTimeWeigh = new frmOneTimeWeigh();
            frmOneTimeWeigh.MdiParent = this;
            frmOneTimeWeigh.Show();
        }

        private void findTravelOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CloseForm();
            //EventHelper.RaisedEvent(sender, _Main_newTravelOrderToolStripMenuItem_Click, e); 
            frmTravelOrderExternal frmTravelOrderExternal = new frmTravelOrderExternal();
            frmTravelOrderExternal.MdiParent = this;
            frmTravelOrderExternal.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            //EventHelper.RaisedEvent(sender, _Main_newTravelOrderToolStripMenuItem_Click, e); 
            frmTravelOrders frmTravelOrders = new frmTravelOrders();
            frmTravelOrders.MdiParent = this;
            frmTravelOrders.Show();

            //frmTravelOrders.btnFind_Click(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CloseForm();
            this.Hide();
            this.Closed += (s, args) => this.Close();


            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();





        }

        private void ListOfTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            frmDateParam frmDateParam = new frmDateParam();
            frmDateParam.MdiParent = this;
            frmDateParam.Show();
        }

        private void summaryOfTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            frmSummaryOfTransactions frmSummaryOfTransactions = new frmSummaryOfTransactions();
            frmSummaryOfTransactions.MdiParent = this;
            frmSummaryOfTransactions.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("hh:mm tt MM/dd/yyyy");
        }

        private void localTransactionsNotUploadedToSAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            frmLocalTransactions frmLocalTransactions = new frmLocalTransactions();
            frmLocalTransactions.MdiParent = this;
            frmLocalTransactions.Show();
        }
    }
}
