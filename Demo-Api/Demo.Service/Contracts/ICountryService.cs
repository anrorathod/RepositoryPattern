using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contracts
{
    public interface ICountryService : IService<VMCountry>
    {
        Task<Response<List<VMCountry>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        Task<Response<List<VMCountryForUser>>> GetCountryList();
    }
}
