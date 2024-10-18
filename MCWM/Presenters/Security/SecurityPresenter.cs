using MCWM.Presenters.Main;
using MCWM.Presenters.TravelOrder;
using MCWM.Services.Security;
using MCWM.Views.Main;
using MCWM.Views.Security;
using MCWM.Views.TravelOrder;
using System;

namespace MCWM.Presenters.Security
{
    public class SecurityPresenter : ISecurityPresenter
    {
        IfrmLogin _frmLogin;
        ILoginService _LoginService;
        IfrmMain _frmMain;
        IfrmTravelOrders _frmTravelOrders;

        public SecurityPresenter(IfrmLogin frmLogin,
                                ILoginService loginService,
                                IfrmMain frmMain,
                                IMainPresenter mainPresenter,
                                IfrmTravelOrders frmTravelOrders,
                                ITravelOrderPresenter travelOrderPresenter)
        {
            _frmLogin = frmLogin;
            _LoginService = loginService;

            _frmMain = frmMain;

            mainPresenter._frmMain = frmMain;
            mainPresenter.RegisterEvents();

            _frmTravelOrders = frmTravelOrders;
            //travelOrderPresenter._IfrmTravelOrders = frmTravelOrders;


            RegisterEvents();
        }

        public IfrmLogin GetMainForm() { return _frmLogin; }

        void RegisterEvents()
        {
            #region Login Register Events
            _frmLogin._Login_btnLogin_Click += new EventHandler(Login_btnLogin_Click); 
            #endregion
        }

        #region Login Events
        private void Login_btnLogin_Click(object sender, EventArgs e)
        {
            if (_LoginService.LoginViaLocalDatabase(_frmLogin._TextBoxUsername, _frmLogin._TextBoxPassword, out string err))
            {
                _frmLogin.HideForm();
                _frmMain.ShowForm();
            }
            else
            {
                _frmLogin.NotiMsg(err);
            }
        }
        #endregion
    }
}
