using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Tables;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : AppBaseController
    {
        public ILogger<FileUploadController> Logger { get; }
        private IServiceWrapper service;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileUploadController(ILogger<FileUploadController> logger, IServiceWrapper _service, IWebHostEnvironment _webHostEnvironment)
        {
            service = _service;
            Logger = logger;
            webHostEnvironment = _webHostEnvironment;
        }

        //[Authorize(Roles = Role.Admin)]
        //[HttpPost("Create", Name = "UploadProjectFile")]
        //[ProducesResponseType(typeof(List<VMProjectFile>), StatusCodes.Status200OK)]
        //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        //public async Task<IActionResult> UploadProductImageAsync([FromForm] VMProjectFile inputData)
        //{
        //    //string webRootPath = _webHostEnvironment.WebRootPath;
        //    string contentRootPath = webHostEnvironment.ContentRootPath;
        //    var azureStoragePath = ""; // configuration.GetValue<string>("imageUpload:imagepath");
        //    var doUpload = false; // configuration.GetValue<bool>("imageUpload:upload");

        //    string path = "";
        //    path = Path.Combine(contentRootPath, inputData.FileFor);
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    var productImages = await service.ProjectFile.UploadProductImagesAsync(inputData, path, azureStoragePath, doUpload);
        //    return Ok(productImages);
        //}

        //[Authorize(Roles = Role.Admin)]
        //[HttpGet("List", Name = "ProjectFileList")]
        //[ProducesResponseType(typeof(List<VMProjectFileDisplay>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> ProductImagesList(string FileFor= "", decimal FileForId = 0)
        //{
        //    Logger.LogInformation($"Executing ProductImagesList");
        //    var productImages = await service.ProjectFile.GetFilesList(FileFor, FileForId);
        //    return Ok(productImages);
        //}

        //[Authorize(Roles = Role.Admin)]
        //[HttpPost("Delete", Name = "DeleteFile")]
        //[ProducesResponseType(typeof(VMProjectFileDisplay), StatusCodes.Status200OK)]
        //public async Task<IActionResult> DeleteFileAsync(decimal file=0)
        //{
        //    Logger.LogInformation($"Executing DeleteFile");
        //    var responseData = await service.ProjectFile.DeleteDataAsync(file, CurrentUser.UserID);
        //    return Ok(responseData);
        //}
    }
}
