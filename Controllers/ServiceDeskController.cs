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
                DbContext.Incidents.Add(new Incident()
                {
                    Title = incident.Title,
                    Description = incident.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = incident.UserId,
                    User = user
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
            return View(incidents);
        }


        [HttpGet("/ServiceDesk/NewIncident")]
        public IActionResult NewIncident()
        {
            return View();
        }
    }
}
