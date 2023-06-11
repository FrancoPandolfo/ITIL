using ITIL.Data;
using ITIL.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{
    public class MetricsController: Controller
    {
        public ITILDbContext DbContext {get;set;}
        public MetricsController(ITILDbContext dbContext){
            DbContext = dbContext;
        }

        [HttpGet("/Metrics")]
        public IActionResult Metrics()
        {
            return View();
        }

        [HttpGet("/Metrics/Incidents")]
        public IActionResult IncidentMetrics()
        {
            var incidents = DbContext.Incidents;
            if (incidents == null) {return NotFound();}
            var incidentMetrics = GetIncidentMetrics();
            return Ok(incidentMetrics);
        }

        public IncidentMetricsDto GetIncidentMetrics()
        {
            DateTime startDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = DateTime.UtcNow;

            int totalDays = (int)(endDate - startDate).TotalDays;
            int totalHours = (int)(endDate - startDate).TotalHours;

            float[] incidentsPerDay = new float[7];
            float[] incidentsPerHour = new float[24];

            int[] incidentsCountPerDayOfWeek = DbContext.Incidents
                .Where(i => i.CreatedDate >= startDate && i.CreatedDate <= endDate)
                .GroupBy(i => i.CreatedDate.DayOfWeek)
                .Select(g => g.Count())
                .ToArray();

            for (int i = 0; i < incidentsCountPerDayOfWeek.Length; i++)
            {
                incidentsPerDay[i] = (float)incidentsCountPerDayOfWeek[i] / totalDays;
            }

            int[] incidentsCountPerHourOfDay = DbContext.Incidents
                .Where(i => i.CreatedDate >= startDate && i.CreatedDate <= endDate)
                .GroupBy(i => i.CreatedDate.Hour)
                .Select(g => g.Count())
                .ToArray();

            for (int i = 0; i < incidentsCountPerHourOfDay.Length; i++)
            {
                incidentsPerHour[i] = (float)incidentsCountPerHourOfDay[i] / totalHours;
            }

            int maxDay = incidentsCountPerDayOfWeek.Max();
            int maxIndex = incidentsCountPerDayOfWeek.ToList().IndexOf(maxDay);
            string dayWithMostIncidents = Enum.GetName(typeof(DayOfWeek), maxIndex) ?? "Unknown";

            int maxHour = incidentsCountPerHourOfDay.Max();
            int hourWithMostIncidents = incidentsCountPerHourOfDay.ToList().IndexOf(maxHour);

            TimeSpan totalResolutionTime = TimeSpan.Zero;
            int closedIncidentsCount = DbContext.Incidents
                .Count(i => i.State == "IMPLEMENTADO" && i.ClosedDate >= startDate && i.ClosedDate <= endDate);

            if (closedIncidentsCount > 0)
            {
                totalResolutionTime = TimeSpan.FromMilliseconds(DbContext.Incidents
                    .Where(i => i.State == "IMPLEMENTADO" && i.ClosedDate >= startDate && i.ClosedDate <= endDate)
                    .Sum(i => (i.ClosedDate - i.CreatedDate).TotalMilliseconds));
            }

            TimeSpan avgResolutionTime = TimeSpan.Zero;
            if (closedIncidentsCount > 0)
            {
                avgResolutionTime = TimeSpan.FromTicks(totalResolutionTime.Ticks / closedIncidentsCount);
            }

            var incidentMetrics = new IncidentMetricsDto()
            {
                IncidentsPerDay = incidentsPerDay,
                IncidentsPerHour = incidentsPerHour,
                DayWithMostIncidents = dayWithMostIncidents,
                HourWithMostIncidents = hourWithMostIncidents,
                AvgResolutionTime = avgResolutionTime.ToString()
            };

            return incidentMetrics;
        }
    }
}