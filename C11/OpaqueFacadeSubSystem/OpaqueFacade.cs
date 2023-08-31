using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpaqueFacadeSubSystem
{
    internal class OpaqueFacade : IOpaqueFacade
    {
        private readonly ComponentA _componentA;
        private readonly ComponentB _componentB;
        private readonly ComponentC _componentC;
        //instruction injection
        internal OpaqueFacade(ComponentA componentA, ComponentB componentB, ComponentC componentC)
        {
            _componentA = componentA;
            _componentB = componentB;
            _componentC = componentC;
        }
        public string ExecuteOperationA() {
            return new StringBuilder()
                .AppendLine(_componentA.OperationA())
                .AppendLine(_componentA.OperationB())
                .AppendLine(_componentB.OperationD())
                .AppendLine(_componentC.OperationE())
                .ToString();
        }
        public string ExecuteOperationB() {
            return new StringBuilder()
                .AppendLine(_componentB.OperationC())
                .AppendLine(_componentB.OperationD())
                .AppendLine(_componentC.OperationF())
                .ToString();
        }
    }
}
