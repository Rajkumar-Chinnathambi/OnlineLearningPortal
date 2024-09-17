using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineLearningPortal.Models;
using OnlineLearningPortal.Repository;
using System.Web.Security;

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
                    Session["UserId"] = item.Id;
                    FormsAuthentication.SetAuthCookie(item.UserName, false);
                    if (!Roles.IsUserInRole(item.UserName, item.UserType))
                    {
                        Roles.AddUserToRole(item.UserName, item.UserType);
                    }
                    if (item.UserType == "Admin")
                    {
                        
                        return Redirect("~/Admin/Index");
                    }
                    else
                    {
                        return Redirect("~/User/Index");
                    }
                }
                TempData["LoginResult"] = "Invalid UserName and Password";
                return View();
            }
            else
            {
                TempData["LoginResult"] = "Invalid UserName and Password";
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
            else
            {
                ViewBag.Error = "Something Error, Please try again";
                return View();
            }
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
        public ActionResult LogOut()
        {
            TempData["Logout"] = "Logout";
            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("~/Home/Index");
        }
        public ActionResult State()
        {
            string filePath = Server.MapPath("~/Content/Json/State.json");            
            string jsonData = System.IO.File.ReadAllText(filePath);            
            return Content(jsonData, "application/json");
        }
        public ActionResult GetUserByUsername(string username)
        {

            using (SqlConnection con = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    string Isusername = null;
                    SqlCommand cmd = new SqlCommand("select * from UserDetails where UserName = @username", con);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Isusername = (string)reader["UserName"];
                    }
                    return Content(Isusername);

                }
                catch (Exception ex) { }

            }
            return null;
        }
        public ActionResult GetUserByEmail(string email)
        {

            using (SqlConnection con = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    string Isusername = null;
                    SqlCommand cmd = new SqlCommand("select * from UserDetails where Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", email);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Isusername = (string)reader["Email"];
                    }
                    return Content(Isusername);

                }
                catch (Exception ex) { }

            }
            return null;
        }
    }
}