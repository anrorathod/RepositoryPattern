using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : AppBaseController
    {
        public ILogger<ContactUsController> Logger { get; }
        private IServiceWrapper service;

        public ContactUsController(ILogger<ContactUsController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        #region For Front End
        [HttpPost("submit", Name = "submitContactUs")]
        [ProducesResponseType(typeof(List<VMContactUs>), StatusCodes.Status200OK)]
        public async Task<IActionResult> submitContactUsAsync(VMContactUs request)
        {
            Logger.LogInformation($"Executing submiteContactUs");
            var responseData = await service.ContactUs.CreateDataAsync(request, 0);
            return Ok(responseData);
        }
        //[HttpGet("GetContactUsList", Name = "GetContactUsList")]
        //[ProducesResponseType(typeof(List<VMContactUsForUser>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetContactUsListAsync()
        //{
        //    Logger.LogInformation($"Executing GetContactUsList");
        //    var responseData = await service.ContactUs.GetContactUsList();
        //    return Ok(responseData);
        //}

        #endregion

        #region For Admin
        [Authorize(Roles = Role.Admin)]
        [HttpGet("All", Name = "GetAllContactUs")]
        [ProducesResponseType(typeof(List<VMContactUs>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllContactUsAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllContactUs");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var responseData = await service.ContactUs.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetContactUsById", Name = "GetContactUsById")]
        [ProducesResponseType(typeof(VMContactUs), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContactUsById(int ContactUsId)
        {
            Logger.LogInformation($"Executing GetAllContactUs");

            var responseData = await service.ContactUs.GetDatabyIdAsync(ContactUsId);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateContactUs")]
        [ProducesResponseType(typeof(List<VMContactUs>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateContactUsAsync(VMContactUs request)
        {
            Logger.LogInformation($"Executing CreateContactUs");
            var responseData = await service.ContactUs.CreateDataAsync(request, CurrentUser.UserID);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateContactUs")]
        [ProducesResponseType(typeof(List<VMContactUs>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminContactUsAsync(VMContactUs request)
        {
            Logger.LogInformation($"Executing UpdateContactUsAsync");

            var category = await service.ContactUs.UpdateDataAsync(request, CurrentUser.UserID);
            return Ok(category);
        }

        #endregion
    }
}
