﻿using System;

namespace PearReview.Areas.Courses.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
    }
}
 