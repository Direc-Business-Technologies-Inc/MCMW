using System;
using System.Windows.Forms;

namespace MCWM.Views.Main
{
    public interface IfrmMain
    {
        event EventHandler _Main_newTravelOrderToolStripMenuItem_Click;
        Form mainForm { get; }
        void CloseForm();
        void DisposeForm();
        void HideForm();
        void NotiMsg();
        void NotiMsg(string message);
        void ShowForm();
    }
}