using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversion
{
    public class LocalDataPersistence : IDataPersistence
    {
        public void Persist()
        {
            Console.WriteLine("Data persisted by LocalDataPersistence.");
        }
    }
}
