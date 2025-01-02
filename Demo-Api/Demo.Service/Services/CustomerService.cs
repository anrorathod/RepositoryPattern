using AutoMapper;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contract;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Service
{
    public class CustomerService : ICustomerService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CustomerService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        
        public async Task<Response<bool>> CreateDataAsync(VMCustomer inputData, int LoggedInUserId = 0)
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

                var duplicateDataQuery = await repository.Customer.GetAsync(g => g.EmailId == inputData.EmailId);
                var duplicateData = duplicateDataQuery.Any();

                if(!duplicateData)
                {
                    var model = mapper.Map<Customer>(inputData);
                    repository.Customer.Add(model);
                    response.Data = await repository.Customer.CommitAsync();
                }
                else
                {
                    response.Message = "Customer email already exists.";
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
                var data = await repository.Customer.GetAsync(a => a.CustomerId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.Customer.Update(dataExists);
                    response.Data = await repository.Customer.CommitAsync();
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

        public void DetachEntity(VMCustomer entityToDetach)
        {
            var model = mapper.Map<Customer>(entityToDetach);
            repository.Customer.DetachEntities(model);
        }

        public async Task<Response<List<VMCustomer>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCustomer>>();
            try
            {
                var model = await repository.Customer.GetAsync();
                var vmData = mapper.Map<List<VMCustomer>>(model);
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

        public async Task<Response<VMCustomer>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMCustomer>();
            try
            {
                var model = await repository.Customer.GetByIdAsync((decimal)id);
                var vmData = mapper.Map<VMCustomer>(model);
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

        public async Task<Response<bool>> UpdateDataAsync(VMCustomer updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.Customer.GetAsync(a => a.CustomerId != Convert.ToInt32(updateData.CustomerId) && (a.EmailId == updateData.EmailId ));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "Customer email already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.Customer.GetAsync(a => a.CustomerId == Convert.ToInt32(updateData.CustomerId));
                    var newData = newDatacheck.FirstOrDefault();
                    if(newData != null)
                    { 
                        newData.FirstName = updateData.FirstName;
                        newData.EmailId = updateData.EmailId;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.Customer.Update(newData);
                        response.Data = await repository.Customer.CommitAsync();
                    }
                    else
                    {
                        response.Message = "Customer id not found.";
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

        public async Task<Response<List<VMCustomer>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCustomer>>();
            try
            {
                var queryData = repository.Customer.GetAll();//.Where(s => s.Discontinued == false && (s.ProductName.Contains(request.search) || s.ProductNumber.Contains(request.search)));

                var Datas = (from Customer in queryData.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            select new VMCustomer
                            {
                                CustomerId = Customer.CustomerId,
                                FirstName = Customer.FirstName,
                                LastName = Customer.LastName,
                                EmailId = Customer.EmailId,
                                MobileNumber = Customer.MobileNumber
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

        //public async Task<Response<List<VMCustomerForUser>>> GetCustomerList()
        //{
        //    var exceptions = new Dictionary<string, string>();
        //    var response = new Response<List<VMCustomerForUser>>();
        //    try
        //    {
        //        var baseData = repository.Customer.GetAll().Where(a=>a.Status == Status.Active.ToString()).OrderBy(a=>a.CustomerName).ToList();
        //        var Datas = mapper.Map<List<VMCustomerForUser>>(baseData);
        //        response.Data = Datas; 
        //    }
        //    catch (Exception ex)
        //    {
        //        exceptions.Add("Exception", ex.Message);
        //        response.Exceptions = exceptions;
        //        response.Success = false;
        //    }
        //    return response;
        //}

        public async Task<Response<List<VMCustomer>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMCustomer>>();
            try
            {
                var baseData = repository.Customer.GetAll();
                var queryData = baseData.Where(s => (s.FirstName.Contains(param.search) || s.LastName.Contains(param.search) || s.EmailId.Contains(param.search)));

                var Datas = (from Customer in queryData.Skip((param.currentPage - 1) * param.pageSize).Take(param.pageSize)
                             select new VMCustomer
                             {
                                 CustomerId = Customer.CustomerId,
                                 FirstName = Customer.FirstName,
                                 LastName = Customer.LastName,
                                 EmailId = Customer.EmailId,
                                 MobileNumber = Customer.MobileNumber,
                                 Status = Customer.Status,
                                 CreatedBy = Customer.CreatedBy,
                                 CreatedDate = Customer.CreatedDate,
                                 UpdatedBy = Customer.UpdatedBy,
                                 UpdatedDate = Customer.UpdatedDate
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


