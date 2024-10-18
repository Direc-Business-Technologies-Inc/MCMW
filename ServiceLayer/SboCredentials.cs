using DirecLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class SboCredentials
    {
        public string ServiceLayer { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public SboCredentials()
        {
            ServiceLayer = AppConfig.AppSettings("ServiceLayer");
            CompanyDB = AppConfig.AppSettings("CompanyDB");
            UserName = AppConfig.AppSettings("UserName");
            Password = AppConfig.AppSettings("Password");
        }
    }
}
