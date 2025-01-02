using AutoMapper;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contracts;
using Demo.ViewModel;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.StoreProcedure;
using Demo.ViewModel.Tables;

namespace Demo.Service.Services
{
    public class CityService : ICityService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CityService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMCity inputData, int LoggedInUserId = 0)
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

                var duplicateDataQuery = await repository.City.GetAsync(g => (g.CityName == inputData.CityName || g.CityCode == inputData.CityCode) && g.StateId == inputData.StateId);
                var duplicateData = duplicateDataQuery.Any();

                if (!duplicateData)
                {
                    var model = mapper.Map<City>(inputData);
                    repository.City.Add(model);
                    response.Data = await repository.City.CommitAsync();
                }
                else
                {
                    response.Message = "City name or City code already exists.";
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
                var data = await repository.City.GetAsync(a => a.CityId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.City.Update(dataExists);
                    response.Data = await repository.City.CommitAsync();
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

        public void DetachEntity(VMCity entityToDetach)
        {
            var model = mapper.Map<City>(entityToDetach);
            repository.City.DetachEntities(model);
        }

        public async Task<Response<List<VMCity>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCity>>();
            try
            {
                var model = await repository.City.GetAsync();
                var vmData = mapper.Map<List<VMCity>>(model);
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

        public async Task<Response<VMCity>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMCity>();
            try
            {
                var model = await repository.City.GetByIdAsync((decimal)id);
                var vmData = mapper.Map<VMCity>(model);
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

        public async Task<Response<bool>> UpdateDataAsync(VMCity updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.City.GetAsync(a => a.CityId != Convert.ToInt32(updateData.CityId) && (a.CityName == updateData.CityName && a.StateId != updateData.StateId));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "City name or City code already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.City.GetAsync(a => a.CityId == Convert.ToInt32(updateData.CityId));
                    var newData = newDatacheck.FirstOrDefault();
                    if (newData != null)
                    {
                        newData.CityName = updateData.CityName;
                        newData.CityCode = updateData.CityCode;
                        newData.Description = updateData.Description;
                        newData.StateId = updateData.StateId;                        
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.City.Update(newData);
                        response.Data = await repository.City.CommitAsync();
                    }
                    else
                    {
                        response.Message = "City id not found.";
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

        public async Task<Response<List<VMCity>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCity>>();
            try
            {
                var model = await repository.City.GetAsync(null, a => a.OrderBy(s => s.CityName), pageNumber, pageSize);
                var vmData = mapper.Map<List<VMCity>>(model);
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

        public async Task<Response<List<VMCityForUser>>> GetCityList(int StateId)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCityForUser>>();
            try
            {
                var baseData = repository.City.GetAll().Where(a => a.StateId == StateId && a.Status == Status.Active.ToString()).OrderBy(a => a.CityName).ToList();
                var Datas = mapper.Map<List<VMCityForUser>>(baseData); 
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

        public async Task<Response<List<VMCityForUser>>> GetCitiesList(RequestData StateId)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCityForUser>>();
            try
            {
                var baseData = repository.City.GetAll().Where(a => StateId.stateId.Contains(a.StateId) && a.Status == Status.Active.ToString()).OrderBy(a => a.CityName).ToList();
                var Datas = mapper.Map<List<VMCityForUser>>(baseData);
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

        public async Task<Response<List<VMCityForUser>>> GetDestinationList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCityForUser>>();
            try
            {
                //var pakcagesData = repository.Package.GetAll().Where(a=> a.Status == "Active");
                //var packagecityData = repository.PackageCity.GetAll();
                //var cityData = repository.City.GetAll();
                //var stateData = repository.State.GetAll();
                //var countryData = repository.Country.GetAll();

                ////country
                //var countryDatas = (from p in pakcagesData
                //                    join pct in packagecityData on p.PackageId equals pct.PackageId
                //                    join ct in cityData on pct.CityId equals ct.CityId
                //                    join st in stateData on ct.StateId equals st.StateId
                //                    join c in countryData on st.CountryId equals c.CountryId
                //                    where pct.FromTo == "to"
                //                    select new VMCityForUser
                //                    {
                //                        CityId = c.CountryId,
                //                        CityName = c.CountryName,
                //                        StateName = c.CountryName
                //                    }).OrderBy(a => a.CityName).ToList().DistinctBy(x => x.CityName).ToList();
                ////state
                //var stateDatas = (from p in pakcagesData
                //             join pct in packagecityData on p.PackageId equals pct.PackageId
                //             join ct in cityData on pct.CityId equals ct.CityId
                //             join st in stateData on ct.StateId equals st.StateId
                //             where pct.FromTo == "to"
                //             select new VMCityForUser
                //             {
                //                 CityId = st.StateId,
                //                 CityName = st.StateName,
                //                 StateName = st.StateName
                //             }).OrderBy(a => a.CityName).ToList().DistinctBy(x => x.CityName).ToList();
                ////city
                //var cityDatas = (from p in pakcagesData
                //             join pct in packagecityData on p.PackageId equals pct.PackageId
                //             join ct in cityData on pct.CityId equals ct.CityId
                //             join st in stateData on ct.StateId equals st.StateId
                //             where pct.FromTo =="to"
                //             select new VMCityForUser
                //             {
                //                 CityId = ct.CityId,
                //                 CityName = ct.CityName,
                //                 StateName = st.StateName
                //             }).OrderBy(a => a.CityName).ToList().DistinctBy(x => x.CityName).ToList();

                //cityDatas.AddRange(countryDatas);
                //cityDatas.AddRange(stateDatas);
                //cityDatas = cityDatas.OrderBy(a => a.CityName).ToList();
                //cityDatas.Insert(0, new VMCityForUser { CityId=0, CityName = "All", StateName= "" });
                


                //response.Data = cityDatas;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }

        public async Task<Response<List<VMDestination>>> GetDestinationcList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMDestination>>();
            try
            {
                //var pakcagesData = repository.Package.GetAll();
                //var cityData = repository.City.GetAll();
                //var fileData = repository.ProjectFile.GetAll().Where(a => a.FileFor == "city");
                 
                //var Datas = (from p in pakcagesData                                 
                //             join ct in cityData on p.CityTo equals ct.CityId
                //             join f in fileData on ct.CityId equals f.FileForId into Inners
                //             from ctf in Inners.DefaultIfEmpty()
                //             where p.Status == "Active" 
                //             select new VMDestination
                //             { 
                //                 Name = ct.CityName,
                //                 Description = ct.Description,                                 
                //                 FileName = ctf.FileName
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


        public async Task<Response<VMDestination>> DestinationcDetail(string name)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMDestination>();
            try
            {
                //var pakcagesData = repository.Package.GetAll();
                //var cityData = repository.City.GetAll().Where(a => a.CityName == name);
                //var stateData = repository.State.GetAll();
                //var fileData = repository.ProjectFile.GetAll().Where(a => a.FileFor == "city"); ;
                //var filepData = repository.ProjectFile.GetAll().Where(a => a.FileFor == "package");
                //var packageCity = repository.PackageCity.GetAll();

                //var results = filepData.GroupBy(
                //                    p => p.FileForId,
                //                    p => p.FileId,
                //                    (key, g) => new { FileForId = key, Cars = g.ToList().FirstOrDefault() });

                //var packagefile = (from f in filepData
                //                   join r in results on f.FileId equals r.Cars
                //                   select new VMProjectFileDisplay
                //                   {   
                //                       FileId = r.FileForId,
                //                       FileName = f.FileName                                       
                //                   }).AsQueryable();
                                

          
                //var pData = (from p in pakcagesData
                //             join tct in cityData on p.CityTo equals tct.CityId
                //             join ts in stateData on tct.StateId equals ts.StateId
                //             join f in packagefile on p.PackageId equals f.FileId //into Inners
                //             //from stf in Inners.DefaultIfEmpty() 
                //             select new VMSPPackages
                //             {
                //                 PackageId = p.PackageId,
                //                 PackageType = p.PackageType,
                //                 PackageName = p.PackageName,
                //                 Cityname = tct.CityName,
                //                 StateName = ts.StateName,
                //                 Imagename = f.FileName,
                //                 Days = p.Days,
                //                 Nights = p.Nights,
                //                 PriceForDouble = p.PriceForDouble,
                //             }).ToList();

                //var Datas = (from p in pakcagesData
                //             join ct in cityData on p.CityTo equals ct.CityId
                //             join f in fileData on ct.CityId equals f.FileForId into Inners
                //             from ctf in Inners.DefaultIfEmpty()
                //             where p.Status == "Active"
                //             select new VMDestination
                //             {   
                //                 Name = ct.CityName,
                //                 Description = ct.Description,
                //                 FileName = ctf.FileName,
                //                 Packages = pData,
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

        public async Task<Response<List<VMCity>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCity>>();
            try
            {
                var orderby = param.orderBy == "desc" ? true : false;
                var ordercolumn = param.orderColumn == "" ? "CityName" : param.orderColumn.ToUpperFirstLetter();

                var baseData = repository.City.GetAll();
                var queryData = baseData.Where(s => (s.CityName.Contains(param.search) || s.CityName.Contains(param.search)));

                var Datas = (from city in queryData
                             join state in repository.State.GetAll() on city.StateId equals state.StateId
                             join country in repository.Country.GetAll() on state.CountryId equals country.CountryId
                             select new VMCity
                             {
                                 CityId = city.CityId,
                                 StateId = city.StateId,
                                 CountryId = country.CountryId,
                                 CityName = city.CityName,
                                 StateName = state.StateName,
                                 CityCode = city.CityCode,
                                 Description = city.Description,
                                 Status = city.Status,
                                 CreatedBy = city.CreatedBy,
                                 CreatedDate = city.CreatedDate,
                                 UpdatedBy = city.UpdatedBy,
                                 UpdatedDate = city.UpdatedDate
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

