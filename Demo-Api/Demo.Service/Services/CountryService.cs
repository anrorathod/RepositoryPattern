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

namespace Demo.Service.Services
{
    public class CountryService : ICountryService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CountryService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMCountry inputData, int LoggedInUserId = 0)
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

                var duplicateDataQuery = await repository.Country.GetAsync(g => g.CountryName == inputData.CountryName || g.CountryCode == inputData.CountryCode);
                var duplicateData = duplicateDataQuery.Any();

                if(!duplicateData)
                {
                    var model = mapper.Map<Country>(inputData);
                    repository.Country.Add(model);
                    response.Data = await repository.Country.CommitAsync();
                }
                else
                {
                    response.Message = "Country name or Country Code already exists.";
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
                var data = await repository.Country.GetAsync(a => a.CountryId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.Country.Update(dataExists);
                    response.Data = await repository.Country.CommitAsync();
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

        public void DetachEntity(VMCountry entityToDetach)
        {
            var model = mapper.Map<Country>(entityToDetach);
            repository.Country.DetachEntities(model);
        }

        public async Task<Response<List<VMCountry>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCountry>>();
            try
            {
                var model = await repository.Country.GetAsync();
                var vmData = mapper.Map<List<VMCountry>>(model);
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

        public async Task<Response<VMCountry>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMCountry>();
            try
            {
                var model = await repository.Country.GetByIdAsync((decimal)id);
                var vmData = mapper.Map<VMCountry>(model);
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

        public async Task<Response<bool>> UpdateDataAsync(VMCountry updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.Country.GetAsync(a => a.CountryId != Convert.ToInt32(updateData.CountryId) && (a.CountryName == updateData.CountryName || a.CountryCode == updateData.CountryCode));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "Country name or country code already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.Country.GetAsync(a => a.CountryId == Convert.ToInt32(updateData.CountryId));
                    var newData = newDatacheck.FirstOrDefault();
                    if(newData != null)
                    { 
                        newData.CountryName = updateData.CountryName;
                        newData.CountryCode = updateData.CountryCode;
                        newData.Description = updateData.Description;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.Country.Update(newData);
                        response.Data = await repository.Country.CommitAsync();
                    }
                    else
                    {
                        response.Message = "Country id not found.";
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

        public async Task<Response<List<VMCountry>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCountry>>();
            try
            {
                var queryData = repository.Country.GetAll();//.Where(s => s.Discontinued == false && (s.ProductName.Contains(request.search) || s.ProductNumber.Contains(request.search)));

                var Datas = (from country in queryData.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            select new VMCountry
                            {
                                CountryId = country.CountryId,
                                CountryCode = country.CountryCode,
                                CountryName = country.CountryName,
                                Description = country.Description
                            }).ToList();

                var model = Datas; 
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

        public async Task<Response<List<VMCountryForUser>>> GetCountryList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCountryForUser>>();
            try
            {
                var baseData = repository.Country.GetAll().Where(a=>a.Status == Status.Active.ToString()).OrderBy(a=>a.CountryName).ToList();
                var Datas = mapper.Map<List<VMCountryForUser>>(baseData);
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

        public async Task<Response<List<VMCountry>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCountry>>();
            try
            {
                var orderby = param.orderBy == "desc" ? true : false;
                var ordercolumn = param.orderColumn == "" ? "CountryName" : param.orderColumn.ToUpperFirstLetter();

                var baseData = repository.Country.GetAll();
                var queryData = baseData.Where(s => (s.CountryName.Contains(param.search) || s.CountryCode.Contains(param.search)));

                var Datas = (from country in queryData.Skip((param.currentPage - 1) * param.pageSize).Take(param.pageSize)
                             select new VMCountry
                             {
                                 CountryId = country.CountryId,
                                 CountryCode = country.CountryCode,
                                 CountryName = country.CountryName,
                                 CountryPhoneCode = country.CountryPhoneCode,
                                 Description = country.Description,
                                 Status = country.Status,
                                 CreatedBy = country.CreatedBy,
                                 CreatedDate = country.CreatedDate,
                                 UpdatedBy = country.UpdatedBy,
                                 UpdatedDate = country.UpdatedDate
                             }).OrderBy(ordercolumn, orderby).ToList();
                 
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

