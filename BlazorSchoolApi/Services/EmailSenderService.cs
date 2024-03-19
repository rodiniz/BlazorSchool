using BlazorSchoolApi.Interfaces;
using FluentEmail.Core;
using FluentEmail.Mailgun;

namespace BlazorSchoolApi.Services
{
    public class EmailSenderService : IEmailSenderService
    {


        public EmailSenderService(IConfiguration configuration)
        {
            var apiKey = configuration["MailGunApiKey"];
            var sender = new MailgunSender(
                "sandboxc9ab5a4158094adaaa9e8b6191cec683.mailgun.org",
                apiKey
            );
            Email.DefaultSender = sender;

        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var email = Email
               .From("rochagasdiniz@gmail.com")
               .To(toEmail)
               .Subject(subject)
               .Body(body);

            var response = await email.SendAsync();
            if (!response.Successful)
            {
                throw new Exception(response.ErrorMessages.ToString());
            }
        }

    }
}
