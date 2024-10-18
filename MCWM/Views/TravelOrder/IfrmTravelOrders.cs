using System;
using System.Windows.Forms;

namespace MCWM.Views.TravelOrder
{
    public interface IfrmTravelOrders
    {
        event EventHandler _Main_newTravelOrderToolStripMenuItem_Click;
        void CloseForm();
        void DisposeForm();
        void HideForm();
        void NotiMsg();
        void NotiMsg(string message);
        void ShowForm();

    }
}