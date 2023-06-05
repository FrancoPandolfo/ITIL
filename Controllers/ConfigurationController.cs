using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{
    public class ConfigurationController : Controller
    {
        public ITILDbContext DbContext {get;set;}
        public ConfigurationController(ITILDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("/Configuration")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost("/Configuration/SaveItem")]
        public IActionResult SaveItem([FromBody] ConfigurationItemDto item)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == item.UserId);
                DbContext.Configuration.Add(new ConfigurationItem()
                {
                    Title = item.Title,
                    Description = item.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = item.UserId,
                    User = user,
                    VersionId = item.VersionId
                });

                DbContext.SaveChanges();
                return Ok(item);
            }

            return BadRequest();
        }

        [HttpPatch("/Configuration/Items/{itemId}")]
        public IActionResult UpdateItem([FromBody] ConfigurationItemDto modifiedItem, long itemId)
        {
            if (ModelState.IsValid)
            {
                var item = DbContext.Configuration.SingleOrDefault(i => i.Id == itemId);
                if(item != null)
                {
                    item.Title = modifiedItem.Title;
                    item.Description = modifiedItem.Description;
                    item.VersionId = modifiedItem.VersionId;
                    DbContext.Configuration.Update(item);
                    DbContext.SaveChanges();
                    return Ok(item);
                }
            }
            return BadRequest();
        }

        [HttpGet("/Configuration/Items")]
        public IActionResult Items()
        {
            var items = DbContext.Configuration.OrderByDescending(i => i.CreatedDate);;
            return View(items);
        }

        [HttpGet("/Configuration/Items/{itemId}")]
        public IActionResult ItemInfo(long itemId)
        {
            var item = DbContext.Configuration.SingleOrDefault(i => i.Id == itemId);
            if(item != null)
            {
              return View(item);
            }
            return NotFound($"{itemId} not found");
                
        }

        [HttpGet("/Configuration/NewItem")]
        public IActionResult NewItem()
        {
            return View();
        }

        [HttpPost("/Configuration/DeleteItem")]
        public IActionResult DeleteItem([FromBody] long itemId)
        {
            var item = DbContext.Configuration.SingleOrDefault(i => i.Id == itemId);
            if(item != null)
            { 
                DbContext.Configuration.Remove(item);
                DbContext.SaveChanges();
                return Ok($"Item {itemId} deleted succesfuly");
            }

            return NotFound($"{itemId} not found");
        }

    }
}