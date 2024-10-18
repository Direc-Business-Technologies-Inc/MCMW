using System;
using MCWM.Views.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirecLayer;
using MCWM.Helper;

namespace MCWM.Views.TravelOrder
{
    public partial class frmOneTimeWeigh : Form
    {
        SerialPort serialPort { get; set; }
        private const int BaudRate = 9600;
        public string iDTag = "";

        public frmOneTimeWeigh()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmOneTimeWeigh_Load(object sender, EventArgs e)
        {
            LoadAllComPorts();
            timer1.Enabled = true;
        }
        SerialPort SetSerialPort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }

            serialPort = new SerialPort(cmbPorts.Text, BaudRate, Parity.None, 7, StopBits.One);
            serialPort.Open();
            return serialPort;
        }

        void LoadAllComPorts()
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (var portName in portNames)
            {
                cmbPorts.Items.Add(portName);
            }

            if (cmbPorts.Items.Count > 0)
            {
                cmbPorts.SelectedIndex = 0;
                serialPort = SetSerialPort();
            }
            else
            {
                //disable controls if the port weighing scale is not connected
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy -- HH:mm:ss");
        }

        private void btnNoWaste_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstWeight.Text) == false && Convert.ToDecimal(txtFirstWeight.Text) > 0)
            {
                txtSecondWeight.Text = txtFirstWeight.Text;
                txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();
                lblWgtOutDate.Text = DateTime.Now.ToShortDateString();
                lblWgtOutTime.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                MessageBox.Show("No First Weight detected. Supply First Weight to apply this function.");
            }
        }

        private void btnWeigh_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWeight.ReadOnly)
                {
                    txtWeight.Text = ReadLine(serialPort.ReadExisting());
                    weightCondition();
                }
                else
                {
                    weightCondition();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void weightCondition()
        {
            if (txtWeight.Text != null || txtWeight.Text == "")
            {
                if (Convert.ToDecimal(txtWeight.Text) > 0)
                {
                    if (string.IsNullOrWhiteSpace(txtFirstWeight.Text) || Convert.ToDecimal(txtFirstWeight.Text) == 0)
                    {
                        txtFirstWeight.Text = (decimal.Parse(txtWeight.Text) / 1000).ToString();
                        lblWgtInDate.Text = DateTime.Now.ToShortDateString();
                        lblWgtInTime.Text = DateTime.Now.ToShortTimeString();
                    }
                    else
                    {
                        if ((Convert.ToDecimal(txtFirstWeight.Text) - Convert.ToDecimal(txtSecondWeight.Text)) >= 0)
                        {
                            txtSecondWeight.Text = (decimal.Parse(txtWeight.Text) / 1000).ToString();
                            txtNetWeight.Text = (decimal.Parse(txtSecondWeight.Text) - decimal.Parse(txtFirstWeight.Text)).ToString();
                            lblWgtOutDate.Text = DateTime.Now.ToShortDateString();
                            lblWgtOutTime.Text = DateTime.Now.ToShortTimeString();
                        }
                        else
                        {
                            MessageBox.Show("Negative Net weight is not allowed. Please supply proper first weight.");
                        }
                    }
                }
                else { MessageBox.Show("No weight detected."); }
            }
            else { MessageBox.Show("No weight detected."); }
        }


        string ReadLine(string value)
        {
            value = Regex.Replace(value, "[^a-zA-Z0-9]", "");
            value = Regex.Replace(value, @"[^\u0009\u000A\u000D\u0020-\u007E]", "");
            value = value.Replace("k", "").Replace(" ", "").Replace(" ", "").Replace(@"\u0002", "").Replace(@"\u0003", "").Trim();
            value = value.Replace("!", "");
            value = value.Replace("C", "");
            value = new string(value.Where(c => char.IsDigit(c)).ToArray());
            foreach (var item in value.Split('g'))
            {
                value = item;
                break;
            }
            return value;
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //when manual typing is allowed
                if (txtWeight.ReadOnly)
                {
                    weightCondition();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the recorded weights? (The data will be updated to SAP upon Update of the document.)", "CONFIRMATION",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                txtFirstWeight.Text = "0.00";
                txtSecondWeight.Text = "0.00";
                txtNetWeight.Text = "0.00";


                lblWgtInDate.Text = "";
                lblWgtInTime.Text = "";
                lblWgtOutDate.Text = "";
                lblWgtOutTime.Text = "";

            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (txtWeight.ReadOnly)
            {
                txtWeight.ReadOnly = false;
                lblToggle.Text = "Manual";
            }
            else
            {
                txtWeight.ReadOnly = true;
                lblToggle.Text = "Auto";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtSecondWeight.Text) > 0)
            {
                frmCrystalReport frmCrystalReport = new frmCrystalReport("oneweigh", "");
                frmCrystalReport.ShowDialog();
            }
        }



        void loadReport()
        {
            if (Convert.ToDecimal(txtSecondWeight.Text) > 0)
            {
                frmCrystalReport frmCrystalReport = new frmCrystalReport("oneweigh", "");
                frmCrystalReport.oDocKey = iDTag;
                Close();
                frmCrystalReport.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (!string.IsNullOrWhiteSpace(txtClientName.Text))
            {
                if (Convert.ToDecimal(txtSecondWeight.Text) != 0)
                {
                    qry = $"UPDATE OOTW SET Weight2 = {txtSecondWeight.Text}, netWt = {txtNetWeight.Text}," +
                        $" WgtOutDate = '{lblWgtOutDate.Text}', WgtOutTime = '{lblWgtOutTime.Text}' " +
                        $" WHERE Id = {iDTag}";
                    if (MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qry) >= 0)
                    {
                        MessageBox.Show("Transaction updated.");
                        loadReport();

                    }
                    else { MessageBox.Show("Error updating the transaction. Please contact administrator."); }
                }
                else
                {
                    qry = $"SELECT TOP 1 'True' FROM OOTW WHERE clientName = '{txtClientName.Text}' AND WgtInDate = '{lblWgtInDate.Text}'";
                    DataTable dtIfExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                        qry);
                    if (Convert.ToDecimal(txtFirstWeight.Text) != 0)
                    {

                        if (dtIfExist.Rows.Count == 0)
                        {
                            qry = $"INSERT INTO OOTW (Weight1, Weight2, netwt,clientName, WgtInDate, WgtInTime)  " +
                                        $" VALUES ({txtFirstWeight.Text},0,0,'{txtClientName.Text}', '{lblWgtInDate.Text}','{lblWgtInTime.Text}')";
                            if (MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qry) >= 0)
                            {
                                MessageBox.Show("Transaction added.");
                                Close();
                                frmOneTimeWeigh frmOneTimeWeigh = new frmOneTimeWeigh();
                                frmOneTimeWeigh.MdiParent = PublicStatic._frmMain;
                                frmOneTimeWeigh.Show();
                            }
                            else
                            {
                                MessageBox.Show("Error updating the transaction. Please contact administrator.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Transaction already exists.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Tare Weight Supplied.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No Client Name Supplied.");
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmSearch = new frmSearch("onetimeweigh");
                frmSearch.ShowDialog();

                if (frmSearch.TransNo != null)
                {
                    fromSearch(frmSearch.TransNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void fromSearch(string TORNo)
        {
            try
            {
                iDTag = TORNo;
                if (!string.IsNullOrWhiteSpace(iDTag))
                {
                    string qry = $"SELECT Weight1, clientName, WgtInDate, WgtInTime FROM OOTW WHERE Id = {iDTag}";
                    DataTable dtOTW = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                        qry);


                    if (dtOTW.Rows.Count > 0)
                    {
                        txtFirstWeight.Text = DataHelper.ReadDataRow(dtOTW, "Weight1", "", 0);
                        txtClientName.Text = DataHelper.ReadDataRow(dtOTW, "clientName", "", 0);
                        lblWgtInDate.Text = DataHelper.ReadDataRow(dtOTW, "WgtInDate", "", 0);
                        lblWgtInTime.Text = DataHelper.ReadDataRow(dtOTW, "WgtInTime", "", 0);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSecondWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmOneTimeWeigh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }
            Dispose();
        }
    }
}
