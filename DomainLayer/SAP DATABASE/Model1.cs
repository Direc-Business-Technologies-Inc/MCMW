namespace DomainLayer.SAP_DATABASE
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=TRUCK_EXTERNAL")
        {
        }

        public virtual DbSet<C_TRUCK_EXTERNAL> C_TRUCK_EXTERNAL { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
