using Demo.ViewModel;
using Demo.ViewModel.Response;

namespace Demo.Service.Contracts
{
    public interface IService<T> where T : class
    {
        Task<Response<List<T>>> GetDataAsync();
        Task<Response<T>> GetDatabyIdAsync(object id);
        Task<Response<List<T>>> GetDataWithPaginationAsync(int pageNumber, int pageSize);
        Task<Response<bool>> CreateDataAsync(T inputData, int LoggedInUserId = 0);
        Task<Response<bool>> UpdateDataAsync(T updateData, int LoggedInUserId = 0);
        Task<Response<bool>> DeleteDataAsync(object id, int LoggedInUserId = 0);
        void DetachEntity(T entityToDetach);

    }
}
