using AutoMapper;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contracts;
using Demo.ViewModel;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using Demo.ViewModel.StoreProcedure;

namespace Demo.Service.Services
{
    public class StateService : IStateService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public StateService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMState inputData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();

            try
            {
                inputData.CreatedBy = LoggedInUserId;
                inputData.Status = Status.Active.ToString();
                inputData.UpdatedBy = LoggedInUserId;
                inputData.CreatedDate = DateTime.Now;
                inputData.UpdatedDate = DateTime.Now;

                var duplicateDataQuery = await repository.State.GetAsync(g => (g.StateName == inputData.StateName || g.StateCode == inputData.StateCode) && g.CountryId == inputData.CountryId);
                var duplicateData = duplicateDataQuery.Any();
                                
                if (!duplicateData)
                {
                    var model = mapper.Map<State>(inputData);
                    repository.State.Add(model);
                    response.Data = await repository.State.CommitAsync();
                }
                else
                {
                    response.Message = "State name or state code already exists.";
                    response.Success = false;
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

        public async Task<Response<bool>> DeleteDataAsync(object id, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.State.GetAsync(a => a.StateId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.State.Update(dataExists);
                    response.Data = await repository.State.CommitAsync();
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

        public void DetachEntity(VMState entityToDetach)
        {
            var model = mapper.Map<State>(entityToDetach);
            repository.State.DetachEntities(model);
        }

        public async Task<Response<List<VMState>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMState>>();
            try
            {
                var model = await repository.State.GetAsync();
                var vmData = mapper.Map<List<VMState>>(model);
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

        public async Task<Response<VMState>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMState>();
            try
            {
                var model = await repository.State.GetByIdAsync((decimal)id);
                var vmData = mapper.Map<VMState>(model);
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

        public async Task<Response<bool>> IsDataExistAsync(string name, string statecode, decimal countryid)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var State = await repository.State.GetAsync(g => (g.StateName == name || g.StateCode ==statecode) && g.CountryId == countryid);
                response.Data = State.Any();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public async Task<Response<bool>> UpdateDataAsync(VMState updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.State.GetAsync(a => a.StateId != Convert.ToInt32(updateData.StateId) && a.CountryId == updateData.CountryId && (a.StateName == updateData.StateName || a.StateCode == updateData.StateCode));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "State name or State code already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.State.GetAsync(a => a.StateId == Convert.ToInt32(updateData.StateId));
                    var newData = newDatacheck.FirstOrDefault();
                    if (newData != null)
                    {
                        newData.StateName = updateData.StateName;
                        newData.StateCode = updateData.StateCode;
                        newData.Description = updateData.Description;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.State.Update(newData);
                        response.Data = await repository.State.CommitAsync();
                    }
                    else
                    {
                        response.Message = "State id not found.";
                        response.Success = false;
                    }
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

        public async Task<Response<List<VMState>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMState>>();
            try
            {
                var model = await repository.State.GetAsync(null, a => a.OrderBy(s => s.StateName), pageNumber, pageSize);
                var vmData = mapper.Map<List<VMState>>(model);
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

        public async Task<Response<List<VMStateForUser>>> GetStateList(int countryId)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMStateForUser>>();
            try
            {
                var baseData = repository.State.GetAll().Where(a => a.CountryId == countryId && a.Status == Status.Active.ToString()).OrderBy(a => a.StateName).ToList();
                var Datas = mapper.Map<List<VMStateForUser>>(baseData);
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

        public async Task<Response<List<VMStateForUser>>> GetStatesList(RequestData countryId)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMStateForUser>>();
            try
            {
                var baseData = repository.State.GetAll().Where(a => countryId.countryId.Contains(a.CountryId) && a.Status == Status.Active.ToString()).OrderBy(a => a.StateName).ToList();
                var Datas = mapper.Map<List<VMStateForUser>>(baseData);
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

        public async Task<Response<List<VMDestination>>> GetDestinationsList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMDestination>>();
            try
            {
                //var pakcagesData = repository.Package.GetAll();
                //var cityData = repository.City.GetAll();
                //var stateData = repository.State.GetAll();
                //var fileData = repository.ProjectFile.GetAll().Where(a => a.FileFor == "state");
                //var Datas = (from p in pakcagesData
                //             join ct in cityData on p.CityTo equals ct.CityId
                //             join st in stateData on ct.StateId equals st.StateId
                //             join f in fileData on st.StateId equals f.FileForId  into Inners
                //             from stf in Inners.DefaultIfEmpty()
                //             where p.Status == "Active"
                //             select new VMDestination
                //             {
                //                 Name = st.StateName,
                //                 Description = st.Description,
                //                 FileName = stf.FileName
                //             }).OrderBy(a => a.Name).ToList().DistinctBy(x => x.Name).ToList();
                //response.Data = Datas;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }

        public async Task<Response<VMDestination>> DestinationsDetail(string name)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMDestination>();
            try
            {
                //var pakcagesData = repository.Package.GetAll();
                //var cityData = repository.City.GetAll();
                //var stateData = repository.State.GetAll().Where(a=>a.StateName== name);
                //var fileData = repository.ProjectFile.GetAll().Where(a=> a.FileFor == "state"); 
                //var filepData = repository.ProjectFile.GetAll().Where(a => a.FileFor == "package").Take(1);

                //var pData = (from p in pakcagesData
                //             join tct in cityData on p.CityTo equals tct.CityId
                //             join ts in stateData on tct.StateId equals ts.StateId 
                //             join f in filepData on p.PackageId equals f.FileForId into Inners
                //             from stf in Inners.DefaultIfEmpty()
                //             select new VMSPPackages
                //             {
                //                 PackageId = p.PackageId,
                //                 PackageType = p.PackageType, 
                //                 PackageName = p.PackageName,
                //                 Cityname = tct.CityName,
                //                 StateName = ts.StateName,
                //                 Imagename = stf.FileName,
                //                 Days = p.Days,
                //                 Nights = p.Nights,
                //                 PriceForDouble = p.PriceForDouble
                //             }).Take(5).ToList();

                //var Datas = (from p in pakcagesData
                //             join ct in cityData on p.CityTo equals ct.CityId
                //             join st in stateData on ct.StateId equals st.StateId
                //             join f in fileData on st.StateId equals f.FileForId into Inners
                //             from stf in Inners.DefaultIfEmpty()
                //             where p.Status == "Active"
                //             select new VMDestination
                //             {
                //                 Name = st.StateName,
                //                 Description = st.Description,
                //                 FileName = stf.FileName,
                //                 Packages = pData
                //             }).FirstOrDefault();
                //response.Data = Datas;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<List<VMState>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMState>>();
            try
            {
                var orderby = param.orderBy == "desc" ? true : false;
                var ordercolumn = param.orderColumn == "" ? "StateName" : param.orderColumn.ToUpperFirstLetter();

                var baseData = repository.State.GetAll();
                var queryData = baseData.Where(s => (s.StateName.Contains(param.search) || s.StateName.Contains(param.search)));
                var queryContryData = repository.Country.GetAll();

                var Datas = (from state in queryData
                             join country in queryContryData on state.CountryId equals country.CountryId
                             select new VMState
                             {
                                 StateId = state.StateId,
                                 CountryId = state.CountryId,
                                 CountryName = country.CountryName,
                                 StateCode = state.StateCode,
                                 StateName = state.StateName,
                                 Description = state.Description,
                                 Status = state.Status,
                                 CreatedBy = state.CreatedBy,
                                 CreatedDate = state.CreatedDate,
                                 UpdatedBy = state.UpdatedBy,
                                 UpdatedDate = state.UpdatedDate
                             }).OrderBy(ordercolumn, orderby).Skip((param.currentPage - 1) * param.pageSize).Take(param.pageSize).ToList();

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

