using OnlineLearningPortal.Models;
using OnlineLearningPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLearningPortal.Controllers
{
    public class UserController : Controller
    {
        CourseRepository courseRepository;
        public UserController()
        {
            courseRepository = new CourseRepository();
        }
        // GET: User
        public ActionResult Index()
        {
            CourseRepository courseRepository = new CourseRepository();
            UserModel usermodel = new UserModel();
            usermodel.Id = (int)Session["UserId"];
            var courseList = courseRepository.GetAllCourses();
           /* var unenrollcourse = courseRepository.UnEnrollCourse(usermodel);*/
            /*courseList.AddRange(unenrollcourse);*/
            return View(courseList);
        }
        [HttpGet]
        public ActionResult CourseEnroll(CourseModel course) {
            CourseRepository courseRepository = new CourseRepository();
            var list = courseRepository.GetCourseById(course);
            foreach(var item in list)
            {
                if (item.CourseType == "Free")
                {
                    UserModel usermodel = new UserModel();
                    usermodel.Id = (int)Session["UserId"];
                    var result = courseRepository.CourseEnroll(usermodel, course);
                    TempData["Result"] = result;
                    return Redirect("~/User/Index");
                }
                else
                {
                    TempData["Result"] = "Paid Course";
                    return Redirect("~/User/PaidCourseView?courseid=" + course.CourseID);
                }
            }
            TempData["Result"] = "Something Error";
            return Redirect("~/User/VideoPlayView?courseid = "+course.CourseID);
        }
        public ActionResult PaidCourseView(CourseModel coursemodel)
        {
            var singleCourse = courseRepository.GetCourseById(coursemodel);
            return View(singleCourse);
        }
        public ActionResult MyCourse()
        {
            CourseRepository courseRepository = new CourseRepository();
            UserModel usermodel = new UserModel();
            usermodel.Id = (int)Session["UserId"];
            var courseList = courseRepository.GetCourseByUser(usermodel);
            return View(courseList); 
        }
        public ActionResult VideoPlayView(CourseModel coursemodel)
        {

            CourseRepository courseRepository = new CourseRepository();
            UserModel usermodel = new UserModel();
            usermodel.Id =(int)Session["UserId"];
            var list = courseRepository.IsUserCourseEnroll(usermodel, coursemodel);
            var enrollStatus = "Enroll";
            if (list != null)
            {
                if (list.EnrollStatus == "Start")
                {
                    enrollStatus = "Enroll";

                }
                else if(list.EnrollStatus == "Pending")
                {
                    enrollStatus = "Pending";
                }
                else  if( list.EnrollStatus == "Reject")
                {
                    enrollStatus = "Reject";
                }
                else
                {
                    enrollStatus = "Approved";
                }
                
            }
            else
            {
                enrollStatus = "Enroll";
            }
            var singleCourse = courseRepository.GetCourseById(coursemodel);
            singleCourse[0].Enroll=enrollStatus;
            return View(singleCourse);
        }
        public ActionResult DeleteMyCourse(CourseModel coursemodel) {
            CourseRepository courseRepository = new CourseRepository();
            UserModel usermodel = new UserModel();
            usermodel.Id = (int)Session["UserId"];
            var singleCourse = courseRepository.DeleteCourse(coursemodel,usermodel);
            TempData["Message"] = singleCourse;
            return Redirect("~/User/MyCourse");
        }

        public ActionResult videoGetView()
        {
            CourseRepository courseRepository = new CourseRepository();
            var list = courseRepository.GetAllVideos();
            return View(list);
        }
        [HttpPost]
        public ActionResult videoGetView(string videoName,HttpPostedFileBase videoPath)
        {
            byte[] videoContent = new byte[videoPath.ContentLength];
            videoPath.InputStream.Read(videoContent, 0, videoPath.ContentLength);
            VideoModel videoModel = new VideoModel();
            videoModel.videoName=videoName;
            videoModel.videoPath=videoContent;
            CourseRepository courseRepository = new CourseRepository();
            TempData["VideoResult"] = courseRepository.VideoSave(videoModel);
            return Redirect("~/User/VideoGetView");
        }
        public ActionResult CoursePage(CourseModel courseModel) {            
            var singleCourseDetail = courseRepository.GetCourseById(courseModel);
            return View(singleCourseDetail);
        }
        public ActionResult QuizeView(CourseModel course)
        {
            var allQuizes = courseRepository.GetAllQuize(course);
            return View(allQuizes);
        }
    }
}