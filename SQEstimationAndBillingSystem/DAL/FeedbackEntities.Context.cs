//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQEstimationAndBillingSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SQEstimationAndBillingSystemDbEntities : DbContext
    {
        public SQEstimationAndBillingSystemDbEntities()
            : base("name=SQEstimationAndBillingSystemDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<M_Roles> M_Roles { get; set; }
        public virtual DbSet<M_Users> M_Users { get; set; }
    }
}
