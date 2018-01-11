namespace DeviceHardwareApp2.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DeviceHardwareApp2.DAL.DeviceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DeviceHardwareApp2.DAL.DeviceContext context)
        {
            var users = new List<User>
            {
            new User{FirstMidName="Carson",LastName="Alexander",UserName="calexander", Position="CSR", PhoneNumber="", EmployeeID=1202, DepartmentID=3 },
            new User{FirstMidName="Meredith",LastName="Alonso",UserName="malonso", Position="Clerk", PhoneNumber="5121", EmployeeID=1422, DepartmentID=2 },
            new User{FirstMidName="Arturo",LastName="Anand", UserName="aanand", Position="Graphic Designer", PhoneNumber="2978", EmployeeID=1852, DepartmentID=4 }
            };
            users.ForEach(s => context.Users.AddOrUpdate(u => u.EmployeeID, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
            new Department{Name="IT", Location="Offices"},
            new Department{Name="Accounting", Location="Offices"},
            new Department{Name="Call Center", Location="Call Center"},
            new Department{Name="Layout", Location="Layout Room"},
            new Department{Name="Wholesale", Location="Warehouse"}
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(u => u.Name, s));
            context.SaveChanges();

            var devices = new List<Device>
            {
            new Device{ DepartmentID=4, UserID=4, Name="Mac-Mini-OSX", Type=DeviceType.Server, Active=true, CriticalRating=CriticalRating.B, InventoryNumber=801, IP="10.1.2.150", Manufacturer="Apple", Model="Mac Mini", OperatingSystem="OSX" },
            new Device{ DepartmentID=2, UserID=2, Name="JHOVANEC2IT-W7P", Type=DeviceType.Workstation, Active=true, CriticalRating=CriticalRating.F, InventoryNumber=888, IP="10.1.1.125", Manufacturer="Dell", Model="Vostro 230", OperatingSystem="Windows 7 Pro" },
            new Device{ DepartmentID=1, UserID=1, Name="DAEDvRtl-W12", Type=DeviceType.Server, Active=true, CriticalRating=CriticalRating.B, InventoryNumber=832, IP="10.1.2.166", Manufacturer="Dell", Model="PowerEdge 720", OperatingSystem="Windows Server 2012 R2" }
            };
            devices.ForEach(s => context.Devices.AddOrUpdate(s));
            context.SaveChanges();

            foreach (Device d in devices)
            {
                var deviceInDataBase = context.Devices.Where(
                    s =>
                         s.User.ID == d.UserID &&
                         s.Department.DepartmentID == d.DepartmentID).SingleOrDefault();
                if (deviceInDataBase == null)
                {
                    context.Devices.Add(d);
                }
            }
            context.SaveChanges();
        }
    }
}
