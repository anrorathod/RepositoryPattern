using Demo.Data.Contracts;
using Demo.Data.Repositories;
using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    { 
        private IBannerRepository _banner; 
        private ICityRepository _city;
        private ICMSRepository _cMS;
        private IContactUsRepository _contactUs;
        private ICountryRepository _country;
        private ICustomerRepository _customer; 
        private IStateRepository _state; 
        private IUserRepository _user;
        private IFaqRepository _faq; 
        private IStoreProcedureRepository _sp;
        private DemoDbContext _repoContext;
        public RepositoryWrapper(DemoDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
         
        public IBannerRepository Banner
        {
            get
            {
                if (_banner == null)
                {
                    _banner = new BannertRepository(_repoContext);
                }
                return _banner;
            }
        }
         

        public ICityRepository City
        {
            get
            {
                if (_city == null)
                {
                    _city = new CityRepository(_repoContext);
                }
                return _city;
            }
        }

        public ICMSRepository CMS
        {
            get
            {
                if (_cMS == null)
                {
                    _cMS = new CMSRepository(_repoContext);
                }
                return _cMS;
            }
        }

     
        public IContactUsRepository ContactUs
        {
            get
            {
                if (_contactUs == null)
                {
                    _contactUs = new ContactUsRepository(_repoContext);
                }
                return _contactUs;
            }
        }

        public ICountryRepository Country
        {
            get
            {
                if (_country == null)
                {
                    _country = new CountryRepository(_repoContext);
                }
                return _country;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_repoContext);
                }
                return _customer;
            }
        } 

        public IStateRepository State
        {
            get
            {
                if (_state == null)
                {
                    _state = new StateRepository(_repoContext);
                }
                return _state;
            }
        } 
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IFaqRepository Faq
        {
            get
            {
                if (_faq == null)
                {
                    _faq = new FaqRepository(_repoContext);
                }
                return _faq;
            }
        }
         
        public IStoreProcedureRepository SP
        {
            get
            {
                if (_sp == null)
                {
                    _sp = new StoreProcedureRepository(_repoContext);
                }
                return _sp;
            }
        }
    }
}
