using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregation
{
    public class PublicProductReader:IProductReader
    {
        public ValueTask<IEnumerable<Product>> GetAllAsync()
            => throw new NotImplementedException();

        public ValueTask<Product> GetOneAsync(int productId)
            => throw new NotImplementedException();
    }
}
