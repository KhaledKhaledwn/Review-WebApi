namespace ReviewApiApp.Services
{
    public interface IMailService
    {
        public void Send(string message, string subject);
    }
}
