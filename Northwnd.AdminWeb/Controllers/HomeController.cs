using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Northwnd.AdminWeb.ViewModels;
using Northwnd.Models;
using Northwnd.Service.Service;
using Northwnd.Service.Interface;


namespace Northwnd.AdminWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService service)
        {
            _userService = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Kevin")]
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            string pwd = GetPassword(loginViewModel.Password);

            var user = _userService.GetInfo(x => x.Account == loginViewModel.Id && x.Password == pwd);

            if (user != null)
            {
                LoginProcess(user, loginViewModel.IsRemember);
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

        private string GetPassword(string password)
        {
            return password;
        }

        private void LoginProcess(User user, bool isRemeber)
        {
            var roles = GetRole();
            var ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: user.Account,
                        issueDate: DateTime.Now,
                        expiration:DateTime.Now.AddMinutes(30),
                        isPersistent: isRemeber,
                        userData: roles,
                        cookiePath: FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }
    }
}