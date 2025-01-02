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
    public class ContactUsService : IContactUsService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public ContactUsService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        
        public async Task<Response<bool>> CreateDataAsync(VMContactUs inputData, int LoggedInUserId = 0)
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

                //var duplicateDataQuery = await repository.ContactUs.GetAsync(g => g.ContactUsName == inputData.ContactUsName);
                //var duplicateData = duplicateDataQuery.Any();

                //if(!duplicateData)
                //{
                    var model = mapper.Map<ContactUs>(inputData);
                    repository.ContactUs.Add(model);
                    response.Data = await repository.ContactUs.CommitAsync();
                //}
                //else
                //{
                //    response.Message = "ContactUs name already exists.";
                //    response.Success = false;
                //}                
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
                var data = await repository.ContactUs.GetAsync(a => a.ContactId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.ContactUs.Update(dataExists);
                    response.Data = await repository.ContactUs.CommitAsync();
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

        public void DetachEntity(VMContactUs entityToDetach)
        {
            var model = mapper.Map<ContactUs>(entityToDetach);
            repository.ContactUs.DetachEntities(model);
        }

        public async Task<Response<List<VMContactUs>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMContactUs>>();
            try
            {
                var model = await repository.ContactUs.GetAsync();
                var vmData = mapper.Map<List<VMContactUs>>(model);
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

        public async Task<Response<VMContactUs>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMContactUs>();
            try
            {
                var model = await repository.ContactUs.GetByIdAsync(Convert.ToDecimal(id));
                var vmData = mapper.Map<VMContactUs>(model);
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

        public async Task<Response<bool>> UpdateDataAsync(VMContactUs updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                //var data = await repository.ContactUs.GetAsync(a => a.ContactId != Convert.ToInt32(updateData.ContactUsId) && (a.ContactUsName == updateData.ContactUsName || a.ContactUsCode == updateData.ContactUsCode));
                //var exstingData = data.FirstOrDefault();
                //if (exstingData != null)
                //{
                //    response.Message = "ContactUs name or ContactUs code already exists.";
                //    response.Success = false;
                //}
                //else
                //{
                    var newDatacheck = await repository.ContactUs.GetAsync(a => a.ContactId == Convert.ToInt32(updateData.ContactId));
                    var newData = newDatacheck.FirstOrDefault();
                    if(newData != null)
                    { 
                        newData.EmailId = updateData.EmailId;
                        newData.Name = updateData.Name;
                        newData.Message = updateData.Message;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.ContactUs.Update(newData);
                        response.Data = await repository.ContactUs.CommitAsync();
                    }
                    else
                    {
                        response.Message = "ContactUs id not found.";
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

        public async Task<Response<List<VMContactUs>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMContactUs>>();
            try
            {
                var queryData = repository.ContactUs.GetAll();//.Where(s => s.Discontinued == false && (s.ProductName.Contains(request.search) || s.ProductNumber.Contains(request.search)));

                var Datas = (from ContactUs in queryData.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            select new VMContactUs
                            {
                                //ContactUsId = ContactUs.ContactUsId,
                                //ContactUsCode = ContactUs.ContactUsCode,
                                //ContactUsName = ContactUs.ContactUsName
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

        //public async Task<Response<List<VMContactUsForUser>>> GetContactUsList()
        //{
        //    var exceptions = new Dictionary<string, string>();
        //    var response = new Response<List<VMContactUsForUser>>();
        //    try
        //    {
        //        var baseData = repository.ContactUs.GetAll().Where(a=>a.Status == Status.Active.ToString()).OrderBy(a=>a.ContactUsName).ToList();
        //        var Datas = mapper.Map<List<VMContactUsForUser>>(baseData);
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

        public async Task<Response<List<VMContactUs>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMContactUs>>();
            try
            {
                var orderby = param.orderBy == "desc" ? true : false;
                var ordercolumn = param.orderColumn.ToUpperFirstLetter();

                var baseData = repository.ContactUs.GetAll();
                var queryData = baseData.Where(s => (s.Name.Contains(param.search) || s.EmailId.Contains(param.search) || s.Phone.Contains(param.search)  || s.Message.Contains(param.search)));

                var Datas = (from ContactUs in queryData
                             select new VMContactUs
                             {
                                 ContactId = ContactUs.ContactId,
                                 Name = ContactUs.Name,
                                 Subject = ContactUs.Subject,
                                 EmailId = ContactUs.EmailId,
                                 Message = ContactUs.Message,
                                 Phone = ContactUs.Phone,
                                 Status = ContactUs.Status,
                                 CreatedBy = ContactUs.CreatedBy,
                                 CreatedDate = ContactUs.CreatedDate,
                                 UpdatedBy = ContactUs.UpdatedBy,
                                 UpdatedDate = ContactUs.UpdatedDate
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


