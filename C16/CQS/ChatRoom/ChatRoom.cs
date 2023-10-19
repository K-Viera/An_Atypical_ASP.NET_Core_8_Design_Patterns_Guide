namespace Mediator
{
    public class ChatRoom : IChatRoom
    {
        private readonly List<IParticipant> _participants = new();
        public void Join(IParticipant participant)
        {
            _participants.Add(participant);
            participant.ChatRoomJoined(this);
            Send(new ChatMessage(participant, "Has joined the channel"));
        }
        public void Send(ChatMessage message)
        {
            _participants.ForEach(participant => { 
                participant.ReceiveMessage(message);
            });
        }
    }
}
