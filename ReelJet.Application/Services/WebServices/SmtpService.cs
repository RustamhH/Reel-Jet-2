using System;
using System.Net;
using System.Windows;
using System.Net.Mail;
using Reel_Jet.Models;

namespace Reel_Jet.Services.WebServices;

public static class SmtpService {

    public static bool isHtml = false;
    public static MailAddress to;
    private static MailAddress from = new MailAddress("reeljett@gmail.com");
    private static MailMessage email;

    public static void sendMail(string mail, NotificationService notification) {
        try {
            to = new MailAddress(mail);
            email = new MailMessage(from, to);
        }catch(Exception ex) {
            MessageBox.Show(ex.Message);
        }

        email.IsBodyHtml = isHtml;
        email.Subject = notification.Title;
        email.Body = notification.Text;

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 25;
        smtp.Credentials = new NetworkCredential("reeljett@gmail.com", "kyzrgjrzqyhxknua");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;

        try {
            smtp.Send(email);
            isHtml = false;
        }
        catch (SmtpException ex) {
            MessageBox.Show(ex.Message);
        }
    }
}