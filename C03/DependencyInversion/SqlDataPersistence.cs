using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversion
{
    internal class SqlDataPersistence: IDataPersistence
    {
        public void Persist()
        {
            Console.WriteLine("Data persisted by SqlDataPersistence.");
        }
    }
}
