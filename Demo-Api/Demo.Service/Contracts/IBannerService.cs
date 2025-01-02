using Demo.Core.Tables;
using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;

namespace Demo.Service.Contracts
{
    public interface IBannerService : IService<VMBanner>
    {
        Task<Response<List<VMListBanner>>> GetBannerDataWithPaginationAsync(int pageNumber, int pageSize, string bannerType);
        Task<Response<VMBanner>> UploadBannerImagesAsync(VMBanner inputData, string path);
        Task<Response<List<VMBanner>>> GetOnAdminSearchWithPaginationAsync(RequestParam request);
    }
}
