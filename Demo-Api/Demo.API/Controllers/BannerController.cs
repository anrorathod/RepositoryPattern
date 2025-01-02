using Demo.Core.Tables;
using Demo.Service.Contracts;
using Demo.ViewModel;
using Demo.ViewModel.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Demo.ViewModel.Request;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : Controller
    {
        public ILogger<BannerController> Logger { get; }
        private IServiceWrapper service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BannerController(ILogger<BannerController> logger, IServiceWrapper _service, IWebHostEnvironment webHostEnvironment)
        {
            service = _service;
            Logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("BannerSlider", Name = "GetAllBanner")]
        [ProducesResponseType(typeof(List<VMListBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBannerAsync(string bannerType = "")
        {
            Logger.LogInformation($"Executing GetAllBannerAsync");
            var category = await service.Banner.GetBannerDataWithPaginationAsync(1, 100, bannerType);
            return Ok(category);
        }

        [HttpPost("Create", Name = "CreateBanner")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBannerAsync(VMBanner vMBanner)
        {
            var category = await service.Banner.CreateDataAsync(vMBanner);
            return Ok(category);
        }
        [HttpGet("ByBannerId", Name = "GetBannerById")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBannerByIdAsync(Int64 BannerId)
        {
            var category = await service.Banner.GetDatabyIdAsync(BannerId);
            return Ok(category);
        }

        [HttpPut("Update", Name = "UpdateBanner")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBannerAsync(VMBanner vMBanner)
        {
            Logger.LogInformation($"Executing UpdateBannerAsync");
            var category = await service.Banner.UpdateDataAsync(vMBanner);
            return Ok(category);
        }

        [HttpPost("Delete", Name = "DeleteBanner")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBannerAsync(Int64 recordId)
        {
            Logger.LogInformation($"Executing DeleteBannerAsync");
            var category = await service.Banner.DeleteDataAsync(recordId);
            return Ok(category);
        }
        [Authorize]
        [HttpGet("BannerList", Name = "GetBannerList")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BannerListAsync(int currentPage = 1, int pageSize = 10, string? search = "", string? orderColumn = "", string? orderBy = "")
        {
            Logger.LogInformation($"Executing GetProductListAsync");

            RequestParam request = new RequestParam
            {
                currentPage = currentPage,
                pageSize = pageSize,
                search = search,
                orderColumn = orderColumn,
                orderBy = orderBy
            };

            var banner = await service.Banner.GetOnAdminSearchWithPaginationAsync(request);
            return Ok(banner);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateBanner", Name = "UploadBannerImage")]
        [ProducesResponseType(typeof(List<VMBanner>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadBannerImageAsync([FromForm] VMBanner inputData)
        {
            //string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = "Banner";
            //path = Path.Combine(contentRootPath, "bannerimage");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var productImages = await service.Banner.UploadBannerImagesAsync(inputData, path);
            return Ok(productImages);
        }
    }
}