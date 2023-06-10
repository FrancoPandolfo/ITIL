using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult SaveProblem([FromBody] ProblemDto problemDto)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == problemDto.UserId);
                var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == problemDto.ConfigurationItemId);
                var assignedUser = DbContext.Users.SingleOrDefault(u => u.Id == problemDto.AssignedUserId);
                var incidents = DbContext.Incidents.Include(i => i.Problems).Where(i => problemDto.IncidentIds.Contains(i.Id)).ToList();
                var problem = new Problem()
                {
                    Title = problemDto.Title,
                    Description = problemDto.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = problemDto.UserId,
                    User = user,
                    ConfigurationItemId = problemDto.ConfigurationItemId,
                    ConfigurationItem = configurationItem,
                    AssignedUserId = problemDto.AssignedUserId,
                    AssignedUser = assignedUser,
                    Impact = problemDto.Impact,
                    Priority = problemDto.Priority,
                    Incidents = incidents
                };

                foreach (var incident in incidents)
                {
                    incident.Problems.Add(problem);
                }

                DbContext.Problems.Add(problem);
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
                var problem = DbContext.Problems.Include(i => i.AssignedUser).SingleOrDefault(i => i.Id == problemId);
                var assignedUser = DbContext.Users.SingleOrDefault(u => u.Id == modifiedProblem.AssignedUserId);
                if(problem != null)
                {
                    problem.Title = modifiedProblem.Title;
                    problem.Description = modifiedProblem.Description;
                    problem.AssignedUserId = modifiedProblem.AssignedUserId;
                    problem.AssignedUser = assignedUser;
                    problem.Impact = modifiedProblem.Impact;
                    problem.Priority = modifiedProblem.Priority;
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
            var problems = DbContext.Problems
            .Include(i => i.AssignedUser)
            .Include(i => i.Incidents)
            .OrderByDescending(p => p.CreatedDate);
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

              var userInfo = DbContext.Users.SingleOrDefault(u=> u.Id == problem.UserId);
              if(userInfo != null) {
                problem.User = userInfo;
              }

              ViewBag.Users = DbContext.Users;
              return View(problem);
            }
            return NotFound($"{problemId} not found");
                
        }

        [HttpGet("/Problems/NewProblem")]
        public IActionResult NewProblem()
        {
            var items = DbContext.Configuration;
            ViewBag.Users = DbContext.Users;
            var incidents = DbContext.Incidents;
            ViewBag.Incidents = incidents;
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

        [HttpPost("/Problems/{problemId}/Comments/SaveComment")]
        public IActionResult SaveComment(long problemId, [FromBody] string comment)
        {
            var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
            if (problem == null)
            {
                return NotFound();
            }
            problem.Comments.Add(comment);

            DbContext.SaveChanges();

            return Ok(comment);
        }

        [HttpGet("/Problems/{problemId}/Comments")]
        public IActionResult Comments(long problemId)
        {
            var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
            if (problem == null)
            {
                return NotFound();
            }
            return Ok(new { Comments = problem.Comments });
        }

        [HttpDelete("/Problems/{problemId}/Comments/{commentIndex}")]
        public IActionResult DeleteComment(long problemId, int commentIndex)
        {
            var problem = DbContext.Problems.SingleOrDefault(i => i.Id == problemId);
            if (problem == null)
            {
                return NotFound();
            }

            if (commentIndex < 0 || commentIndex >= problem.Comments.Count)
            {
                return NotFound();
            }

            problem.Comments.RemoveAt(commentIndex);

            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("/Problems/{problemId}/Incidents")]
        public IActionResult ProblemIncidents(long problemId)
        {
            var problem = DbContext.Problems.Include(i => i.Incidents).SingleOrDefault(i => i.Id == problemId);
            if(problem != null)
            {
                return Ok(problem.Incidents);
            }
            return NotFound();
        }

        [HttpPatch("/Problems/{problemId}/Incidents/{incidentId}")]
        public IActionResult AddIncident(long problemId, long incidentId)
        {
            var problem = DbContext.Problems.Include(i => i.Incidents).SingleOrDefault(i => i.Id == problemId);
            var incident = DbContext.Incidents.Include(i => i.Problems).SingleOrDefault(i => i.Id == incidentId);
            if(problem != null && incident != null)
            {
                problem.Incidents.Add(incident);
                incident.Problems.Add(problem);
                DbContext.SaveChanges();
                return Ok(incident);
            }
            return NotFound();
        }

        [HttpDelete("/Problems/{problemId}/Incidents/{incidentId}")]
        public IActionResult RemoveIncident(long problemId, long incidentId)
        {
            var problem = DbContext.Problems.Include(i => i.Incidents).SingleOrDefault(i => i.Id == problemId);
            var incident = DbContext.Incidents.Include(i => i.Problems).SingleOrDefault(i => i.Id == incidentId);
            if(problem != null && incident != null)
            {
                problem.Incidents.Remove(incident);
                incident.Problems.Remove(problem);
                DbContext.SaveChanges();
                return Ok(incident);
            }
            return NotFound();
        }
    }
}