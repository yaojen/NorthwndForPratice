using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Northwnd.AdminWeb.ViewModels;

namespace Northwnd.AdminWeb.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Kevin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Id == "kevin" && loginViewModel.Password == "123456")
            {
                var now = DateTime.Now;
                var roles = GetRole();
                var ticket = new FormsAuthenticationTicket(
                            version: 1,
                            name: loginViewModel.Id,
                            issueDate: now,
                            expiration: now.AddMinutes(30),
                            isPersistent: false,
                            userData: roles,
                            cookiePath: FormsAuthentication.FormsCookiePath);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private string GetRole()
        {
            return "Admin,User,HR";
        }
    }
}