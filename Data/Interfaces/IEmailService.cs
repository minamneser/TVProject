namespace TVProject.Data.Interfaces
{
    public interface Iemailsetting
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
