using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collections.Models;

namespace Collections.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            if (!IsLogged())
                return RedirectToAction("Index", "Home");

            int id = (IsLogged()) ? (Session["user"] as User).ID : 0;

            var itemList = ItemTable.Get(id);
            ViewBag.typeList = TypeTable.Get();
            ViewBag.userList = UsereTable.Get();

            return View(itemList);
        }

        public ActionResult Details(int id)
        {
            if (!IsLogged())
                return RedirectToAction("Index", "Home");

            var item = ItemTable.Get(id, (Session["user"] as User).ID);

            if (item.IDItem == 0)
                return RedirectToAction("Index", "Home");

            ViewBag.userName = (Session["user"] as User).Name;
            ViewBag.typeName = TypeTable.Get(item.IDType);

            return View(item);
        }

        public ActionResult Add()
        {
            if (!IsLogged())
                return RedirectToAction("Index", "Home");

            ViewBag.typeList = TypeTable.Get();
            ViewBag.userList = UsereTable.Get();

            var item = new Item();

            item.IDUser = (Session["user"] as User).ID;

            return View(item);
        }

        [HttpPost]
        public ActionResult Add(Item item)
        {
            if (IsLogged())
            {
                item.IDUser = (Session["user"] as User).ID;
                ItemTable.Input(item);
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            if (!IsLogged())
                return RedirectToAction("Index", "Home");

            ViewBag.typeList = TypeTable.Get();
            ViewBag.userList = UsereTable.Get();

            var item = ItemTable.Get(id, (Session["user"] as User).ID);

            if (item.IDType == 0 || item.IDUser != (Session["user"] as User).ID)
                return RedirectToAction("Index", "Home");

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (IsLogged())
                ItemTable.Update(item);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            if (!IsLogged())
                return RedirectToAction("Index", "Home");

            ViewBag.typeList = TypeTable.Get();
            ViewBag.userList = UsereTable.Get();

            var item = ItemTable.Get(id, (Session["user"] as User).ID);

            if (item.IDType == 0 || item.IDUser != (Session["user"] as User).ID)
                return RedirectToAction("Index", "Home");

            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(Item item)
        {
            if (IsLogged())
                ItemTable.Delete(item);

            return RedirectToAction("Index", "Home");
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