namespace ReviewApiApp.Services
{
    public class LocalMailService:IMailService
    {
        private readonly ILogger<LocalMailService> logger;
        private readonly IConfiguration configuration;
        private string MailTo = string.Empty;
        private string MailFrom = string.Empty;
        public LocalMailService(ILogger<LocalMailService> logger,IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

            MailTo = configuration["MailServiceSetting:MailToo"];
            MailFrom = configuration["MailServiceSetting:MailFrom"];
        }

        
        public void Send(string message, string subject)
        {
            this.logger.LogInformation($"send over loacally {message}  with {subject}  from {MailFrom} to {MailTo}");
        }
    }
}
