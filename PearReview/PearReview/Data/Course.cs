using System.ComponentModel.DataAnnotations;

namespace PearReview.Data
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
