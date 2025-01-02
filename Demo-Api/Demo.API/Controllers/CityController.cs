using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : AppBaseController
    {
        public ILogger<CityController> Logger { get; }
        private IServiceWrapper service;

        public CityController(ILogger<CityController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        #region For Front End
        [HttpGet("GetCityList", Name = "GetCityList")]
        [ProducesResponseType(typeof(List<VMCityForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityListAsync(int stateId)
        {
            Logger.LogInformation($"Executing GetCityList");
            var responseData = await service.City.GetCityList(stateId);
            return Ok(responseData);
        }

        [HttpPost("GetCitiesList", Name = "GetCitiesList")]
        [ProducesResponseType(typeof(List<VMCityForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesListAsync(RequestData stateId)
        {
            Logger.LogInformation($"Executing GetCitiesList");
            var responseData = await service.City.GetCitiesList(stateId);
            return Ok(responseData);
        }

        #endregion

        #region For Admin
        [Authorize(Roles = Role.Admin)]
        [HttpGet("All", Name = "GetAllCity")]
        [ProducesResponseType(typeof(List<VMCity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCityAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllCity");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var responseData = await service.City.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetCityById", Name = "GetCityById")]
        [ProducesResponseType(typeof(VMCity), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityById(decimal CityId)
        {
            Logger.LogInformation($"Executing GetAllCity");

            var responseData = await service.City.GetDatabyIdAsync(CityId);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateCity")]
        [ProducesResponseType(typeof(List<VMCity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCityAsync(VMCity request)
        {
            Logger.LogInformation($"Executing CreateCity");
            var responseData = await service.City.CreateDataAsync(request, CurrentUser.UserID);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateCity")]
        [ProducesResponseType(typeof(List<VMCity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminCityAsync(VMCity request)
        {
            Logger.LogInformation($"Executing UpdateCityAsync");

            var category = await service.City.UpdateDataAsync(request, CurrentUser.UserID);
            return Ok(category);
        }

        #endregion
    }
}
