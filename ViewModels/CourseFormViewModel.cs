namespace Crud.ViewModels
{
    public class CourseFormViewModel
    {
        [Required(ErrorMessage = "Course title is required")]
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Course price is required")]
        [Range(1, 100000, ErrorMessage = "price must be between 0 and 100000")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        [Display(Name = "Instructors")]
        public List<int> SelectedInstructors { get; set; } = default!;

        public IEnumerable<SelectListItem> Instructors { get; set; } = Enumerable.Empty<SelectListItem>();


        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
        public string Description { get; set; }
    }
}
