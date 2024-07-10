namespace SimpleTutorialWebApplication.Services;

public class LocalMailService(ILogger<LocalMailService> logger): IMailService
{

    private readonly ILogger<LocalMailService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private string _mailTo = "admin@company.com";

    private string _mailFrom = "noreply@company.com";

    public void Send(string subject, string body) {
        _logger.LogInformation($"Mail sent from {_mailFrom} to {_mailTo}");
        _logger.LogInformation($"Subject: {subject}");
        _logger.LogInformation($"Body: {body}");
    }
}
