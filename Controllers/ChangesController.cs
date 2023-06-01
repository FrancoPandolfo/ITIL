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
        public IActionResult SaveChange([FromBody] ChangeDto change)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == change.UserId);
                var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == change.ConfigurationItemId);
                var assignedUser = DbContext.Users.SingleOrDefault(u => u.Id == change.AssignedUserId);
                DbContext.Changes.Add(new Change()
                {
                    Title = change.Title,
                    Description = change.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = change.UserId,
                    User = user,
                    ConfigurationItemId = change.ConfigurationItemId,
                    ConfigurationItem = configurationItem,
                    ClientName = change.ClientName,
                    ClientEmail = change.ClientEmail,
                    State = State.ABIERTO,
                    AssignedUserId = change.AssignedUserId,
                    AssignedUser = assignedUser,
                    Impact = change.Impact,
                    Priority = change.Priority
                });

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
    }
}