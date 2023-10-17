using AutoMapper;
using Core.Models;
using Core;

namespace MapperWeb
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<Product, ProductDetails>();
            CreateMap<NotEnoughStockException, NotEnoughStock>();
            CreateMap<ProductNotFoundException, ProductNotFound>();
        }

    }
}
