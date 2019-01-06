using Football.DAL;
using Football.ViewModels;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}