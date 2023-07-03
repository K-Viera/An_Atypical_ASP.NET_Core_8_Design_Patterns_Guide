using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Classes
{
    public interface IContravariant<in T>
    {
        void Set(T value);
    }
}
