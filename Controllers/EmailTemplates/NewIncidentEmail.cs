using System.Text;
using ITIL.Data;
using ITIL.Model;
using tp7518.Sys;

namespace ITIL.Controllers.EmailTemplates
{
    public class NewIncidentEmail
    {
        internal static void Send(IncidentEmailDto incident)
        {

            var sb = new StringBuilder();
            sb.Append($"<h1>{incident.Title}</h1>");
            sb.Append($"<p>Se ha generado el numero de seguimiento: {incident.TrackingNumber}</p>");
            sb.Append("<br/>");
            sb.Append($"<p>Le volveremos a escribir cuando haya alguna novedad.</p>");
            
            EmailHelper.Send(incident.ClientEmail, $"Numero de seguimiento del incidente {incident.Title}", sb.ToString());
        }
    }
}