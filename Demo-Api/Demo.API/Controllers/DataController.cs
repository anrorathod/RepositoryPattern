using Microsoft.AspNetCore.Mvc;
using Demo.Core.StoreProcedure;
using Demo.Service.Contracts;
using Demo.ViewModel.StoreProcedure;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : AppBaseController
    {
        public ILogger<DataController> Logger { get; }
        private IServiceWrapper service;

        public DataController(ILogger<DataController> logger, IServiceWrapper _service)
        {
            service = _service;
            Logger = logger;
        }


        [HttpGet("GetDestination3", Name = "GetDestinationList3")]
        [ProducesResponseType(typeof(List<VMSPData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDestination3()
        {
            Logger.LogInformation($"Executing GetDestinationList");
            var Data = await service.SP.GetDestinationList();
            return Ok(Data);
        }

        [HttpGet("GetDestination2", Name = "GetDestinationList2")]
        [ProducesResponseType(typeof(List<VMSPData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDestination2()
        {
            Logger.LogInformation($"Executing GetDestinationList");
            var Data = await service.SP.GetDestinationList2();
            return Ok(Data);
        }


        [HttpGet("GetDestination", Name = "GetDestination")]
        [ProducesResponseType(typeof(List<VMCityForUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityDestinationList()
        {
            Logger.LogInformation($"Executing GetCityList");
            var responseData = await service.City.GetDestinationList();
            return Ok(responseData);
        }
          
    }
}
