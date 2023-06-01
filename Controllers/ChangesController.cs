using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

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
                    State = State.ABIERTO
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
                var change = DbContext.Changes.SingleOrDefault(i => i.Id == changeId);
                if(change != null)
                {
                    change.Title = modifiedChange.Title;
                    change.Description = modifiedChange.Description;
                    change.State = modifiedChange.State;
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
              return View(change);
            }
            return NotFound($"{changeId} not found");
                
        }

        [HttpGet("/Changes/NewChange")]
        public IActionResult NewChange()
        {
            var items = DbContext.Configuration;
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