using Microsoft.Extensions.Options;

namespace CommonScenarios
{
    public class EmailOptions
    {
        public string? SenderEmailAddress { get; set; }
    }

    public class NotificationService
    {
        private EmailOptions _emailOptions;
        private readonly ILogger _logger;

        public NotificationService(IOptionsMonitor<EmailOptions> emailOptionsMonitor, ILogger<NotificationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ArgumentNullException.ThrowIfNull(emailOptionsMonitor);
            _emailOptions = emailOptionsMonitor.CurrentValue;
            emailOptionsMonitor.OnChange((options) => _emailOptions = options);
        }

        public Task NotifyAsync(string to)
        {
            _logger.LogInformation(
                "Notification sent by '{SenderEmailAddress}' to '{to}'.",
                _emailOptions.SenderEmailAddress,
                to
            );
            return Task.CompletedTask;
        }
    }
}
