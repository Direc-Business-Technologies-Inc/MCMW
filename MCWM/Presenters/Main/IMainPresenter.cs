using MCWM.Views.Main;

namespace MCWM.Presenters.Main
{
    public interface IMainPresenter
    {
        IfrmMain _frmMain { get; set; }

        void RegisterEvents();
    }
}