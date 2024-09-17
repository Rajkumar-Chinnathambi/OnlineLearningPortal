using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class CourseUsersModel
    {
        public List<CourseModel> Coursemodel { get; set; }
        public List<UserModel>  Usermodel { get; set; }
        public List<CommentModel> Commentmodel { get; set; }

    }
}