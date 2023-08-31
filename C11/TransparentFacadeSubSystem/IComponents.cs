using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransparentFacadeSubSystem
{
    public interface IComponentA

    {

        string OperationA();

        string OperationB();

    }

    public interface IComponentB

    {

        string OperationC();

        string OperationD();

    }

    public interface IComponentC

    {

        string OperationE();

        string OperationF();

    }
}
