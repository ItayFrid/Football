﻿using Football.DAL;
using Football.Models;
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
    }
}