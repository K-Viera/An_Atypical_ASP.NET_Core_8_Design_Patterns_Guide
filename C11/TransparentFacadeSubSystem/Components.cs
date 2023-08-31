using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransparentFacadeSubSystem
{
    public class ComponentA : IComponentA

    {

        public string OperationA() => "Component A, Operation A";

        public string OperationB() => "Component A, Operation B";

    }

    public class ComponentB : IComponentB

    {

        public string OperationC() => "Component B, Operation C";

        public string OperationD() => "Component B, Operation D";

    }

    public class ComponentC : IComponentC

    {

        public string OperationE() => "Component C, Operation E";

        public string OperationF() => "Component C, Operation F";

    }
}
