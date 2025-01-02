using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : AppBaseController
    {
        public ILogger<FaqController> Logger { get; }
        private IServiceWrapper service;

        public FaqController(ILogger<FaqController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        #region For Front End
        [HttpGet("GetFaqList", Name = "GetFaqList")]
        [ProducesResponseType(typeof(List<VMFaqForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFaqListAsync()
        {
            Logger.LogInformation($"Executing GetFaqList");
            var responseData = await service.Faq.GetFaqList();
            return Ok(responseData);
        }

        #endregion

        #region For Admin
        [Authorize(Roles = Role.Admin)]
        [HttpGet("All", Name = "GetAllFaq")]
        [ProducesResponseType(typeof(List<VMFaq>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFaqAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllFaq");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var responseData = await service.Faq.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetFaqById", Name = "GetFaqById")]
        [ProducesResponseType(typeof(VMFaq), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFaqById(decimal FaqId )
        {
            Logger.LogInformation($"Executing GetAllFaq");
             
            var responseData = await service.Faq.GetDatabyIdAsync(FaqId);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateFaq")]
        [ProducesResponseType(typeof(List<VMFaq>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFaqAsync(VMFaq request)
        {
            Logger.LogInformation($"Executing CreateFaq");
            var responseData = await service.Faq.CreateDataAsync(request, CurrentUser.UserID);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateFaq")]
        [ProducesResponseType(typeof(List<VMFaq>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminFaqAsync(VMFaq request)
        {
            Logger.LogInformation($"Executing UpdateFaqAsync");
             
            var category = await service.Faq.UpdateDataAsync(request, CurrentUser.UserID);
            return Ok(category);
        }

        #endregion
    }
}
