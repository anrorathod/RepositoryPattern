using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : AppBaseController
    {
        public ILogger<CountryController> Logger { get; }
        private IServiceWrapper service;

        public CountryController(ILogger<CountryController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }

        #region For Front End
        [HttpGet("GetCountryList", Name = "GetCountryList")]
        [ProducesResponseType(typeof(List<VMCountryForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryListAsync()
        {
            Logger.LogInformation($"Executing GetCountryList");
            var responseData = await service.Country.GetCountryList();
            return Ok(responseData);
        }

        #endregion

        #region For Admin
        [Authorize(Roles = Role.Admin)]
        [HttpGet("All", Name = "GetAllCountry")]
        [ProducesResponseType(typeof(List<VMCountry>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCountryAsync(int currentPage = 1, int pageSize = 12, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetAllCountry");
            RequestParam param = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };
            var responseData = await service.Country.AdminGetDataSearchWithPaginationAsync(param);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetCountryById", Name = "GetCountryById")]
        [ProducesResponseType(typeof(VMCountry), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryById(decimal countryId )
        {
            Logger.LogInformation($"Executing GetAllCountry");
             
            var responseData = await service.Country.GetDatabyIdAsync(countryId);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Create", Name = "CreateCountry")]
        [ProducesResponseType(typeof(List<VMCountry>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCountryAsync(VMCountry request)
        {
            Logger.LogInformation($"Executing CreateCountry");
            var responseData = await service.Country.CreateDataAsync(request, CurrentUser.UserID);
            return Ok(responseData);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Update", Name = "UpdateCountry")]
        [ProducesResponseType(typeof(List<VMCountry>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminCountryAsync(VMCountry request)
        {
            Logger.LogInformation($"Executing UpdateCountryAsync");
             
            var category = await service.Country.UpdateDataAsync(request, CurrentUser.UserID);
            return Ok(category);
        }

        #endregion
    }
}
