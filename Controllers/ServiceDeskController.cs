using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers.Domain
{ 
    [Route("[controller]")]
    public class ServiceDeskController : Controller
    {
        public ITILDbContext DbContext {get;set;}
        public ServiceDeskController(ITILDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("/ServiceDesk/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProblemDto problem)
        {
            if (ModelState.IsValid)
            {
                DbContext.Problems.Add(new Problem()
                {
                  Title = problem.Title,
                  Description = problem.Description,
                  CreatedDate = DateTime.Now  
                });
                DbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(problem);
        }
    }
}
