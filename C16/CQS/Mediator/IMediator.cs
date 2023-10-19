namespace Mediator
{
    public interface IMediator
    {
        void Send(Message message);
    }

    public interface IColleague
    {
        string Name { get; }
        void ReceiveMessage(Message message);
    }

    public record class Message(IColleague Sender, string Content);
}
