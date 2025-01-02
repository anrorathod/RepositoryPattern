using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contracts
{
    public interface IStateService : IService<VMState>
    {
        Task<Response<List<VMStateForUser>>> GetStateList(int countryId);
        Task<Response<List<VMStateForUser>>> GetStatesList(RequestData countryId); 
        Task<Response<List<VMState>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        
    }
}
