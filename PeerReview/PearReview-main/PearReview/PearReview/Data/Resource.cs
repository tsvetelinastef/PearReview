using PearReview.Areas.Courses.Data;
using PearReview.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PearReview.Data
{
    public class Resource
    {
        public static string StoragePath = "./Resources/";
        public static string CoursesPath = "Courses/";

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Filename { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Path { get; set; } = string.Empty;

        public long Size { get; set; }

        [Required]
        public string UploaderId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        // Navigation properties

        public AppUser Uploader { get; set; }

        public Course? Course { get; set; }
    }
}