using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Eshopping_WebAPI.DBservices
{
   public class Utilities { 
    public bool send_email(string userEmail,string userName, string verificatioLink)
    {
            bool IsEmailSent = false;
            String body = "Dear " + userName + "," + System.Environment.NewLine + System.Environment.NewLine;
             body = body + "To verify your account, please click on the following link:" + System.Environment.NewLine + System.Environment.NewLine;
             body= body + verificatioLink + System.Environment.NewLine
                + System.Environment.NewLine+ System.Environment.NewLine +
                "Regards,"  +System.Environment.NewLine + "User Management Team";

        MailMessage mail = new MailMessage();
        mail.To.Add(userEmail);
        mail.From = new MailAddress("ramarchana.singh@gmail.com", "Take Away: my e-order");
        mail.Subject = "Accout Verification Email";
        //mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = body;
        //mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = false;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
            client.EnableSsl = true;
            client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.Credentials = new System.Net.NetworkCredential("ramarchana.singh@gmail.com", "swuttyzehwvsrxdt");
           
        
       
        try
        {
            client.Send(mail);
                IsEmailSent = true;
            }
        catch (Exception ex)
        {
            string errorMessage = string.Empty;
            errorMessage = ex.ToString();
                IsEmailSent = false;

            }

            return IsEmailSent;
    }

        public bool send_email(string userEmail, string VerificationCode)
        {
            bool IsEmailSent = false;
            String body = "Dear User," + System.Environment.NewLine + System.Environment.NewLine;
            body = body + "Below is your password reset verification code:" + System.Environment.NewLine + System.Environment.NewLine;
            body = body + VerificationCode + System.Environment.NewLine
               + System.Environment.NewLine + System.Environment.NewLine +
               "Regards," + System.Environment.NewLine + "User Management Team";

            MailMessage mail = new MailMessage();
            mail.To.Add(userEmail);
            mail.From = new MailAddress("ramarchana.singh@gmail.com", "Take Away: my e-order");
            mail.Subject = "Accout Verification Email";
            //mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.EnableSsl = true;
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("ramarchana.singh@gmail.com", "swuttyzehwvsrxdt");

            try
            {
                client.Send(mail);
                IsEmailSent=true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Empty;
                errorMessage = ex.ToString();
                IsEmailSent=false;

            }

            return IsEmailSent;
        }

        public bool send_email_Gen(string userEmail, String Subject, string Body)
        {
            bool IsEmailSent = false;
            String body = Body;

            MailMessage mail = new MailMessage();
            mail.To.Add(userEmail);
            mail.From = new MailAddress("ramarchana.singh@gmail.com", "Take Away:e-order");
            mail.Subject = Subject;
            mail.Body = body;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.EnableSsl = true;
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("ramarchana.singh@gmail.com", "swuttyzehwvsrxdt");



            try
            {
                client.Send(mail);
                IsEmailSent = true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Empty;
                errorMessage = ex.ToString();
                IsEmailSent = false;

            }

            return IsEmailSent;
        }



        ///********
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("pID", typeof(Int32));
            dt.Columns.Add("pricePerItem", typeof(decimal));
            dt.Columns.Add("pDsicountPercentage", typeof(Int32));
            dt.Columns.Add("pQuantity", typeof(Int32));
            return dt;
        }

    }
}

