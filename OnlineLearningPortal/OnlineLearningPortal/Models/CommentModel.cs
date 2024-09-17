using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class CommentModel
    {
        public int commentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string commentText { get; set; }
    }
}