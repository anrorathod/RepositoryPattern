using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contracts
{
    public interface ICityService : IService<VMCity>
    { 
        Task<Response<List<VMCityForUser>>> GetCityList(int StateId);
        Task<Response<List<VMCityForUser>>> GetCitiesList(RequestData StateId);

        Task<Response<List<VMCityForUser>>> GetDestinationList();
        Task<Response<List<VMDestination>>> GetDestinationcList();
        Task<Response<VMDestination>> DestinationcDetail(string name);
        Task<Response<List<VMCity>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
    }
}
