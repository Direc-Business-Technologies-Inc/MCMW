using DirecLayer;
using DomainLayer.CONTEXT;
using DomainLayer.SAO_DATABASE;
using DomainLayer.SAP_DATABASE;
using MCWM.Helper;
using MCWM.Views.Main;
using MCWM.Views.Tools;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MCWM.Views.TravelOrder
{
    //public partial class frmTravelOrders : Form, IfrmTravelOrders
    public partial class frmTravelOrders : Form
    {
        SerialPort serialPort { get; set; }
        private const int BaudRate = 9600;
        private string DocNum;
        public frmTravelOrders()
        {
            InitializeComponent();
        }


        #region Event Registration
        //public event EventHandler _Main_newTravelOrderToolStripMenuItem_Click;
        #endregion

        #region Variables

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



        #endregion

        public void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (PublicStatic.IsConnectedToServer)
                {
                    frmSearch frmSearch = new frmSearch("internal");
                    frmSearch.ShowDialog();

                    if (frmSearch.TransNo != null)
                    {
                        fromSearch(frmSearch.TransNo);
                    }
                }
                else
                {
                    MessageBox.Show("Not connceted to SAP.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void frmTravelOrders_Load(object sender, EventArgs e)
        {

            LoadAllComPorts();
            loadCMB();
            timer1.Enabled = true;
            txtSecondWeight.Text = "0.00";
            getWBNo();
            loadCustomers();
            loadPlateNo();

            if (!PublicStatic.IsConnectedToServer)
            {
                enableControls();
            }

        }

        void loadCustomers()
        {
            if (!PublicStatic.IsConnectedToServer)
            {
                cmbCustomerCode.Enabled = true;
                cmbCustomerName.Enabled = true;
                txtDriver.Enabled = true;

                string query = "SELECT CardCode, CardName FROM OCRD WHERE frozenFor = 'N' " +
                $"{(PublicStatic.IsConnectedToServer ? "AND CardType = 'C'" : "")}";
                DataTable dtCustomer = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                      query);
                cmbCustomerCode.DataSource = dtCustomer;
                cmbCustomerCode.DisplayMember = "CardCode";
                cmbCustomerName.DataSource = dtCustomer;
                cmbCustomerName.DisplayMember = "CardName";

            }
            cmbCustomerCode.Text = "";
            cmbCustomerName.Text = "";
        }

        void loadPlateNo()
        {
            if (!PublicStatic.IsConnectedToServer)
            {
                cmbPlateNo.Enabled = true;

                string query = "select PlateNo from TRUCK_FLEET";

                DataTable dtPlateNo = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                       query);

                cmbPlateNo.DataSource = dtPlateNo;
                cmbPlateNo.DisplayMember = "PlateNo";
            }
            cmbPlateNo.Text = "";
        }


        void getWBNo()
        {
            //WEIGHBRIDGE NO.
            //string qry = "SELECT TOP 1 ISNULL(CAST(U_WBNo as int),0) + 1  [U_WBNo] FROM ORDR ORDER BY CAST(U_WBNo as int) DESC";
            //string qry = "SELECT TOP 1 A.U_WBNo FROM (SELECT ISNULL(CAST(U_WBNo as int),0) + 1  [U_WBNo] FROM ORDR  UNION ALL SELECT ISNULL(CAST(U_WBNo as int),0) + 1  [U_WBNo] FROM OQUT ) A ORDER BY A.U_WBNo DESC";
            //string qry = "SELECT TOP 1 A.U_WBNo + 1 [U_WBNo] FROM (SELECT ISNULL(CAST(U_WBNo as int),0)  [U_WBNo] FROM ORDR  " +
            //             " UNION ALL SELECT ISNULL(CAST(U_WBNo as int),0)   [U_WBNo] FROM OQUT ) A ORDER BY A.U_WBNo DESC";
            string query = "SELECT TOP 1 A.WBNo + 1 [WBNo] FROM Series A";
            DataTable dtDocNum = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                    query);

            if (dtDocNum.Rows.Count == 0)
            {
                txtWBNo.Text = "1";
            }
            else
            {
                txtWBNo.Text = DataHelper.ReadDataRow(dtDocNum, "WBNo", "", 0);
            }
        }


        void loadCombination()
        {
            try
            {
                string qry = $" SELECT DISTINCT U_Combination FROM [@TRUCK_WEIGHT] WHERE U_PlateNo = '{cmbPlateNo.Text}' ";
                DataTable dtCombination = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                               qry);

                if (dtCombination.Rows.Count > 0)
                {
                    cmbCombination.DisplayMember = "U_Combination";
                    cmbCombination.DataSource = dtCombination;
                    cmbCombination.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadCMB()
        {
            try
            {
                if (PublicStatic.IsConnectedToServer)
                {
                    string qry = "SELECT A.U_TORNo FROM ORDR A WHERE DocStatus = 'O' AND ISNULL(A.U_WBNo,'') = '' AND ISNULL(U_TorNo,'') <> '' ORDER BY DocEntry DESC";
                    DataTable dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   qry);
                    cmbOrderNo.DisplayMember = "U_TORNo";
                    cmbOrderNo.DataSource = dtAllOpenOrders;

                    //qry = $" SELECT DISTINCT U_BinNo FROM OITM WHERE ItemType = 'F' AND ISNULL(U_BinNo,'') <> ''";
                    //DataTable dtBin1 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                    //                               qry);
                    //DataTable dtBin2 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                    //                                qry);

                    cmbOrderNo.Text = "";
                }


                string query = "SELECT Code, Name FROM " +
                 $"{(PublicStatic.IsConnectedToServer ? "[@WASTE_TYPE]" : "CUFD WHERE AliasID = 'WASTE_TYPE'")}";

                DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                       query);

                cmbWasteTypeName.DataSource = dtWasteType;
                cmbWasteTypeName.DisplayMember = "Name";
                cmbWasteTypeCode.DataSource = dtWasteType;
                cmbWasteTypeCode.DisplayMember = "Code";
                cmbWasteTypeName.Text = "";

                string qry2 = $" SELECT DISTINCT BinNo_ , BinSize FROM BIN WHERE ISNULL(BinSize,'') <> ''";
                DataTable dtBin1 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                               qry2);
                DataTable dtBin2 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                qry2);

                if (!string.IsNullOrWhiteSpace(cmbOrderNo.Text))
                {
                    cmbBinNo1.DataSource = dtBin1;
                    cmbBinNo1.Text = "";
                    cmbBinNo2.DataSource = dtBin2;
                    cmbBinNo2.Text = "";
                    lblCon1.Text = "";
                    lblCon2.Text = "";
                }
                else
                {
                    cmbBinNo1.DataSource = dtBin1;
                    cmbBinNo2.DataSource = dtBin2;
                    if (!PublicStatic.IsConnectedToServer)
                    {
                        cmbBinNo1.DisplayMember = "BinNo_";
                        cmbBinNo2.DisplayMember = "BinNo_";

                    }
                    cmbBinNo1.Text = "";
                    cmbBinNo2.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

        void CloseForm([Optional]Form form)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm == form)
                { frm.Dispose(); return; }
                else { frm.Dispose(); }
            }
        }


        void enableControls()
        {
            PanelHeader.Enabled = true;
            PanelDetails.Enabled = true;
            btnPrint.Enabled = true;
            btnSave.Enabled = true;
        }

        public void fromSearch(string TORNo)
        {
            try
            {
                cmbOrderNo.Text = TORNo;
                if (!string.IsNullOrWhiteSpace(cmbOrderNo.Text))
                {
                    string qryGetOrder = "SELECT A.DocNum, A.CardCode, A.CardName, isnull(A.U_Weight1,0) U_Weight1, ISNULL(A.U_Weight2,0) U_Weight2, " +
                                        " A.U_NetWt, A.U_DRNo, ISNULL((SELECT x.Name FROM [@LIST_DRIVER_EXT] x WHERE CAST(x.Code AS nvarchar(50)) = A.U_Driver),A.U_Driver) U_Driver, A.U_BinNo1, A.U_BinNo2, A.U_Destination,  " +
                                        " A.U_PlateNo, A.U_Cont1, A.U_Cont2, A.U_SWMFNo, A.U_YPNo, A.U_WasteType, " +
                                        " A.U_Disposal, " +
                                        $@" (CASE WHEN LEN(a.U_TripStart) = 3 
                                            THEN CONCAT(LEFT(a.U_TripStart,1),':',RIGHT(a.U_TripStart, 2),' AM')
                                            WHEN a.U_TripStart > 1159 AND LEN(a.U_TripStart) = 4 and LEFT(a.U_TripStart,2) <> 12
                                            THEN CONCAT(LEFT(a.U_TripStart,2)-12,':',RIGHT(a.U_TripStart, 2),' PM')
                                            WHEN a.U_TripStart > 1159 AND LEN(a.U_TripStart) = 4 and LEFT(a.U_TripStart,2) = 12
                                            THEN CONCAT(LEFT(a.U_TripStart,2),':',RIGHT(a.U_TripStart, 2),' PM')
                                            WHEN a.U_TripStart < 1159 AND LEN(a.U_TripStart) = 4
                                            THEN CONCAT(LEFT(a.U_TripStart,2),':',RIGHT(a.U_TripStart, 2),' AM')
                                            WHEN LEN(a.U_TripStart) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_TripStart, 2),' AM')
                                            END) U_TripStart" +
                                        ", A.U_Origin, A.U_Rem1, A.U_Rem2, " +
                                        $@"(CASE WHEN LEN(a.U_GateIn) = 3 
                                            THEN CONCAT(LEFT(a.U_GateIn,1),':',RIGHT(a.U_GateIn, 2),' AM')
                                            WHEN a.U_GateIn > 1159 AND LEN(a.U_GateIn) = 4 and LEFT(a.U_GateIn,2) <> 12
                                            THEN CONCAT(LEFT(a.U_GateIn,2)-12,':',RIGHT(a.U_GateIn, 2),' PM')
                                            WHEN a.U_GateIn > 1159 AND LEN(a.U_GateIn) = 4 and LEFT(a.U_GateIn,2) = 12
                                            THEN CONCAT(LEFT(a.U_GateIn,2),':',RIGHT(a.U_GateIn, 2),' PM')
                                            WHEN a.U_GateIn < 1159 AND LEN(a.U_GateIn) = 4
                                            THEN CONCAT(LEFT(a.U_GateIn,2),':',RIGHT(a.U_GateIn, 2),' AM')
                                            WHEN LEN(a.U_GateIn) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_GateIn, 2),' AM')
                                            END) U_GateIn, " +
                                        $@" (CASE WHEN LEN(a.U_GateOut) = 3 
                                            THEN CONCAT(LEFT(a.U_GateOut,1),':',RIGHT(a.U_GateOut, 2),' AM')
                                            WHEN a.U_GateOut > 1159 AND LEN(a.U_GateOut) = 4 and LEFT(a.U_GateOut,2) <> 12
                                            THEN CONCAT(LEFT(a.U_GateOut,2)-12,':',RIGHT(a.U_GateOut, 2),' PM')
                                            WHEN a.U_GateOut > 1159 AND LEN(a.U_GateOut) = 4 and LEFT(a.U_GateOut,2) = 12
                                            THEN CONCAT(LEFT(a.U_GateOut,2),':',RIGHT(a.U_GateOut, 2),' PM')
                                            WHEN a.U_GateOut < 1159 AND LEN(a.U_GateOut) = 4
                                            THEN CONCAT(LEFT(a.U_GateOut,2),':',RIGHT(a.U_GateOut, 2),' AM')
                                            WHEN LEN(a.U_GateOut) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_GateOut, 2),' AM')
                                            END) U_GateOut, " +
                                        $@"(CASE WHEN LEN(a.U_TripEnd) = 3 
                                            THEN CONCAT(LEFT(a.U_TripEnd,1),':',RIGHT(a.U_TripEnd, 2),' AM')
                                            WHEN a.U_TripEnd > 1159 AND LEN(a.U_TripEnd) = 4 and LEFT(a.U_TripEnd,2) <> 12
                                            THEN CONCAT(LEFT(a.U_TripEnd,2)-12,':',RIGHT(a.U_TripEnd, 2),' PM')
                                            WHEN a.U_TripEnd > 1159 AND LEN(a.U_TripEnd) = 4 and LEFT(a.U_TripEnd,2) = 12
                                            THEN CONCAT(LEFT(a.U_TripEnd,2),':',RIGHT(a.U_TripEnd, 2),' PM')
                                            WHEN a.U_TripEnd < 1159 AND LEN(a.U_TripEnd) = 4
                                            THEN CONCAT(LEFT(a.U_TripEnd,2),':',RIGHT(a.U_TripEnd, 2),' AM')
                                            WHEN LEN(a.U_TripEnd) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_TripEnd, 2),' AM')
                                            END) U_TripEnd" +
                                        " , A.U_Source, ISNULL((SELECT x.Name FROM [@LIST_DISPATCHER] x WHERE CAST(x.Code AS nvarchar(50)) = A.U_Dispatcher),A.U_Dispatcher) U_Dispatcher, " +
                                        " A.U_LoadTime, A.U_Fuel, (SELECT x.U_Price FROM OCRD x WHERE x.CardCode = A.CardCode) Price," +
                                        " A.U_WgtOutDate, A.U_WgtInDate," +
                                        $@" (CASE WHEN LEN(a.U_WgtInTime) = 3 
                                            THEN CONCAT(LEFT(a.U_WgtInTime,1),':',RIGHT(a.U_WgtInTime, 2),' AM')
                                            WHEN a.U_WgtInTime > 1159 AND LEN(a.U_WgtInTime) = 4 and LEFT(a.U_WgtInTime,2) <> 12
                                            THEN CONCAT(LEFT(a.U_WgtInTime,2)-12,':',RIGHT(a.U_WgtInTime, 2),' PM')
                                            WHEN a.U_WgtInTime > 1159 AND LEN(a.U_WgtInTime) = 4 and LEFT(a.U_WgtInTime,2) = 12
                                            THEN CONCAT(LEFT(a.U_WgtInTime,2),':',RIGHT(a.U_WgtInTime, 2),' PM')
                                            WHEN a.U_WgtInTime < 1159 AND LEN(a.U_WgtInTime) = 4 
                                            THEN CONCAT(LEFT(a.U_WgtInTime,2),':',RIGHT(a.U_WgtInTime, 2),' AM')
                                            WHEN LEN(a.U_WgtInTime) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_WgtInTime, 2),' AM')
                                            END) U_WgtInTime, " +
                                        $@" (CASE WHEN LEN(a.U_WgtOutTime) = 3 
                                            THEN CONCAT(LEFT(a.U_WgtOutTime,1),':',RIGHT(a.U_WgtOutTime, 2),' AM')
                                            WHEN a.U_WgtOutTime > 1159 AND LEN(a.U_WgtOutTime) = 4 and LEFT(a.U_WgtOutTime,2) <> 12
                                            THEN CONCAT(LEFT(a.U_WgtOutTime,2)-12,':',RIGHT(a.U_WgtOutTime, 2),' PM')
                                            WHEN a.U_WgtOutTime > 1159 AND LEN(a.U_WgtOutTime) = 4 and LEFT(a.U_WgtOutTime,2) = 12
                                            THEN CONCAT(LEFT(a.U_WgtOutTime,2),':',RIGHT(a.U_WgtOutTime, 2),' PM')
                                            WHEN a.U_WgtOutTime < 1159 AND LEN(a.U_WgtOutTime) = 4 
                                            THEN CONCAT(LEFT(a.U_WgtOutTime,2),':',RIGHT(a.U_WgtOutTime, 2),' AM')
                                            WHEN LEN(a.U_WgtOutTime) = 2
                                            THEN CONCAT(12,':',RIGHT(a.U_WgtOutTime, 2),' AM')
                                            END) U_WgtOutTime, A.U_TORNo, ( select U_VehicleType from oitm where U_PlateNo = A.U_PlateNo) [vehicleType], ISNULL(U_PrepBy,'') U_PrepBy " +
                                         $" FROM ORDR A WHERE A.U_TORNo = '{TORNo}' AND ISNULL(A.U_WBNo,'') = ''";
                    DataTable dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                        qryGetOrder);

                    //string cardCode, cardName, Weight1, Weight2, NetWt, DRNo, Driver, BinNo1, BinNo2;
                    //string Destination, PlateNo, Cont1, Cont2, SWMFNo, YPNo, WasteType, Disposal, TripStart, Origin;
                    //string Rem1, Rem2;

                    if (dtAllOpenOrders.Rows.Count > 0)
                    {
                        enableControls();

                        //cmbOrderNo.Text = TORNo;
                        DocNum = DataHelper.ReadDataRow(dtAllOpenOrders, "DocNum", "", 0);
                        cmbCustomerCode.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "CardCode", "", 0);
                        //txtWBNo.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_WBNo", "", 0);
                        cmbCustomerName.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "CardName", "", 0);
                        txtFirstWeight.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Weight1", "", 0);
                        txtNetWeight.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_NetWt", "", 0);
                        txtDRNo.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_DRNo", "", 0);
                        txtDriver.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Driver", "", 0);
                        cmbBinNo1.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_BinNo1", "", 0);
                        cmbBinNo2.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_BinNo2", "", 0);
                        txtDestination.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Destination", "", 0);
                        cmbPlateNo.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_PlateNo", "", 0);
                        txtContainer1.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Cont1", "", 0);
                        txtContainer2.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Cont2", "", 0);
                        //txtSWMFNo.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_SWMFNo", "", 0);
                        txtYardPassNo.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_YPNo", "", 0);
                        cmbWasteTypeName.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_WasteType", "", 0);
                        txtDisposal.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Disposal", "", 0);
                        dtpTripStart.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_TripStart", "", 0);
                        txtRemarks1.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Rem1", "", 0);
                        dtpGateIn.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_GateIn", "", 0);
                        dtpGateOut.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_GateOut", "", 0);
                        dtpTripEnd.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_TripEnd", "", 0);
                        txtSource.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Source", "", 0);
                        txtDispatcher.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Dispatcher", "", 0);
                        txtLoading.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_LoadTime", "", 0);
                        txtFuelTopUp.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_Fuel", "", 0);
                        txtVehicleType.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "vehicleType", "", 0);
                        txtPreparedBy.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_PrepBy", "", 0);

                        lblWgtInTime.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtInTime", "", 0);
                        lblWgtOutTime.Text = DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtOutTime", "", 0);

                        if (string.IsNullOrWhiteSpace(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtInDate", "", 0)) == false)
                        {
                            string tet = Convert.ToDateTime(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtInDate", "", 0)).ToShortDateString();
                            if ((Convert.ToDateTime(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtInDate", "", 0)) > DateTime.Parse("01/01/2000")))
                            {
                                lblWgtInDate.Text = Convert.ToDateTime(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtInDate", "", 0)).ToShortDateString();
                            }
                            else lblWgtInDate.Text = "";

                        }
                        else lblWgtInDate.Text = "";


                        if (string.IsNullOrWhiteSpace(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtOutDate", "", 0)) == false)
                        {
                            if ((Convert.ToDateTime(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtOutDate", "", 0)) > DateTime.Parse("01/01/2000")))
                            {
                                lblWgtOutDate.Text = Convert.ToDateTime(DataHelper.ReadDataRow(dtAllOpenOrders, "U_WgtOutDate", "", 0)).ToShortDateString();
                            }
                            else lblWgtOutDate.Text = "";
                        }
                        else { lblWgtOutDate.Text = ""; }

                        decimal netweight;
                        if (string.IsNullOrWhiteSpace(txtNetWeight.Text))
                        {
                            netweight = 0;
                        }
                        else
                        {
                            netweight = Convert.ToDecimal(txtNetWeight.Text);
                        }



                    }
                    else
                    {
                        //MessageBox.Show("No data collected from the supplied Transaction Number. Please select proper transaction.");
                        //btnFind.PerformClick();
                        //frmTravelOrders.btnFind_Click(sender, e);
                    }
                }
                else
                {
                    foreach (TextBox textBox in Controls.OfType<TextBox>())
                        textBox.Text = "";
                    getWBNo();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        SerialPort SetSerialPort(string comPort)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }
            serialPort = new SerialPort(comPort, BaudRate, Parity.Space, 7, StopBits.One);
            serialPort.Open();
            return serialPort;
        }

        void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    serialPort.RtsEnable = true;
                    string convertResult = serialPort.ReadLine().Trim().Replace("Gross", "").Replace("Net", "").Replace(" ", "").Replace("kg", "");
                    Invoke(new Action(() =>
                    txtWeight.Text = convertResult
                    ));
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
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
                break;
            }
            return value;

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

        void readPort()
        {
            //serialPort = SetSerialPort(cmbPorts.Text);
            //string test = ReadLine(serialPort.ReadExisting());

            //txtWeight.Text = test;
            //weightCondition();

            //serialPort.Dispose();

            using (SerialPort serial = new SerialPort(cmbPorts.Text, BaudRate, Parity.Space, 7, StopBits.One))
            {
                serial.Open();
                string test = ReadLine(serial.ReadExisting());
                txtWeight.Text = test;
                weightCondition();
                serial.Dispose();
            }
        }

        void weightCondition()
        {
            try
            {
                if (Convert.ToDecimal(txtWeight.Text) > 0)
                {
                    txtFirstWeight.Text = (decimal.Parse(txtWeight.Text) / 1000).ToString();
                    lblWgtInDate.Text = DateTime.Now.ToShortDateString();
                    lblWgtInTime.Text = DateTime.Now.ToShortTimeString();
                    lblWgtOutDate.Text = DateTime.Now.ToShortDateString();
                    lblWgtOutTime.Text = DateTime.Now.ToShortTimeString();
                    txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();

                    if (Convert.ToDecimal(txtNetWeight.Text) < 0)
                    {
                        MessageBox.Show("Negative Net weight is not allowed. Please supply proper first weight.");
                        //txtWeight.Text = "0.00";
                        txtFirstWeight.Text = "0.00";
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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


        DataTable dtPricing(string binSize, string CustomerCode, string wasteType, string validFrom, string validTo)
        {
            string qry = $" select ISNULL(a.U_Price,0) U_Price, ISNULL(a.U_UOM,'') U_UOM, " +
                                                       $"(select k.UomEntry from ouom k where k.UomCode = a.U_Uom) UomEntry" +
                                                       $", ISNULL(a.U_Limit,0) U_Limit, " +
                                                       $" ISNULL(a.U_LimitType,'') U_LimitType, ISNULL(a.U_ExcessCharge,0) U_ExcessCharge, " +
                                                       $" isnull((select sum(x.Quantity) sumQty FROM ORDR z inner join " +
                                                       $" RDR1 x ON z.DocEntry = x.DocEntry AND z.CardCode = a.Code and CONCAT(MONTH(z.U_WgtOutDate), " +
                                                       $" YEAR(z.U_WgtOutDate))  = CONCAT(MONTH(GETDATE()),YEAR(GETDATE()))  ),0) sumQty," +
                                                       $" ISNULL(U_Size,1) size " +
                                                       $" from [@OWT1] a LEFT JOIN OUOM b ON a.U_UOM = b.UomName where b.Volume <> 0 AND " +
                                                       $" a.U_UOM like '%{binSize}%' AND " +
                                                       $" a.Code = '{CustomerCode}' AND " +
                                                       $" a.U_Type = '{wasteType}' AND " +
                                                       $" a.U_ValidFrom <= '{validFrom}' " +
                                                       $" AND a.U_ValidTo >= '{validTo}'  ";
            DataTable dtPrice;
            return dtPrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                               qry);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //#################################################################################
                //##################        DECLARATION OF VARIABLES         ######################
                //#################################################################################

                if (!string.IsNullOrWhiteSpace(txtYardPassNo.Text))
                {

                    if (Convert.ToDecimal(txtNetWeight.Text) >= 0)
                    {


                        if (Convert.ToDecimal(txtFirstWeight.Text) > 0)
                        {
                            if (Convert.ToDecimal(txtSecondWeight.Text) > 0)
                            {



                                if (Convert.ToDecimal(txtFirstWeight.Text) != Convert.ToDecimal(txtNetWeight.Text))
                                {
                                    var model = new List<ORDR>();
                                    var lineModel = new List<RDR1>();
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

                                    string itemCode = "R0001";


                                    //qry = $" select top 1 isnulL(U_WBNo,0) U_WBNo from ordr order by cast(isnull(U_WBNo,0) as int) desc";
                                    //DataTable dbWBNo = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                    //                        qry);
                                    //int wbNo1 = Convert.ToInt32(DataHelper.ReadDataRow(dbWBNo, "U_WBNo", "", 0));
                                    int wbNo1 = Convert.ToInt32(txtWBNo.Text);
                                    int torno = Convert.ToInt32(cmbOrderNo.Text);

                                    string query = $"SELECT ISNULL(U_Dim1,'') U_Dim1, ISNULL(U_Dim2,'') U_Dim2, ISNULL(U_Dim3,'') U_Dim3 FROM  OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                                    DataTable dtDim = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                                                   query);
                                    string dim1 = DataHelper.ReadDataRow(dtDim, "U_Dim1", "", 0);
                                    string dim2 = DataHelper.ReadDataRow(dtDim, "U_Dim2", "", 0);
                                    string dim3 = DataHelper.ReadDataRow(dtDim, "U_Dim3", "", 0);


                                    //#################################################################################
                                    //##################        DATA POSTING                     ######################
                                    //################################################################################# 


                                    // @@@@@@@@@@@@@  BLOCK IF TYPE OF WASTE DOES NOT EXIST IN THE DATABASE  @@@@@@@@@@@@@ //
                                    query = "SELECT Code, Name FROM " +
                                   $"{(PublicStatic.IsConnectedToServer ? $"[@WASTE_TYPE] WHERE Name = '{cmbWasteTypeName.Text}'" : $"CUFD WHERE AliasID = 'WASTE_TYPE'  and name = '{cmbWasteTypeName.Text}'")}";

                                    DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                                           query);



                                    if (dtWasteType.Rows.Count > 0)
                                    {

                                        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                        // @@@@@@@@@@@@@  if connected to SAP, post to SAP                   @@@@@@@@@@@@@ //  
                                        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                        if (PublicStatic.IsConnectedToServer)
                                        {


                                            // @@@@@@@@@@@@@  GET PRICE FROM SAP  @@@@@@@@@@@@@ //   



                                            // ############### GET DIMENSIONS FOR NEW ITEM ############// 
                                            string qry = $"SELECT ISNULL(U_Dim1,'') U_Dim1, ISNULL(U_Dim2,'') U_Dim2, ISNULL(U_Dim3,'') U_Dim3" +
                                                $"  FROM OCRD WHERE CardCode = '{cmbCustomerCode.Text}'";
                                            DataTable dtDimensions = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                     qry);
                                            string Dim1 = DataHelper.ReadDataRow(dtDimensions, "U_Dim1", "", 0);
                                            string Dim2 = DataHelper.ReadDataRow(dtDimensions, "U_Dim2", "", 0);
                                            string Dim3 = DataHelper.ReadDataRow(dtDimensions, "U_Dim3", "", 0);

                                            int ctr = 0;
                                            decimal binSize1 = 0;
                                            decimal binSize2 = 0;
                                            decimal totalBin = 0;


                                            if (lblCon1.Text == "NO BIN")
                                            {
                                                binSize1 = 0;
                                                binSize2 = 0;
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrWhiteSpace(lblCon1.Text))
                                                { binSize1 = Convert.ToDecimal(lblCon1.Text); }
                                                else { binSize1 = 0; }

                                                if (!string.IsNullOrWhiteSpace(lblCon2.Text))
                                                { binSize2 = Convert.ToDecimal(lblCon2.Text); }
                                                else { binSize2 = 0; }

                                            }
                                            totalBin = binSize1 + binSize2;



                                            int UOM = 4;
                                            string UOMCode = "TON";
                                            string limitType = "";
                                            decimal size = 0;
                                            decimal limit = 0;
                                            decimal price = 0;
                                            decimal ExcessCharge = 0;
                                            decimal sumQty = 0;
                                            decimal qty = Math.Round(netWt, 2);
                                            decimal basePrice = 0;


                                            // ############### if NetWt = 0 ############//
                                            if (Convert.ToDecimal(txtNetWeight.Text) == 0)
                                            {
                                                qty = 1;
                                                itemCode = "R0003";
                                                price = 15000;
                                                UOM = 25;
                                                netWt = 0;

                                                lineModel.Add(new RDR1
                                                {
                                                    LineNum = 0,
                                                    ItemCode = itemCode,
                                                    //Quantity = netWt,
                                                    Quantity = 1,
                                                    UoMEntry = 25,
                                                    UnitPrice = Math.Round(price, 2),
                                                    U_YPNo = txtYardPassNo.Text,
                                                    TaxCode = "OT0",
                                                    CostingCode = Dim1,
                                                    CostingCode2 = Dim2,
                                                    CostingCode3 = Dim3
                                                });
                                            }
                                            else
                                            {


                                                if (!string.IsNullOrWhiteSpace(lblCon1.Text))
                                                {
                                                    if (lblCon1.Text != "NO BIN")
                                                    {
                                                        DataTable dtPrice = dtPricing(lblCon1.Text, cmbCustomerCode.Text, cmbWasteTypeName.Text,
                                                                    DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                                                        if (dtPrice.Rows.Count > 0)
                                                        {
                                                            UOM = Convert.ToInt32(DataHelper.ReadDataRow(dtPrice, "UomEntry", "", 0));
                                                            UOMCode = DataHelper.ReadDataRow(dtPrice, "U_UOM", "", 0);
                                                            limitType = DataHelper.ReadDataRow(dtPrice, "U_LimitType", "", 0);
                                                            limit = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Limit", "", 0));
                                                            price = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));
                                                            ExcessCharge = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_ExcessCharge", "", 0));
                                                            sumQty = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "sumQty", "", 0));
                                                            basePrice = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));
                                                            size = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "size", "", 0));


                                                            if (limitType.ToUpper() == "MONTH")
                                                            {
                                                                // ############### if per MONTH ang condition ############//
                                                                if ((sumQty + netWt) >= limit)
                                                                {
                                                                    price = ExcessCharge;
                                                                }

                                                                qty = netWt;

                                                            }
                                                            else
                                                            {
                                                                if (UOMCode == "CU.M")
                                                                {
                                                                    qty = binSize1;
                                                                }
                                                                else if (UOMCode == "TON")
                                                                {
                                                                    qty = netWt;
                                                                }
                                                                else
                                                                {
                                                                    qty = 1;
                                                                }
                                                            }

                                                            lineModel.Add(new RDR1
                                                            {
                                                                ItemCode = "R0001",
                                                                LineNum = ctr,
                                                                //Quantity = netWt,
                                                                Quantity = qty,
                                                                UoMEntry = UOM,
                                                                UnitPrice = Math.Round(price, 2),
                                                                U_YPNo = txtYardPassNo.Text,
                                                                CostingCode = Dim1,
                                                                CostingCode2 = Dim2,
                                                                CostingCode3 = Dim3
                                                            });
                                                            ctr = ctr + 1;
                                                        }
                                                        else
                                                        {
                                                            qry = $" select ISNULL(a.U_Price,0) U_Price, U_Uom, " +
                                                                   $" (select k.UomEntry from ouom k where k.UomCode = a.U_Uom) UomEntry, " +
                                                                   $" ISNULL(U_LimitType,'') U_LimitType, " +
                                                                   $"  ISNULL(a.U_ExcessCharge,0) U_ExcessCharge ," +
                                                                   $" isnull((select sum(x.Quantity) sumQty FROM ORDR z inner join" +
                                                                   $" RDR1 x ON z.DocEntry = x.DocEntry AND z.CardCode = a.Code and CONCAT(MONTH(z.U_WgtOutDate)," +
                                                                   $" YEAR(z.U_WgtOutDate))  = CONCAT(MONTH(GETDATE()),YEAR(GETDATE()))  ),0) sumQty ," +
                                                                   $" ISNULL(a.U_Limit,0) U_Limit, ISNULL(A.U_Size,0) U_Size " +
                                                                   $" from [@OWT1] a where " +
                                                                   $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                                   $" a.U_Type = '{cmbWasteTypeName.Text}' AND " +
                                                                   $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                                   $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}'";
                                                            DataTable dtBasePrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                               qry);
                                                            if (dtBasePrice.Rows.Count > 0)
                                                            {
                                                                price = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Price", "", 0));
                                                                limit = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Limit", "", 0));
                                                                UOMCode = DataHelper.ReadDataRow(dtBasePrice, "U_UOM", "", 0);
                                                                limitType = DataHelper.ReadDataRow(dtBasePrice, "U_LimitType", "", 0);
                                                                ExcessCharge = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_ExcessCharge", "", 0));
                                                                sumQty = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "sumQty", "", 0));
                                                                size = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Size", "", 0));

                                                                if (limitType.ToUpper() == "MONTH")
                                                                {
                                                                    // ############### if per MONTH ang condition ############//
                                                                    if ((sumQty + netWt) >= limit)
                                                                    {
                                                                        price = ExcessCharge;
                                                                    }

                                                                    qty = netWt;

                                                                }
                                                                else
                                                                {
                                                                    if (UOMCode == "CU.M")
                                                                    {
                                                                        qty = binSize1;
                                                                        UOM = Convert.ToInt32(DataHelper.ReadDataRow(dtBasePrice, "UomEntry", "", 0));
                                                                    }
                                                                    else if (UOMCode == "TON")
                                                                    {
                                                                        qty = netWt;
                                                                    }
                                                                    else
                                                                    {
                                                                        qty = 1;
                                                                    }
                                                                }

                                                            }

                                                            lineModel.Add(new RDR1
                                                            {
                                                                ItemCode = "R0001",
                                                                LineNum = ctr,
                                                                //Quantity = netWt,
                                                                Quantity = qty,
                                                                UoMEntry = UOM,
                                                                UnitPrice = Math.Round(price, 2),
                                                                U_YPNo = txtYardPassNo.Text,
                                                                CostingCode = Dim1,
                                                                CostingCode2 = Dim2,
                                                                CostingCode3 = Dim3
                                                            });
                                                            ctr = ctr + 1;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        qry = $" select ISNULL(a.U_Price,0) U_Price " +
                                                                 $" from [@OWT1] a where " +
                                                                 $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                                 $" a.U_Type = '{cmbWasteTypeName.Text}' AND " +
                                                                 $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                                 $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}'";
                                                        DataTable dtBasePrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                           qry);
                                                        if (dtBasePrice.Rows.Count > 0)
                                                        {
                                                            price = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Price", "", 0));
                                                        }

                                                        lineModel.Add(new RDR1
                                                        {
                                                            ItemCode = "R0001",
                                                            LineNum = ctr,
                                                            //Quantity = netWt,
                                                            Quantity = qty,
                                                            UoMEntry = UOM,
                                                            UnitPrice = Math.Round(price, 2),
                                                            U_YPNo = txtYardPassNo.Text,
                                                            CostingCode = Dim1,
                                                            CostingCode2 = Dim2,
                                                            CostingCode3 = Dim3
                                                        });
                                                        ctr = ctr + 1;
                                                    }
                                                }


                                                if (!string.IsNullOrWhiteSpace(lblCon2.Text))
                                                {
                                                    if (lblCon2.Text != "NO BIN")
                                                    {
                                                        DataTable dtPrice = dtPricing(lblCon2.Text, cmbCustomerCode.Text, cmbWasteTypeName.Text,
                                                                    DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                                                        if (dtPrice.Rows.Count > 0)
                                                        {
                                                            UOM = Convert.ToInt32(DataHelper.ReadDataRow(dtPrice, "UomEntry", "", 0));
                                                            UOMCode = DataHelper.ReadDataRow(dtPrice, "U_UOM", "", 0);
                                                            limitType = DataHelper.ReadDataRow(dtPrice, "U_LimitType", "", 0);
                                                            limit = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Limit", "", 0));
                                                            price = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));
                                                            ExcessCharge = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_ExcessCharge", "", 0));
                                                            sumQty = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "sumQty", "", 0));
                                                            basePrice = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "U_Price", "", 0));
                                                            size = Convert.ToDecimal(DataHelper.ReadDataRow(dtPrice, "size", "", 0));

                                                            if (limitType.ToUpper() == "MONTH")
                                                            {
                                                                // ############### if per MONTH ang condition ############//
                                                                if ((sumQty + netWt) >= limit)
                                                                {
                                                                    price = ExcessCharge;
                                                                }

                                                                qty = netWt;
                                                            }
                                                            else
                                                            {
                                                                if (UOMCode == "CU.M")
                                                                {
                                                                    qty = totalBin;
                                                                }
                                                                else if (UOMCode == "TON")
                                                                {
                                                                    qty = netWt;
                                                                }
                                                            }

                                                            lineModel.Add(new RDR1
                                                            {
                                                                ItemCode = "R0001",
                                                                LineNum = ctr,
                                                                //Quantity = netWt,
                                                                Quantity = qty,
                                                                UoMEntry = UOM,
                                                                UnitPrice = Math.Round(price, 2),
                                                                U_YPNo = txtYardPassNo.Text,
                                                                CostingCode = Dim1,
                                                                CostingCode2 = Dim2,
                                                                CostingCode3 = Dim3
                                                            });
                                                            ctr = ctr + 1;
                                                        }
                                                        else
                                                        {
                                                            qry = $" select ISNULL(a.U_Price,0) U_Price, U_Uom, " +
                                                                   $" (select k.UomEntry from ouom k where k.UomCode = a.U_Uom) UomEntry, " +
                                                                   $" ISNULL(U_LimitType,'') U_LimitType, " +
                                                                   $"  ISNULL(a.U_ExcessCharge,0) U_ExcessCharge ," +
                                                                   $" isnull((select sum(x.Quantity) sumQty FROM ORDR z inner join" +
                                                                   $" RDR1 x ON z.DocEntry = x.DocEntry AND z.CardCode = a.Code and CONCAT(MONTH(z.U_WgtOutDate)," +
                                                                   $" YEAR(z.U_WgtOutDate))  = CONCAT(MONTH(GETDATE()),YEAR(GETDATE()))  ),0) sumQty ," +
                                                                   $" ISNULL(a.U_Limit,0) U_Limit, ISNULL(A.U_Size,0) U_Size " +
                                                                   $" from [@OWT1] a where " +
                                                                   $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                                   $" a.U_Type = '{cmbWasteTypeName.Text}' AND " +
                                                                   $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                                   $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}'";
                                                            DataTable dtBasePrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                               qry);
                                                            if (dtBasePrice.Rows.Count > 0)
                                                            {
                                                                price = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Price", "", 0));
                                                                limit = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Limit", "", 0));
                                                                UOMCode = DataHelper.ReadDataRow(dtBasePrice, "U_UOM", "", 0);
                                                                limitType = DataHelper.ReadDataRow(dtBasePrice, "U_LimitType", "", 0);
                                                                ExcessCharge = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_ExcessCharge", "", 0));
                                                                sumQty = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "sumQty", "", 0));
                                                                size = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Size", "", 0));


                                                                //ISANG LINE LANG KAPAG TON YUNG UOM
                                                                if (UOMCode != "TON")
                                                                {

                                                                    if (limitType.ToUpper() == "MONTH")
                                                                    {
                                                                        // ############### if per MONTH ang condition ############//
                                                                        if ((sumQty + netWt) >= limit)
                                                                        {
                                                                            price = ExcessCharge;
                                                                        }

                                                                        qty = netWt;


                                                                    }
                                                                    else
                                                                    {
                                                                        if (UOMCode == "CU.M")
                                                                        {
                                                                            qty = binSize2;
                                                                            UOM = Convert.ToInt32(DataHelper.ReadDataRow(dtBasePrice, "UomEntry", "", 0));
                                                                        }
                                                                        else if (UOMCode == "TON")
                                                                        {
                                                                            qty = netWt;
                                                                        }
                                                                        else
                                                                        {
                                                                            qty = 1;
                                                                        }
                                                                    }


                                                                    lineModel.Add(new RDR1
                                                                    {
                                                                        ItemCode = "R0001",
                                                                        LineNum = ctr,
                                                                        //Quantity = netWt,
                                                                        Quantity = qty,
                                                                        UoMEntry = UOM,
                                                                        UnitPrice = Math.Round(price, 2),
                                                                        U_YPNo = txtYardPassNo.Text,
                                                                        CostingCode = Dim1,
                                                                        CostingCode2 = Dim2,
                                                                        CostingCode3 = Dim3
                                                                    });
                                                                    ctr = ctr + 1;
                                                                }

                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        qry = $" select ISNULL(a.U_Price,0) U_Price " +
                                                                 $" from [@OWT1] a where " +
                                                                 $" a.Code = '{cmbCustomerCode.Text}' AND " +
                                                                 $" a.U_Type = '{cmbWasteTypeName.Text}' AND " +
                                                                 $" a.U_ValidFrom <= '{DateTime.Now.ToShortDateString()}' " +
                                                                 $" AND a.U_ValidTo >= '{DateTime.Now.ToShortDateString()}'";
                                                        DataTable dtBasePrice = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                   qry);
                                                        if (dtBasePrice.Rows.Count > 0)
                                                        {
                                                            price = Convert.ToDecimal(DataHelper.ReadDataRow(dtBasePrice, "U_Price", "", 0));
                                                        }

                                                        lineModel.Add(new RDR1
                                                        {
                                                            ItemCode = "R0001",
                                                            LineNum = ctr,
                                                            //Quantity = netWt,
                                                            Quantity = qty,
                                                            UoMEntry = UOM,
                                                            UnitPrice = Math.Round(price, 2),
                                                            U_YPNo = txtYardPassNo.Text,
                                                            CostingCode = Dim1,
                                                            CostingCode2 = Dim2,
                                                            CostingCode3 = Dim3
                                                        });
                                                        ctr = ctr + 1;
                                                    }
                                                }


                                                if (limitType.ToUpper() == "TON")
                                                {
                                                    // ############### if per TON ang condition ############//
                                                    limit = (limit / size) * totalBin;
                                                    if (netWt > limit)
                                                    {

                                                        // ############### if mas malaki ang NetWT vs Limit ############//
                                                        decimal tempLimit = Math.Round((limit * totalBin), 2);
                                                        decimal tempPrice = Math.Round(netWt, 2) - Math.Round(tempLimit, 2);
                                                        decimal tempPriceWithCharge = (Math.Abs(tempPrice) * ExcessCharge);
                                                        price = (price * Math.Round(totalBin, 2)) + Math.Round(tempPriceWithCharge, 2);
                                                        qty = totalBin;


                                                        lineModel.Add(new RDR1
                                                        {
                                                            LineNum = ctr,
                                                            ItemCode = "R0001",
                                                            //Quantity = netWt,
                                                            Quantity = netWt - limit,
                                                            UoMEntry = 4,
                                                            UnitPrice = Math.Round(ExcessCharge, 2),
                                                            U_YPNo = txtYardPassNo.Text,
                                                            TaxCode = "OT0",
                                                            CostingCode = Dim1,
                                                            CostingCode2 = Dim2,
                                                            CostingCode3 = Dim3
                                                        });
                                                    }
                                                }


                                            }



                                            model.Add(new ORDR
                                            {
                                                //U_TripStart = dtpTripStart.Text,
                                                //U_TripEnd = dtpTripEnd.Text,
                                                //U_GateIn = dtpGateIn.Text,
                                                //U_GateOut = dtpGateOut.Text,

                                                U_Weight1 = wt1,
                                                U_Weight2 = wt2,
                                                U_NetWt = netWt,
                                                U_BinNo1 = cmbBinNo1.Text,
                                                U_BinNo2 = cmbBinNo2.Text,
                                                U_Cont1 = lblCon1.Text,
                                                U_Cont2 = lblCon2.Text,
                                                U_YPNo = txtYardPassNo.Text,
                                                U_TORNo = torno,
                                                U_Combination = cmbCombination.Text,

                                                U_WgtInDate = wtInDate.AddDays(1),
                                                U_WgtOutDate = wtOutDate.AddDays(1),
                                                U_Operator1 = PublicStatic._frmMain.lblUser.Text,
                                                U_Operator2 = PublicStatic._frmMain.lblUser.Text,
                                                U_WasteType = cmbWasteTypeName.Text,
                                                U_Rem1 = txtRemarks1.Text,

                                                U_WgtInTime = Convert.ToDateTime(lblWgtInTime.Text).ToString("HH:mm"),
                                                U_WgtOutTime = Convert.ToDateTime(lblWgtInTime.Text).ToString("HH:mm"),
                                                U_WBNo = wbNo1,
                                                Comments = $"Created by Weighbridge (DCS) | Data Transfer : Fleet Travel Order Transactions  : {DateTime.Now} | Powered By : DIREC",

                                                DocumentLines = lineModel
                                            });

                                            Orders orders = new Orders();
                                            bool ret = orders.Patch(model, Convert.ToInt32(DocNum));
                                            if (ret)
                                            {
                                                MessageBox.Show("Transaction Updated.");
                                                updateWBNo();
                                                loadReport();
                                            }
                                            else
                                            {

                                                MessageBox.Show("Transaction to be saved locally; had an ERROR posting in SAP. Please contact administrator.");

                                                var model1 = new Document
                                                {
                                                    CustomerCode = cmbCustomerCode.Text,
                                                    CustomerName = cmbCustomerName.Text,
                                                    DRNo = txtDRNo.Text,
                                                    PlateNo = cmbPlateNo.Text,
                                                    YardPassNo = txtYardPassNo.Text,
                                                    TypeOfWaste = cmbWasteTypeName.Text,
                                                    Driver = txtDriver.Text,
                                                    Transporter = "",
                                                    Lengthh = 0,
                                                    Width = 0,
                                                    Height = 0,
                                                    Container = 0,
                                                    OriginOfWaste = "",
                                                    Weight1 = wt1,
                                                    Weight2 = wt2,
                                                    NetWt = netWt,
                                                    Remarks = txtRemarks1.Text,
                                                    WgtInDate = wtInDate.ToString(),
                                                    WgtOutDate = wtInDate.ToString(),
                                                    WgtInTime = wtIntime,
                                                    Operator1 = PublicStatic._frmMain.lblUser.Text,
                                                    Operator2 = PublicStatic._frmMain.lblUser.Text,
                                                    TransType = "FLEET",
                                                    VehicleType = "",
                                                    WgtOutTime = wtIntime,
                                                    WBNo = wbNo1.ToString(),

                                                    Dim1 = dim1,
                                                    Dim2 = dim2,
                                                    Dim3 = dim3,

                                                    BinNo1 = cmbBinNo1.Text,
                                                    BinNo2 = cmbBinNo2.Text,
                                                    Cont1 = lblCon1.Text,
                                                    Cont2 = lblCon2.Text,
                                                    TORNo = cmbOrderNo.Text,
                                                    Combination = cmbCombination.Text
                                                };

                                                using (Context db = new Context())
                                                {
                                                    db.Document.Add(model1);
                                                    db.SaveChanges();
                                                    MessageBox.Show("Transaction Added.");
                                                    updateWBNo();

                                                    loadReport(ret);
                                                }
                                            }
                                        }

                                        else
                                        {
                                            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                            // @@@@@@@@@@@@@  if not connected to SAP, post to LOCAL             @@@@@@@@@@@@@ //  
                                            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 

                                            var model1 = new Document
                                            {
                                                CustomerCode = cmbCustomerCode.Text,
                                                CustomerName = cmbCustomerName.Text,
                                                DRNo = txtDRNo.Text,
                                                PlateNo = cmbPlateNo.Text,
                                                YardPassNo = txtYardPassNo.Text,
                                                TypeOfWaste = cmbWasteTypeName.Text,
                                                Driver = txtDriver.Text,
                                                Transporter = "",
                                                Lengthh = 0,
                                                Width = 0,
                                                Height = 0,
                                                Container = 0,
                                                OriginOfWaste = "",
                                                Weight1 = wt1,
                                                Weight2 = wt2,
                                                NetWt = netWt,
                                                Remarks = txtRemarks1.Text,
                                                WgtInDate = wtInDate.ToString(),
                                                WgtOutDate = wtInDate.ToString(),
                                                WgtInTime = wtIntime,
                                                Operator1 = PublicStatic._frmMain.lblUser.Text,
                                                Operator2 = PublicStatic._frmMain.lblUser.Text,
                                                TransType = "FLEET",
                                                VehicleType = "",
                                                WgtOutTime = wtIntime,
                                                WBNo = wbNo1.ToString(),

                                                Dim1 = dim1,
                                                Dim2 = dim2,
                                                Dim3 = dim3,

                                                BinNo1 = cmbBinNo1.Text,
                                                BinNo2 = cmbBinNo2.Text,
                                                Cont1 = lblCon1.Text,
                                                Cont2 = lblCon2.Text,
                                                TORNo = cmbOrderNo.Text,
                                                Combination = cmbCombination.Text

                                            };

                                            using (Context db = new Context())
                                            {
                                                db.Document.Add(model1);
                                                db.SaveChanges();
                                                MessageBox.Show("Transaction Added.");
                                                updateWBNo();

                                                loadReport();
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
                                    MessageBox.Show("First weight shall not be equal to net weight.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please supply proper first / second weight.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please supply proper first / second weight.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Negative net weight is not allowed.");
                    }
                }
                else
                {
                    MessageBox.Show("No Yard Pass No. supplied. Please provide proper Yard Pass No..");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void updateWBNo()
        {
            string qry = $"Update Series set WBNo = WBNo +1, CreateDate = '{DateTime.Now}'";
            MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qry);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy -- HH:mm:ss");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
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

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //when manual typing is allowed
            //    if (txtWeight.ReadOnly)
            //    {
            //        weightCondition();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void txtSecondWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNetWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                txtFirstWeight.Text = "0.00";
                txtNetWeight.Text = "0.00";
            }
        }

        void loadReport(bool ret = true)
        {
            try
            {
                if (Convert.ToDecimal(txtFirstWeight.Text) > 0)
                {
                    string tag = "";
                    if (PublicStatic.IsConnectedToServer && ret)
                    {
                        tag = "internal";
                    }
                    else { tag = "internalNotSAP"; }

                    frmCrystalReport frmCrystalReport = new frmCrystalReport(tag, "SO");
                    frmCrystalReport.oDocKey = txtWBNo.Text;
                    Close();
                    frmCrystalReport.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No First Weight detected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fromSearch(cmbOrderNo.Text);
            //txtWeight.Text = "0.00";
            //loadCombination();
            //cmbBinNo1.DisplayMember = "U_BinNo";
            //cmbBinNo2.DisplayMember = "U_BinNo";
        }

        private void cmbCombination_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string qry = $" SELECT DISTINCT U_NetWT FROM [@TRUCK_WEIGHT] WHERE U_Combination = '{cmbCombination.Text}'  ";
            //DataTable dtTare = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
            //                               qry);
            //if (dtTare.Rows.Count > 0)
            //{
            //    txtSecondWeight.Text = DataHelper.ReadDataRow(dtTare, "U_NetWT", "", 0);
            //}
            //else
            //{
            //    txtSecondWeight.Text = "0.00";
            //}
        }

        private void cmbOrderNo_TextChanged(object sender, EventArgs e)
        {

            if (PublicStatic.IsConnectedToServer)
            {
                fromSearch(cmbOrderNo.Text);
                txtWeight.Text = "0.00";
                loadCombination();
                cmbBinNo1.DisplayMember = "BinNo_";
                lblCon1.Text = "";
                cmbBinNo2.DisplayMember = "BinNo_";
                lblCon2.Text = "";
            }
        }

        private void btnNoWaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSecondWeight.Text) && Convert.ToDecimal(txtSecondWeight.Text) > 0)
                {
                    txtFirstWeight.Text = txtSecondWeight.Text;
                    txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();
                    lblWgtOutDate.Text = DateTime.Now.ToShortDateString();
                    lblWgtOutTime.Text = DateTime.Now.ToShortTimeString();
                }
                else
                {
                    MessageBox.Show("No Second Weight detected. Supply Second Weight to apply this function.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadTareWeight()
        {
            string qry = "";
            DataTable dtTare;
            if (PublicStatic.IsConnectedToServer)
            {
                qry = $" SELECT DISTINCT U_NetWT FROM [@TRUCK_WEIGHT] WHERE U_Combination = '{cmbCombination.Text}' " +
                   $" AND U_PlateNo = '{cmbPlateNo.Text}' ";
                dtTare = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                              qry);

                if (dtTare.Rows.Count > 0)
                {
                    txtSecondWeight.Text = DataHelper.ReadDataRow(dtTare, "U_NetWT", "", 0);
                }
                else
                {
                    txtSecondWeight.Text = "0.00";
                }
            }
            else
            {
                qry = $" SELECT DISTINCT NetWt FROM [TRUCK_WEIGHT] WHERE Combination = '{cmbCombination.Text}' " +
                       $" AND PlateNo = '{cmbPlateNo.Text}' ";
                dtTare = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                              qry);

                if (dtTare.Rows.Count > 0)
                {
                    txtSecondWeight.Text = DataHelper.ReadDataRow(dtTare, "NetWT", "", 0);
                }
                else
                {
                    txtSecondWeight.Text = "0.00";
                }
            }

        }

        private void cmbCombination_TextChanged(object sender, EventArgs e)
        {
            try
            {
                loadTareWeight();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void autoSelectCombination()
        {
            try
            {
                string var1 = lblCon1.Text;
                string var2 = "";
                if (!string.IsNullOrWhiteSpace(lblCon2.Text))
                {
                    var2 = "-" + lblCon2.Text;
                }
                string cubicMeter = var1 + var2;
                string qry = "";

                if (PublicStatic.IsConnectedToServer)
                {
                    qry = $"SELECT A.U_Combination FROM [@TRUCK_WEIGHT] A WHERE A.U_PlateNo = '{cmbPlateNo.Text}' AND A.U_Remarks = replace('{cubicMeter}',' cm','')";
                    DataTable dtCombination = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"), qry);
                    if (dtCombination.Rows.Count > 0)
                    {
                        cmbCombination.Text = DataHelper.ReadDataRow(dtCombination, "U_Combination", "", 0);
                    }
                    else
                    {
                        cmbCombination.Text = "";
                    }
                }
                else
                {
                    qry = $"SELECT A.Combination FROM [TRUCK_WEIGHT] A WHERE A.PlateNo = '{cmbPlateNo.Text}' AND A.Remarks = replace('{cubicMeter}',' cm','')";
                    DataTable dtCombination = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);
                    if (dtCombination.Rows.Count > 0)
                    {
                        cmbCombination.Text = DataHelper.ReadDataRow(dtCombination, "Combination", "", 0);
                    }
                    else
                    {
                        cmbCombination.Text = "";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbBinNo1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cmbOrderNo.Text))
                {
                    if (!string.IsNullOrWhiteSpace(cmbBinNo1.Text))
                    {
                        cmbBinNo2.Enabled = true;
                        if (cmbBinNo1.Text != cmbBinNo2.Text)
                        {

                            string qry = $"SELECT DISTINCT BinSize FROM BIN WHERE BinNo_ = '{cmbBinNo1.Text}'";
                            DataTable dtBinSize2 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                           qry);
                            if (dtBinSize2.Rows.Count > 0)
                            {
                                lblCon1.Text = DataHelper.ReadDataRow(dtBinSize2, "BinSize", "", 0);
                                autoSelectCombination();
                                loadTareWeight();
                            }
                            else
                            {
                                lblCon1.Text = "";
                                cmbBinNo2.Enabled = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("BIN NO.s should be different.");
                            cmbBinNo1.Text = "";
                            cmbBinNo1.Focus();
                        }

                        txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();

                        if (Convert.ToDecimal(txtNetWeight.Text) < 0)
                        {
                            txtFirstWeight.Text = "0.00";
                            txtNetWeight.Text = "0.00";
                        }
                    }

                    else { cmbBinNo2.Enabled = false; lblCon1.Text = ""; autoSelectCombination(); }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbBinNo2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cmbOrderNo.Text))
                {
                    if (!string.IsNullOrWhiteSpace(cmbBinNo2.Text))
                    {
                        if (cmbBinNo1.Text != cmbBinNo2.Text)
                        {

                            string qry = $" SELECT DISTINCT BinSize FROM BIN WHERE BinNo_ = '{cmbBinNo2.Text}'";
                            DataTable dtBinSize2 = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                           qry);
                            if (dtBinSize2.Rows.Count > 0)
                            {
                                lblCon2.Text = DataHelper.ReadDataRow(dtBinSize2, "BinSize", "", 0);
                                autoSelectCombination();
                                loadTareWeight();
                            }
                            else
                            {
                                lblCon2.Text = "";
                            }

                            txtNetWeight.Text = (decimal.Parse(txtFirstWeight.Text) - decimal.Parse(txtSecondWeight.Text)).ToString();

                            if (Convert.ToDecimal(txtNetWeight.Text) < 0)
                            {
                                txtFirstWeight.Text = "0.00";
                                txtNetWeight.Text = "0.00";
                            }
                        }
                        else
                        {
                            MessageBox.Show("BIN NO.s should be different.");
                            cmbBinNo2.Text = "";
                            cmbBinNo1.Focus();
                        }
                    }
                    else
                    {
                        lblCon2.Text = "";
                        cmbBinNo2.Enabled = false;
                        autoSelectCombination();

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void frmTravelOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Dispose();
            }
            Dispose();
        }

        private void label7_Click(object sender, EventArgs e)
        {

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
            }

        }

        private void cmbWasteTypeName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbWasteTypeName.Text))
            {

                string query = "SELECT Code, Name FROM " +
                $"{(PublicStatic.IsConnectedToServer ? $"[@WASTE_TYPE] WHERE Name = '{cmbWasteTypeName.Text}'" : $"CUFD WHERE AliasID = 'WASTE_TYPE'  and name = '{cmbWasteTypeName.Text}'")}";

                DataTable dtWasteType = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                       query);

                if (dtWasteType.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Waste Type. Please provide proper Waste Type.");
                    cmbWasteTypeName.Focus();
                }
            }
        }

        private void cmbBinNo1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbBinNo1.Text))
            {
                if (string.IsNullOrWhiteSpace(lblCon1.Text))
                {
                    MessageBox.Show("Invalid BIN. Provided BIN No does not exist in our system. Please contact administrator.");
                    cmbBinNo1.Focus();
                }
            }
        }

        private void cmbBinNo2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbBinNo2.Text))
            {
                if (string.IsNullOrWhiteSpace(lblCon2.Text))
                {
                    MessageBox.Show("Invalid BIN. Provided BIN No does not exist in our system. Please contact administrator.");
                    cmbBinNo2.Focus();
                }
            }
        }


    }
}
