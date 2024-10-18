using DirecLayer;
using MCWM.Views.Main;
using MCWM.Views.TravelOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MCWM.Views.Tools
{
    public partial class frmSearch : Form
    {
        private string DocNum;
        private string oTransType;
        public string TransNo { get { return DocNum; } set { DocNum = value; } }
        public string TransType { get { return oTransType; } set { oTransType = value; } }
        public string tag;
        private static int _colIndex = 0;
        private static int _rowIndex = 0;
        private static string _columnName = "";

        public frmSearch(string tagLoad)
        {
            InitializeComponent();
            tag = tagLoad;
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                loadDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadDGV()
        {
            string qryAllOpenOrders;
            DataTable dtAllOpenOrders;
            if (tag == "internal")
            {
                qryAllOpenOrders = " SELECT A.U_TORNo [TOR No.] ,A.U_WBNo [Transaction No.], A.DocNum, A.CardCode, A.CardName, A.U_DRNo FROM ORDR A WHERE DocStatus = 'O' AND ISNULL(A.U_WBNo,'') = '' AND ISNULL(U_TorNo,'') <> ''";
                dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                               qryAllOpenOrders);
            }
            else if (tag == "external")
            {
                //qryAllOpenOrders = "SELECT U_WBNo [Transaction No.], CardName [Customer Name], U_DRNo [DR No.], U_PlateNo [Plate No.], U_YPNo [Yard Pass No.]  FROM ORDR " +
                //                   " WHERE DocStatus = 'O' AND ISNULL(U_WBNo,0) <> 0 AND ISNULL(U_Weight2,0) = 0 and ISNULL(U_Weight1,0) <> 0" +
                //                   " UNION ALL " +
                //                   " SELECT U_WBNo [Transaction No.], CardName [Customer Name], U_DRNo [DR No.], U_PlateNo [Plate No.], U_YPNo [Yard Pass No.]  FROM OQUT " +
                //                   " WHERE DocStatus = 'O' AND ISNULL(U_WBNo,0) <> 0 AND ISNULL(U_Weight2,0) = 0 and ISNULL(U_Weight1,0) <> 0 ";
                qryAllOpenOrders = "SELECT WBNo [Transaction No.], CustomerName [Customer Name], DRNo [DR No.], PlateNo [Plate No.], YardPassNo [Yard Pass No.] " +
                                     " FROM ODOC WHERE ISNULL(Weight2,0) = 0  ORDER BY CAST(WBNo AS INT) DESC ";
                dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                               qryAllOpenOrders);

            }
            else
            {
                qryAllOpenOrders = "SELECT ID, clientName [Client Name], WgtInDate [Weight In Date], WgtInTime [Weight In Time] FROM OOTW WHERE ISNULL(WgtOutDate,'') = '' ";
                dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                               qryAllOpenOrders);
            }


            dgvSearch.DataSource = dtAllOpenOrders;
        }

        private void dgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Choose();
        }

        void Choose()
        {
            try
            {
                int rowindex = dgvSearch.CurrentCell.RowIndex;

                if (rowindex != -1)
                {
                    TransNo = dgvSearch.Rows[rowindex].Cells[0].Value.ToString();
                }

                this.Dispose();
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




        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Choose();
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    loadDGV();
                }
                else
                {
                    int columnIndex = dgvSearch.CurrentCell.ColumnIndex;

                    {
                        foreach (DataGridViewRow row in dgvSearch.Rows)
                        {
                            if (row.Cells[_colIndex].Value.ToString().ToUpper().Contains(txtSearch.Text.ToUpper()))
                            {
                                row.Selected = true;
                                _rowIndex = row.Index;
                                dgvSearch.FirstDisplayedScrollingRowIndex = _rowIndex;
                                break;
                            }
                            else
                            {
                                row.Selected = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSearch_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _colIndex = e.ColumnIndex;
            _columnName = dgvSearch.Columns[_colIndex].Name;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                int columnIndex = dgvSearch.CurrentCell.ColumnIndex;
                _columnName = dgvSearch.Columns[_colIndex].Name;

                //POPULAT DGV WITH PROPER FILTERING 
                string qryAllOpenOrders;
                DataTable dtAllOpenOrders;
                string fltrColumn = "";


                if (tag == "internal")
                {
                    if (_columnName == "Transaction No.") { fltrColumn = "U_WBNo"; }
                    else if (_columnName == "TOR No.") { fltrColumn = "U_TORNo"; }
                    else if (_columnName == "DR No.") { fltrColumn = "U_DRNo"; }
                    else if (_columnName == "Customer Code") { fltrColumn = "CardCode"; }
                    else if (_columnName == "Customer Name") { fltrColumn = "CardName"; }
                    else fltrColumn = _columnName;

                    qryAllOpenOrders = " SELECT A.U_TORNo [TOR No.] ,A.U_WBNo [Transaction No.], A.DocNum, A.CardCode [Customer Code], A.CardName [Customer Name], A.U_DRNo [DR No.]" +
                        $"FROM ORDR A WHERE DocStatus = 'O' AND ISNULL(A.U_WBNo,'') = '' AND ISNULL(U_TorNo,'') <> '' " +
                        $"AND {fltrColumn} like '%{txtSearch.Text}%'";
                    dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("SAP"),
                                                   qryAllOpenOrders);
                }
                else if (tag == "external")
                {
                    if (_columnName == "Transaction No.") { fltrColumn = "WBNo"; }
                    else if (_columnName == "Customer Name") { fltrColumn = "CustomerName"; }
                    else if (_columnName == "DR No.") { fltrColumn = "DRNo"; }
                    else if (_columnName == "Plate No.") { fltrColumn = "PlateNo"; }
                    else if (_columnName == "Yard Pass No.") { fltrColumn = "YardPassNo"; }
                    else fltrColumn = _columnName;



                    qryAllOpenOrders = "SELECT WBNo [Transaction No.], CustomerName [Customer Name], DRNo [DR No.], PlateNo [Plate No.], YardPassNo [Yard Pass No.] FROM ODOC WHERE" +
                        $" {fltrColumn} LIKE '%{txtSearch.Text}%' ORDER BY WBNo DESC ";

                    dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                      qryAllOpenOrders);

                }
                else
                {
                    if (_columnName == "Client Name") { fltrColumn = "clientName"; }
                    else if (_columnName == "Weight In Date") { fltrColumn = "WgtInDate"; }
                    else if (_columnName == "Weight In Time") { fltrColumn = "WgtInTime"; }
                    else fltrColumn = _columnName;

                    qryAllOpenOrders = $"SELECT ID, clientName [Client Name], WgtInDate [Weight In Date], WgtInTime [Weight In Time]" +
                        $" FROM OOTW WHERE ISNULL(WgtOutDate,'') = '' AND " +
                        $" {fltrColumn} LIKE '%{txtSearch.Text}%' ";


                    dtAllOpenOrders = MsSqlAccess.Get(MsSqlAccess.ConnectionString("Addon"),
                                                    qryAllOpenOrders);
                }
                dgvSearch.DataSource = dtAllOpenOrders;


                //SELECT THE MOST ACCURATE ROW
                if (dgvSearch.Columns.Count > 1)
                {
                    foreach (DataGridViewRow row in dgvSearch.Rows)
                    {
                        if (row.Cells[_colIndex].Value.ToString().ToUpper().Contains(txtSearch.Text.ToUpper()))
                        {

                            row.Selected = true;
                            _rowIndex = row.Index;
                            dgvSearch.FirstDisplayedScrollingRowIndex = _rowIndex;

                            break;
                        }

                        else
                        {
                            row.Selected = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Choose();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    loadDGV();
                }
                else
                {
                    btnFind.PerformClick();
                }
            }
        }
    }
}
