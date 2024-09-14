using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Models
{
    public class VideoModel
    {
        public int videoId { get; set; }
        public string videoName { get; set; }
        public byte[] videoPath { get; set; }
        public string fileExt { get; set; } = "mp4";
        public VideoModel() { }

    }
}