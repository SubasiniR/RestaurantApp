using Restaurant.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Restaurant.DAL
{
    public class RestaurantContext : DbContext
    {

        public RestaurantContext() : base("RestaurantContext")
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Table>()
            //    .HasKey(t => t.TableID);

            //modelBuilder.Entity<Customer>()
            //    .HasRequired(c => c.TableID);
        }
    }
}