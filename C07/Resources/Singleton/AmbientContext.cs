using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Singleton
{
    public class AmbientContext
    {
        public static AmbientContext Current { get; } = new AmbientContext();
        private AmbientContext() { }
        public void WriteSomething(string something)
        {
            Console.WriteLine(something);
        }
    }
}
