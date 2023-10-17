namespace Mediator
{
    public interface IMessageWriter<Tmessage>
    {
        void Write(Tmessage message);
    }
}
