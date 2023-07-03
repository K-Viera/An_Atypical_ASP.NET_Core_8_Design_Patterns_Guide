using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitution
{
    public class SuperClass
    {
        public virtual void Do()
            => throw new SuperException();

        public virtual bool IsValid(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "Value must be positive.",
                    nameof(value)
                );
            }
            return true;
        }
        public virtual Model Do(int value)
        {
            return new(value);
        }
    }
    public class SuperException : Exception { }
}
