using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIL.Controllers
{
    public class ChangesController : Controller
    {
        public ITILDbContext DbContext {get;set;}
        public ChangesController(ITILDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("/Changes/SaveChange")]
        public IActionResult SaveChange([FromBody] ChangeDto changeDto)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == changeDto.UserId);
                var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == changeDto.ConfigurationItemId);
                var assignedUser = DbContext.Users.SingleOrDefault(u => u.Id == changeDto.AssignedUserId);
                DateTime scheduled = DateTime.Parse(changeDto.ScheduledDate);
                var incidents = DbContext.Incidents.Include(i => i.Changes).Where(i => changeDto.IncidentIds.Contains(i.Id)).ToList();
                var problems = DbContext.Problems.Include(p => p.Changes).Where(p => changeDto.ProblemIds.Contains(p.Id)).ToList();

                var change = new Change()
                {
                    Title = changeDto.Title,
                    Description = changeDto.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = changeDto.UserId,
                    User = user,
                    ConfigurationItemId = changeDto.ConfigurationItemId,
                    ConfigurationItem = configurationItem,
                    ClientName = changeDto.ClientName,
                    ClientEmail = changeDto.ClientEmail,
                    State = State.ABIERTO,
                    AssignedUserId = changeDto.AssignedUserId,
                    AssignedUser = assignedUser,
                    Impact = changeDto.Impact,
                    Priority = changeDto.Priority,
                    ScheduledDate =  DateTime.SpecifyKind(scheduled, DateTimeKind.Utc),
                    Incidents = incidents,
                    Problems = problems
                };

                foreach (var incident in incidents)
                {
                    incident.Changes.Add(change);
                }
                foreach (var problem in problems)
                {
                    problem.Changes.Add(change);
                }
                
                DbContext.Changes.Add(change);
                DbContext.SaveChanges();
                return Ok(change);
            }

            return BadRequest();
        }

        [HttpPatch("/Changes/{changeId}")]
        public IActionResult UpdateChange([FromBody] ChangeDto modifiedChange, long changeId)
        {
            if (ModelState.IsValid)
            {
                var change = DbContext.Changes.Include(i => i.AssignedUser).SingleOrDefault(i => i.Id == changeId);
                var assignedUser = DbContext.Users.SingleOrDefault(u => u.Id == modifiedChange.AssignedUserId);
                if(change != null)
                {
                    change.Title = modifiedChange.Title;
                    change.Description = modifiedChange.Description;
                    change.State = modifiedChange.State;
                    change.AssignedUserId = modifiedChange.AssignedUserId;
                    change.AssignedUser = assignedUser;
                    change.Impact = modifiedChange.Impact;
                    change.Priority = modifiedChange.Priority;
                    DbContext.Changes.Update(change);
                    DbContext.SaveChanges();
                    return Ok(change);
                }
            }
            return BadRequest();
        }

        [HttpGet("/Changes")]
        public IActionResult Changes()
        {
            var changes = DbContext.Changes
            .Include(i => i.AssignedUser)
            .Include(i => i.Incidents)
            .Include(i => i.Problems)
            .OrderByDescending(c => c.CreatedDate);
            return View(changes);
        }

        [HttpGet("/Changes/{changeId}")]
        public IActionResult ChangeInfo(long changeId)
        {
            var change = DbContext.Changes
            .SingleOrDefault(i => i.Id == changeId);
            if(change != null)
            {
              var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == change.ConfigurationItemId);
              if(configurationItem != null) {
                change.ConfigurationItem = configurationItem;
              }

              var userInfo = DbContext.Users.SingleOrDefault(u=> u.Id == change.UserId);
              if(userInfo != null) {
                change.User = userInfo;
              }

              ViewBag.Users = DbContext.Users;
              return View(change);
            }
            return NotFound($"{changeId} not found");
                
        }

        [HttpGet("/Changes/NewChange")]
        public IActionResult NewChange()
        {
            var items = DbContext.Configuration;
            ViewBag.Users = DbContext.Users;
            var incidents = DbContext.Incidents;
            ViewBag.Incidents = incidents;
            var problems = DbContext.Problems;
            ViewBag.Problems = problems;
            return View(items);
        }

        [HttpPost("/Changes/DeleteChange")]
        public IActionResult DeleteChange([FromBody] long changeId)
        {
            var change = DbContext.Changes.SingleOrDefault(i => i.Id == changeId);
            if(change != null)
            { 
                DbContext.Changes.Remove(change);
                DbContext.SaveChanges();
                return Ok($"Incident {changeId} deleted succesfuly");
            }

            return NotFound($"{changeId} not found");
        }

        [HttpPost("/Changes/{changeId}/Comments/SaveComment")]
        public IActionResult SaveComment(long changeId, [FromBody] string comment)
        {
            var change = DbContext.Changes.SingleOrDefault(i => i.Id == changeId);
            if (change == null)
            {
                return NotFound();
            }
            change.Comments.Add(comment);

            DbContext.SaveChanges();

            return Ok(comment);
        }

        [HttpGet("/Changes/{changeId}/Comments")]
        public IActionResult Comments(long changeId)
        {
            var change = DbContext.Changes.SingleOrDefault(i => i.Id == changeId);
            if (change == null)
            {
                return NotFound();
            }
            return Ok(new { Comments = change.Comments });
        }

        [HttpDelete("/Changes/{changeId}/Comments/{commentIndex}")]
        public IActionResult DeleteComment(long changeId, int commentIndex)
        {
            var change = DbContext.Changes.SingleOrDefault(i => i.Id == changeId);
            if (change == null)
            {
                return NotFound();
            }

            if (commentIndex < 0 || commentIndex >= change.Comments.Count)
            {
                return NotFound();
            }

            change.Comments.RemoveAt(commentIndex);

            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("/Changes/{changeId}/Incidents")]
        public IActionResult ChangeIncidents(long changeId)
        {
            var change = DbContext.Changes.Include(i => i.Incidents).SingleOrDefault(i => i.Id == changeId);
            if(change != null)
            {
                return Ok(change.Incidents);
            }
            return NotFound();
        }

        [HttpGet("/Changes/{changeId}/Problems")]
        public IActionResult ChangeProblems(long changeId)
        {
            var change = DbContext.Changes.Include(i => i.Problems).SingleOrDefault(i => i.Id == changeId);
            if(change != null)
            {
                return Ok(change.Problems);
            }
            return NotFound();
        }
    }
}