﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregation
{
    public class PrivateProductRepository: IProductReader,IProductWriter
    {
        public ValueTask<IEnumerable<Product>> GetAllAsync()
            => throw new NotImplementedException();
        public ValueTask<Product> GetOneAsync(int productId)
            => throw new NotImplementedException();
        public ValueTask CreateAsync(Product product)
            => throw new NotImplementedException();
        public ValueTask DeleteAsync(Product product)
            => throw new NotImplementedException();
        public ValueTask UpdateAsync(Product product)
            => throw new NotImplementedException();
    }
}
