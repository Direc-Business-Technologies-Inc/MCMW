using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.SAO_DATABASE
{
    [Table("CUFD")]
    public class UserDefined
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AliasID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
