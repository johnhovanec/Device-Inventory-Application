using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceHardwareApp2.DAL;
using DeviceHardwareApp2.ViewModels;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace DeviceHardwareApp2.Controllers
{
    public class HomeController : Controller
    {
        private DeviceContext db = new DeviceContext();

        public ActionResult Authorized()
        {
            // set up domain context
            PrincipalContext domain = new PrincipalContext(ContextType.Domain, "DAEDALUS");
            // find the logged-in user
            UserPrincipal user = UserPrincipal.FindByIdentity(domain, User.Identity.Name);
            // only want to allow the Tech group
            GroupPrincipal group = GroupPrincipal.FindByIdentity(domain, "Tech");

            if (user != null)
            {
                if (user.IsMemberOf(group))
                    return RedirectToAction("Index", "Device");
                else
                    return View("UnAuthorized");
            }
            else
                return Content("No user found"); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}