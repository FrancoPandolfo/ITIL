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

        [HttpPost("/ServiceDesk/SaveIncident")]
        public IActionResult SaveIncident([FromBody] IncidentDto incident)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.SingleOrDefault(u => u.Id == incident.UserId);
                var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == incident.ConfigurationItemId);
                DbContext.Incidents.Add(new Incident()
                {
                    Title = incident.Title,
                    Description = incident.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = incident.UserId,
                    User = user,
                    ConfigurationItemId = incident.ConfigurationItemId,
                    ConfigurationItem = configurationItem
                });

                DbContext.SaveChanges();
                return Ok(incident);
            }

            return BadRequest();
        }

        [HttpPatch("/ServiceDesk/Incidents/{incidentId}")]
        public IActionResult UpdateIncident([FromBody] IncidentDto modifiedIncident, long incidentId)
        {
            if (ModelState.IsValid)
            {
                var incident = DbContext.Incidents.SingleOrDefault(i => i.Id == incidentId);
                if(incident != null)
                {
                    incident.Title = modifiedIncident.Title;
                    incident.Description = modifiedIncident.Description;
                    DbContext.Incidents.Update(incident);
                    DbContext.SaveChanges();
                    return Ok(incident);
                }
            }
            return BadRequest();
        }

        [HttpGet("/ServiceDesk/Incidents")]
        public IActionResult Incidents()
        {
            var incidents = DbContext.Incidents.OrderByDescending(i => i.CreatedDate);
            return View(incidents);
        }

        [HttpGet("/ServiceDesk/Incidents/{incidentId}")]
        public IActionResult IncidentInfo(long incidentId)
        {
            var incident = DbContext.Incidents.SingleOrDefault(i => i.Id == incidentId);
            if(incident != null)
            {
              var configurationItem = DbContext.Configuration.SingleOrDefault(c => c.Id == incident.ConfigurationItemId);
              if(configurationItem != null) {
                incident.ConfigurationItem = configurationItem;
              }
              return View(incident);
            }
            return NotFound($"{incidentId} not found");
                
        }

        [HttpGet("/ServiceDesk/NewIncident")]
        public IActionResult NewIncident()
        {
            var items = DbContext.Configuration;
            return View(items);
        }

        [HttpPost("/ServiceDesk/DeleteIncident")]
        public IActionResult DeleteIncident([FromBody] long incidentId)
        {
            var incident = DbContext.Incidents.SingleOrDefault(i => i.Id == incidentId);
            if(incident != null)
            { 
                DbContext.Incidents.Remove(incident);
                DbContext.SaveChanges();
                return Ok($"Incident {incidentId} deleted succesfuly");
            }

            return NotFound($"{incidentId} not found");
        }

    }
}
