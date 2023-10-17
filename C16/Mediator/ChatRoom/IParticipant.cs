namespace Mediator
{
    public interface IParticipant
    {
        string Name { get; }
        void Send(string message);
        void ReceiveMessage(ChatMessage message);
        void ChatRoomJoined(IChatRoom chatRoom);
    }
}
