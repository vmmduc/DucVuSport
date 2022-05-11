using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLib.Entities
{
    public partial class dataContext : DbContext
    {
        public dataContext()
            : base("name=dataContext")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<tbUser> tbUsers { get; set; }
        public virtual DbSet<tbRole> tbRoles { get; set; }
        public virtual DbSet<tbUserRole> tbUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.img)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbUser>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.tbUser)
                .HasForeignKey(e => e.userID)
                .WillCascadeOnDelete(false);
        }
    }
}
