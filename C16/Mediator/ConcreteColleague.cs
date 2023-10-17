using System.Xml.Linq;

namespace Mediator
{
    public class ConcreteColleague : IColleague
    {
        private readonly IMessageWriter<Message> _messageWriter;
        public ConcreteColleague(string name, IMessageWriter<Message> messageWriter)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
        }
        public string Name { get; }

        public void ReceiveMessage(Message message)
        {
            _messageWriter.Write(message);
        }
    }
}
