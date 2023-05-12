using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{ 
    public class ServiceDeskController : Controller
    {
        public ITILDbContext DbContext {get;set;}
        public ServiceDeskController(ITILDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("/ServiceDesk")]
        public IActionResult Index()
        {
            return View("Index");
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
