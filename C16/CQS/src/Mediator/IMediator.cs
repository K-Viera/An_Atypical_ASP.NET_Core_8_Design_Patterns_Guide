namespace Mediator;

public interface IMediator
{
    TReturn Send<TQuery, TReturn>(TQuery query)
            where TQuery : IQuery<TReturn>;
    void Send<TCommand>(TCommand command)
        where TCommand : ICommand;

    void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
        where TCommand : ICommand;

    void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> commandHandler)
        where TQuery : IQuery<TReturn>;
}
public interface ICommand { }
public interface  ICommandHandler<TCommand> where TCommand:ICommand
{
    void Handle(TCommand command);
}
public interface IQuery<TReturn> { }
public interface IQueryHandler <TQuery, TReturn> where TQuery : IQuery<TReturn>
{
    TReturn Handle(TQuery query);
}
public interface IColleague
{
    string Name { get; }
    void ReceiveMessage(Message message);
}

public record class Message(IColleague Sender, string Content);

//public class ConcreteMediator : IMediator
//{
//    private readonly List<IColleague> _colleagues;
//    public ConcreteMediator(params IColleague[] colleagues)
//    {
//        ArgumentNullException.ThrowIfNull(colleagues);
//        _colleagues = new List<IColleague>(colleagues);
//    }

//    public void Send(Message message)
//    {
//        foreach (var colleague in _colleagues)
//        {
//            colleague.ReceiveMessage(message);
//        }
//    }
//}

//public class ConcreteColleague : IColleague
//{
//    private readonly IMessageWriter<Message> _messageWriter;
//    public ConcreteColleague(string name, IMessageWriter<Message> messageWriter)
//    {
//        Name = name ?? throw new ArgumentNullException(nameof(name));
//        _messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
//    }

//    public string Name { get; }

//    public void ReceiveMessage(Message message)
//    {
//        _messageWriter.Write(message);
//    }
//}

public class Mediator : IMediator
{
    private readonly HandlerDictionary _handlers = new();

    public void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
        where TCommand : ICommand
    {
        _handlers.AddHandler(commandHandler);
    }

    public void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> commandHandler)
        where TQuery : IQuery<TReturn>
    {
        _handlers.AddHandler(commandHandler);
    }

    public TReturn Send<TQuery, TReturn>(TQuery query)
        where TQuery : IQuery<TReturn>
    {
        var handler = _handlers.Find<TQuery, TReturn>();
        return handler.Handle(query);
    }

    public void Send<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var handlers = _handlers.FindAll<TCommand>();
        foreach (var handler in handlers)
        {
            handler.Handle(command);
        }
    }

    private class HandlerList
    {
        private readonly List<object> _commandHandlers = new();
        private readonly List<object> _queryHandlers = new();

        public void Add<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            _commandHandlers.Add(handler);
        }

        public void Add<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn>
        {
            _queryHandlers.Add(handler);
        }

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            foreach (var handler in _commandHandlers)
            {
                if (handler is ICommandHandler<TCommand> output)
                {
                    yield return output;
                }
            }
        }
        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            foreach (var handler in _queryHandlers)
            {
                if (handler is IQueryHandler<TQuery, TReturn> output)
                {
                    return output;
                }
            }
            throw new QueryHandlerNotFoundException(typeof(TQuery));
        }
    }

    private class HandlerDictionary
    {
        private readonly Dictionary<Type, HandlerList> _handlers = new();

        public void AddHandler<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            // if the Handler doesn't exists inside the dictionary 
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }


        public void AddHandler<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.FindAll<TCommand>();
        }

        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.Find<TQuery, TReturn>();
        }

        private void EnforceTypeEntry(Type type)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new HandlerList());
            }
        }
    }
}

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException(Type queryType)
        : base($"No handler found for query '{queryType}'.")
    {
    }
}