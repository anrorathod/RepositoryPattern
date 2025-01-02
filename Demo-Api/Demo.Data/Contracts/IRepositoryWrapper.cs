using Demo.Data.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Data.Contracts
{
    public interface IRepositoryWrapper
    { 
        IBannerRepository Banner { get; } 
        ICityRepository City { get; }
        ICMSRepository CMS { get; }
        IContactUsRepository ContactUs { get; }
        ICountryRepository Country { get; } 
        ICustomerRepository Customer { get; } 
        IStateRepository State { get; } 
        IUserRepository User { get; }
        IFaqRepository Faq { get; } 
        IStoreProcedureRepository SP { get; }
    }
}
