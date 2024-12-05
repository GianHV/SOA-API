using AutoMapper;
using SOA_API.Models;

namespace SOA_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPutDTO, Product>();
        }
    }
}
