namespace MessageInterpreter.MixingChain_Template
{
    public class ImpAlarmTriggeredHandler : MessageHandlerBase
    {
        protected override string HandledMessageName => "AlarmTriggered";
        public ImpAlarmTriggeredHandler(IMessageHandler? handler = null) : base(handler) { }
        protected override void Process(Message message)
        {
            
        }
    }
    public class ImpAlarmPausedHandler : MessageHandlerBase
    {
        protected override string HandledMessageName => "AlarmPaused";
        public ImpAlarmPausedHandler(IMessageHandler? next = null) : base(next) { }
        protected override void Process(Message message)
        {
            // Do something clever with the Payload 
        }
    }

    public class ImpAlarmStoppedHandler : MessageHandlerBase
    {
        protected override string HandledMessageName => "AlarmStopped";
        public ImpAlarmStoppedHandler(IMessageHandler? next = null)
            : base(next) { }
        protected override void Process(Message message)
        {
            // Do something clever with the Payload 
        }
    }
}
