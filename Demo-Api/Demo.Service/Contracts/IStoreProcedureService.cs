using System;
using Demo.Core.StoreProcedure;
using Demo.ViewModel.Response;
using Demo.ViewModel.StoreProcedure;

namespace Demo.Service.Contracts
{
    public interface IStoreProcedureService : IService<VMSPData>
    {
        Task<Response<List<VMSPDestination>>>  GetDestinationList();
        Task<Response<List<VMSPDestination>>> GetDestinationList2(); 
    }
}
