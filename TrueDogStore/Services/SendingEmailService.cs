using MimeKit;

namespace TrueDogStore.Services
{
    public class SendingEmailService
    {
        private readonly ILogger<SendingEmailService> _logger;
        public SendingEmailService(ILogger<SendingEmailService> logger)
        {
            _logger = logger;
        }
      
    }
}

