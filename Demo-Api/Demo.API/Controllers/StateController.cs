using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : AppBaseController
    {
        public ILogger<StateController> Logger { get; }
        private IServiceWrapper service;

        public StateController(ILogger<StateController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        #region For Front End
        [HttpGet("GetStateList", Name = "GetStateList")]
        [ProducesResponseType(typeof(List<VMStateForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStateListAsync(int countryId)
        {
            Logger.LogInformation($"Executing GetStateList");
            var responseData = await service.State.GetStateList(countryId);
            return Ok(responseData);
        }

        [HttpPost("GetStatesList", Name = "GetStatesList")]
        [ProducesResponseType(typeof(List<VMStateForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatesListAsync(RequestData countryId)
        {
            Logger.LogInformation($"Executing GetStatesList");
            var responseData = await service.State.GetStatesList(countryId);
            return Ok(responseData);
        }

        #endregion

        #region For Admin
        [HttpGet("All", Name = "GetAllState")]
        [ProducesResponseType(typeof(List<VMState>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStateAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllState");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var responseData = await service.State.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetStateById", Name = "GetStateById")]
        [ProducesResponseType(typeof(VMCountry), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStateById(decimal stateId)
        {
            Logger.LogInformation($"Executing Get State by id");

            var responseData = await service.State.GetDatabyIdAsync(stateId);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateState")]
        [ProducesResponseType(typeof(List<VMState>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateStateAsync(VMState request)
        {
            Logger.LogInformation($"Executing CreateState");
            
            var responseData = await service.State.CreateDataAsync(request, CurrentUser.UserID);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateState")]
        [ProducesResponseType(typeof(List<VMState>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminStateAsync(VMState request)
        {
            Logger.LogInformation($"Executing UpdateStateAsync");

            var category = await service.State.UpdateDataAsync(request, CurrentUser.UserID);
            return Ok(category);
        }

        #endregion
    }
}
