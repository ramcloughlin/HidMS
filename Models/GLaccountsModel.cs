namespace hidMy.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using hidMy.Models;


    public partial class GLaccountsModel : DbContext
    {
        public GLaccountsModel()
            : base("name=GLaccountsModel")
        {
            Database.SetInitializer<GLaccountsModel>(null);
        }

        public virtual DbSet<GLaccounts> GLaccounts { get; set; }
        public virtual DbSet<GLLeglislation> GLLeglislation { get; set; }

        public virtual DbSet<GLAspectsandElements> GLAspectsandElements { get; set; }

        public virtual DbSet<InspectionDetails> InspectionDetails { get; set;}

        public virtual DbSet<NationalPremisesRegister> NationalPremisesRegister { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GLaccounts>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            
    
            modelBuilder.Entity<GLaccounts>()
                .HasMany(e => e.GLAspectsandElements)
                .WithOptional(e => e.GLaccounts)
                .HasForeignKey(e => e.LeglislationLINK);

            modelBuilder.Entity<NationalPremisesRegister>()
                .HasMany(e => e.InspectionDetails)
                .WithOptional(e => e.NationalPremisesRegister)
                .HasForeignKey(e => e.PremisesRef);

            //modelBuilder.Entity<GLaccounts>()
            //    .ToTable("Contas");
        }

        public System.Data.Entity.DbSet<hidMy.Models.BusinessCategoryType> BusinessCategoryTypes { get; set; }

        //public System.Data.Entity.DbSet<hidMy.Models.GLLeglislation> GLLeglislation { get; set; }
    }
}
