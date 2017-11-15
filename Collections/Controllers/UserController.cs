using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collections.Models;
using System.Security.Cryptography;
using System.Text;

namespace Collections.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignIn()
        {
            if (IsLogged())
                return RedirectToAction("Index", "Item");

            return View();
        }
        
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            user.Password = GetMD5(user.Password);
            var userList = UsereTable.Get();
            int id = 0;
            
            foreach(var u in userList)
            {
                if (u.SignIn(user))
                    id = u.ID;
            }

            if (id == 0)
                return View();

            user = UsereTable.Get(id);

            Session["user"] = user;

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult SignOut()
        {
            Session["user"] = new User();

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

        private string GetMD5(string text)
        {
            string hashText;

            MD5 md5 = MD5.Create();
            Byte[] oByte = Encoding.ASCII.GetBytes(text);
            Byte[] eBye = md5.ComputeHash(oByte);
            hashText = BitConverter.ToString(eBye);
            hashText = hashText.ToLower().Replace("-", "");

            return hashText;
        }
    }
}
