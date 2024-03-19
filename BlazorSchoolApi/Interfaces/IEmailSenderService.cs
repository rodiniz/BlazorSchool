namespace BlazorSchoolApi.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
