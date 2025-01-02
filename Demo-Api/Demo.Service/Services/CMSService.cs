using AutoMapper;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contracts;
using Demo.ViewModel;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Services
{
    public class CMSService : ICMSService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CMSService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMCMS inputData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var duplicateDataQuery = await repository.CMS.GetAsync(g => g.CmsType == inputData.CmsType );
                var duplicateData = duplicateDataQuery.Any();

                if (!duplicateData)
                {
                    inputData.CreatedBy = LoggedInUserId;
                    inputData.Status = Status.Active.ToString();
                    inputData.UpdatedBy = LoggedInUserId;
                    inputData.CreatedDate = DateTime.Now;
                    inputData.UpdatedDate = DateTime.Now;
                    inputData.MenuName = inputData.MenuName == null ? "" : inputData.MenuName;

                    var model = mapper.Map<CMS>(inputData);
                    repository.CMS.Add(model);
                    response.Data = await repository.CMS.CommitAsync();
                }
                else
                {
                    var existingData = duplicateDataQuery.FirstOrDefault();

                    existingData.Contents = inputData.Contents;
                    existingData.Status = Status.Active.ToString();
                    existingData.UpdatedDate = DateTime.Now;
                    existingData.UpdatedBy = LoggedInUserId;
                    repository.CMS.Update(existingData);
                    response.Data = await repository.CMS.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                if(ex.InnerException != null)
                    exceptions.Add("Exception", ex.InnerException.Message);
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
                var data = await repository.CMS.GetAsync(a => a.cmsId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.CMS.Update(dataExists);
                    response.Data = await repository.CMS.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;

        }

        public void DetachEntity(VMCMS entityToDetach)
        {
            var model = mapper.Map<CMS>(entityToDetach);
            repository.CMS.DetachEntities(model);
        }

        public async Task<Response<List<VMCMS>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCMS>>();
            try
            {
                var model = await repository.CMS.GetAsync();
                var vmData = mapper.Map<List<VMCMS>>(model);
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

        public async Task<Response<VMCMS>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMCMS>();
            try
            {
                var model = await repository.CMS.GetByIdAsync((int)id);
                var vmData = mapper.Map<VMCMS>(model);
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
                var CMS = await repository.CMS.GetAsync(g => g.MenuName == name);
                response.Data = CMS.Any();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public async Task<Response<bool>> UpdateDataAsync(VMCMS updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                //var data = await repository.CMS.GetAsync(a => a.cmsId !=updateData.cmsId && a.CmsType == updateData.CmsType);
                //var exstingData = data.FirstOrDefault();
                //if (exstingData != null)
                //{
                //    response.Message = "Contents type already exists.";
                //    response.Success = false;
                //}
                //else
                //{
                    var newDatacheck = await repository.CMS.GetAsync(a => a.CmsType == updateData.CmsType);
                    var newData = newDatacheck.FirstOrDefault();
                    if (newData != null)
                    {
                        newData.Contents = updateData.Contents;
                        newData.Title = updateData.Title;
                        newData.Status = Status.Active.ToString(); // updateData.Status;
                        newData.MenuName = updateData.MenuName == null ? "" : updateData.MenuName;

                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.CMS.Update(newData);
                        response.Data = await repository.CMS.CommitAsync();
                    }
                    else
                    {
                        response.Message = "CMS Type not found.";
                        response.Success = false;
                    }
               // }
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }

        public async Task<Response<List<VMCMS>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCMS>>();
            try
            {
                var model = repository.CMS.GetAll();
                var vmData = mapper.Map<List<VMCMS>>(model);
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

        public async Task<Response<VMCMSUser>> GetDatabyNameAsync(string name)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMCMSUser>();
            try
            {
                var model = await repository.CMS.GetAsync(a => a.CmsType == name.Replace("-", " "));
                var vmData = mapper.Map<VMCMSUser>(model.FirstOrDefault());
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
        public async Task<Response<List<VMCMSType>>> GetCMSTypeAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCMSType>>();

            try
            {
                var Datas = (from CMS in repository.CMS.GetAll().OrderBy(a => a.CmsType)
                             select new VMCMSType
                             {
                                 CMSId = CMS.cmsId,
                                 CMSType = CMS.CmsType
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
        public async Task<Response<List<VMCMS>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCMS>>();
            try
            {
                var baseData = repository.CMS.GetAll();
                var queryData = baseData.Where(s => (s.Contents.Contains(param.search) || s.Title.Contains(param.search)));

                var Datas = (from cms in queryData.Skip((param.currentPage - 1) * param.pageSize).Take(param.pageSize)
                             select new VMCMS
                             {
                                 cmsId = cms.cmsId,
                                 CmsType = cms.CmsType,
                                 Title = cms.Title,
                                 Contents = cms.Contents,                                 
                                 Status = cms.Status,
                                 CreatedBy = cms.CreatedBy,
                                 CreatedDate = cms.CreatedDate,
                                 UpdatedBy = cms.UpdatedBy,
                                 UpdatedDate = cms.UpdatedDate
                             }).ToList();

                response.Data = Datas;
                response.recordsFiltered = queryData.Count();
                response.recordsTotal = baseData.Count();
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

