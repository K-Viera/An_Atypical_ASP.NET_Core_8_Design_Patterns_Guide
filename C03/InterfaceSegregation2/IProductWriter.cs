using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregation
{
    internal interface IProductWriter
    {
        public ValueTask CreateAsync(Product product);
        public ValueTask UpdateAsync(Product product);
        public ValueTask DeleteAsync(Product product);
    }
}
