namespace MessageInterpreter
{
    public class AlarmTriggeredHandler : IMessageHandler
    {
        private readonly IMessageHandler? _next;
        public AlarmTriggeredHandler(IMessageHandler? next = null)
        {
            _next = next;
        }
        public void Handle(Message message)
        {
            if (message.Name == "AlarmTriggered")
            {
                // do something
            }
            else _next?.Handle(message);
        }
    }
    public class AlarmPausedHandler : IMessageHandler
    {
        private readonly IMessageHandler? _next;
        public AlarmPausedHandler(IMessageHandler? next = null)
        {
            _next = next;
        }
        public void Handle(Message message)
        {
            if (message.Name == "AlarmPaused")
            {
                // Do something clever with the Payload 
            }
            else
            {
                _next?.Handle(message);
            }
        }
    }
    public class AlarmStoppedHandler : IMessageHandler
    {
        private readonly IMessageHandler? _next;
        public AlarmStoppedHandler(IMessageHandler? next = null)
        {
            _next = next;
        }

        public void Handle(Message message)
        {
            if (message.Name == "AlarmStopped")
            {
                // Do something clever with the Payload 
            }

            else
            {
                _next?.Handle(message);
            }

        }
    }
    public class DefaultHandler : IMessageHandler
    {
        public void Handle(Message message)
        {
            throw new NotSupportedException(
                $"Messages named '{message.Name}' are not supported.");
        }
    }
}
