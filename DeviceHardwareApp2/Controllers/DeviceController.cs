using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeviceHardwareApp2.DAL;
using DeviceHardwareApp2.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace DeviceHardwareApp2.Controllers
{
    // To restrict access in this controller to only users in the Tech group
    [Authorize(Roles = "DAEDALUS\\Tech")]
    public class DeviceController : Controller
    {
        private DeviceContext db = new DeviceContext();

        // GET: Device
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.InvNumberSortParm = String.IsNullOrEmpty(sortOrder) ? "invnumber_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.IPSortParm = sortOrder == "IP" ? "ip_desc" : "IP";
            ViewBag.DeptNameSortParm = sortOrder == "Department" ? "department_desc" : "Department";
            ViewBag.UserNameSortParm = sortOrder == "User" ? "user_desc" : "User";
            ViewBag.ManufacturerSortParm = sortOrder == "Manufacturer" ? "manufacturer_desc" : "Manufacturer";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "model_desc" : "Model";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.ActiveSortParm = sortOrder == "Active" ? "active_desc" : "Active";
            ViewBag.OSSortParm = sortOrder == "OS" ? "os_desc" : "OS";
            ViewBag.NotesSortParm = sortOrder == "Notes" ? "notes_desc" : "Notes";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var devices = from d in db.Devices
                        select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                devices = devices.Where(d => d.Name.Contains(searchString)
                                       || d.IP.Contains(searchString)
                                       || d.User.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "invnumber_desc":
                    devices = devices.OrderByDescending(d => d.InventoryNumber);
                    break;
                case "Name":
                    devices = devices.OrderBy(d => d.Name);
                    break;
                case "name_desc":
                    devices = devices.OrderByDescending(d => d.Name);
                    break;
                case "IP":
                    devices = devices.OrderBy(d => d.IP);
                    break;
                case "ip_desc":
                    devices = devices.OrderByDescending(d => d.IP);
                    break;
                case "Department":
                    devices = devices.OrderBy(d => d.Department.Name);
                    break;
                case "department_desc":
                    devices = devices.OrderByDescending(d => d.Department.Name);
                    break;
                case "User":
                    devices = devices.OrderBy(d => d.User.UserName);
                    break;
                case "user_desc":
                    devices = devices.OrderByDescending(d => d.User.UserName);
                    break;
                case "Manufacturer":
                    devices = devices.OrderBy(d => d.Manufacturer);
                    break;
                case "manufacturer_desc":
                    devices = devices.OrderByDescending(d => d.Manufacturer);
                    break;
                case "Model":
                    devices = devices.OrderBy(d => d.Model);
                    break;
                case "model_desc":
                    devices = devices.OrderByDescending(d => d.Model);
                    break;
                case "Type":
                    devices = devices.OrderBy(d => d.Type);
                    break;
                case "type_desc":
                    devices = devices.OrderByDescending(d => d.Type);
                    break;
                case "Active":
                    devices = devices.OrderBy(d => d.Active);
                    break;
                case "active_desc":
                    devices = devices.OrderByDescending(d => d.Active);
                    break;
                case "OS":
                    devices = devices.OrderBy(d => d.OperatingSystem);
                    break;
                case "os_desc":
                    devices = devices.OrderByDescending(d => d.OperatingSystem);
                    break;
                case "Notes":
                    devices = devices.OrderBy(d => d.Notes);
                    break;
                case "notes_desc":
                    devices = devices.OrderByDescending(d => d.Notes);
                    break;
                default:
                    devices = devices.OrderBy(d => d.InventoryNumber);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(devices.ToPagedList(pageNumber, pageSize));
        }
        //// GET: Device
        //public ActionResult Index()
        //{
        //    var devices = db.Devices.Include(d => d.Department).Include(d => d.User);
        //    return View(devices.ToList());
        //}

        // GET: Device/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Device/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName");
            return View();
        }

        // POST: Device/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,UserID,InventoryNumber,Name,Model,Manufacturer,IP,MAC,SerialNumber,ServiceTag,WallJack,SwitchPortNumber,Active,OperatingSystem,Notes,CriticalRating,Type")] Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Devices.Add(device);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /*dex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. Error adding Device.");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", device.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", device.UserID);
            return View(device);
        }

        // GET: Device/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", device.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", device.UserID);
            return View(device);
        }

        // POST: Device/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceID,DepartmentID,UserID,InventoryNumber,Name,Model,Manufacturer,IP,MAC,SerialNumber,ServiceTag,WallJack,SwitchPortNumber,Active,OperatingSystem,Notes,CriticalRating,Type")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", device.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", device.UserID);
            return View(device);
        }

        // GET: Device/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Device failed. Try again, and if the problem persists see your system administrator.";
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Device device = db.Devices.Find(id);
                db.Devices.Remove(device);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)    // was DataException before transient logging was added
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
