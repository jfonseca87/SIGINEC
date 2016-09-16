using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Model.Helpers
{
    public class Mail
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        MailMessage mail = null;

        public void sendMail(string asunto, string mensaje, string correo)
        {
            client.Credentials = new System.Net.NetworkCredential("postmaster.siginec@gmail.com", "Siginec.123");
            client.EnableSsl = true;
            mail = new MailMessage("jorge.fonseca87@gmail.com", correo);
            mail.Subject = asunto;
            mail.Body = mensaje;

            client.Send(mail);
        }
    }
}
