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
                DbContext.Incidents.Add(new Incident()
                {
                  Title = incident.Title,
                  Description = incident.Description,
                  CreatedDate = DateTime.UtcNow 
                });
                DbContext.SaveChanges();
                return Ok(incident);
            }
            return BadRequest();
        }

        [HttpGet("/ServiceDesk/Incidents")]
        public IActionResult Incidents()
        {
            var incidents = DbContext.Incidents;
            if(incidents.Any()){ return View(incidents); }
            return NotFound();
        }
    }
}
