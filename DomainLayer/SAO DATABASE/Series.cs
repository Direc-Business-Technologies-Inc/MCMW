using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.SAO_DATABASE
{

    [Table("SERIES")]

    public class Series
    {
        public int Id { get; set; }
        public string WBNo { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
