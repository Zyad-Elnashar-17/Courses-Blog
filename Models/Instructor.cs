namespace Crud.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Instructor name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = null!; // Non-nullable property
        [Range(25, 65, ErrorMessage = "Age must be between 25 and 65")]
        public int Age { get; set; }
        [StringLength(100, ErrorMessage = "Specialization can't be longer than 100 characters")]
        public string Specialization { get; set; } = null!;


        public ICollection<CourseInstructor>? CourseInstructor { get; set; }
    }
}
