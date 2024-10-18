using DirecLayer;
using DomainLayer.CONTEXT;
using DomainLayer.SAO_DATABASE;
using DomainLayer.SAP_DATABASE;
using MCWM.Helper;
using MCWM.Views.Tools;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MCWM.Views.TravelOrder
{
    public partial class frmTravelOrderExternal : Form
    {
        SerialPort serialPort { get; set; }
        private const int BaudRate = 9600;
        private string transType;
        public frmTravelOrderExternal()
        {
            InitializeComponent();
        }

        void loadCMB()
        {
            string query = "SELECT CardCode, CardName FROM OCRD WHERE frozenFor = 'N' " +
                $"{(PublicStatic.IsConnectedToServer ? "AND CardType = 'C'" : "")}";

            DataTable dtCustomer = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);
            cmbCustomerCode.DataSource = dtCustomer;
            cmbCustomerCode.DisplayMember = "CardCode";
            cmbCustomerName.DataSource = dtCustomer;
            cmbCustomerName.DisplayMember = "CardName";

            query = "SELECT " +
                $"{(PublicStatic.IsConnectedToServer ? "FldValue [Code] FROM CUFD a INNER JOIN UFD1 b ON a.FieldID = b.FieldID AND a.Tableid = b.TableID WHERE a.TableID = 'ORDR' AND" : "Code FROM CUFD WHERE")} AliasID = 'Disposal'";

            DataTable dtDisposal = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);
            cmbDisposal.DataSource = dtDisposal;
            cmbDisposal.DisplayMember = "Code";



            query = "SELECT Code, Name FROM " +
               $"{(PublicStatic.IsConnectedToServer ? "[@WASTE_TYPE]" : "CUFD WHERE AliasID = 'WASTE_TYPE'")}";

            DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);

            cmbTypeOfWasteName.DataSource = dtWasteType;
            cmbTypeOfWasteName.DisplayMember = "Name";
            cmbTypeOfWasteCode.DataSource = dtWasteType;
            cmbTypeOfWasteCode.DisplayMember = "Code";


            query = "SELECT Name FROM " +
              $"{(PublicStatic.IsConnectedToServer ? "[@LIST_TRANSPORTER]" : "CUFD WHERE AliasID = 'LIST_TRANSPORTER'")}";

            DataTable dtTransporter = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);
            cmbTransporter.DataSource = dtTransporter;
            cmbTransporter.DisplayMember = "Name";


            query = "SELECT Name FROM " +
                         $"{(PublicStatic.IsConnectedToServer ? "[@LIST_DRIVER_EXT]" : "CUFD WHERE AliasID = 'LIST_DRIVER_EXT'")}";

            DataTable dtDriver = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);
            cmbDriver.DataSource = dtDriver;
            cmbDriver.DisplayMember = "Name";


            query = "SELECT DISTINCT Code, " +
                        $"{(PublicStatic.IsConnectedToServer ? "U_VehicleType [Name] FROM [@TRUCK_EXTERNAL]" : "Name FROM CUFD WHERE AliasID = 'TRUCK_EXTERNAL'")}";

            DataTable dtPlateNo = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);

            cmbPlateNo.DataSource = dtPlateNo;
            cmbPlateNo.DisplayMember = "Code";
            if (DataHelper.DataTableExist(dtPlateNo))
            {
                cmbVehicleType.Text = DataHelper.ReadDataRow(dtPlateNo, "Name", "", 0);
            }

            query = "SELECT DISTINCT Code,Name " +
                        $"{(PublicStatic.IsConnectedToServer ? "FROM [@TRUCK_TYPE]" : "FROM CUFD WHERE AliasID = 'TRUCK_TYPE'")}";

            DataTable dtVehicleType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                   query);

            cmbVehicleType.DataSource = dtVehicleType;
            cmbVehicleType.DisplayMember = "Code";

        }

        private void frmTravelOrderExternal_Load(object sender, EventArgs e)
        {
            LoadAllComPorts();
            loadCMB();

            //if (PublicStatic.IsConnectedToServer)
            //{
            // string query = "SELECT TOP 1 A.U_WBNo + 1 [U_WBNo] FROM (SELECT ISNULL(CAST(U_WBNo as int),0)  [U_WBNo] FROM ORDR  " +
            //"  UNION ALL SELECT ISNULL(CAST(U_WBNo as int),0)   [U_WBNo] FROM OQUT ) A ORDER BY A.U_WBNo DESC";
            //DataTable dtDocNum = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
            string query = "SELECT TOP 1 A.WBNo + 1 [WBNo] FROM Series A";
            DataTable dtDocNum = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                query);
            cmbOrderNo.Text = DataHelper.ReadDataRow(dtDocNum, "WBNo", "1", 0);
            //}

            //
            //cmbOrderNo.Text = strDocNum;
            clearTexts();
            //IsConnectedToServer();
            //timer1.Enabled = true;

        }

        void IsConnectedToServer()
        {
            cmbOrderNo.Enabled = !PublicStatic.IsConnectedToServer;
            cmbCustomerCode.Enabled = !PublicStatic.IsConnectedToServer;
            btnFind.Visible = PublicStatic.IsConnectedToServer;
        }

        void LoadAllComPorts()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void clearTexts()
        {
            cmbCustomerCode.Text = "";
            cmbCustomerName.Text = "";
            cmbDriver.Text = "";
            cmbPlateNo.Text = "";
            cmbVehicleType.Text = "";
            cmbTypeOfWasteName.Text = "";
            cmbDisposal.Text = "";
            cmbTransporter.Text = "";
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

        void readPort()
        {
            //connect to serial
            using (SerialPort serial = new SerialPort(cmbPorts.Text, BaudRate, Parity.Space, 8, StopBits.One))
            {
                serial.Open();
                string value = ReadLine(serial.ReadExisting());
                txtWeight.Text = value;
                weightCondition();
                serial.Dispose();
                if (string.IsNullOrEmpty(value) == false)
                {
                    timer1.Stop();
                }
            }
            //serialPort = SetSerialPort(cmbPorts.Text);
            //string test = ReadLine(serialPort.ReadExisting());
            //txtWeight.Text = test;
            //weightCondition();
            //serialPort.Dispose();

        }

        string ReadLine(string value)
        {   
            value = Regex.Replace(value, "[^a-zA-Z0-9]", "");
            value = Regex.Replace(value, @"[^\u0009\u000A\u000D\u0020-\u007E]", "");
            value = value.Replace("k", "").Replace(" ", "").Replace(" ", "").Replace(@"\u0002", "").Replace(@"\u0003", "").Trim();
            value = value.Replace("!", "");
            value = value.Replace("C", ""); 
            foreach (var item in value.Split('g'))
            {
                value = item;
                if (string.IsNullOrEmpty(value) == false)
                {
                    break;
                }
            }
            return value;
        }


        void weightCondition()
        {
            if (!string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                if (Convert.ToDecimal(txtWeight.Text) > 0)
                {
                    if (string.IsNullOrWhiteSpace(txtFirstWeight.Text) || Convert.ToDecimal(txtFirstWeight.Text) == 0 || !cmbOrderNo.Enabled)
                    {
                        txtFirstWeight.Text = (decimal.Parse(txtWeight.Text) / 1000).ToString();
                        lblWgtInDate.Text = DateTime.Now.ToShortDateString();
                        lblWgtInTime.Text = DateTime.Now.ToShortTimeString();
                    }
                    else
                    {
                        txtSecondWeight.Text = (decimal.Parse(txtWeight.Text) / 1000).ToString();
                        txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();
                        lblWgtOutDate.Text = DateTime.Now.ToShortDateString();
                        lblWgtOutTime.Text = DateTime.Now.ToShortTimeString();

                        if (Convert.ToDecimal(txtNetWeight.Text) < 0)
                        {
                            MessageBox.Show("Negative Net weight is not allowed. Please supply proper first weight.");
                            txtWeight.Text = "0.00";
                        }
                    }
                }
            }
        }

        SerialPort SetSerialPort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }

            serialPort = new SerialPort(cmbPorts.Text, BaudRate, Parity.None, 8, StopBits.One);
            serialPort.Open();
            return serialPort;
        }

        private void btnWeigh_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWeight.ReadOnly)

                {
                    //serialPort = SetSerialPort();
                    txtWeight.Text = ReadLine(serialPort.ReadExisting());
                    weightCondition();

                    //readPort();
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

        private void cmbCustomerCode_Leave(object sender, EventArgs e)
        {
            //CustustomerCode();
            if (!string.IsNullOrWhiteSpace(cmbCustomerCode.Text))
            {
                string query = $"SELECT CardName FROM  OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                DataTable dtCardName = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                               query);
                cmbCustomerName.Text = DataHelper.ReadDataRow(dtCardName, "CardName", "", 0);
            }

        }
        void CustustomerCode()
        {
            try
            {
                string query = "";
                DataTable dtCustomer;
                //if (PublicStatic.IsConnectedToServer)
                //{
                //    query = $"SELECT z.* FROM " +
                //        $" (select DISTINCT A.U_Name [Name] FROM [@OWT4] A WHERE A.Code = '{cmbCustomerCode.Text}' AND ISNULL(A.U_Name,'') <> ''  " +
                //        $" UNION ALL  " +
                //        $" SELECT DISTINCT A.U_Destination FROM ORDR A where ISNULL(A.U_Destination,'') <> '' AND " +
                //        $" A.CardCode = '{cmbCustomerCode.Text}') Z ORDER BY Z.Name ";
                //}
                //else
                //{
                //query = $"select DISTINCT Code,Name from CUFD where AliasID = 'OWT4' and  isnull(name,'') <> '' AND Code = '{cmbCustomerCode.Text}' order by Name";
                //}


                //query = "SELECT " +
                //                $"{(PublicStatic.IsConnectedToServer ? $"U_Name [Name] FROM [@OWT4] WHERE Code = '{cmbCustomerCode.Text}'" : $"Name FROM CUFD WHERE AliasId = 'OWT4'")}";
                query = $"  Select GroupType FROM OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                DataTable dtGrpType = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                string grpType = "";

                if (dtGrpType.Rows.Count > 0)
                {
                    grpType = DataHelper.ReadDataRow(dtGrpType, "GroupType", "", 0);

                    if (grpType == "Local Govt Unit")
                    {
                        query = $"SELECT U_Municipality FROM OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                        DataTable dtMunicipality = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        if (dtMunicipality.Rows.Count > 0)
                        {
                            //string municipality = DataHelper.ReadDataRow(dtMunicipality, "U_Municipality", "", 0);
                            cmbOriginOfWaste.DataSource = dtMunicipality;
                            cmbOriginOfWaste.DisplayMember = "U_Municipality";
                            //cmbOriginOfWaste.Text = municipality;
                        }
                    }
                    else if (grpType == "Industrial")
                    {
                        cmbOriginOfWaste.DataSource = null;
                        cmbOriginOfWaste.Text = cmbCustomerName.Text;
                    }
                    else if (grpType == "Locator")
                    {
                        query = $"SELECT U_Zone FROM OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                        DataTable dtZone = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        if (dtZone.Rows.Count > 0)
                        {
                            string Zone = DataHelper.ReadDataRow(dtZone, "U_Zone", "", 0);
                            cmbOriginOfWaste.DataSource = dtZone;
                            cmbOriginOfWaste.DisplayMember = "U_Zone";
                        }
                    }
                    else if (grpType == "Haulers & Recyclers")
                    {
                        if (PublicStatic.IsConnectedToServer)
                        {
                            query = $"SELECT z.* FROM " +
                                $" (select DISTINCT A.U_Name [Name] FROM [@OWT4] A WHERE A.Code = '{cmbCustomerCode.Text}' AND ISNULL(A.U_Name,'') <> ''  " +
                                $" UNION ALL  " +
                                $" SELECT DISTINCT A.U_Destination FROM ORDR A where ISNULL(A.U_Destination,'') <> '' AND " +
                                $" A.CardCode = '{cmbCustomerCode.Text}') Z ORDER BY Z.Name ";
                        }
                        else
                        {
                            query = $"select DISTINCT Code,Name from CUFD where AliasID = 'OWT4' and  isnull(name,'') <> '' AND Code = '{cmbCustomerCode.Text}' order by Name";
                        }

                        dtCustomer = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                                      query);
                        if (dtCustomer.Rows.Count > 0)
                        {
                            cmbOriginOfWaste.DataSource = dtCustomer;
                            cmbOriginOfWaste.DisplayMember = "Name";
                        }
                    }
                    else if (grpType == "Others")
                    {
                        cmbOriginOfWaste.DataSource = null;
                        cmbOriginOfWaste.Text = cmbCustomerName.Text;
                    }
                }




                //if (DataHelper.DataTableExist(dtCustomer))
                //{
                //    cmbOriginOfWaste.DataSource = dtCustomer;
                //    cmbOriginOfWaste.DisplayMember = "Name";
                //    cmbOriginOfWaste.Text = "";
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbCustomerCode_TextChanged(object sender, EventArgs e)
        {
            //CustustomerCode();
        }

        private void cmbCustomerName_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(cmbCustomerName.Text))
            {
                string query = $"SELECT CardCode FROM  OCRD WHERE replace(CardName ,'''','')= replace('{cmbCustomerName.Text.Replace("'", "")}','''','')";
                DataTable dtCardName = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                               query);
                if (dtCardName.Rows.Count != 0)
                {
                    cmbCustomerCode.Text = DataHelper.ReadDataRow(dtCardName, "CardCode", "", 0);
                }
                else
                {
                    MessageBox.Show("This customer does not exist in our record. Please select actual customer.");
                    cmbCustomerName.Focus();
                }
                CustustomerCode();

            }
            else
            {
                cmbCustomerCode.Text = "";
                cmbOriginOfWaste.Text = "";
            }

        }

        void updateWBNo()
        {
            string qry = $"Update Series set WBNo = WBNo +1, CreateDate = '{DateTime.Now}'";
            MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qry);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //#################################################################################
                //##################        DECLARATION OF VARIABLES         ######################
                //#################################################################################

                decimal len = 0;
                decimal width = 0;
                decimal height = 0;
                decimal con1 = 0;
                if (!string.IsNullOrWhiteSpace(txtLength.Text)) { len = Convert.ToDecimal(txtLength.Text); }
                if (!string.IsNullOrWhiteSpace(txtWidth.Text)) { width = Convert.ToDecimal(txtWidth.Text); }
                if (!string.IsNullOrWhiteSpace(txtHeight.Text)) { height = Convert.ToDecimal(txtHeight.Text); }
                if (!string.IsNullOrWhiteSpace(txtContainer1.Text)) { con1 = Convert.ToDecimal(txtContainer1.Text); }


                decimal wt1 = Convert.ToDecimal(txtFirstWeight.Text);
                decimal wt2 = Convert.ToDecimal(txtSecondWeight.Text);
                decimal netWt = Convert.ToDecimal(txtNetWeight.Text);

                if (netWt == 0)
                {
                    netWt = 1;
                }

                DateTime wtInDate = Convert.ToDateTime("12/31/1999");
                DateTime wtOutDate = Convert.ToDateTime("12/31/1999");
                if (string.IsNullOrWhiteSpace(lblWgtInDate.Text) == false)
                {
                    wtInDate = Convert.ToDateTime(lblWgtInDate.Text);
                }
                if (string.IsNullOrWhiteSpace(lblWgtOutDate.Text) == false)
                {
                    wtOutDate = Convert.ToDateTime(lblWgtOutDate.Text);
                }


                string wtIntime = null;
                string wtOutTime = null;
                if (string.IsNullOrWhiteSpace(lblWgtInTime.Text) == false)
                {
                    wtIntime = Convert.ToDateTime(lblWgtInTime.Text).ToString(@"hh\:mm\:ss tt");
                }
                if (string.IsNullOrWhiteSpace(lblWgtOutTime.Text) == false)
                {
                    wtOutTime = Convert.ToDateTime(lblWgtOutTime.Text).ToString(@"hh\:mm\:ss tt");
                }
                int wbNo1 = Convert.ToInt32(cmbOrderNo.Text);


                string query = $"SELECT ISNULL(U_Dim1,'') U_Dim1, ISNULL(U_Dim2,'') U_Dim2, ISNULL(U_Dim3,'') U_Dim3 FROM  OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                DataTable dtDim = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                               query);
                string dim1 = DataHelper.ReadDataRow(dtDim, "U_Dim1", "", 0);
                string dim2 = DataHelper.ReadDataRow(dtDim, "U_Dim2", "", 0);
                string dim3 = DataHelper.ReadDataRow(dtDim, "U_Dim3", "", 0);


                string itemCode = "R0001";



                //#################################################################################
                //##################        DATA POSTING                     ######################
                //#################################################################################

                if (Convert.ToDecimal(txtNetWeight.Text) >= 0)
                {

                    if (!string.IsNullOrWhiteSpace(cmbCustomerCode.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(cmbPlateNo.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(txtYardPassNo.Text))
                            {
                                // @@@@@@@@@@@@@  BLOCK IF TYPE OF WASTE DOES NOT EXIST IN THE DATABASE  @@@@@@@@@@@@@ //
                                query = "SELECT Code, Name FROM " +
                               $"{(PublicStatic.IsConnectedToServer ? $"[@WASTE_TYPE] WHERE Name = '{cmbTypeOfWasteName.Text}'" : $"CUFD WHERE AliasID = 'WASTE_TYPE'  and name = '{cmbTypeOfWasteName.Text}'")}";

                                DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                                       query);


                                if (dtWasteType.Rows.Count > 0)
                                {
                                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                    // @@@@@@@@@@@@@  if comboboxOrder No is not enabled, post in LOCAL  @@@@@@@@@@@@@ //  
                                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                    if (!cmbOrderNo.Enabled)
                                    {
                                        if (Convert.ToDecimal(txtFirstWeight.Text) > 0)
                                        {
                                            // @@@@@@@@@@@@@  POSTING SA LOCAL  @@@@@@@@@@@@@ //

                                            string transTypee = "SO";
                                            if (cmbCustomerCode.Text == "TEMP") { transTypee = "SQ"; }

                                            var model = new Document
                                            {
                                                CustomerCode = cmbCustomerCode.Text,
                                                CustomerName = cmbCustomerName.Text,
                                                DRNo = txtDRNo.Text,
                                                Driver = cmbDriver.Text,
                                                PlateNo = cmbPlateNo.Text,
                                                YardPassNo = txtYardPassNo.Text,
                                                TypeOfWaste = cmbTypeOfWasteName.Text,
                                                Transporter = cmbTransporter.Text,
                                                Lengthh = len,
                                                Width = width,
                                                Height = height,
                                                Container = con1,
                                                OriginOfWaste = cmbOriginOfWaste.Text,
                                                Weight1 = wt1,
                                                Weight2 = 0,
                                                NetWt = netWt,
                                                Remarks = txtRemarks1.Text,
                                                WgtInDate = wtInDate.ToString(),
                                                WgtOutDate = wtInDate.ToString(),
                                                WgtInTime = wtIntime,
                                                Operator1 = PublicStatic._frmMain.lblUser.Text,
                                                Operator2 = PublicStatic._frmMain.lblUser.Text,
                                                TransType = transTypee,
                                                VehicleType = cmbVehicleType.Text,
                                                WgtOutTime = wtIntime,
                                                WBNo = wbNo1.ToString(),

                                                Dim1 = dim1,
                                                Dim2 = dim2,
                                                Dim3 = dim3,

                                                BinNo1 = "",
                                                BinNo2 = "",
                                                Cont1 = "",
                                                Cont2 = "",
                                                TORNo = ""
                                            };

                                            Orders orders = new Orders();

                                            ////IF TEMP, ADD TO QUOTATION
                                            //if (PublicStatic.IsConnectedToServer)
                                            //{
                                            //    orders.Post(model, cmbCustomerCode.Text.Equals("TEMP") ? "Quotations" : "Orders");
                                            //    MessageBox.Show(orders.ErrMsg);
                                            //}
                                            //else
                                            //{
                                            using (Context db = new Context())
                                            {
                                                db.Document.Add(model);
                                                db.SaveChanges();
                                                MessageBox.Show("Transaction Added.");
                                                updateWBNo();
                                            }
                                            //}



                                            // @@@@@@@@@@@@@  POSTING OF TRUCK  @@@@@@@@@@@@@ // 
                                            var model2 = new List<C_TRUCK_EXTERNAL>();
                                            model2.Add(new C_TRUCK_EXTERNAL
                                            {
                                                Code = cmbPlateNo.Text,
                                                Name = cmbPlateNo.Text,
                                                U_VehicleType = cmbVehicleType.Text
                                            });
                                            Orders truck = new Orders();




                                            if (PublicStatic.IsConnectedToServer)
                                            {
                                                // @@@@@@@@@@@@@  POSTING OF VEHICLE TYPE   @@@@@@@@@@@@@ //  
                                                if (cmbVehicleType.Enabled)
                                                {
                                                    if (truck.Post(model2, "U_TRUCK_EXTERNAL"))
                                                    {
                                                        string patchString = $@"{{""Code"":""{cmbVehicleType.Text}"", ""Name"":""{cmbVehicleType.Text}""}}";
                                                        string qry2 = "";
                                                        qry2 = $"SELECT TOP 1 'TRUE' FROM [@TRUCK_TYPE] WHERE Code ='{cmbVehicleType.Text}'";
                                                        DataTable dtVehicleType = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                                qry2);
                                                        if (dtVehicleType.Rows.Count == 0)
                                                        {
                                                            if (!truck.PostString(patchString, "U_TRUCK_TYPE"))
                                                            {
                                                                MessageBox.Show("Error with Vehicle Type posting. Please contact administrator.");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error with Truck Posting. Please contact administrator.");
                                                    }
                                                }

                                                // @@@@@@@@@@@@@  POSTING OF DRIVER   @@@@@@@@@@@@@ //   
                                                string qry = $"SELECT TOP 1 'TRUE' FROM [@LIST_DRIVER_EXT] WHERE Name ='{cmbDriver.Text}'";
                                                DataTable dtDriver = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                        qry);
                                                string patchString2 = $@"{{""Name"":""{cmbDriver.Text}""}}";

                                                if (dtDriver.Rows.Count == 0)
                                                {
                                                    if (!truck.PostString(patchString2, "U_LIST_DRIVER_EXT"))
                                                    {
                                                        MessageBox.Show("Error with New External Driver enrollment. Please contact administrator.");
                                                    }
                                                }
                                            }

                                            // @@@@@@@@@@@@@  RELOAD THE FORM @@@@@@@@@@@@@ //   
                                            this.Controls.Clear();
                                            this.InitializeComponent();
                                            frmTravelOrderExternal_Load(sender, e);

                                        }
                                        else
                                        {
                                            MessageBox.Show("No Gross weight detected.");
                                        }
                                    }


                                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                    // @@@@@@@@@@@@@ if comboboxOrder is not enabled, Update Local then Post in SAP  @@@@@@@@@@@@@ //  
                                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                    // 
                                    else
                                    {

                                        var model = (dynamic)null;
                                        var lineModel = (dynamic)null;

                                        if (Convert.ToDecimal(txtSecondWeight.Text) > 0)
                                        {

                                            if (Convert.ToDecimal(txtFirstWeight.Text) != Convert.ToDecimal(txtNetWeight.Text))
                                            {

                                                //string patchString = $@"{{""U_Weight2"":""{wt2}"", ""U_NetWt"":""{netWt}"",""U_WgtOutTime"":""{wtOutTime}"",""U_Operator2"":""{PublicStatic._frmMain.lblUser.Text}"",""DocumentLines"":[{{""LineNum"":""0"",""Quantity"":""{netWt}""}}]}}";

                                                //string qry = "SELECT * FROM ODOC";
                                                //DataTable dtDOC = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                                                //int WBNo = Convert.ToInt32(DataHelper.ReadDataRow(dtDOC, "WBNo", "", 0));


                                                string qryUpdate = $" UPDATE ODOC SET Weight2 = {txtSecondWeight.Text}, NetWt = {txtNetWeight.Text}, " +
                                                                    $" WgtOutDate = '{DateTime.Now.ToShortDateString()}', WgtOutTime = '{lblWgtOutTime.Text}', " +
                                                                    $" Operator2 = '{PublicStatic._frmMain.lblUser.Text}', CustomerCode = '{cmbCustomerCode.Text}', " +
                                                                    $" CustomerName = '{cmbCustomerName.Text}', OriginOfWaste = '{cmbOriginOfWaste.Text}'," +
                                                                    $" Transporter = '{cmbTransporter.Text}', TypeOfWaste = '{cmbTypeOfWasteName.Text}', " +
                                                                    $" DRNo = '{txtDRNo.Text}', Driver = '{cmbDriver.Text}', PlateNo = '{cmbPlateNo.Text}', " +
                                                                    $" VehicleType = '{cmbVehicleType.Text}', YardPassNo = '{txtYardPassNo.Text}', Remarks = '{txtRemarks1.Text}' " +
                                                                    $" WHERE WBNO = '{cmbOrderNo.Text}'";
                                                if (MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qryUpdate) >= 0)
                                                {

                                                    // @@@@@@@@@@@@@  GET OUT DATE  @@@@@@@@@@@@@ //   
                                                    wtOutDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());



                                                    if (PublicStatic.IsConnectedToServer)
                                                    {
                                                        // @@@@@@@@@@@@@  POSTING SA SAP  @@@@@@@@@@@@@ //   

                                                        if (cmbCustomerCode.Text == "TEMP")
                                                        {
                                                            model = new List<OQUT>();
                                                            lineModel = new List<QUT1>();

                                                            lineModel.Add(new QUT1
                                                            {
                                                                ItemCode = itemCode,
                                                                Quantity = netWt,
                                                                UomCode = "TON",
                                                                TaxCode = "OT0",
                                                                U_YPNo = txtYardPassNo.Text

                                                            });
                                                            model.Add(new OQUT
                                                            {
                                                                CardCode = cmbCustomerCode.Text,
                                                                U_Transporter = cmbTransporter.Text,
                                                                U_Destination = cmbOriginOfWaste.Text,
                                                                U_DRNo = txtDRNo.Text,
                                                                U_Driver = cmbDriver.Text,
                                                                U_PlateNo = cmbPlateNo.Text,
                                                                U_YPNo = txtYardPassNo.Text,
                                                                U_WasteType = cmbTypeOfWasteName.Text,
                                                                U_Length = len,
                                                                U_Width = width,
                                                                U_Height = height,
                                                                U_Cont1 = con1.ToString(),
                                                                U_Weight1 = wt1,
                                                                U_Weight2 = wt2,
                                                                U_NetWt = netWt,
                                                                U_Rem1 = txtRemarks1.Text,
                                                                U_Origin = "Walk-in",

                                                                U_WgtInDate = wtInDate.AddDays(1),
                                                                U_WgtOutDate = wtOutDate.AddDays(1),
                                                                U_WgtInTime = Convert.ToDateTime(lblWgtInTime.Text).ToString("HH:mm"),
                                                                U_WgtOutTime = Convert.ToDateTime(lblWgtOutTime.Text).ToString("HH:mm"),

                                                                U_Operator1 = lblOperator1.Text,
                                                                U_Operator2 = PublicStatic._frmMain.lblUser.Text,

                                                                U_WBNo = wbNo1,

                                                                Comments = $"Created by Weighbridge (DCS) | Data Transfer : External Transactions  : {DateTime.Now} | Powered By : DIREC",
                                                                DocDate = DateTime.Now,
                                                                DocDueDate = DateTime.Now,
                                                                TaxDate = DateTime.Now,
                                                                DocumentLines = lineModel
                                                            });
                                                        }
                                                        else
                                                        {
                                                            model = new List<ORDR>();
                                                            lineModel = new List<RDR1>();




                                                            // @@@@@@@@@@@@@  GET PRICE FROM SAP  @@@@@@@@@@@@@ //   
                                                            int UOM = 4;
                                                            string UOMCode = "TON";
                                                            decimal size = 0;
                                                            string limitType = "";
                                                            decimal limit = 0;
                                                            decimal BasePrice = 0;
                                                            decimal ExcessCharge = 0;
                                                            decimal sumQty = 0;
                                                            decimal qty = Math.Round(netWt, 2);
                                                            decimal price = 0;

                                                            string qry = $" select ISNULL(a.U_Price,0) U_Price, ISNULL(a.U_UOM,'') U_UOM, " +
                                                                         $"(select k.UomEntry from ouom k where k.UomCode = a.U_Uom) UomEntry" +
                                                                         $", ISNULL(a.U_Limit,0) U_Limit, " +
                                                                         $" ISNULL(a.U_LimitType,'') U_LimitType, ISNULL(a.U_ExcessCharge,0) U_ExcessCharge, " +
                                                                         $" isnull((select sum(x.Quantity) sumQty FROM ORDR z inner join " +
                                                                         $" RDR1 x ON z.DocEntry = x.DocEntry AND z.CardCode = a.Code and CONCAT(MONTH(z.U_WgtOutDate), " +
                                                                         $" YEAR(z.U_WgtOutDate))  = CONCAT(MONTH(GETDATE()),YEAR(GETDATE()))  ),0) sumQty, " +
                                                                         $" ISNULL(U_Size,1) size " +
                                                                         $" from [@OWT1] a where " +
                                                                         $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                                         $" a.U_Type = '{cmbTypeOfWasteName.Text}' AND " +
                                                                         $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                                         $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}'  " +
                                                                         $"  AND a.U_UOM IN ('CU.M','TON')  ";
                                                            DataTable dtPrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                               qry);

                                                            if (dtPrice.Rows.Count > 0)
                                                            {
                                                                UOM = Convert.ToInt32(DataHelper.ReadDataRow(dtPrice, "UomEntry", "", 0));
                                                                UOMCode = DataHelper.ReadDataRow(dtPrice, "U_UOM", "", 0);
                                                                limitType = DataHelper.ReadDataRow(dtPrice, "U_LimitType", "", 0);
                                                                limit = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Limit", "", 0));
                                                                BasePrice = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));
                                                                ExcessCharge = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_ExcessCharge", "", 0));
                                                                sumQty = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "sumQty", "", 0));
                                                                size = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "size", "", 0));
                                                                price = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));

                                                                if (limitType.ToUpper() == "MONTH")
                                                                {
                                                                    // ############### if per MONTH ang condition ############//
                                                                    if ((sumQty + netWt) >= limit)
                                                                    {
                                                                        // ############### if per MONTH ang condition ############//
                                                                        BasePrice = ExcessCharge;
                                                                        qty = Math.Round(netWt, 2);
                                                                    }
                                                                    else
                                                                    {
                                                                        qty = Math.Round(netWt, 2);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (UOMCode == "CU.M")
                                                                    {
                                                                        qty = con1;
                                                                    }
                                                                    else if (UOMCode == "TON")
                                                                    {
                                                                        qty = netWt;
                                                                    }
                                                                }
                                                            }



                                                            // ############### if NetWt = 0 ############//
                                                            if (Convert.ToDecimal(txtNetWeight.Text) == 0)
                                                            {
                                                                qty = 1;
                                                                itemCode = "R0003";
                                                                BasePrice = 15000;
                                                                UOM = 25;
                                                                netWt = 0;
                                                            }



                                                            //qry = $" select ISNULL(a.U_Price,0) U_Price " +
                                                            //           $" from [@OWT1] a where " +
                                                            //           $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                            //           $" a.U_Type = '{cmbTypeOfWasteName.Text}' AND " +
                                                            //           $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                            //           $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}' AND " +
                                                            //           $" a.U_UOM = 'TON' and ISNULL(U_Limit,0) = 0 ";
                                                            //DataTable dtBasePrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                            //                   qry);
                                                            //decimal TonPrice = 0;
                                                            //if (dtBasePrice.Rows.Count > 0)
                                                            //{
                                                            //    TonPrice = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Price", "", 0));
                                                            //}
                                                            //else
                                                            //{
                                                            //    TonPrice = BasePrice;
                                                            //}

                                                            if (limitType.ToUpper() == "TON")
                                                            {
                                                                limit = (limit / size) * con1;
                                                                // ############### if per TON ang condition ############//
                                                                if (netWt > limit)
                                                                {
                                                                    // ############### if mas malaki ang NetWT vs Limit ############//
                                                                    decimal tempLimit = Math.Round((limit * con1), 2);
                                                                    decimal tempPrice = Math.Round(netWt, 2) - Math.Round(tempLimit, 2);
                                                                    decimal tempPriceWithCharge = (Math.Abs(tempPrice) * ExcessCharge);
                                                                    BasePrice = (BasePrice * Math.Round(con1, 2)) + Math.Round(tempPriceWithCharge, 2);
                                                                    qty = con1;



                                                                    lineModel.Add(new RDR1
                                                                    {
                                                                        LineNum = 0,
                                                                        ItemCode = itemCode,
                                                                        //Quantity = netWt,
                                                                        Quantity = qty,
                                                                        //UomCode = "TON",
                                                                        UoMEntry = UOM,
                                                                        UnitPrice = Math.Round(price, 2),

                                                                        TaxCode = "OT0",
                                                                        U_YPNo = txtYardPassNo.Text,
                                                                        CostingCode = dim1,
                                                                        CostingCode2 = dim2,
                                                                        CostingCode3 = dim3
                                                                    });


                                                                    lineModel.Add(new RDR1
                                                                    {
                                                                        LineNum = 1,
                                                                        ItemCode = itemCode,
                                                                        //Quantity = netWt,
                                                                        Quantity = netWt - limit,
                                                                        //UomCode = "TON",
                                                                        UoMEntry = 4,
                                                                        UnitPrice = Math.Round(ExcessCharge, 2),

                                                                        TaxCode = "OT0",
                                                                        U_YPNo = txtYardPassNo.Text,
                                                                        CostingCode = dim1,
                                                                        CostingCode2 = dim2,
                                                                        CostingCode3 = dim3
                                                                    });

                                                                }
                                                                else
                                                                {
                                                                    if (con1 == 0)
                                                                    {
                                                                        qty = netWt;
                                                                        UOM = 4;
                                                                    }

                                                                    lineModel.Add(new RDR1
                                                                    {
                                                                        LineNum = 0,
                                                                        ItemCode = itemCode,
                                                                        //Quantity = netWt,
                                                                        Quantity = qty,
                                                                        //UomCode = "TON",
                                                                        UoMEntry = UOM,
                                                                        UnitPrice = Math.Round(BasePrice, 2),

                                                                        TaxCode = "OT0",
                                                                        U_YPNo = txtYardPassNo.Text,
                                                                        CostingCode = dim1,
                                                                        CostingCode2 = dim2,
                                                                        CostingCode3 = dim3
                                                                    });

                                                                }
                                                            }
                                                            else
                                                            {

                                                                if (con1 == 0)
                                                                {
                                                                    qty = netWt;
                                                                    UOM = 4;
                                                                }

                                                                lineModel.Add(new RDR1
                                                                {
                                                                    LineNum = 0,
                                                                    ItemCode = itemCode,
                                                                    //Quantity = netWt,
                                                                    Quantity = qty,
                                                                    //UomCode = "TON",
                                                                    UoMEntry = UOM,
                                                                    UnitPrice = Math.Round(BasePrice, 2),

                                                                    TaxCode = "OT0",
                                                                    U_YPNo = txtYardPassNo.Text,
                                                                    CostingCode = dim1,
                                                                    CostingCode2 = dim2,
                                                                    CostingCode3 = dim3
                                                                });
                                                            }


                                                            model.Add(new ORDR
                                                            {
                                                                CardCode = cmbCustomerCode.Text,
                                                                U_Transporter = cmbTransporter.Text,
                                                                U_Destination = cmbOriginOfWaste.Text,
                                                                U_DRNo = txtDRNo.Text,
                                                                U_Driver = cmbDriver.Text,
                                                                U_PlateNo = cmbPlateNo.Text,
                                                                U_YPNo = txtYardPassNo.Text,
                                                                U_WasteType = cmbTypeOfWasteName.Text,
                                                                U_Length = len,
                                                                U_Width = width,
                                                                U_Height = height,
                                                                U_Cont1 = con1.ToString(),
                                                                U_Weight1 = wt1,
                                                                U_Weight2 = wt2,
                                                                U_NetWt = netWt,
                                                                U_Rem1 = txtRemarks1.Text,
                                                                U_Origin = "Walk-in",

                                                                U_WgtInDate = wtInDate.AddDays(1),
                                                                U_WgtOutDate = wtOutDate.AddDays(1),
                                                                U_WgtInTime = Convert.ToDateTime(lblWgtInTime.Text).ToString("HH:mm"),

                                                                U_Operator1 = lblOperator1.Text,
                                                                U_Operator2 = PublicStatic._frmMain.lblUser.Text,
                                                                U_WgtOutTime = Convert.ToDateTime(lblWgtOutTime.Text).ToString("HH:mm"),

                                                                U_WBNo = wbNo1,
                                                                Comments = $"Created by Weighbridge (DCS) | Data Transfer : External Transactions  : {DateTime.Now} | Powered By : DIREC",
                                                                DocDate = DateTime.Now,
                                                                DocDueDate = DateTime.Now,
                                                                TaxDate = DateTime.Now,
                                                                DocumentLines = lineModel
                                                            });

                                                        }

                                                        Orders orders = new Orders();

                                                        if (PublicStatic.IsConnectedToServer)
                                                        {
                                                            bool ret = orders.Post(model, cmbCustomerCode.Text.Equals("TEMP") ? "Quotations" : "Orders");
                                                            if (ret)
                                                            {
                                                                MessageBox.Show("Transaction uploaded to SAP.");
                                                                loadReport();
                                                                //DeleteLocalData();

                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Updated in Local but cannot upload to SAP due to error connection. Please contact administrator.");
                                                                loadReport(ret);
                                                                //Close();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Updated in Local but cannot upload to SAP due to error connection. Please contact administrator.");
                                                            loadReport();
                                                            //Close();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Updating second weight did not proceed. Please contact administrator.");
                                                        loadReport();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Updating second weight did not proceed. Please contact administrator.");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Updating second weight did not proceed. Please contact administrator.");
                                            }

                                        }//TILL HERE
                                        else
                                        {
                                            MessageBox.Show("No Tare weight detected.");
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Invalid Waste Type. Please provide proper Waste Type.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No Yard Pass No. supplied. Please provide proper Yard Pass No..");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Plate No. Please provide Plate Number.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Customer supplied. Please provide customer.");
                    }
                }
                else
                {
                    MessageBox.Show("Negative net weight is not allowed.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadReport(bool ret = true)
        {
            if (Convert.ToDecimal(txtSecondWeight.Text) > 0)
            {
                frmCrystalReport frmCrystalReport = new frmCrystalReport(PublicStatic.IsConnectedToServer && ret ? "connected" : "external", cmbTransType.Text);
                frmCrystalReport.oDocKey = cmbOrderNo.Text;
                Close();
                frmCrystalReport.ShowDialog();
                //DeleteLocalData();
            }
        }


        void DeleteLocalData()
        {
            if (PublicStatic.IsConnectedToServer)
            {
                string qryDel = $"DELETE FROM ODOC WHERE WBNo = {cmbOrderNo.Text} ";
                if (MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qryDel) >= 0)
                {
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtNetWeight.Text) >= 0)
            {
                loadReport();
            }
            else
            {
                MessageBox.Show("Negative Net weight is not allowed. Please supply proper first weight.");
            }
        }

        void computeVolume()
        {
            decimal length = 0;
            decimal width = 0;
            decimal height = 0;

            if (!string.IsNullOrWhiteSpace(txtLength.Text)) { length = Convert.ToDecimal(txtLength.Text); }
            if (!string.IsNullOrWhiteSpace(txtWidth.Text)) { width = Convert.ToDecimal(txtWidth.Text); }
            if (!string.IsNullOrWhiteSpace(txtHeight.Text)) { height = Convert.ToDecimal(txtHeight.Text); }

            txtContainer1.Text = (length * width * height).ToString();
        }

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            computeVolume();
        }

        private void cmbPlateNo_Leave(object sender, EventArgs e)
        {


            if (DataHelper.DataTableExist(PlateNo()))
            {
                cmbVehicleType.Enabled = false;

            }
            else
            {
                cmbVehicleType.Enabled = true;
                cmbVehicleType.Text = "";
                cmbVehicleType.Focus();
            }
        }

        DataTable PlateNo()
        {
            string query = "SELECT Code, " +
                   $"{(PublicStatic.IsConnectedToServer ? $"U_VehicleType [Name] FROM [@TRUCK_EXTERNAL] WHERE " : $"Name FROM CUFD WHERE AliasID = 'TRUCK_EXTERNAL' AND ")}Code = '{cmbPlateNo.Text}'";

            return MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                           query);
        }

        private void cmbPlateNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbVehicleType.Enabled = false;

            cmbVehicleType.Text = DataHelper.ReadDataRow(PlateNo(), "Name", "", 0);
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearch frmSearch = new frmSearch("external");
                frmSearch.ShowDialog();

                if (frmSearch.TransNo != null)
                {
                    //enable all necessary fields after searching
                    cmbOrderNo.Enabled = true;
                    cmbDocNum.Enabled = true;

                    //populate cmb
                    //string qry = $" SELECT A.* FROM (SELECT U_WBNo [U_WBNo], DocNum, CardName [Customer Name], U_DRNo [DR No.], 'SO' [TransType] " +
                    //             $" FROM ORDR WHERE DocStatus = 'O' AND ISNULL(U_WBNo,0) <> 0 " +
                    //             $" AND ISNULL(U_Weight2,0) = 0 and ISNULL(U_Weight1,0) <> 0 " +
                    //             $" UNION ALL " +
                    //             $" SELECT U_WBNo [U_WBNo], DocNum, CardName [Customer Name], U_DRNo [DR No.], 'SQ' [TransType] " +
                    //             $" FROM OQUT WHERE DocStatus = 'O' AND ISNULL(U_WBNo,0) <> 0 AND ISNULL(U_Weight2,0) = 0 and ISNULL(U_Weight1,0) <> 0 " +
                    //             $" ) A ORDER BY A.U_WBNo";
                    //DataTable dtCMB = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                    //                        qry);
                    string qry = $" SELECT WBNo [U_WBNo] , CustomerName [Customer Name], DRNo [DR No.], TransType " +
                                    $" FROM ODOC WHERE ISNULL(Weight2,0) = 0  ORDER BY CAST(WBNo AS INT) DESC ";
                    DataTable dtCMB = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                            qry);
                    cmbOrderNo.DisplayMember = "U_WBNo";
                    cmbOrderNo.DataSource = dtCMB;
                    cmbTransType.DisplayMember = "TransType";
                    cmbTransType.DataSource = dtCMB;
                    //cmbDocNum.DisplayMember = "DocNum";
                    //cmbDocNum.DataSource = dtCMB;

                    fromSearch(frmSearch.TransNo);
                    //txtSecondWeight.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fromSearch(string DocNum)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(cmbOrderNo.Text) == false)
                {
                    cmbOrderNo.Text = DocNum;
                    //if (PublicStatic.IsConnectedToServer)
                    //{
                    loadData();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtOrderNo_Leave(object sender, EventArgs e)
        {

            txtWeight.Text = "0.00";
            fromSearch(cmbOrderNo.Text);
        }


        void loadData()
        {

            //populate cmb
            //loadCMB();

            //string qry = $"SELECT A.CardCode, A.CardName, A.U_DRNo, A.U_Driver, A.U_PlateNo, " +
            //            $"(SELECT U_VehicleType FROM [@TRUCK_EXTERNAL] WHERE Code = A.U_PlateNo) U_VehicleType," +
            //            $" A.U_YPNo, A.U_WasteType, A.U_Disposal, A.U_Transporter, A.U_Origin, A.U_Length," +
            //            $" A.U_Width, A.U_Height, A.U_Weight1, A.U_Cont1, A.U_Destination, " +
            //            $@"(CASE WHEN LEN(a.U_WgtInTime) = 3 
            //                                THEN CONCAT(LEFT(a.U_WgtInTime,1),':',RIGHT(a.U_WgtInTime, 2),' AM')
            //                                WHEN a.U_WgtInTime > 1159 AND LEN(a.U_WgtInTime) = 4 and LEFT(a.U_WgtInTime,2) <> 12
            //                                THEN CONCAT(LEFT(a.U_WgtInTime,2)-12,':',RIGHT(a.U_WgtInTime, 2),' PM')
            //                                WHEN a.U_WgtInTime > 1159 AND LEN(a.U_WgtInTime) = 4 and LEFT(a.U_WgtInTime,2) = 12
            //                                THEN CONCAT(LEFT(a.U_WgtInTime,2),':',RIGHT(a.U_WgtInTime, 2),' PM')
            //                                WHEN a.U_WgtInTime < 1159 AND LEN(a.U_WgtInTime) = 4
            //                                THEN CONCAT(LEFT(a.U_WgtInTime,2),':',RIGHT(a.U_WgtInTime, 2),' AM')
            //                                WHEN LEN(a.U_WgtInTime) = 2
            //                                THEN CONCAT(12,':',RIGHT(a.U_WgtInTime, 2),' AM')
            //                                END) U_WgtInTime, " +
            //            $@" (CASE WHEN LEN(a.U_WgtOutTime) = 3 
            //                                THEN CONCAT(LEFT(a.U_WgtOutTime,1),':',RIGHT(a.U_WgtOutTime, 2),' AM')
            //                                WHEN a.U_WgtOutTime > 1159 AND LEN(a.U_WgtOutTime) = 4 and LEFT(a.U_WgtOutTime,2) <> 12
            //                                THEN CONCAT(LEFT(a.U_WgtOutTime,2)-12,':',RIGHT(a.U_WgtOutTime, 2),' PM')
            //                                WHEN a.U_WgtOutTime > 1159 AND LEN(a.U_WgtOutTime) = 4 and LEFT(a.U_WgtOutTime,2) = 12
            //                                THEN CONCAT(LEFT(a.U_WgtOutTime,2),':',RIGHT(a.U_WgtOutTime, 2),' PM')
            //                                WHEN a.U_WgtOutTime < 1159 AND LEN(a.U_WgtOutTime) = 4 
            //                                THEN CONCAT(LEFT(a.U_WgtOutTime,2),':',RIGHT(a.U_WgtOutTime, 2),' AM')
            //                                WHEN LEN(a.U_WgtOutTime) = 2
            //                                THEN CONCAT(12,':',RIGHT(a.U_WgtOutTime, 2),' AM')
            //                                END) U_WgtOutTime, A.U_WgtOutDate, A.U_WgtInDate, U_Rem1";

            string qry = $"SELECT * FROM ODOC WHERE WBNo = {cmbOrderNo.Text} ";

            //string qry2 = "";
            //if (cmbTransType.Text == "SQ")
            //{
            //    qry2 = $" FROM OQUT A WHERE A.U_WBNo = '{cmbOrderNo.Text}'";
            //}
            //else
            //{
            //    qry2 = $" FROM ORDR A WHERE A.U_WBNo = '{cmbOrderNo.Text}'";
            //}
            //qry = qry + qry2;
            DataTable dtLoad = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                    qry);

            if (dtLoad.Rows.Count > 0)
            {
                //cmbCustomerCode.Text = DataHelper.ReadDataRow(dtLoad, "CardCode", "", 0);
                //cmbCustomerName.Text = DataHelper.ReadDataRow(dtLoad, "CardName", "", 0);
                //cmbOriginOfWaste.Text = DataHelper.ReadDataRow(dtLoad, "U_Destination", "", 0);
                //txtDRNo.Text = DataHelper.ReadDataRow(dtLoad, "U_DRNo", "", 0);
                //cmbDriver.Text = DataHelper.ReadDataRow(dtLoad, "U_Driver", "", 0);
                //cmbPlateNo.Text = DataHelper.ReadDataRow(dtLoad, "U_PlateNo", "", 0);
                //cmbVehicleType.Text = DataHelper.ReadDataRow(dtLoad, "U_VehicleType", "", 0);
                //txtYardPassNo.Text = DataHelper.ReadDataRow(dtLoad, "U_YPNo", "", 0);
                //cmbTypeOfWasteName.Text = DataHelper.ReadDataRow(dtLoad, "U_WasteType", "", 0);
                //cmbDisposal.Text = DataHelper.ReadDataRow(dtLoad, "U_Disposal", "", 0);
                //cmbTransporter.Text = DataHelper.ReadDataRow(dtLoad, "U_Transporter", "", 0);
                //txtLength.Text = DataHelper.ReadDataRow(dtLoad, "U_Length", "", 0);
                //txtWidth.Text = DataHelper.ReadDataRow(dtLoad, "U_Width", "", 0);
                //txtHeight.Text = DataHelper.ReadDataRow(dtLoad, "U_Height", "", 0);
                //txtFirstWeight.Text = DataHelper.ReadDataRow(dtLoad, "U_Weight1", "", 0);
                //txtContainer1.Text = DataHelper.ReadDataRow(dtLoad, "U_Cont1", "", 0);
                //lblWgtInTime.Text = DataHelper.ReadDataRow(dtLoad, "U_WgtInTime", "", 0);
                //txtRemarks1.Text = DataHelper.ReadDataRow(dtLoad, "U_Rem1", "", 0);

                cmbCustomerCode.Text = DataHelper.ReadDataRow(dtLoad, "CustomerCode", "", 0);
                cmbCustomerName.Text = DataHelper.ReadDataRow(dtLoad, "CustomerName", "", 0);
                cmbOriginOfWaste.Text = DataHelper.ReadDataRow(dtLoad, "OriginOfWaste", "", 0);
                txtDRNo.Text = DataHelper.ReadDataRow(dtLoad, "DRNo", "", 0);
                cmbDriver.Text = DataHelper.ReadDataRow(dtLoad, "Driver", "", 0);
                cmbPlateNo.Text = DataHelper.ReadDataRow(dtLoad, "PlateNo", "", 0);
                cmbVehicleType.Text = DataHelper.ReadDataRow(dtLoad, "VehicleType", "", 0);
                txtYardPassNo.Text = DataHelper.ReadDataRow(dtLoad, "YardPassNo", "", 0);
                cmbTypeOfWasteName.Text = DataHelper.ReadDataRow(dtLoad, "TypeOfWaste", "", 0);
                cmbTransporter.Text = DataHelper.ReadDataRow(dtLoad, "Transporter", "", 0);
                txtLength.Text = DataHelper.ReadDataRow(dtLoad, "Lengthh", "", 0);
                txtWidth.Text = DataHelper.ReadDataRow(dtLoad, "Width", "", 0);
                txtHeight.Text = DataHelper.ReadDataRow(dtLoad, "Height", "", 0);
                txtFirstWeight.Text = DataHelper.ReadDataRow(dtLoad, "Weight1", "", 0);
                txtSecondWeight.Text = DataHelper.ReadDataRow(dtLoad, "Weight2", "", 0);
                txtNetWeight.Text = DataHelper.ReadDataRow(dtLoad, "NetWt", "", 0);
                txtContainer1.Text = DataHelper.ReadDataRow(dtLoad, "Container", "", 0);
                lblWgtInTime.Text = DataHelper.ReadDataRow(dtLoad, "WgtInTime", "", 0);
                lblWgtOutTime.Text = DataHelper.ReadDataRow(dtLoad, "WgtOutTime", "", 0);
                txtRemarks1.Text = DataHelper.ReadDataRow(dtLoad, "Remarks", "", 0);
                cmbTransType.Text = DataHelper.ReadDataRow(dtLoad, "TransType", "", 0);
                lblOperator1.Text = DataHelper.ReadDataRow(dtLoad, "Operator1", "", 0);
                //string patchString = $@"{{""U_Weight2"":""{wt2}"", ""U_NetWt"":""{netWt}"",""U_WgtOutTime"":""{wtOutTime}"",""U_Operator2"":""{PublicStatic._frmMain.lblUser.Text}"",""DocumentLines"":[{{""LineNum"":""0"",""Quantity"":""{netWt}""}}]}}";

                if (string.IsNullOrWhiteSpace(DataHelper.ReadDataRow(dtLoad, "WgtInDate", "", 0)) == false)
                {
                    string tet = Convert.ToDateTime(DataHelper.ReadDataRow(dtLoad, "WgtInDate", "", 0)).ToShortDateString();
                    if ((Convert.ToDateTime(DataHelper.ReadDataRow(dtLoad, "WgtInDate", "", 0)) > DateTime.Parse("01/01/2000")))
                    {
                        lblWgtInDate.Text = Convert.ToDateTime(DataHelper.ReadDataRow(dtLoad, "WgtInDate", "", 0)).ToShortDateString();
                    }
                    else lblWgtInDate.Text = "";

                }
                else lblWgtInDate.Text = "";


                if (string.IsNullOrWhiteSpace(DataHelper.ReadDataRow(dtLoad, "WgtOutDate", "", 0)) == false)
                {
                    if ((Convert.ToDateTime(DataHelper.ReadDataRow(dtLoad, "WgtOutDate", "", 0)) > DateTime.Parse("01/01/2000")))
                    {
                        lblWgtOutDate.Text = Convert.ToDateTime(DataHelper.ReadDataRow(dtLoad, "WgtOutDate", "", 0)).ToShortDateString();
                    }
                    else lblWgtOutDate.Text = "";
                }
                else { lblWgtOutDate.Text = ""; }

            }
        }

        private void cmbWBNo_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cmbOrderNo_Leave(object sender, EventArgs e)
        {
            txtWeight.Text = "0.00";
            fromSearch(cmbOrderNo.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtLength_Leave(object sender, EventArgs e)
        {
            computeVolume();
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            computeVolume();
        }

        private void frmTravelOrderExternal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            readPort();
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void txtRemarks1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbTypeOfWasteName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbTypeOfWasteName.Text))
            {

                string query = "SELECT Code, Name FROM " +
                $"{(PublicStatic.IsConnectedToServer ? $"[@WASTE_TYPE] WHERE Name = '{cmbTypeOfWasteName.Text}'" : $"CUFD WHERE AliasID = 'WASTE_TYPE'  and name = '{cmbTypeOfWasteName.Text}'")}";

                DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                       query);

                if (dtWasteType.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Waste Type. Please provide proper Waste Type.");
                    cmbTypeOfWasteName.Focus();
                }
            }
        }
    }
}