using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceHardwareApp2.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<User> Users { get; set; }  // to link one dept with many users
    }
}