using MCWM.Views.TravelOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCWM.Presenters.TravelOrder
{
    public class TravelOrderPresenter : ITravelOrderPresenter
    {
        IfrmTravelOrders _IfrmTravelOrders;

        public TravelOrderPresenter(IfrmTravelOrders frmTravelOrders)
        {
            _IfrmTravelOrders = frmTravelOrders;
        }

        public void RegisterEvents()    
        {
            #region Main Register Events
            //_IfrmTravelOrders._Main_newTravelOrderToolStripMenuItem_Click += new EventHandler(Main_newTravelOrderToolStripMenuItem_Click);
            #endregion
        }
    }
}
