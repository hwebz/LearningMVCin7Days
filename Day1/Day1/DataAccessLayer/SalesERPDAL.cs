using System.Data.Entity;
using Day1.Models;

namespace Day1.DataAccessLayer
{
    public class SalesERPDAL : DbContext
    {

        //public SalesERPDAL() : base("newName") // If you want to change the name in connectionString to another name
        //{
            
        //}

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
    }
}