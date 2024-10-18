using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.SAO_DATABASE
{
    [Table("BIN")]
    public class BinNo
    {
        public int Id { get; set; }
        public string BinNo_ { get; set; }
        public string BinSize { get; set; }
    }
}
