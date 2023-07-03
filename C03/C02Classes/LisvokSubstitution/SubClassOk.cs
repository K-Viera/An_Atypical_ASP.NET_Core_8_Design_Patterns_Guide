using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitution
{
    public class SubClassOk : SuperClass
    {
        public override void Do()
            => throw new SubException();

        public override bool IsValid(int value)
        {
            if (value < -10)
            {
                throw new ArgumentException(
                    "Value must be greater or equal to -10.",
                    nameof(value)
                );
            }
            return true;
        }

        private int _doCount = 0;

        public override Model Do(int value)
        {
            var baseModel = base.Do(value);
            return new SubModel(baseModel.Value, ++_doCount);
        }
    }

    public class SubException : SuperException { }
    public record class SubModel(int Value, int DoCount) : Model(Value);
    public record class Model(int Value);
}
