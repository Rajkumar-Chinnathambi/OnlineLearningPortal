using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class UserCoursesModel
    {
        public List<UserModel> Users { get; set; }  
        public List<CourseModel> Courses { get; set; }  
    }
}