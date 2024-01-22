using AutoMapper;

namespace Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product, Providers.Product>();
        }
    }
}
