using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public string CourseCatagory { get; set; }
        public string Coursename { get; set; }
        public string Coursesrc { get; set; }
        public string Coursedesc { get; set; }
    }
}