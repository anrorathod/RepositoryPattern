using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;
using Role = Demo.ViewModel.Request.Role;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppBaseController
    {
        public ILogger<UserController> Logger { get; }
        private IServiceWrapper service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public UserController(ILogger<UserController> logger, IServiceWrapper _service, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            service = _service;
            Logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _config = configuration;
        }

        #region For Front
        [HttpGet("All", Name = "GetAllUser")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUserAsync(int currentPage)
        {
            Logger.LogInformation($"Executing GetAllUserAsync");
            var Data = await service.User.GetDataWithPaginationAsync(currentPage, 10);
            return Ok(Data);
        }

        [HttpGet("ByUserId", Name = "GetUserById")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            Logger.LogInformation($"Executing GetAllUserAsync");
            var Data = await service.User.GetDatabyIdAsync(userId);
            return Ok(Data);
        }

        [Authorize]
        [HttpGet("MyProfile", Name = "MyProfile")]
        [ProducesResponseType(typeof(VMMyProfile), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyProfile()
        {
            Logger.LogInformation($"Executing GetAllUserAsync");
            var userId = Convert.ToInt32(CurrentUser.UserID);
            var responseData = await service.User.GetMyProfile(userId);
            return Ok(responseData);
        }

        [Authorize]
        [HttpPost("UpdateProfile", Name = "UpdateProfile")]
        [ProducesResponseType(typeof(List<VMMyProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatUpdateProfileeUserAsync(VMMyProfile inputData)
        {
            Logger.LogInformation($"Executing UpdateUserAsync");
            var userId = Convert.ToDecimal(CurrentUser.UserID);
            var responseData = await service.User.UpdateProfile(inputData, userId);
            return Ok(responseData);
        }

        [Authorize]
        [HttpPost("ChangePassword", Name = "ChangePassword")]
        [ProducesResponseType(typeof(List<VMMyProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(VMChangePassword inputData)
        {
            Logger.LogInformation($"Executing ChangePassword");
            var userId = Convert.ToDecimal(CurrentUser.UserID);
            var responseData = await service.User.ChangePassword(inputData, userId);
            return Ok(responseData);
        }

        [HttpPost("Create", Name = "CreateUser")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUserAsync(VMUser model)
        {
            Logger.LogInformation($"Executing CreateUserAsync");
            try
            {
                var Data = await service.User.CreateDataAsync(model);
                return Ok(Data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Register", Name = "RegisterUser")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterUserAsync(VMRegister vMRegister)
        {
            Logger.LogInformation($"Executing CreateUserAsync");
            try
            {
                var Data = await service.User.RegisterUserAsync(vMRegister);
                return Ok(Data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("Update", Name = "UpdateUser")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync(VMUser model)
        {
            Logger.LogInformation($"Executing UpdateUserAsync");
            var Data = await service.User.UpdateDataAsync(model);
            return Ok(Data);
        }

        [HttpGet("ForgotPassword", Name = "ForgotPasswordByUserName")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPasswordAsync(string userName)
        {
            Logger.LogInformation($"Executing ForgotPassword");
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string strTemplatePath = contentRootPath + "/EmailTemplate/ForgotPassword.html";
            string strEmailTemplate = System.IO.File.ReadAllText(strTemplatePath);
            //VMAppSetting vMAppSetting = new VMAppSetting();
            string strURL = _config["SiteSettings:SiteUrl"];
            var product = await service.User.ForgotPasswordByUserName(userName, strEmailTemplate, strURL);
            return Ok(product);
        }

        [HttpGet("GetResetPasswordDetails", Name = "GetResetPasswordDetailsAsync")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResetPasswordDetailsAsync(string request)
        {
            Logger.LogInformation($"Executing ForgotPassword");
            var product = await service.User.GetResetPasswordDetailsAsync(request);
            return Ok(product);
        }
        [HttpPost("UpdatePassword", Name = "ChangeUserPasswordAsync")]
        [ProducesResponseType(typeof(List<VMMyProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeUserPasswordAsync(string EmailId, string Password)
        {
            Logger.LogInformation($"Executing UpdateUserAsync");
            var responseData = await service.User.ChangePasswordAsync(EmailId, Password);
            return Ok(responseData);
        }
        #endregion

        #region For Admin
        //[Authorize(Roles = "user,admin")]
        //[Authorize(Roles = Role.Admin + "," + Role.User)]
        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAdminCustomerListAsync", Name = "GetAdminCustomerListAsync")]
        [ProducesResponseType(typeof(List<VMUserForList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminCustomerListAsync(int currentPage = 1, int pageSize = 10, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAdminCustomerListAsync");
            RequestParam request = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var productreview = await service.User.GetOnAdminSearchWithPaginationAsync(request);
            return Ok(productreview);
        }

        //[Authorize]
        //[HttpGet("GetAdminCustomersListAsync", Name = "GetAdminCustomersListAsync")]
        //[ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAdminCustomersListAsync(RequestParam request)
        //{
        //    Logger.LogInformation($"Executing GetAdminCustomersListAsync");
        //    var productreview = await service.User.GetOnAdminSearchWithPaginationAsync(request);
        //    return Ok(productreview);
        //}
        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetCustomerById", Name = "GetCustomerByIdAsync")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerByIdAsync(int userId)
        {
            Logger.LogInformation($"Executing GetAllUserAsync");
            var Data = await service.User.GetDatabyIdAsync(userId);
            return Ok(Data);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CustomerCreate", Name = "CustomerCreateAsync")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CustomerCreateAsync(VMUser model)
        {
            Logger.LogInformation($"Executing CustomerCreateAsync");
            var Data = await service.User.CreateDataAsync(model);
            return Ok(Data);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CustomerUpdate", Name = "CustomerUpdateAsync")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CustomerUpdateAsync(VMUser model)
        {
            Logger.LogInformation($"Executing CustomerUpdateAsync");
            var Data = await service.User.UpdateDataAsync(model);
            return Ok(Data);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete", Name = "DeleteCustomerAsync")]
        [ProducesResponseType(typeof(List<VMUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomerAsync(string CustomerID)
        {
            Logger.LogInformation($"Executing DeleteCustomerAsync");
            var Data = await service.User.DeleteDataAsync(CustomerID);
            return Ok(Data);
        }
        #endregion
    }
}
