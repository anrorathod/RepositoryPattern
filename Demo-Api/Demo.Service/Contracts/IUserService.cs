using Demo.ViewModel;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Contracts
{
    public interface IUserService : IService<VMUser>
    {
        Task<VMUser> GetUserTokenAsync(AuthenticateRequest userData); 
        Task<Response<VMMyProfile>> GetMyProfile(object id);
        Task<Response<VMRegister>> RegisterUserAsync(VMRegister inputData);
        Task<Response<VMMyProfile>> UpdateProfile(VMMyProfile inputData, object id);
        Task<Response<bool>> ChangePassword(VMChangePassword inputData, object id);
        Task<Response<VMResetPassword>> GetResetPasswordDetailsAsync(string request);
        Task<Response<VMUpdatePassword>> ChangePasswordAsync(string EmailID, string Password);

        Task<Response<VMResetPassword>> ForgotPasswordByUserName(object userName, string strEmailTemplate, string strURL);
        Task<Response<List<VMUserForList>>> GetOnAdminSearchWithPaginationAsync(RequestParam request);
    }
}
