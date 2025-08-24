namespace Crud.Controllers
{
    public class CoursesController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IInstructorsService _instructorsService;
        private readonly ICoursesService _coursesService;
        public CoursesController(AppDbContext context, IInstructorsService instructorsService, ICoursesService coursesService)
        {
            _context = context;
            _instructorsService = instructorsService;
            _coursesService = coursesService;
        }
        public IActionResult Index()
        {

            var courses = _coursesService.GetAll();
            return View(courses);
        }
         
        public IActionResult Details(int id)
        {
            var course = _coursesService.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }


        [HttpGet]
        public IActionResult Create()
        {

            CreateCourseFormViewModel viewModel = new()
            {
                Instructors = _instructorsService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Instructors = _instructorsService.GetSelectList();
                return View(model);
            }
            await _coursesService.create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _coursesService.GetById(id);
            if (course is null)
                return NotFound();

            EditCourseFormViewModel ViewModel = new()
            {
                Id = course.Id,
                Title = course.Title,
                Price = course.Price,
                Instructors = _instructorsService.GetSelectList(),
                Description = course.Description!,
                SelectedInstructors = course.CourseInstructor!.Select(ci => ci.InstructorId).ToList(),
                ExistingCover = course.Cover

            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Instructors = _instructorsService.GetSelectList();
                return View(model);
            }

            var course = await _coursesService.Edit(model);
            if (course is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _coursesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
