using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contracts
{
    public interface ICMSService : IService<VMCMS>
    {
        Task<Response<List<VMCMS>>> AdminGetDataSearchWithPaginationAsync(RequestParam param);
        Task<Response<VMCMSUser>> GetDatabyNameAsync(string name);
        Task<Response<List<VMCMSType>>> GetCMSTypeAsync();
    }
}
