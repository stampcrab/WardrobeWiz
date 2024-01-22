using AutoMapper;

namespace Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, Providers.Customer>();
        }
    }
}
