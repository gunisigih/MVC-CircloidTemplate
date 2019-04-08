using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public UserAccountController()
        {
            ViewBag.UserSelected = "selected";
        }
        public ActionResult Index()
        {
            MembershipUserCollection userCollection= Membership.GetAllUsers();

            return View(userCollection);
        }
        public ActionResult AddUserAccount()
        {
            return View();
        }

    }
}