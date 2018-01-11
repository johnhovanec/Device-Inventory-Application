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
    public class UserController : Controller
    {
        private DeviceContext db = new DeviceContext();

        // GET: User
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.LnameSortParm = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewBag.FnameSortParm = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewBag.EmpIDSortParm = sortOrder == "EmployeeID" ? "employeeID_desc" : "EmployeeID";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "dept_desc" : "Department";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from u in db.Users
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.LastName.Contains(searchString)
                                       || u.FirstMidName.Contains(searchString)
                                       || u.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "username_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "LastName":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "lastname_desc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                case "FirstName":
                    users = users.OrderBy(u => u.FirstMidName);
                    break;
                case "firstname_desc":
                    users = users.OrderByDescending(u => u.FirstMidName);
                    break;
                case "EmployeeID":
                    users = users.OrderBy(u => u.EmployeeID);
                    break;
                case "employeeID_desc":
                    users = users.OrderByDescending(u => u.EmployeeID);
                    break;
                case "Phone":
                    users = users.OrderBy(u => u.PhoneNumber);
                    break;
                case "phone_desc":
                    users = users.OrderByDescending(u => u.PhoneNumber);
                    break;
                case "Department":
                    users = users.OrderBy(u => u.Department.Name);
                    break;
                case "dept_desc":
                    users = users.OrderByDescending(u => u.Department.Name);
                    break;
                case "Position":
                    users = users.OrderBy(u => u.Position);
                    break;
                case "position_desc":
                    users = users.OrderByDescending(u => u.Position);
                    break;
                default:
                    users = users.OrderBy(d => d.UserName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
     

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,LastName,FirstMidName,EmployeeID,PhoneNumber,DepartmentID,Position")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)   // was DataException before transient logging was added
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(user);
        }


        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", user.DepartmentID);
            //ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", user.ID);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, UserName, LastName, FirstMidName, EmployeeID, PhoneNumber, DepartmentID, Position")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", user.DepartmentID);
            //ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", user.ID);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete User failed. Try again, and if the problem persists see your system administrator.";
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
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
