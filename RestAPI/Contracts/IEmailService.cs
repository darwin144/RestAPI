using System.Net.Mail;
using RestAPI.Utility;


namespace RestAPI.Contracts
{
    public interface IEmailService
    {
        void SentEmailAsynch();
        EmailService SetEmail(string email);
        EmailService SetSubject(string subject);
        EmailService SetHtmlMessage(string htmlMessage);
    }
}
