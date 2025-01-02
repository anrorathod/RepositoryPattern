using AutoMapper;
using Demo.Core.Tables;
using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ViewModel.Enums;
using Demo.Core.StoreProcedure;
using Demo.ViewModel.StoreProcedure;
using Demo.Core.Tables;
using Demo.ViewModel;

namespace Demo.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {    
            CreateMap<Banner, VMBanner>().ReverseMap();
            CreateMap<Banner, VMListBanner>().ReverseMap(); 
            CreateMap<City, VMCity>().ReverseMap();
            CreateMap<City, VMCityForUser>().ReverseMap();
            CreateMap<CMS, VMCMS>().ReverseMap();
            CreateMap<CMS, VMCMSUser>().ReverseMap();
            CreateMap<ContactUs, VMContactUs>().ReverseMap();
            CreateMap<Country, VMCountry>().ReverseMap();
            CreateMap<Country, VMCountryForUser>().ReverseMap();
            CreateMap<Customer, VMCustomer>().ReverseMap();
            CreateMap<State, VMState>().ReverseMap();
            CreateMap<State, VMStateForUser>().ReverseMap();
            CreateMap<User, VMUser>().ReverseMap();
            CreateMap<User, VMRegister>().ReverseMap();
            CreateMap<User, VMMyProfile>().ReverseMap();
            CreateMap<User, VMUserForList>().ReverseMap();
            CreateMap<Faq, VMFaq>().ReverseMap();
            CreateMap<Faq, VMFaqForUser>().ReverseMap(); 
            CreateMap<SPData, VMSPDestination>().ReverseMap();
        }
    }
}
