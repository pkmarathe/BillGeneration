using System;
using System.Net;
using System.IO; 
using System.Net.Mail;
using System.Collections.Generic; 

public class SendEmailSMS
{
    #region "Public Methods Send Email With Attachment"
    public string Sending_Email(String From, String To, String CC, String Subject, String Body, String strFileName)
    { 
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        List<string> EmailTo = new List<string>();
        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress(From);
            message.From = fromAddress;

            string[] ToMailStr = To.Split(',');
            for (int i = 0; i < ToMailStr.Length; i++)
            {
                if (ToMailStr[i].Contains(";"))
                {
                    string[] ToMail = ToMailStr[i].Split(';');
                    for (int ig = 0; ig < ToMail.Length; ig++)
                    {
                        EmailTo.Add(ToMail[ig]);
                    }
                }
                else
                {
                    EmailTo.Add(ToMailStr[i]);
                }
            }

            if (EmailTo.Count > 0)
            {
                for (int k = 0; k < EmailTo.Count; k++)
                {
                    message.To.Add(EmailTo[k].ToString());
                }
            }

            if (CC.Length > 0)
            {
                string[] ccMailStr = CC.Split(',');
                for (int i = 0; i < ccMailStr.Length; i++)
                {
                    message.CC.Add(new MailAddress(ccMailStr[i]));
                }
            }
            //if (BCC.Length > 0)
            //{
            //    string[] BccMailStr = BCC.Split(',');
            //    for (int i = 0; i < BccMailStr.Length; i++)
            //    {
            //        message.Bcc.Add(new MailAddress(BccMailStr[i]));
            //    }
            //}

            if (strFileName.Length != 0)
            {
                try
                {
                    string[] AttObjstr = strFileName.Split(',');
                    for (int i = 0; i < AttObjstr.Length; i++)
                    {
                        byte[] data = new WebClient().DownloadData(AttObjstr[i].ToString());
                        string FileName = System.IO.Path.GetFileName(AttObjstr[i].ToString());
                        Attachment att = new Attachment(new MemoryStream(data), FileName);
                        message.Attachments.Add(att);
                    }
                }
                catch
                {
                }
            }

            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = Body;

            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.Default;

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                               (System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

            message.AlternateViews.Add(plainView);
            message.AlternateViews.Add(htmlView);

            //authenticate emails

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential NetworkCred = new NetworkCredential("support@citizencop.org", "greenGENE0000");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = NetworkCred;
            smtpClient.Port = 587;

            smtpClient.Send(message);
            msg = "Send Mail!!";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally
        {
            message.Dispose();
        }
        return msg;
    } 
    #endregion
}