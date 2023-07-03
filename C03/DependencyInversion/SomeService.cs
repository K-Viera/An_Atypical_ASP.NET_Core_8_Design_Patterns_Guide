using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversion
{
    public class SomeService
    {
        public void Operation(IDataPersistence someDataPersistence)
        {
            Console.WriteLine("Beginning SomeService.Operation.");
            someDataPersistence.Persist();
            Console.WriteLine("SomeService.Operation has ended.");
        }
    }
}
