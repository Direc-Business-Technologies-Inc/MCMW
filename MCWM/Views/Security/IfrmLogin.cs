using System;

namespace MCWM.Views.Security
{
    public interface IfrmLogin
    {
        event EventHandler _Login_btnLogin_Click;
        void CloseForm();
        void DisposeForm();
        void HideForm();
        void ShowForm();
        void NotiMsg();
        void NotiMsg(string message);
        string _TextBoxUsername { get; set; }
        string _TextBoxPassword { get; set; }
    }
}