using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainLayer.SAO_DATABASE
{
    [Table("OOTW")]
    public class OneTimeWeigh
    {
        public int Id { get; set; }
        public decimal Weight1 { get; set; }
        public decimal Weight2 { get; set; }
        public decimal netWt { get; set; }
        public string clientName { get; set; }
        public string WgtInDate { get; set; }
        public string WgtInTime { get; set; }
        public string WgtOutDate { get; set; }
        public string WgtOutTime { get; set; }
    }
}
