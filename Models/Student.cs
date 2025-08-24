namespace Crud.Models
{
    public class Student
    {

        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } =null!; // Non-nullable property

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        // Navigation property
        // Foreign Key
        public int CourseId { get; set; }

        // Navigation Property: One Student belongs to One Course
        public Course? Course { get; set; }

    }
}
