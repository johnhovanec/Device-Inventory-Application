using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceHardwareApp2.Models
{
    public class User
    {
        public int ID { get; set; }

        public int? DepartmentID { get; set; }

        [Required]
        [DisplayName("User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstMidName { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual Department Department { get; set; }  // Adding to try and reference users by dept
    }
}
