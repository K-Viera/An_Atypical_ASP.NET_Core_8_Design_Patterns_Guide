using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsole
{
    public static class Helper
    {
        public static (ConcreteMessageWriter, ConcreteColleague) CreateConcreteColleague(string name)
        {
            var messageWriter = new ConcreteMessageWriter();
            var concreateColleague = new ConcreteColleague(name, messageWriter);
            return (messageWriter, concreateColleague);
        }
    }
}
