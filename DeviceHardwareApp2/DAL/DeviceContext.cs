using DeviceHardwareApp2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceHardwareApp2.DAL
{
    public class DeviceContext : DbContext
    {
        public DeviceContext() : base("DeviceContext")      // conn string name
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //for one to many department|user relationship
            modelBuilder.Entity<User>()
            .HasOptional(i => i.Department)
            .WithMany(pc => pc.Users)
            .HasForeignKey(i => i.DepartmentID);
        }
    }
}
