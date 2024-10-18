using DirecLayer;
using MCWM.Helper;
using MCWM.Views.Main;
using System;
using System.Data;
using DomainLayer.CONTEXT;
using System.Windows.Forms;
using DomainLayer.SAO_DATABASE;
using System.Linq;
using System.Data.Entity;
using ServiceLayer;
using System.Collections.Generic;
using DomainLayer.SAP_DATABASE;

namespace MCWM.Views.Security
{
    //public partial class frmLogin : Form, IfrmLogin
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Event Registration
        //public event EventHandler _Login_btnLogin_Click;
        #endregion 
        #region Variables
        //public string _TextBoxUsername
        //{
        //    get => txtUser.Text;
        //    set => txtUser.Text = value;
        //}

        //public string _TextBoxPassword
        //{
        //    get => txtPassword.Text;
        //    set => txtPassword.Text = value;
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
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //EventHelper.RaisedEvent(sender, _Login_btnLogin_Click, e);
            try
            {
                checkSAPConnection();
                string query = "SELECT 'True' FROM " +
                                $"{(PublicStatic.IsConnectedToServer ? $"[@USERS] WHERE U_IsActive = 'Y' AND U_UserType = 'WB' AND U_AUserId = '{txtUser.Text}' AND U_APassword = '{txtPassword.Text}'" : $"OUSR WHERE Username = '{txtUser.Text}' AND Password = '{txtPassword.Text}'")}";

                DataTable dtUsers = MsSqlAccess.Get(PublicStatic.IsConnectedToServer ? MsSqlAccess.ConnectionString("SAP") : MsSqlAccess.ConnectionString("Addon"),
                                               query);

                if (DataHelper.DataTableExist(dtUsers))
                {

                    frmMain frmMain = new frmMain(txtUser.Text);
                    PublicStatic._frmMain = frmMain;

                    //if (PublicStatic.IsConnectedToServer)
                    //{
                    //    string lastSync = AppConfig.AppSettings("LastSync");
                    //    SyncCustomersFromServer(lastSync);
                    //    syncUploadDataToSAP();
                    //    SyncOriginOfWaste();
                    //    SyncTruckFleet();
                    //    SyncUsersFromServer(lastSync);
                    //    SyncUserDefinedFromServer();
                    //    SyncCombinations();
                    //    SyncBIN();

                    //    AppConfig.UpdateConfig("appSettings", "LastSync", DateTime.Now.ToShortDateString());

                    //    //posting to sap

                    //}
                    frmMain.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void checkSAPConnection()
        {
            bool isConnected = false;

            //Check if the you can ping the server
            if (SystemSettings.PingHost(AppConfig.AppSettings("Server")) == false)
            {
                MessageBox.Show("You are not connected to the server. Local connection is activated.");
                PublicStatic.IsConnectedToServer = false;
            }
            else
            {
                //trying to get certificate for service layer
                SboServiceLayer sboServiceLayer = new SboServiceLayer();
                isConnected = sboServiceLayer.Initialize();
                if (isConnected)
                {
                    MessageBox.Show("You are connected to SAP.");
                    PublicStatic.IsConnectedToServer = true;
                }
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Focus();
            lblVersion.Text = $"Login v{SystemSettings.Info.AssemblyVersion}";
        }

        void SyncUsersFromServer(string lastSync)
        {
            //Sync users
            string query = "SELECT Name, U_AUserId, U_APassword FROM [@USERS] " +
                $"WHERE ISNULL(UpdateDate,CreateDate) >= '{lastSync}'";
            DataTable dtUsers = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                               query);


            if (DataHelper.DataTableExist(dtUsers))
            {
                foreach (DataRow user in dtUsers.Rows)
                {
                    using (Context db = new Context())
                    {
                        Users users = new Users();
                        string userCode = DataHelper.ReadDataRow(user, "U_AUserId", "");
                        users = db.Users
                                .Where(x => x.Username == userCode)
                                .FirstOrDefault();

                        if (users != null && string.IsNullOrEmpty(users.Username) == false)
                        {
                            Users patchUser = db.Users.Find(users.Id);
                            patchUser.Name = DataHelper.ReadDataRow(user, "Name", "");
                            patchUser.Password = DataHelper.ReadDataRow(user, "U_APassword", "");
                            patchUser.UpdateDate = DateTime.Now;
                            db.Entry(patchUser).State = EntityState.Modified;
                        }
                        else
                        {
                            Users postUser = new Users
                            {
                                Name = DataHelper.ReadDataRow(user, "Name", ""),
                                Username = DataHelper.ReadDataRow(user, "U_AUserId", ""),
                                Password = DataHelper.ReadDataRow(user, "U_APassword", ""),
                                CreateDate = DateTime.Now
                            };

                            db.Users.Add(postUser);
                        }

                        db.SaveChanges();
                    }
                }
            }
        }


        void SyncCombinations()
        {
            try
            {

                string query = "SELECT A.* FROM [@TRUCK_WEIGHT] A ";
                //+
                //" WHERE " +
                //$" NOT EXISTS (SELECT TOP 1 'TRUE' FROM " +
                //$" [{AppConfig.AppSettings("SqlServer")}].[{AppConfig.AppSettings("SqlDatabase")}].[dbo].[TRUCK_WEIGHT] x WHERE " +
                //$" x.Code = A.Code AND x.NetWt = a.U_NetWt AND x.PlateNo = a.U_PlateNo AND x.Combination = a.U_Combination AND" +
                //$" x.Remarks = A.U_Remarks) ";
                DataTable tbTruckWeight = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   query);

                if (tbTruckWeight.Rows.Count > 0)
                {
                    foreach (DataRow row in tbTruckWeight.Rows)
                    {
                        string code = DataHelper.ReadDataRow(row, "Code", "");
                        string name = DataHelper.ReadDataRow(row, "Name", "");
                        decimal netWt = Convert.ToDecimal(DataHelper.ReadDataRow(row, "U_NetWt", ""));
                        string plateNo = DataHelper.ReadDataRow(row, "U_PlateNo", "");
                        string combination = DataHelper.ReadDataRow(row, "U_Combination", "");
                        string remarks = DataHelper.ReadDataRow(row, "U_Remarks", "");

                        string qry = $"SELECT TOP 1 'True' FROM TRUCK_WEIGHT WHERE PlateNo = '{plateNo}'";
                        DataTable tbExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                        if (tbExist.Rows.Count > 0)
                        {
                            query = $"UPDATE TRUCK_WEIGHT SET Code = '{code}', Name = '{name}', NetWt = {netWt}, Remarks = '{remarks}' " +
                                        $" WHERE PlateNo = '{plateNo}' AND Combination = '{combination}'";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }
                        else
                        {
                            query = $"INSERT INTO TRUCK_WEIGHT VALUES ('{code}', '{name}', {netWt}, '{plateNo}', '{combination}', '{remarks}')";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        void SyncBIN()
        {
            try
            {

                string query = "  SELECT DISTINCT A.U_BinNo, A.U_BinSize FROM OITM A WHERE A.ItemType = 'F' AND ISNULL(A.U_BinNo,'') <> '' " +
                    $" AND ISNULL(A.U_BinSize,'' ) <> ''  ";
                //+
                //$"AND NOT EXISTS (  SELECT TOP 1 'TRUE' FROM " +
                //$" [{AppConfig.AppSettings("SqlServer")}].[{AppConfig.AppSettings("SqlDatabase")}].[dbo].[BIN] x " +
                //$" WHERE x.BinNo_ = A.U_BinNo AND x.BinSize = A.U_BinSize ) ";
                DataTable tbBin = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   query);
                if (tbBin.Rows.Count > 0)
                {
                    foreach (DataRow row in tbBin.Rows)
                    {
                        string binNo = DataHelper.ReadDataRow(row, "U_BinNo", "");
                        string binSize = DataHelper.ReadDataRow(row, "U_BinSize", "");

                        string qry = $"SELECT TOP 1 'True' FROM BIN WHERE BinNo_ = '{binNo}'";
                        DataTable tbExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                        if (tbExist.Rows.Count > 0)
                        {
                            query = $"UPDATE BIN SET BinSize = '{binSize}'" +
                                        $" WHERE BinNo_ = '{binNo}'";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }
                        else
                        {
                            query = $"INSERT INTO BIN VALUES ('{binNo}', '{binSize}')";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SyncTruckFleet()
        {
            try
            {

                string query = " SELECT DISTINCT A.U_PlateNo  FROM OITM A WHERE A.ItemType = 'F' AND ISNULL(U_PlateNo,'') <> ''  ";
                //+
                //$" AND NOT EXISTS ( SELECT TOP 1 'TRUE' FROM " +
                //$" [{AppConfig.AppSettings("SqlServer")}].[{AppConfig.AppSettings("SqlDatabase")}].[dbo].[TRUCK_FLEET] x " +
                //$" WHERE x.PlateNo = A.U_PlateNo ) ";
                DataTable tblTruckFleet = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   query);
                if (tblTruckFleet.Rows.Count > 0)
                {
                    foreach (DataRow row in tblTruckFleet.Rows)
                    {
                        string plateNo = DataHelper.ReadDataRow(row, "U_PlateNo", "");

                        string qry = $"SELECT TOP 1 'True' FROM [TRUCK_FLEET] WHERE PlateNo = '{plateNo}'";
                        DataTable tbExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                        if (tbExist.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            query = $"INSERT INTO TRUCK_FLEET VALUES ('{plateNo}')";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        void SyncOriginOfWaste()
        {
            try
            {

                string query = $" SELECT DISTINCT z.[Code], z.Name [Name] " +
                               $" FROM (select DISTINCT A.Code,A.U_Name [Name] FROM [@OWT4] A WHERE ISNULL(A.U_Name,'') <> '' " +
                               $" union all" +
                               $" SELECT DISTINCT A.CardCode [Code], A.U_Destination FROM ORDR A where ISNULL(A.U_Destination,'') <> '' ) Z ORDER BY Z.Name  ";

                DataTable tblOriginOfWaste = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   query);
                if (tblOriginOfWaste.Rows.Count > 0)
                {
                    foreach (DataRow row in tblOriginOfWaste.Rows)
                    {
                        string code = DataHelper.ReadDataRow(row, "Code", "");
                        string name = DataHelper.ReadDataRow(row, "Name", "");

                        string qry = $"SELECT TOP 1 'True' FROM [CUFD] WHERE Code = '{code}' AND Name = '{name.Replace("'", "")}' AND AliasID = 'OWT4'";
                        DataTable tbExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                        if (tbExist.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            query = $"INSERT INTO CUFD VALUES ('{code}', '{name.Replace("'", "")}', 'OWT4', '{DateTime.Now}', '{DateTime.Now}')";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }
                    }
                }

                query = $"DELETE FROM CUFD where AliasID = 'OWT4' AND ISNULL(Code,'') = '' AND ISNULL(Name,'') = ''";
                MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        void SyncCustomersFromServer(string lastSync)
        {
            string query = "SELECT A.CardCode, A.CardName, A.frozenFor, A.U_Dim1, A.U_Dim2, A.U_Dim3, ISNULL(A.U_Municipality,'') U_Municipality, " +
                " ISNULL(A.U_Zone,'') U_Zone, (select b.GroupName from ocrg b where b.GroupCode = A.GroupCode) GroupCode FROM OCRD A WHERE A.CardType = 'C' AND " +
               $"ISNULL(A.UpdateDate,A.CreateDate) >= '{lastSync}'";
            DataTable tbCustomers = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                               query);


            if (DataHelper.DataTableExist(tbCustomers))
            {
                foreach (DataRow customer in tbCustomers.Rows)
                {
                    using (Context db = new Context())
                    {
                        Customers customers = new Customers();
                        string cardCode = DataHelper.ReadDataRow(customer, "CardCode", "");
                        customers = db.Customers
                                .Where(x => x.CardCode == cardCode)
                                .FirstOrDefault();

                        if (customers != null && string.IsNullOrEmpty(customers.CardCode) == false)
                        {
                            Customers patchCustomer = db.Customers.Find(customers.Id);

                            patchCustomer.CardName = DataHelper.ReadDataRow(customer, "CardName", "");
                            patchCustomer.frozenFor = DataHelper.ReadDataRow(customer, "frozenFor", "");
                            patchCustomer.U_Dim1 = DataHelper.ReadDataRow(customer, "U_Dim1", "");
                            patchCustomer.U_Dim2 = DataHelper.ReadDataRow(customer, "U_Dim2", "");
                            patchCustomer.U_Dim3 = DataHelper.ReadDataRow(customer, "U_Dim3", "");
                            patchCustomer.U_Municipality = DataHelper.ReadDataRow(customer, "U_Municipality", "");
                            patchCustomer.U_Zone = DataHelper.ReadDataRow(customer, "U_Zone", "");
                            patchCustomer.GroupType = DataHelper.ReadDataRow(customer, "GroupCode", "");
                            patchCustomer.UpdateDate = DateTime.Now;

                            db.Entry(patchCustomer).State = EntityState.Modified;
                        }
                        else
                        {
                            Customers postCustomer = new Customers
                            {
                                CardCode = DataHelper.ReadDataRow(customer, "CardCode", ""),
                                CardName = DataHelper.ReadDataRow(customer, "CardName", ""),
                                frozenFor = DataHelper.ReadDataRow(customer, "frozenFor", ""),
                                U_Dim1 = DataHelper.ReadDataRow(customer, "U_Dim1", ""),
                                U_Dim2 = DataHelper.ReadDataRow(customer, "U_Dim2", ""),
                                U_Dim3 = DataHelper.ReadDataRow(customer, "U_Dim3", ""),
                                U_Municipality = DataHelper.ReadDataRow(customer, "U_Municipality", ""),
                                U_Zone = DataHelper.ReadDataRow(customer, "U_Zone", ""),
                                GroupType = DataHelper.ReadDataRow(customer, "GroupCode", ""),
                                CreateDate = DateTime.Now
                            };

                            db.Customers.Add(postCustomer);
                        }

                        db.SaveChanges();
                    }
                }
            }
        }

        void SyncUserDefinedFromServer()
        {
            string query = "SELECT FldValue [Code],FldValue [Name] " +
                            "FROM CUFD a INNER JOIN " +
                            "UFD1 b on a.FieldID = b.FieldID and a.Tableid = b.TableID " +
                            "WHERE a.TableID = 'ORDR' AND AliasID = 'Disposal'";
            SyncUserDefined(query,
                            "DISPOSAL");

            query = "SELECT Code,Name FROM [@WASTE_TYPE]";
            SyncUserDefined(query,
                            "WASTE_TYPE");

            query = "SELECT Name [Code],Name FROM [@LIST_TRANSPORTER]";
            SyncUserDefined(query,
                            "LIST_TRANSPORTER");

            query = "SELECT Name [Code],Name FROM [@LIST_DRIVER_EXT]";
            SyncUserDefined(query,
                            "LIST_DRIVER_EXT");

            query = "SELECT DISTINCT Code,U_VehicleType [Name] FROM [@TRUCK_EXTERNAL]";
            SyncUserDefined(query,
                            "TRUCK_EXTERNAL");

            query = "SELECT DISTINCT Code,Name FROM [@TRUCK_TYPE]";
            SyncUserDefined(query,
                            "TRUCK_TYPE");

            //query = "SELECT DISTINCT U_Name [Code],U_Name [Name] FROM [@OWT4]";
            //SyncUserDefined(query,
            //                "OWT4");





        }

        void SyncUserDefined(string query,
                            string aliasId)
        {
            DataTable tbUserDefined = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                    query);


            if (DataHelper.DataTableExist(tbUserDefined))
            {
                foreach (DataRow userDefined in tbUserDefined.Rows)
                {
                    using (Context db = new Context())
                    {
                        UserDefined userDefineds = new UserDefined();
                        string fldValue = DataHelper.ReadDataRow(userDefined, "Code", "");

                        userDefineds = db.UserDefined
                                .Where(x => x.Code == fldValue && x.AliasID == aliasId)
                                .FirstOrDefault();

                        if (userDefineds != null && string.IsNullOrEmpty(userDefineds.Code) == false)
                        {
                            UserDefined patchUserDefined = db.UserDefined.Find(userDefineds.Id);

                            patchUserDefined.Name = DataHelper.ReadDataRow(userDefined, "Name", "");
                            patchUserDefined.UpdateDate = DateTime.Now;

                            db.Entry(patchUserDefined).State = EntityState.Modified;
                        }
                        else
                        {
                            UserDefined postUserDefined = new UserDefined
                            {
                                Code = DataHelper.ReadDataRow(userDefined, "Code", ""),
                                Name = DataHelper.ReadDataRow(userDefined, "Name", ""),
                                AliasID = aliasId,
                                CreateDate = DateTime.Now
                            };

                            db.UserDefined.Add(postUserDefined);
                        }

                        db.SaveChanges();
                    }
                }
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



        void syncUploadDataToSAP()
        {
            try
            {

                string qry = "SELECT x.* FROM ( " +
                      " SELECT A.* FROM ODOC A WHERE A.TransType <> 'FLEET' AND ISNULL(Weight2,0) <> 0 " +
                      "  UNION ALL " +
                      " SELECT A.* FROM ODOC A WHERE A.TransType ='FLEET' ) x ORDER BY CAST(x.WBNo AS INT)  ";
                DataTable dtDoc = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                    qry);
                bool check = false;


                if (dtDoc.Rows.Count != 0)
                {

                    foreach (DataRow row in dtDoc.Rows)
                    {

                        // @@@@@@@@@@@@@@ DECLARE VARIABLES @@@@@@@@@@@ //
                        int wbNo = Convert.ToInt32(DataHelper.ReadDataRow(row, "WBNo", ""));

                        //Check if duplicate from SAP
                        qry = $"SELECT TOP 1 'True' FROM ORDR WHERE ISNULL(U_WbNo,'') = '{wbNo}' UNION ALL " +
                            $" SELECT TOP 1 'True' FROM OQUT WHERE ISNULL(U_WbNo,'') = '{wbNo}' ";
                        DataTable dtExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                qry);

                        //if exist remove data
                        if (dtExist.Rows.Count == 0)
                        //else continue
                        {
                            string CustomerCode = DataHelper.ReadDataRow(row, "CustomerCode", "");

                            if (!string.IsNullOrWhiteSpace(CustomerCode))
                            {

                                string CustomerName = DataHelper.ReadDataRow(row, "CustomerName", "");
                                string Transporter = DataHelper.ReadDataRow(row, "Transporter", "");
                                string OriginOfWaste = DataHelper.ReadDataRow(row, "OriginOfWaste", "");
                                string DRNo = DataHelper.ReadDataRow(row, "DRNo", "");
                                string Driver = DataHelper.ReadDataRow(row, "Driver", "");
                                string PlateNo = DataHelper.ReadDataRow(row, "PlateNo", "");
                                string VehicleType = DataHelper.ReadDataRow(row, "VehicleType", "");
                                string YardPassNo = DataHelper.ReadDataRow(row, "YardPassNo", "");
                                string TypeOfWaste = DataHelper.ReadDataRow(row, "TypeOfWaste", "");
                                decimal Lengthh = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Lengthh", ""));
                                decimal Width = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Width", ""));
                                decimal Height = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Height", ""));
                                decimal Container = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Container", ""));
                                decimal Weight1 = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Weight1", ""));
                                decimal Weight2 = Convert.ToDecimal(DataHelper.ReadDataRow(row, "Weight2", ""));
                                decimal NetWt = Convert.ToDecimal(DataHelper.ReadDataRow(row, "NetWt", ""));
                                DateTime WgtInDate = Convert.ToDateTime(DataHelper.ReadDataRow(row, "WgtInDate", ""));
                                DateTime WgtOutDate = Convert.ToDateTime(DataHelper.ReadDataRow(row, "WgtOutDate", ""));
                                string WgtInTime = Convert.ToDateTime(DataHelper.ReadDataRow(row, "WgtInTime", "")).ToString(@"hh\:mm\:ss tt");
                                string WgtOutTime = Convert.ToDateTime(DataHelper.ReadDataRow(row, "WgtOutTime", "")).ToString(@"hh\:mm\:ss tt");
                                string Operator1 = DataHelper.ReadDataRow(row, "Operator1", "");
                                string Operator2 = DataHelper.ReadDataRow(row, "Operator2", "");
                                string Remarks = DataHelper.ReadDataRow(row, "Operator2", "");
                                string TransType = DataHelper.ReadDataRow(row, "TransType", "");
                                string Dim1 = DataHelper.ReadDataRow(row, "Dim1", "");
                                string Dim2 = DataHelper.ReadDataRow(row, "Dim2", "");
                                string Dim3 = DataHelper.ReadDataRow(row, "Dim3", "");
                                string BinNo1 = DataHelper.ReadDataRow(row, "BinNo1", "");
                                string BinNo2 = DataHelper.ReadDataRow(row, "BinNo2", "");
                                string Cont1 = DataHelper.ReadDataRow(row, "Cont1", "");
                                string Cont2 = DataHelper.ReadDataRow(row, "Cont2", "");
                                string combination = DataHelper.ReadDataRow(row, "Combination", "");
                                int TORNo = DataHelper.ReadDataRow(row, "TORNo", 0);
                                decimal con1 = 0;
                                decimal con2 = 0;
                                if (!string.IsNullOrWhiteSpace(Cont1))
                                {
                                    if (Cont1 != "NO BIN")
                                    {
                                        con1 = Convert.ToDecimal(Cont1);
                                    }
                                }
                                else { con1 = 0; }
                                if (!string.IsNullOrWhiteSpace(Cont2)) { con2 = Convert.ToDecimal(Cont2); } else { con2 = 0; }

                                if (NetWt == 0) { NetWt = 1; }

                                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                // @@@@@@@@@@@@@  if transtype is not "FLEET", POST to SO            @@@@@@@@@@@@@ //  
                                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                                if (TransType != "FLEET")
                                {
                                    check = false;
                                    // @@@@@@@@@@@@@  POSTING SA SAP  @@@@@@@@@@@@@ //   
                                    var model = (dynamic)null;
                                    var lineModel = (dynamic)null;

                                    if (CustomerCode == "TEMP")
                                    {
                                        model = new List<OQUT>();
                                        lineModel = new List<QUT1>();

                                        lineModel.Add(new QUT1
                                        {
                                            ItemCode = "R0001",
                                            Quantity = NetWt,
                                            UomCode = "TON",
                                            TaxCode = "OT0",
                                            U_YPNo = YardPassNo
                                        });
                                        model.Add(new OQUT
                                        {
                                            CardCode = CustomerCode,
                                            U_Transporter = Transporter,
                                            U_Destination = OriginOfWaste,
                                            U_DRNo = DRNo,
                                            U_Driver = Driver,
                                            U_PlateNo = PlateNo,
                                            U_YPNo = YardPassNo,
                                            U_WasteType = TypeOfWaste,
                                            U_Length = Lengthh,
                                            U_Width = Width,
                                            U_Height = Height,
                                            U_Cont1 = Container.ToString(),
                                            U_Weight1 = Weight1,
                                            U_Weight2 = Weight2,
                                            U_NetWt = NetWt,
                                            U_Rem1 = Remarks,
                                            U_Origin = "Walk-in",

                                            U_WgtInDate = WgtInDate.AddDays(1),
                                            U_WgtOutDate = WgtOutDate.AddDays(1),
                                            U_WgtInTime = Convert.ToDateTime(WgtInTime).ToString("HH:mm"),
                                            U_WgtOutTime = Convert.ToDateTime(WgtOutTime).ToString("HH:mm"),

                                            U_Operator1 = Operator1,
                                            U_Operator2 = Operator2,

                                            U_WBNo = wbNo,

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
                                        decimal qty = Math.Round(NetWt, 2);
                                        decimal price = 0;
                                        string itemCode = "R0001";



                                        qry = $" select ISNULL(a.U_Price,0) U_Price, ISNULL(a.U_UOM,'') U_UOM, " +
                                                     $"(select k.UomEntry from ouom k where k.UomCode = a.U_Uom) UomEntry" +
                                                     $", ISNULL(a.U_Limit,0) U_Limit, " +
                                                     $" ISNULL(a.U_LimitType,'') U_LimitType, ISNULL(a.U_ExcessCharge,0) U_ExcessCharge, " +
                                                     $" isnull((select sum(x.Quantity) sumQty FROM ORDR z inner join " +
                                                     $" RDR1 x ON z.DocEntry = x.DocEntry AND z.CardCode = a.Code and CONCAT(MONTH(z.U_WgtOutDate), " +
                                                     $" YEAR(z.U_WgtOutDate))  = CONCAT(MONTH(GETDATE()),YEAR(GETDATE()))  ),0) sumQty, " +
                                                     $" ISNULL(U_Size,1) size " +
                                                     $" from [@OWT1] a where " +
                                                     $" a.Code = '{CustomerCode}' AND " +
                                                     $" a.U_Type = '{TypeOfWaste}' AND " +
                                                     $" a.U_ValidFrom <= '{WgtOutDate.ToShortDateString()}' " +
                                                     $" AND a.U_ValidTo >= '{WgtOutDate.ToShortDateString()}' AND " +
                                                     $" a.U_UOM IN ('CU.M','TON')  ";
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
                                                if ((sumQty + NetWt) >= limit)
                                                {
                                                    // ############### if per MONTH ang condition ############//
                                                    BasePrice = ExcessCharge;
                                                    qty = Math.Round(NetWt, 2);
                                                }
                                                else
                                                {
                                                    qty = Math.Round(NetWt, 2);
                                                }
                                            }
                                            else
                                            {
                                                if (UOMCode == "CU.M")
                                                {
                                                    qty = Convert.ToDecimal(con1);
                                                }
                                                else if (UOMCode == "TON")
                                                {
                                                    qty = NetWt;
                                                }
                                            }
                                        }



                                        // ############### if NetWt = 0 ############//
                                        if (Convert.ToDecimal(NetWt) == 0)
                                        {
                                            qty = 1;
                                            itemCode = "R0003";
                                            BasePrice = 15000;
                                            UOM = 25;
                                            NetWt = 0;
                                        }




                                        if (limitType.ToUpper() == "TON")
                                        {
                                            limit = (limit / size) * con1;
                                            // ############### if per TON ang condition ############//
                                            if (NetWt > limit)
                                            {
                                                // ############### if mas malaki ang NetWT vs Limit ############//
                                                decimal tempLimit = Math.Round((limit * con1), 2);
                                                decimal tempPrice = Math.Round(NetWt, 2) - Math.Round(tempLimit, 2);
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
                                                    U_YPNo = YardPassNo,
                                                    CostingCode = Dim1,
                                                    CostingCode2 = Dim2,
                                                    CostingCode3 = Dim3
                                                });


                                                lineModel.Add(new RDR1
                                                {
                                                    LineNum = 1,
                                                    ItemCode = itemCode,
                                                    //Quantity = netWt,
                                                    Quantity = NetWt - limit,
                                                    //UomCode = "TON",
                                                    UoMEntry = 4,
                                                    UnitPrice = Math.Round(ExcessCharge, 2),

                                                    TaxCode = "OT0",
                                                    U_YPNo = YardPassNo,
                                                    CostingCode = Dim1,
                                                    CostingCode2 = Dim2,
                                                    CostingCode3 = Dim3
                                                });

                                            }
                                            else
                                            {
                                                if (con1 == 0)
                                                {
                                                    qty = NetWt;
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
                                                    U_YPNo = YardPassNo,
                                                    CostingCode = Dim1,
                                                    CostingCode2 = Dim2,
                                                    CostingCode3 = Dim3
                                                });

                                            }
                                        }
                                        else
                                        {

                                            if (con1 == 0)
                                            {
                                                qty = NetWt;
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
                                                U_YPNo = YardPassNo,
                                                CostingCode = Dim1,
                                                CostingCode2 = Dim2,
                                                CostingCode3 = Dim3
                                            });
                                        }



                                        //lineModel.Add(new RDR1
                                        //{
                                        //    ItemCode = "R0001",
                                        //    Quantity = NetWt,
                                        //    UomCode = "TON",
                                        //    TaxCode = "OT0",
                                        //    U_YPNo = YardPassNo,
                                        //    CostingCode = Dim1,
                                        //    CostingCode2 = Dim2,
                                        //    CostingCode3 = Dim3
                                        //});



                                        model.Add(new ORDR
                                        {
                                            CardCode = CustomerCode,
                                            U_Transporter = Transporter,
                                            U_Destination = OriginOfWaste,
                                            U_DRNo = DRNo,
                                            U_Driver = Driver,
                                            U_PlateNo = PlateNo,
                                            U_YPNo = YardPassNo,
                                            U_WasteType = TypeOfWaste,
                                            U_Length = Lengthh,
                                            U_Width = Width,
                                            U_Height = Height,
                                            U_Cont1 = Container.ToString(),
                                            U_Weight1 = Weight1,
                                            U_Weight2 = Weight2,
                                            U_NetWt = NetWt,
                                            U_Rem1 = Remarks,
                                            U_Origin = "Walk-in",

                                            U_WgtInDate = WgtInDate.AddDays(1),
                                            U_WgtOutDate = WgtOutDate.AddDays(1),
                                            U_WgtInTime = Convert.ToDateTime(WgtInTime).ToString("HH:mm"),

                                            U_Operator1 = Operator1,
                                            U_Operator2 = Operator2,
                                            U_WgtOutTime = Convert.ToDateTime(WgtOutTime).ToString("HH:mm"),

                                            U_WBNo = wbNo,
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
                                        bool condition = orders.Post(model, CustomerCode.Equals("TEMP") ? "Quotations" : "Orders");
                                        if (condition)
                                        {
                                            removeLocalTransaction(wbNo.ToString());
                                            //MessageBox.Show("Transaction uploaded to SAP.");
                                            //Dispose();
                                        }
                                        else
                                        {
                                            //MessageBox.Show("Updated in Local but cannot upload to SAP due to er. Please contact administrator.");
                                            //Dispose();
                                            check = true;
                                        }
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Updated in Local but cannot upload to SAP due to error connection. Please contact administrator.");
                                        //Dispose();
                                        check = true;
                                    }


                                }

                                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                // @@@@@@@@@@@@@  if transtype is     "FLEET", PATCH SO              @@@@@@@@@@@@@ //  
                                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 
                                else
                                {
                                    var model = new List<ORDR>();
                                    var lineModel = new List<RDR1>();
                                    check = false;

                                    string itemCode = "R0001";

                                    int ctr = 0;
                                    decimal binSize1 = 0;
                                    decimal binSize2 = 0;
                                    decimal totalBin = 0;


                                    if (Cont1 == "NO BIN")
                                    {
                                        binSize1 = 0;
                                        binSize2 = 0;
                                    }
                                    else
                                    {
                                        binSize1 = con1;
                                        binSize2 = con2;

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
                                    decimal qty = Math.Round(NetWt, 2);
                                    decimal basePrice = 0;


                                    // ############### if NetWt = 0 ############//
                                    if (NetWt == 0)
                                    {
                                        qty = 1;
                                        itemCode = "R0003";
                                        price = 15000;
                                        UOM = 25;
                                        NetWt = 0;

                                        lineModel.Add(new RDR1
                                        {
                                            LineNum = 0,
                                            ItemCode = itemCode,
                                            //Quantity = netWt,
                                            Quantity = 1,
                                            UoMEntry = 25,
                                            UnitPrice = Math.Round(price, 2),
                                            U_YPNo = YardPassNo,
                                            TaxCode = "OT0",
                                            CostingCode = Dim1,
                                            CostingCode2 = Dim2,
                                            CostingCode3 = Dim3
                                        });
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(Cont1))
                                        {
                                            if (Cont1 != "NO BIN")
                                            {
                                                DataTable dtPrice = dtPricing(Cont1, CustomerCode, TypeOfWaste,
                                                            WgtOutDate.ToShortDateString(), WgtOutDate.ToShortDateString());
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
                                                        if ((sumQty + NetWt) >= limit)
                                                        {
                                                            price = ExcessCharge;
                                                        }

                                                        qty = NetWt;


                                                    }
                                                    else
                                                    {
                                                        if (UOMCode == "CU.M")
                                                        {
                                                            qty = binSize1;
                                                        }
                                                        else if (UOMCode == "TON")
                                                        {
                                                            qty = NetWt;
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
                                                        U_YPNo = YardPassNo,
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
                                                           $" a.Code = '{CustomerCode}' AND " +
                                                           $" a.U_Type = '{TypeOfWaste}' AND " +
                                                           $" a.U_ValidFrom <= '{WgtOutDate.ToShortDateString()}' " +
                                                           $" AND a.U_ValidTo >= '{WgtOutDate.ToShortDateString()}'";
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
                                                            if ((sumQty + NetWt) >= limit)
                                                            {
                                                                price = ExcessCharge;
                                                            }

                                                            qty = NetWt;


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
                                                                qty = NetWt;
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
                                                        U_YPNo = YardPassNo,
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
                                                         $" a.Code = '{CustomerCode}' AND " +
                                                         $" a.U_Type = '{TypeOfWaste}' AND " +
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
                                                    U_YPNo = YardPassNo,
                                                    CostingCode = Dim1,
                                                    CostingCode2 = Dim2,
                                                    CostingCode3 = Dim3
                                                });
                                                ctr = ctr + 1;
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(Cont2))
                                        {
                                            if (Cont2 != "NO BIN")
                                            {
                                                DataTable dtPrice = dtPricing(Cont2, CustomerCode, TypeOfWaste,
                                                            WgtOutDate.ToShortDateString(), WgtOutDate.ToShortDateString());
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
                                                        if ((sumQty + NetWt) >= limit)
                                                        {
                                                            price = ExcessCharge;
                                                        }

                                                        qty = NetWt;


                                                    }
                                                    else
                                                    {
                                                        if (UOMCode == "CU.M")
                                                        {
                                                            qty = totalBin;
                                                        }
                                                        else if (UOMCode == "TON")
                                                        {
                                                            qty = NetWt;
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
                                                        U_YPNo = YardPassNo,
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
                                                           $" a.Code = '{CustomerCode}' AND " +
                                                           $" a.U_Type = '{TypeOfWaste}' AND " +
                                                           $" a.U_ValidFrom <= '{WgtOutDate.ToShortDateString()}' " +
                                                           $" AND a.U_ValidTo >= '{WgtOutDate.ToShortDateString()}'";
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



                                                        if (UOMCode != "TON")
                                                        {
                                                            if (limitType.ToUpper() == "MONTH")
                                                            {
                                                                // ############### if per MONTH ang condition ############//
                                                                if ((sumQty + NetWt) >= limit)
                                                                {
                                                                    price = ExcessCharge;
                                                                }

                                                                qty = NetWt;


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
                                                                    qty = NetWt;
                                                                }
                                                                else
                                                                {
                                                                    qty = 1;
                                                                }
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
                                                        U_YPNo = YardPassNo,
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
                                                         $" a.Code = '{CustomerCode}' AND " +
                                                         $" a.U_Type = '{TypeOfWaste}' AND " +
                                                         $" a.U_ValidFrom <= '{WgtOutDate.ToShortDateString()}' " +
                                                         $" AND a.U_ValidTo >= '{WgtOutDate.ToShortDateString()}'";
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
                                                    U_YPNo = YardPassNo,
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
                                            if (NetWt > limit)
                                            {

                                                // ############### if mas malaki ang NetWT vs Limit ############//
                                                decimal tempLimit = Math.Round((limit * totalBin), 2);
                                                decimal tempPrice = Math.Round(NetWt, 2) - Math.Round(tempLimit, 2);
                                                decimal tempPriceWithCharge = (Math.Abs(tempPrice) * ExcessCharge);
                                                price = (price * Math.Round(totalBin, 2)) + Math.Round(tempPriceWithCharge, 2);
                                                qty = totalBin;


                                                lineModel.Add(new RDR1
                                                {
                                                    LineNum = ctr,
                                                    ItemCode = "R0001",
                                                    //Quantity = netWt,
                                                    Quantity = NetWt - limit,
                                                    UoMEntry = 4,
                                                    UnitPrice = Math.Round(ExcessCharge, 2),
                                                    U_YPNo = YardPassNo,
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
                                        U_Weight1 = Weight1,
                                        U_Weight2 = Weight2,
                                        U_NetWt = NetWt,
                                        U_BinNo1 = BinNo1,
                                        U_BinNo2 = BinNo2,
                                        U_Cont1 = Cont1,
                                        U_Cont2 = Cont2,
                                        U_YPNo = YardPassNo,
                                        U_TORNo = TORNo,
                                        U_Combination = combination,

                                        U_WgtInDate = WgtInDate.AddDays(1),
                                        U_WgtOutDate = WgtOutDate.AddDays(1),
                                        U_Operator1 = Operator1,
                                        U_Operator2 = Operator2,
                                        U_WasteType = TypeOfWaste,
                                        U_Rem1 = Remarks,

                                        U_WgtInTime = Convert.ToDateTime(WgtInTime).ToString("HH:mm"),
                                        U_WgtOutTime = Convert.ToDateTime(WgtOutTime).ToString("HH:mm"),
                                        U_WBNo = wbNo,
                                        Comments = $"Created by Weighbridge (DCS) | Data Transfer : Fleet Travel Order Transactions  : {DateTime.Now} | Powered By : DIREC",

                                        DocumentLines = lineModel
                                    });

                                    qry = $"SELECT TOP 1 DocEntry FROM ORDR WHERE U_TORNo = '{TORNo}'  ";
                                    DataTable dtDocEntry = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                                             qry);

                                    if (DataHelper.DataTableExist(dtDocEntry))
                                    {
                                        string DocEntry = DataHelper.DataTableRet(dtDocEntry, 0, "DocEntry", "0");

                                        Orders orders = new Orders();
                                        if (orders.Patch(model, Convert.ToInt32(DocEntry)))
                                        {
                                            removeLocalTransaction(wbNo.ToString());
                                        }
                                        else
                                        {
                                            check = true;
                                        }
                                    }



                                }

                            }
                        }
                        else
                        {
                            removeLocalTransaction(wbNo.ToString());
                        }
                    }

                    if (check)
                    {
                        MessageBox.Show("An error occured while syncing LOCAL transactions to SAP. Please contact Administrator.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void removeLocalTransaction(string wbNO)
        {
            string qry = $"DELETE ODOC WHERE WBNo = '{wbNO}'";
            MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"), qry);
        }


        void SyncPrice()
        {
            try
            {
                string query = "SELECT A.* FROM [@OWT1] A ";
                DataTable tbTruckWeight = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   query);

                if (tbTruckWeight.Rows.Count > 0)
                {
                    foreach (DataRow row in tbTruckWeight.Rows)
                    {
                        string code = DataHelper.ReadDataRow(row, "Code", "");
                        string type = DataHelper.ReadDataRow(row, "U_Type", "");
                        decimal price = Convert.ToDecimal(DataHelper.ReadDataRow(row, "U_Price", ""));
                        string uom = DataHelper.ReadDataRow(row, "U_uom", "");
                        string validFrom = DataHelper.ReadDataRow(row, "U_validFrom", "");
                        string validTo = DataHelper.ReadDataRow(row, "U_validTo", "");
                        string limit = DataHelper.ReadDataRow(row, "U_Limit", "");

                        string qry = $" SELECT TOP 1 'True' FROM PRICE WHERE Code = '{code}' AND Type ='{type}' AND " +
                                     $" pricee = {price} AND uom = '{uom}' and validFrom = '{validFrom}' and validTo = '{validTo}' and " +
                                     $" limit = '{limit}' ";
                        DataTable tbExist = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), qry);

                        if (tbExist.Rows.Count > 0)
                        {
                            query = $"UPDATE TRUCK_WEIGHT SET Code = '{code}', Type = '{type}', price = {price}, uom = '{uom}', " +
                                    $" validFrom = '{validFrom}', validTo = '{validTo}', limit = '{limit}' " +
                                    $" WHERE Code = '{code}' AND type = '{type}' AND UOM = '{uom}' and " +
                                    $" validFrom = '{validFrom}' AND validTo = '{validTo}'";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);
                        }
                        else
                        {
                            query = $"INSERT INTO PRICE VALUES ('{code}', '{type}', {price}, '{uom}', '{validFrom}', '{validTo}', '{limit}')";
                            MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"), query);

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
