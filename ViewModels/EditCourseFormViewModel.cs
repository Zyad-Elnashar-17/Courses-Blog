namespace Crud.ViewModels
{
    public class EditCourseFormViewModel : CourseFormViewModel
    {
        public int Id { get; set; }

        public string? ExistingCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; }
    }
}
