namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoursesService _coursesService;

        public HomeController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        public IActionResult Index()
        {

            var courses = _coursesService.GetAll();
            return View(courses);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
