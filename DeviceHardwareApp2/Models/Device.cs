using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeviceHardwareApp2.Models
{
 
    public enum CriticalRating
    {
        A, B, C, D, F
    }

    public enum DeviceType
    {
        Server, Workstation, Printer, Switch, Router, Copier
    }

    public class Device
    {
        public int DeviceID { get; set; }

        [DisplayName("Department ID")]
        public int DepartmentID { get; set; }

        [DisplayName("User (User Name)")]
        public int UserID { get; set; }

        [Required]
        [DisplayName("Inventory Number")]
        public int InventoryNumber { get; set; }

        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        [DisplayName("Serial Number")]
        public string SerialNumber { get; set; }
        [DisplayName("Service Tag")]
        public string ServiceTag { get; set; }
        [DisplayName("Wall Jack")]
        public string WallJack { get; set; }
        [DisplayName("Switch Port")]
        public string SwitchPortNumber { get; set; }
        public bool Active { get; set; }
        [DisplayName("Operating System")]
        public string OperatingSystem { get; set; }
        public string Notes { get; set; }
        [DisplayName("Critcal Rating")]
        public CriticalRating? CriticalRating { get; set; }
        public DeviceType Type { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }

    }
    
}