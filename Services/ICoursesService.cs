namespace Crud.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetAll();
        Course? GetById(int id);
        Task create(CreateCourseFormViewModel model);
        Task<Course?> Edit(EditCourseFormViewModel model);

        bool Delete(int id);
    }
}
