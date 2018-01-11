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
using System.Data.Entity.Infrastructure;

namespace DeviceHardwareApp2.Controllers
{
    // To restrict access in this controller to only users in the Tech group
    [Authorize(Roles = "DAEDALUS\\Tech")]
    public class DepartmentController : Controller
    {
        private DeviceContext db = new DeviceContext();

        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Location")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /*ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. Error adding Department.");
            }
            

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deptToUpdate = db.Departments.Find(id);
            if (TryUpdateModel(deptToUpdate, "",
               new string[] { "Name", "Location" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)   // was DataException before transient logging was added
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes to Department. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(deptToUpdate);
        }

        // GET: Deptartment/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Department failed. Try again, and if the problem persists see your system administrator.";
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Department dept = db.Departments.Find(id);
                db.Departments.Remove(dept);
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
