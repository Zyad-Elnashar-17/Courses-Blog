namespace Crud.ViewModels
{
    public class CreateCourseFormViewModel : CourseFormViewModel
    {
      

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } // For image upload
    }
}
