namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "admin@mycompany.com";//to address 
        private string _mailFrom = "noreply@mycompany.com";//from address 
         //Mehtod send

         public CloudMailService(IConfiguration configuration)
         {
            _mailTo = configuration["mailSettings:mailToAddress"];//hierarchy
            _mailFrom = configuration["mailSettings:mailFromAddress"];
         }
         public void Send(string subject, string message)
         {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + 
                $"with {nameof(CloudMailService)}.");//cloud mail service written out to console window 
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
            //mail service end 
         } 
    }
}