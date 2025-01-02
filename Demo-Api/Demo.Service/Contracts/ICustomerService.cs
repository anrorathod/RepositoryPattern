using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contract
{
    public interface ICustomerService : IService<VMCustomer>
    {
        Task<Response<List<VMCustomer>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        //Task<Response<List<VMCustomerForUser>>> GetCustomerList();
    }
}

