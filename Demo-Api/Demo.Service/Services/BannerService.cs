using AutoMapper;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contracts;
using Demo.ViewModel; 
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;
using System;
using Demo.ViewModel.Request;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Demo.Service.Services
{
    public class BannerService : IBannerService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public BannerService(IRepositoryWrapper _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMBanner inputData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var model = mapper.Map<Banner>(inputData);
                repository.Banner.Add(model);
                await repository.Banner.CommitAsync();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public async Task<Response<bool>> DeleteDataAsync(object id, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                repository.Banner.Delete(id);
                await repository.Banner.CommitAsync();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public void DetachEntity(VMBanner entityToDetach)
        {
            var model = mapper.Map<Banner>(entityToDetach);
            repository.Banner.DetachEntities(model);
        }

        public async Task<Response<List<VMBanner>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMBanner>>();
            try
            {
                var model = await repository.Banner.GetAsync();
                var vmData = mapper.Map<List<VMBanner>>(model);
                response.Data = vmData;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
            
        }

        public async Task<Response<VMBanner>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMBanner>();
            try
            {
                var model = await repository.Banner.GetByIdAsync(id);
                var vmData = mapper.Map<VMBanner>(model);
                response.Data = vmData;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
            
        }

        public async Task<Response<bool>> IsDataExistAsync(string name)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var Banner = await repository.Banner.GetAsync(g => g.bannerName == name);
                response.Data = Banner.Any();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public async Task<Response<bool>> UpdateDataAsync(VMBanner updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var model = mapper.Map<Banner>(updateData);
                repository.Banner.Update(model);
                response.Data =  await repository.Banner.CommitAsync();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
            
        }

        public async Task<Response<List<VMBanner>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMBanner>>();
            try
            {
                var model = await repository.Banner.GetAsync(null, a => a.OrderBy(s => s.bannerId), pageNumber, pageSize);
                var vmData = mapper.Map<List<VMBanner>>(model);
                response.Data = vmData;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
            
        }
        public async Task<Response<List<VMListBanner>>> GetBannerDataWithPaginationAsync(int pageNumber, int pageSize, string bannerType)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMListBanner>>();

            try
            {
                var Datas = (from banners in repository.Banner.GetAll().Where(w => w.bannerType == bannerType).OrderBy(a => a.bannerId).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                             select new VMListBanner
                             {
                                 bannerDescription = banners.bannerDescription,
                                 imagePath = banners.imagePath,
                                 bannerName = banners.bannerName,
                                 link = banners.link,
                             }).ToList();
                response.Data = Datas;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<VMBanner>> UploadBannerImagesAsync(VMBanner inputData, string path)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMBanner>();
            try
            {
                if (inputData.Files != null)
                {
                    var model = mapper.Map<Banner>(inputData);
                    foreach (var item in inputData.Files)
                    {
                        var filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Second.ToString() + "-" + item.FileName.ToLower().Replace(" ", "-");
                        var fullpath = path + "/" + filename;
                        using (var stream = new FileStream(fullpath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        //var filepuploaded = await Task.Run(() => UploadFile(filename, path, item));
                        UploadFileToFtp(filename, path, item);

                        if (inputData.bannerId == 0)
                        {
                            var newProdImage = new Banner
                            {
                                bannerName = model.bannerName,
                                bannerDescription = model.bannerDescription,
                                bannerType = model.bannerType,
                                imagePath = filename,
                                Status = model.Status,
                                link = model.link,
                                CreatedBy = 1,
                                UpdatedBy = 1,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now
                            };

                            repository.Banner.Add(newProdImage);
                        }
                        else
                        {
                            var newProdImage = new Banner
                            {
                                bannerId = model.bannerId,
                                bannerName = model.bannerName,
                                bannerDescription = model.bannerDescription,
                                bannerType = model.bannerType,
                                imagePath = filename,
                                Status = model.Status,
                                link = model.link,
                            };
                            repository.Banner.Add(newProdImage);
                        }
                    }
                }
                var s = await repository.Banner.CommitAsync();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }

        private bool UploadFileToFtp(string fileName, string folderName, IFormFile file)
        {
            try
            {
                var ftpServerUrl = @"ftp://routesandtours.com/";
                var username = @"filepupload";
                var password = "o5gX@z055";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpServerUrl + folderName +"/" + fileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.KeepAlive = false;
                request.EnableSsl = false;
                byte[] fileContents;

                string path = "";
                path = Path.Combine(ftpServerUrl + folderName);
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                try
                {
                    WebRequest requestDir = WebRequest.Create(path);
                    requestDir.Credentials = new NetworkCredential(username, password);
                    requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                    WebResponse responseDir = requestDir.GetResponse();
                }
                catch
                {

                }



                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    fileContents = memoryStream.ToArray();
                }
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == FtpStatusCode.ClosingData)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<List<VMBanner>>> GetOnAdminSearchWithPaginationAsync(RequestParam request)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMBanner>>();

            try
            {
                var searchQuery = repository.Banner.GetAll();
                int totalRecords = searchQuery.Count();
                searchQuery = searchQuery.Where(s => s.Status == "Active" && (s.bannerName.Contains(request.search) || s.bannerType.Contains(request.search)));
                int totalFilteredRecords = searchQuery.Count();

                var Datas = (from banner in searchQuery.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize)
                             select new VMBanner
                             {
                                 bannerId = banner.bannerId,
                                 bannerType = banner.bannerType,
                                 bannerName = banner.bannerName,
                                 bannerDescription = banner.bannerDescription,
                                 imagePath = banner.imagePath,
                                 link = banner.link
                             }).ToList();

                response.Data = Datas;
                response.recordsTotal = totalRecords;
                response.recordsFiltered = totalFilteredRecords;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
    }
}
