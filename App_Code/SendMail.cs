using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    public bool sendMailWithInfo(string mailSubject, string mailBody, string toMailId)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient mailClient = new SmtpClient();

            //Creating mail structure i.e. Body, ToAddress, FromAddress, Subject etc.
            mail.From = new MailAddress("MyGmailMailId","Scheduleit Ltd.");
            mail.To.Add(toMailId);
            mail.Subject = mailSubject;
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            mailClient.UseDefaultCredentials = true;
            mailClient.Credentials = new NetworkCredential("MyGmailMailId", "MyGmailPassword");
            mailClient.EnableSsl = true;
            mailClient.Host = "smtp.gmail.com";
            mailClient.Port = 25;

            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.Timeout = 100000;
            mailClient.Send(mail);
            return true;
        }
        catch (Exception e)
        {
            //'errMsg' to hold the string regarding the exception
            string errMsg = e.Message.ToString();
            //Do nothing when error occurs
            return false;
        }
    }
}