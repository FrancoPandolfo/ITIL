using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{
    public class ProblemsController : Controller
    {
        public ITILDbContext DbContext {get;set;}
        public ProblemsController(ITILDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("/Problems/SaveProblem")]
        public IActionResult SaveProblem([FromBody] ProblemDto problem)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == problem.UserId);
                var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == problem.ConfigurationItemId);
                DbContext.Problems.Add(new Problem()
                {
                    Title = problem.Title,
                    Description = problem.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = problem.UserId,
                    User = user,
                    ConfigurationItemId = problem.ConfigurationItemId,
                    ConfigurationItem = configurationItem
                });

                DbContext.SaveChanges();
                return Ok(problem);
            }

            return BadRequest();
        }

        [HttpPatch("/Problems/{problemId}")]
        public IActionResult UpdateProblem([FromBody] ProblemDto modifiedProblem, long problemId)
        {
            if (ModelState.IsValid)
            {
                var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
                if(problem != null)
                {
                    problem.Title = modifiedProblem.Title;
                    problem.Description = modifiedProblem.Description;
                    DbContext.Problems.Update(problem);
                    DbContext.SaveChanges();
                    return Ok(problem);
                }
            }
            return BadRequest();
        }

        [HttpGet("/Problems")]
        public IActionResult Problems()
        {
            var problems = DbContext.Problems;
            return View(problems);
        }

        [HttpGet("/Problems/{problemId}")]
        public IActionResult ProblemInfo(long problemId)
        {
            var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
            if(problem != null)
            {
              var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == problem.ConfigurationItemId);
              if(configurationItem != null) {
                problem.ConfigurationItem = configurationItem;
              }
              return View(problem);
            }
            return NotFound($"{problemId} not found");
                
        }

        [HttpGet("/Problems/NewProblem")]
        public IActionResult NewProblem()
        {
            var items = DbContext.Configuration;
            return View(items);
        }

        [HttpPost("/Problems/DeleteProblem")]
        public IActionResult DeleteProblem([FromBody] long problemId)
        {
            var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
            if(problem != null)
            { 
                DbContext.Problems.Remove(problem);
                DbContext.SaveChanges();
                return Ok($"Incident {problemId} deleted succesfuly");
            }

            return NotFound($"{problemId} not found");
        }
    }
}