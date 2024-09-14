using OnlineLearningPortal.Models;
using OnlineLearningPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLearningPortal.Controllers
{
    public class AdminController : Controller
    {
        CourseRepository courseRepository;
        public AdminController() {
            courseRepository = new CourseRepository();
        }
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.CourseCount = courseRepository.AllCourseCount();
            ViewBag.UserCount = courseRepository.AllUserCount();
            var allUser = courseRepository.GetAllUsers();
            return View(allUser);
        }
        public ActionResult Courses() {
            CourseRepository courseRepository = new CourseRepository();

            var allCourses = courseRepository.GetAllCourses();
            return View(allCourses);
        }
        [HttpGet]
        public ActionResult AddCourse() {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(CourseModel coursemodel, HttpPostedFileBase Courseimage) {
            coursemodel.CoursePhoto = new byte[Courseimage.ContentLength];
            Courseimage.InputStream.Read(coursemodel.CoursePhoto, 0, Courseimage.ContentLength);
            CourseRepository courseRepository = new CourseRepository();
            var addStatus = courseRepository.AddCourse(coursemodel);
            if (addStatus)
            {
                ViewBag.Message = "Successfully Added";
                View();
            }
            else
            {
                ViewBag.Message = "Failed";
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult CourseEdit(CourseModel coursemodel) {
            var singleCourse = courseRepository.GetCourseById(coursemodel);
            return View(singleCourse);
        }
        [HttpPost]
        public ActionResult SaveEditCourse(CourseModel coursemodel) {
            var updateStatus = courseRepository.SaveEditCourse(coursemodel);
            if (updateStatus)
            {
                ViewBag.Message = "Successfully Updated";
                return RedirectToAction("Courses");
            }
            else
            {
                ViewBag.Message = "Something Error";
                return RedirectToAction("Courses");
            }
        }

        public ActionResult DeleteCourse(CourseModel coursemodel) {
            var deleteStatus = courseRepository.deleteCourse(coursemodel);

            return RedirectToAction("Courses");
        }

        public ActionResult AllUsers()
        {
            var allUser = courseRepository.GetAllUsers();
            return View(allUser);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserModel user) {
            HomeRepository homeRepository = new HomeRepository();
            var insertStatus = homeRepository.RegisterValid(user);
            if (insertStatus)
            {
                TempData["Message"] = "Successfully Created";
                return View();
            }
            else
            {
                TempData["Message"] = "Something error,try again";
                return View();
            }
        }
        public ActionResult EditUser(UserModel user) {
            var singleUser = courseRepository.GetSingleUser(user);
            return View(singleUser);
        }
        public ActionResult SaveEditUser(UserModel user) {
            courseRepository.EditUser(user);
            return View("AllUsers");
        }

        public ActionResult DeleteUser(UserModel user) {
            courseRepository.DeleteUser(user);
            return Redirect("~/Admin/AllUsers");
        }

        public ActionResult CourseApproval() {
            var list = courseRepository.GetPendingApprovalCourse();
            return View(list);
        }
        [HttpGet]
        public ActionResult CourseAcceptOrReject(int id,int status)
        {
            courseRepository.CourseAcceptOrReject(id, status);
            return Redirect("~/Admin/CourseApproval");
        }
    }
}