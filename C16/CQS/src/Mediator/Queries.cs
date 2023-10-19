using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class ListParticipants
    {
        public record class Query(IChatRoom ChatRoom, IParticipant Requester) : IQuery<IEnumerable<IParticipant>>;
        public class Handler : IQueryHandler<Query, IEnumerable<IParticipant>>
        {
            public IEnumerable<IParticipant> Handle(Query query)
            {
                return query.ChatRoom.ListParticipants();
            }
        }
    }

    public class ListMessages
    {
        public record class Query(IChatRoom ChatRoom, IParticipant Requester) : IQuery<IEnumerable<ChatMessage>>;
        public class Handler : IQueryHandler<Query, IEnumerable<ChatMessage>>
        {
            public IEnumerable<ChatMessage> Handle(Query query)
            {
                return query.ChatRoom.ListMessages();
            }
        }
    }
}
