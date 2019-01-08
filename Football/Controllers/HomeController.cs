using Football.DAL;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Football.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View(new Contact());
        }

        public ActionResult SubmitContact(Contact cont)
        {
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                dal.contacts.Add(cont);
                dal.SaveChanges();
                ViewBag.message = "Contact information was submitted succesfully";
                cont = new Contact();

            }
            else
            {
                ViewBag.message = "Contact information was not submitted";
            }
            return View();
        }
    }
}