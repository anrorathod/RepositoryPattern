using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSController : AppBaseController
    {
        public ILogger<CMSController> Logger { get; }
        private IServiceWrapper service;

        public CMSController(ILogger<CMSController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        [HttpGet("TestSP", Name = "TestSP")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> TestSP()
        {
            Logger.LogInformation($"Executing GetCMSByNameAsync");
            var CMS = await service.SP.GetDestinationList();
            return Ok(CMS);
        }


        #region FrontEnd
        [HttpGet("ByCMSName", Name = "GetCMSByName")]
        [ProducesResponseType(typeof(List<VMCMSUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCMSByNameAsync(string title)
        {
            Logger.LogInformation($"Executing GetCMSByNameAsync");
            var CMS = await service.CMS.GetDatabyNameAsync(title);
            return Ok(CMS);
        }
        #endregion

        #region Admin
        [Authorize(Roles = Role.Admin)]
        [HttpGet("All", Name = "GetAllCMS")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCMSAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllCMSAsync");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var CMS = await service.CMS.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(CMS);
        }

        [HttpGet("CMSType", Name = "GetCMSType")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCMSTypeAsync()
        {
            Logger.LogInformation($"Executing GetCMSTypeAsync");
            var CMS = await service.CMS.GetCMSTypeAsync();
            return Ok(CMS);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("ByCMSId", Name = "GetCMSById")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCMSByIdAsync(int id)
        {
            Logger.LogInformation($"Executing GetAllCMSAsync");
            var CMS = await service.CMS.GetDatabyIdAsync(id);
            return Ok(CMS);
        }
          
        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateCMS")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCMSAsync(VMCMS vMCMS)
        {
            Logger.LogInformation($"Executing CreateCMSAsync");
            var CMS = await service.CMS.CreateDataAsync(vMCMS, CurrentUser.UserID);
            return Ok(CMS);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateCMS")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCMSAsync(VMCMS vMCMS)
        {
            Logger.LogInformation($"Executing UpdateCMSAsync");
            var CMS = await service.CMS.UpdateDataAsync(vMCMS, CurrentUser.UserID);
            return Ok(CMS);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete", Name = "DeleteCMS")]
        [ProducesResponseType(typeof(List<VMCMS>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCMSAsync(short recordId)
        {
            Logger.LogInformation($"Executing DeleteCMSAsync");
            var CMS = await service.CMS.DeleteDataAsync(recordId, CurrentUser.UserID);
            return Ok(CMS);
        }
        #endregion
    }
}
