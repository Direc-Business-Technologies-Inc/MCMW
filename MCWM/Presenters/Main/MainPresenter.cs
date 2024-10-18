using MCWM.Presenters.TravelOrder;
using MCWM.Views;
using MCWM.Views.Main;
using MCWM.Views.TravelOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCWM.Presenters.Main
{
    public class MainPresenter : IMainPresenter
    {
        public IfrmMain _frmMain { get; set; }

        public void RegisterEvents()
        {
            #region Main Register Events
            //_frmMain._Main_newTravelOrderToolStripMenuItem_Click += new EventHandler(Main_newTravelOrderToolStripMenuItem_Click);
            #endregion
        }

        #region Main Events
        //private void Main_newTravelOrderToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmTravelOrders form = new frmTravelOrders();
        //    new TravelOrderPresenter(form);
        //    form.MdiParent = _frmMain.mainForm;
        //    form.Show();
        //}
        #endregion
    }
}
