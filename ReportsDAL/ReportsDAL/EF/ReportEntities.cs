namespace ReportsDAL.EF
{
    using System;
    using ReportsDAL.Models;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using ReportsDAL.Models.Base;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;

    public partial class ReportEntities : DbContext
    {
        

        public ReportEntities() : base("name=ReportEntities")
        {


            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Executor> Executors { get; set; }
        public virtual DbSet<GrantAgreement> GrantAgreements { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<AmountOfCreatedReports> AmountOfCreatedReports { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Executor>().HasMany(e => e.Clients);
            modelBuilder.Entity<GrantAgreement>().HasMany(e => e.Directions);
            modelBuilder.Entity<Direction>().HasMany(e => e.Services);
            modelBuilder.Entity<Contract>().HasMany(e => e.GrantAgreements);
            modelBuilder.Entity<Executor>().HasMany(e => e.Contracts);


            modelBuilder.Entity<GrantAgreement>()
               .Property(e => e.Timestamp)
               .IsFixedLength();

            modelBuilder.Entity<Direction>()
               .Property(e => e.Timestamp)
               .IsFixedLength();

            modelBuilder.Entity<AmountOfCreatedReports>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Contract>()
               .Property(e => e.Timestamp)
               .IsFixedLength();

            modelBuilder.Entity<Executor>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Report>()
                .Property(e => e.Timestamp)
                .IsFixedLength();
        }

        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var model = (e.Entity as EntityBase);
            if (model != null)
            {
                model._IsChanged = false;
            }
        }
    }
}
