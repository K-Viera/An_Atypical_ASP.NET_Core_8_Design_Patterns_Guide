using System.Xml.Linq;

namespace Mediator
{
    public class User : IParticipant
    {
        private IChatRoom? _chatRoom;
        private readonly IMessageWriter<ChatMessage> _messageWriter;
        public User( IMessageWriter<ChatMessage> messageWriter, string name)
        {
            _messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public string Name { get; }
        public void ChatRoomJoined(IChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }
        public void ReceiveMessage(ChatMessage message)
        {
            _messageWriter.Write(message);
        }
        public void Send(string message)
        {
            if (_chatRoom == null)
            {
                throw new ChatRoomNotJoinedException();
            }
            _chatRoom.Send(new ChatMessage(this, message));
        }
    }
    public class ChatRoomNotJoinedException : Exception
    {
        public ChatRoomNotJoinedException()
            : base("You must join a chat room before sending a message.")
        { }
    }
}
