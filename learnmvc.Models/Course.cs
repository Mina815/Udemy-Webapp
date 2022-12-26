using System.ComponentModel.DataAnnotations;

namespace learnmvc.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
