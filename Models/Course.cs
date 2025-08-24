namespace Crud.Models
{
    public class Course
    {

        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Course title is required")]
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters")]
        public string Title { get; set; } = null!;

        [Range(0, 100000, ErrorMessage = "Credits must be between 1 and 10")]
        public int Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
        public string? Description { get; set; }
        
        public string? Cover { get; set; } // For image upload
        public ICollection<Student>? Students { get; set; }

        public ICollection<CourseInstructor>? CourseInstructor { get; set; }

    }
}
