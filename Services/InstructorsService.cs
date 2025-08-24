namespace Crud.Services
{
    public class InstructorsService : IInstructorsService
    {


            private readonly AppDbContext _context;
            public InstructorsService(AppDbContext context)
            {
                _context = context;
            }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Instructors.Select(
             i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).AsNoTracking().ToList();
        }

       
    }
}
