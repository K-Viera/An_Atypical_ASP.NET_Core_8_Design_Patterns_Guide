using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregation
{
    public interface IProductRepository
    {
        public ValueTask<IEnumerable<Product>> GetAllPublicProductAsync();
        public ValueTask<IEnumerable<Product>> GetAllPrivateProductAsync();
        public ValueTask<Product> GetOnePublicProductAsync(int productId);
        public ValueTask<Product> GetOnePrivateProductAsync(int productId);
        public ValueTask CreateAsync(Product product);
        public ValueTask UpdateAsync(Product product);
        public ValueTask DeleteAsync(Product product);
    }
}
