using MCWM.Views.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCWM.Helper
{
    public class PublicStatic
    {
        public static frmMain _frmMain { get; set; }
        public static bool IsConnectedToServer { get; set; }
    }
}
