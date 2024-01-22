
using AutoMapper;

namespace Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Providers.Order>();
            CreateMap<Db.OrderItem, Providers.OrderItem>();
        }
    }
}
