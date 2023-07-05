namespace CityInfo.API.Services
{
    public interface IMailService//refactoring 
    {
        void Send(string subject, string message);
    }

    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;//set as empty string to confirm values are coming from the config files & read only
        private readonly string _mailFrom = string.Empty;//from address 
        
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];//hierarchy
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
            //mail service end 
        }
    }
} 