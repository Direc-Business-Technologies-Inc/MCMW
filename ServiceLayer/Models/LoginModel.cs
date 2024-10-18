using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class LoginModel : ILoginModel
    {
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public interface ILoginModel
    {
        string CompanyDB { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
