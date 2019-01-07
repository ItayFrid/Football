using Football.Classes;
using Football.DAL;
using Football.Models;
using Football.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Football.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPlayer()
        {
            ViewBag.PlayerError = "";
            Player player = new Player();
            return View(player);
        }
        [HttpPost]
        public ActionResult SubmitPlayer()
        {

            Player obj = new Player();
            obj.number      = Request.Form["number"].ToString();
            obj.firstName   = Request.Form["firstName"].ToString();
            obj.lastName    = Request.Form["lastName"].ToString();
            obj.position    = Request.Form["position"].ToString();
            obj.rating      = Request.Form["rating"].ToString();
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                if (NumberExists(obj.number))
                {
                    ViewBag.PlayerError = "Number exists in Database .";
                    return View("AddPlayer", obj);
                }
                dal.players.Add(obj);
                dal.SaveChanges();
                return View("AdminInterface");
            }
            else
                return View("AddPlayer",obj);
        }
        private bool NumberExists(string number)
        {
            DataLayer dal = new DataLayer();
            foreach (Player player in dal.players)
            {
                if (number.Equals(player.number))
                    return true;
            }
            return false;
        }
        public ActionResult AddStaff()
        {
            ViewBag.StaffError = "";
            Staff staff = new Staff();
            return View(staff);
        }
        [HttpPost]
        public ActionResult SubmitStaff()
        {
            Staff obj = new Staff();
            obj.job = Request.Form["job"].ToString();
            obj.firstName = Request.Form["firstName"].ToString();
            obj.lastName = Request.Form["lastName"].ToString();
            obj.age = Request.Form["age"].ToString();
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                if (JobExists(obj.job))
                {
                    ViewBag.StaffError = "Job exists in database .";
                    return View("AddStaff", obj);
                }
                dal.staffs.Add(obj);
                dal.SaveChanges();
                return View("AdminInterface");
            }
            else
                return View("AddStaff", obj);
        }

        private bool JobExists(string job)
        {
            DataLayer dal = new DataLayer();
            foreach (Staff staff in dal.staffs)
            {
                if (job.Equals(staff.job))
                    return true;
            }
            return false;
        }
        public ActionResult AdminInterface()
        {
            return View();
        }

        public ActionResult ShowSearchPlayers()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.players = dal.players.ToList<Player>();
            return View("SearchPlayers", vm);
        }
        [HttpPost]
        public ActionResult SearchPlayers()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            string searchValue = Request.Form["srcFirstName"].ToString();
            List<Player> players =
                (from x in dal.players
                 where x.firstName.Contains(searchValue)
                 select x).ToList<Player>();
            vm.players = players;
            return View(vm);
        }
        [HttpPost]
        public ActionResult DeletePlayer()
        {
            DataLayer dal = new DataLayer();
            string number = Request.Form["delNumber"].ToString();
            foreach(Player player in dal.players)
            {
                if (player.number.Equals(number))
                {
                    dal.players.Remove(player);
                }
            }
            dal.SaveChanges();
            ViewModel vm = new ViewModel();
            vm.players = dal.players.ToList<Player>();
            return View("SearchPlayers", vm);
        }
        public ActionResult ShowSearchStaff()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.staffs = dal.staffs.ToList<Staff>();
            return View("SearchStaff", vm);
        }
        [HttpPost]
        public ActionResult SearchStaff()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            string searchValue = Request.Form["srcFirstName"].ToString();
            List<Staff> staffs =
                (from x in dal.staffs
                 where x.firstName.Contains(searchValue)
                 select x).ToList<Staff>();
            vm.staffs = staffs;
            return View(vm);
        }
        [HttpPost]
        public ActionResult DeleteStaff()
        {
            DataLayer dal = new DataLayer();
            string job = Request.Form["delJob"].ToString();
            foreach (Staff staff in dal.staffs)
            {
                if (staff.job.Equals(job))
                {
                    dal.staffs.Remove(staff);
                }
            }
            dal.SaveChanges();
            ViewModel vm = new ViewModel();
            vm.staffs = dal.staffs.ToList<Staff>();
            return View("SearchStaff", vm);
        }
        public ActionResult AdminLogin()
        {
            Admin admin = new Admin();
            ViewBag.AdminLoginMessage = "";
            return View(admin);
        }
        public ActionResult Login(Admin admin)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<Admin> adminToCheck = (from x in dal.admins
                                        where x.userName == admin.userName
                                        select x).ToList<Admin>();
            if (adminToCheck.Count!=0)
            {
                if(enc.ValidatePassword(admin.password,adminToCheck[0].password))
                {
                    ViewBag.AdminLoginMessage = "Login Successfuly";
                    //TODO: Authentication
                    admin = new Admin();
                }
                else
                {
                    ViewBag.AdminLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.AdminLoginMessage = "Incorrect Username/password";
            return View("AdminLogin",admin);
        }

        public ActionResult AdminRegister()
        {
            Admin admin = new Admin();
            ViewBag.AdminLoginError = "";
            return View(admin);
        }
        public ActionResult AddAdmin(Admin admin)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();

            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(admin.password);
                if (!adminExists(admin.userName)) { 
                    admin.password = hashedPassword;
                    dal.admins.Add(admin);
                    dal.SaveChanges();
                    ViewBag.message = "Admin was added succesfully.";
                    admin = new Admin();
                }
                else
                    ViewBag.message = "Username Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            return View("AdminRegister", admin);
        }

        private bool adminExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<Admin> admins = dal.admins.ToList<Admin>();
            foreach (Admin admin in dal.admins)
                if (admin.userName.Equals(userName))
                    return true;
            return false;
        }
    }
}