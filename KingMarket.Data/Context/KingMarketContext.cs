using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Model.Models;

namespace KingMarket.Data.Context
{
    public class KingMarketContext : DbContext
    {
        public KingMarketContext() : base("name=KingMarketContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new KingMarketSeedData());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public DbSet<ClassDocumentType> ClassDocumentTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<CustomerContact> CustomerContacts { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<UnitMeasure> UnitMeasures { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<SupplierContact> SupplierContacts { get; set; }

        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
    }
}
