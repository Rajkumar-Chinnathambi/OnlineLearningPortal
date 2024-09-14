using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class UserCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set;}
        public int UserId { get; set; }

        public string EnrollStatus { get; set; }
    }
}