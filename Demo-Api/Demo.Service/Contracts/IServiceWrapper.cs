using Demo.Service.Contract;
using Demo.Service.Service;
using Demo.Service.Services;

namespace Demo.Service.Contracts
{
    public interface IServiceWrapper
    { 
        IBannerService Banner { get; } 
        ICityService City { get; }
        ICMSService CMS { get; } 
        IContactUsService ContactUs { get; }
        ICountryService Country { get; }
        ICustomerService Customer { get; } 
        IStateService State { get; } 
        IUserService User { get; }
        IFaqService Faq { get; } 
        IStoreProcedureService SP { get; }
    }
}
