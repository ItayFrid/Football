using Football.DAL;
using Football.ViewModels;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Football.Classes;

namespace Football.Controllers
{
    public class OpenController : Controller
    {
        // GET: Open
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult showStaff()
        {
            DataLayer dal = new DataLayer();
            List<Staff> objStaff = dal.staffs.ToList<Staff>();
            ViewModel vm = new ViewModel();
            vm.staffs = objStaff;
            return View(vm);
        }
        public ActionResult showPlayers()
        {
            DataLayer dal = new DataLayer();
            List<Player> objPlayers = dal.players.ToList<Player>();
            ViewModel vm = new ViewModel();
            vm.players = objPlayers;
            return View(vm);
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
            if (userToCheck.Count!=0)
            {
                if (enc.ValidatePassword(user.password, userToCheck[0].password))
                {
                    ViewBag.UserLoginMessage = "Login Successfuly";
                    //TODO: Authentication
                    user = new User();
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