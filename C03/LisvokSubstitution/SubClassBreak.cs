using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitution
{
    public class SubClassBreak : SuperClass
    {
        public override void Do()
            => throw new AnotherException();

        public override bool IsValid(int value)
        {
            if (value < 10) // Break LSP
            {
                throw new ArgumentException(
                    "Value must be greater than 10.",
                    nameof(value)
                );
            }

            return true;
        }

        public override Model Do(int value)
        {
            if (value == 5)
            {
                return null;
            }
            return base.Do(value);
        }
    }
    public class AnotherException : Exception { }
}
