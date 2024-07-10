namespace SimpleTutorialWebApplication.Services;

public interface IMailService
{
    void Send(string subject, string body);
}
