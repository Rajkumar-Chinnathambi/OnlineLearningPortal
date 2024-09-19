using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPortal.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        [Display(Name = "Payment mode")]
        public string paymentMode { get; set; }
        public string Username { get; set; }
        [Display(Name="Coursename")]
        public string Coursename { get; set; }
        public DateTime CreateAt { get; set; }= DateTime.Now;

    }
}