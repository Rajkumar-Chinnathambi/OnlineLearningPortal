using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class PendingCourse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get;set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
    }
}