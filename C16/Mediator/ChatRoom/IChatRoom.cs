namespace Mediator
{
    public interface IChatRoom
    {
        void Join(IParticipant participant);
        void Send(ChatMessage message);
    }
}
