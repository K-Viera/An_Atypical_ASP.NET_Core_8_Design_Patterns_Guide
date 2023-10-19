namespace Mediator
{
    public record class ChatMessage(IParticipant Sender, string Content);
}
