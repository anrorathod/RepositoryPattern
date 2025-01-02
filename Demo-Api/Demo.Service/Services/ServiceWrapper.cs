using AutoMapper;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Data.Repositories;
using Demo.Service.Contract;
using Demo.Service.Contracts;
using Demo.Service.Service;

namespace Demo.Service.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IBannerService bannerService; 
        private ICityService cityService;
        private ICMSService cmsService;
        private IContactUsService contactUsService;
        private ICountryService countryService;
        private ICustomerService customerService; 
        private IStateService stateService; 
        private IUserService userService;
        private IFaqService faqService; 
        private IStoreProcedureService spService;
        private IRepositoryWrapper repository; 
        private readonly IMapper mapper;
        public ServiceWrapper(IRepositoryWrapper _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        } 

        public IBannerService Banner
        {
            get
            {
                if (bannerService == null)
                {
                    bannerService = new BannerService(repository, mapper);
                }
                return bannerService;
            }
        }
         
        public ICityService City
        {
            get
            {
                if (cityService == null)
                {
                    cityService = new CityService(repository, mapper);
                }
                return cityService;
            }
        }

       
        public ICMSService CMS
        {
            get
            {
                if (cmsService == null)
                {
                    cmsService = new CMSService(repository, mapper);
                }
                return cmsService;
            }
        }

        public IContactUsService ContactUs
        {
            get
            {
                if (contactUsService == null)
                {
                    contactUsService = new ContactUsService(repository, mapper);
                }
                return contactUsService;
            }
        }

        public ICountryService Country
        {
            get
            {
                if (countryService == null)
                {
                    countryService = new CountryService(repository, mapper);
                }
                return countryService;
            }
        }

        public ICustomerService Customer
        {
            get
            {
                if (customerService == null)
                {
                    customerService = new CustomerService(repository, mapper);
                }
                return customerService;
            }
        }
         
        
        public IStateService State
        {
            get
            {
                if (stateService == null)
                {
                    stateService = new StateService(repository, mapper);
                }
                return stateService;
            }
        } 

        public IUserService User
        {
            get
            {
                if (userService == null)
                {
                    userService = new UserService(repository, mapper);
                }
                return userService;
            }
        }
        public IFaqService Faq
        {
            get
            {
                if (faqService == null)
                {
                    faqService = new FaqService(repository, mapper);
                }
                return faqService;
            }
        }
         
        public IStoreProcedureService SP
        {
            get
            {
                if (spService == null)
                {
                    spService = new StoreProcedureService(repository, mapper);
                }
                return spService;
            }
        }

    }
    
}
