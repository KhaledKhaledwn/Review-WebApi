namespace ReviewApiApp.Services
{
    public class CloudMailService:IMailService
    {
        private readonly ILogger<CloudMailService> logger;
        private readonly IConfiguration configuration;
        private string MailTo = string.Empty;
        private string MailFrom = string.Empty;

        public CloudMailService(ILogger<CloudMailService> _logger,IConfiguration configuration)
        {
            this.logger = _logger;
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
