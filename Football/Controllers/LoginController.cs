using Football.Classes;
using Football.DAL;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Football.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            User user = new User();
            ViewBag.UserLoginMessage = "";
            return View(user);
        }
        public ActionResult Login(User user)
        {
            
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<User> userToCheck = (from x in dal.users
                                        where x.userName == user.userName
                                        select x).ToList<User>();
            if (userToCheck.Count != 0)
            {
                if (enc.ValidatePassword(user.password, userToCheck[0].password))
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        user.userName,                      // user name
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,
                        userToCheck[0].role                       // can be used to store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return RedirectToRoute("HomePage");
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("UserLogin", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToRoute("Default");
        }
        public ActionResult UserRegister()
        {
            User user = new User();
            ViewBag.UserLoginError = "";
            return View(user);
        }
        public ActionResult AddUser(User user)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();

            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(user.password);
                if (!userExists(user.userName))
                {
                    user.password = hashedPassword;
                    dal.users.Add(user);
                    dal.SaveChanges();
                    ViewBag.message = "User was added succesfully.";
                    user = new User();
                }
                else
                    ViewBag.message = "Username Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            return View("UserRegister", user);
        }

        private bool userExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<User> users = dal.users.ToList<User>();
            foreach (User user in dal.users)
                if (user.userName.Equals(userName))
                    return true;
            return false;
        }
    }
}