using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collections;
using Collections.Models;

namespace Collections.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (IsLogged())
                return RedirectToAction("Index", "Item");
            else
                return RedirectToAction("SignIn", "User");
        }

        private bool IsLogged()
        {
            User user = Session["user"] as User;
            if (user == null)
            {
                Session["user"] = new User();
                return false;
            }
            else if (UsereTable.Get(user.ID).ID != 0)
                return true;
            else
                return false;
        }
    }
}