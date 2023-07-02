using System.Text;
using ITIL.Model;
using tp7518.Sys;

namespace ITIL.Controllers.EmailTemplates
{
    public class NewProblemEmail
    {
        internal static void Send(ProblemEmailDto problem)
        {

            var sb = new StringBuilder();
            sb.Append($"<h1>{problem.Title}</h1>");
            sb.Append("<p>Se ha generado el problema con los siguientes incidentes relacionados:</p>");
            sb.Append("<br/>");
            sb.Append("<p>numeros de seguimiento:</p>");
            sb.Append("<br/>");
            foreach(var num in problem.TrackingNumbers){
                sb.Append($"<p>{num}</p>");
                sb.Append("<br/>");
            }
            sb.Append($"<p>Por favor revisar si existe la necesidad de un cambio.</p>");
            
            EmailHelper.Send(problem.TeamMemberEmail, $"Nuevo problema {problem.Title}", sb.ToString());
        }
    }
}