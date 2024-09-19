using OnlineLearningPortal.Models;
using OnlineLearningPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLearningPortal.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        CourseRepository courseRepository;
        CommentRepository commentRepository;
        public UserController()
        {
            courseRepository = new CourseRepository();
            commentRepository = new CommentRepository();
        }
        // GET: User
       
        public ActionResult Index()
        {
            CourseRepository courseRepository = new CourseRepository();
            UserModel usermodel = new UserModel();
            usermodel.Id = (int)Session["UserId"];
            var courseList = courseRepository.GetAllCourses();           
            return View(courseList);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
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
                    return RedirectToAction("VideoPlayView","User" ,new { CourseID = course.CourseID });
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
            var singleCourse = courseRepository.GetCourseById(coursemodel);
            if (list != null)
            {
                singleCourse[0].Enroll=list.EnrollStatus;
            }
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
            var commentByCourse = commentRepository.GetAllCommentsByCourse(courseModel);
            commentByCourse.Reverse();
            CourseCommentModel comment = new CourseCommentModel
            {
                Commentmodel = commentByCourse,
                Coursemodel = singleCourseDetail
            };
            return View(comment);
        }
        public ActionResult QuizeView(CourseModel course)
        {
            var allQuizes = courseRepository.GetAllQuize(course);
            return View(allQuizes);
        }

        public ActionResult SaveComment(CommentModel commentmodel) {
            int id =(int)Session["UserId"];
            commentmodel.UserId = id;
            commentRepository.SaveComment(commentmodel);
            return Redirect("~/User/CoursePage?courseid="+commentmodel.CourseId);
        }
       
        public ActionResult PaymentPage(CourseModel courseModel)
        {
            
            return View(courseRepository.GetCourseById(courseModel));
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel payment) { 
            payment.UserId = (int)Session["UserId"];
            var result = courseRepository.SavePayment(payment);
            if (result)
            {
                TempData["PaymentStatus"] = "Paid Successfully";
                UserModel usermodel = new UserModel();
                usermodel.Id = (int)Session["UserId"];
                CourseModel course = new CourseModel();
                course.CourseID = payment.CourseId;
                string enrollStatus = courseRepository.CourseEnroll(usermodel, course);
                TempData["Result"] = enrollStatus;
                return RedirectToAction("VideoPlayView", "User", new { CourseID = course.CourseID });
            }
            else
            {
                TempData["PaymentStatus"] = "Something error,try again";
            }
            return RedirectToAction("PaymentPage", "User", new {CourseId=payment.CourseId});
        }
    }
}