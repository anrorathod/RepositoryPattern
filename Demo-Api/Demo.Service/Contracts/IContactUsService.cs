using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contract
{
    public interface IContactUsService : IService<VMContactUs>
    {
        Task<Response<List<VMContactUs>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        //Task<Response<List<VMContactUsForUser>>> GetContactUsList();
    }
}

