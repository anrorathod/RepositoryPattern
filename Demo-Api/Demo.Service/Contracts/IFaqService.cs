using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contract
{
    public interface IFaqService : IService<VMFaq>
    {
        Task<Response<List<VMFaq>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        Task<Response<List<VMFaqForUser>>> GetFaqList();
    }
}

