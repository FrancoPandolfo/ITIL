using System.Net;
using System.Net.Mail;

namespace tp7518.Sys
{
    public static class EmailHelper
    {
        public static void Send(string to, string subject, string body)
        {
            // Configuración del servidor SMTP
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                // Credenciales de autenticación
                smtpClient.Credentials = new NetworkCredential("potg.arg@gmail.com", "uezpifuajekobsih");

                // Creación del mensaje de correo
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("potg.arg@gmail.com", "Gestio"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                try {
                    // Envío del correo electrónico
                    smtpClient.Send(mailMessage);                
                }
                catch(Exception ex)
                {
                    // Handle any exceptions here
                }
            }
        }
    }
}