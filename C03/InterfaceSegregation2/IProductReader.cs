using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregation
{
    public interface IProductReader
    {
        public ValueTask<IEnumerable<Product>> GetAllAsync();
        public ValueTask<Product> GetOneAsync(int productId);
    }
}
