using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLearningPortal.Models;
using OnlineLearningPortal.Repository;

namespace OnlineLearningPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserModel usermodel)
        {
            HomeRepository homerepo = new HomeRepository();
            var userType = homerepo.UserValid(usermodel);
            if(userType != null)
            {
                foreach (var item in userType) {
                    Session["Name"] = item.UserName;
                    if (item.UserType == "Admin")
                    {
                        return Redirect("~/Admin/Index");
                    }
                    else
                    {
                        return Redirect("~/User/Index");
                    }
                }
                return View();
            }
            else
            {
                ViewBag.Message = "Invalid UserName and Password";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel usermodel)
        {
            HomeRepository homerepo = new HomeRepository();
            if (homerepo.RegisterValid(usermodel)) {
                return Redirect("~/Home/Index");
            }
            return View();
        }
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
    }
}