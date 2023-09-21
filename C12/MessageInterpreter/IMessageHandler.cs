namespace MessageInterpreter
{
    public interface IMessageHandler
    {
        void Handle(Message message);
    }
}
