namespace Crud.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;
        public CoursesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}/{FileSettings.ImagesPath}";
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses
              .Include(c => c.CourseInstructor)
              .ThenInclude(ci => ci.Instructor)
              .AsNoTracking()
                .ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses
              .Include(c => c.CourseInstructor)
              .ThenInclude(ci => ci.Instructor)
              .AsNoTracking()
              .SingleOrDefault(c => c.Id == id);
        }

        public async Task create(CreateCourseFormViewModel model)
        {
            var covername = await SaveCover(model.Cover!);

            Course course = new()
            {
                Title = model.Title,
                Price = model.Price,
                CourseInstructor = model.SelectedInstructors.Select(i => new CourseInstructor()
                {
                    InstructorId = i
                }).ToList(),
                Description = model.Description,
                Cover = covername
            };

            _context.Add(course);
            _context.SaveChanges();
        }

        public async Task<Course?> Edit(EditCourseFormViewModel model)
        {
            var course = _context.Courses
                .Include(c=> c.CourseInstructor)
                .SingleOrDefault(c => c.Id == model.Id);

            if (course is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = course.Cover; 

            course.Title = model.Title;
            course.Price = model.Price;
            course.CourseInstructor.Clear();
            course.CourseInstructor = model.SelectedInstructors
                .Select(i => new CourseInstructor { 
                    InstructorId = i })
                .ToList();
            course.Description = model.Description;

            if (hasNewCover)
            {
                course.Cover = await SaveCover(model.Cover!);
            }

           var effectedRows =  _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                   var cover = Path.Combine(_imagePath, oldCover!);
                   File.Delete(cover);
                }
                return course;
            }
            else
            {
                var cover = Path.Combine(_imagePath, course.Cover!);
                File.Delete(cover);
                return null;
            }
            
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var course = _context.Courses.Find(id);

            if (course is null)
                return isDeleted;

            _context.Courses.Remove(course);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagePath, course.Cover!);
                File.Delete(cover);
                
            }

            return isDeleted;
        }






        private async Task<string> SaveCover(IFormFile Cover)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";

            var path = Path.Combine(_imagePath, covername);

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
                return covername;
        }

       
    }
}
