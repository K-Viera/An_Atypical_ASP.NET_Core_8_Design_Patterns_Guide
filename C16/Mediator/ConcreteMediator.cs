namespace Mediator
{
    public class ConcreteMediator : IMediator
    {
        private readonly List<IColleague> _colleagues;
        public ConcreteMediator (params IColleague[] colleagues)
        {
            ArgumentNullException.ThrowIfNull(colleagues);
            _colleagues = new List<IColleague>(colleagues);
        }
        public void Send(Message message)
        {
            foreach(var colleague in _colleagues)
            {
                colleague.ReceiveMessage(message);
            }
        }
    }
}
