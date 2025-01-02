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
    public class FaqService : IFaqService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public FaqService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        
        public async Task<Response<bool>> CreateDataAsync(VMFaq inputData, int LoggedInUserId = 0)
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

                var duplicateDataQuery = await repository.Faq.GetAsync(g => g.Question == inputData.Question);
                var duplicateData = duplicateDataQuery.Any();

                if(!duplicateData)
                {
                    var model = mapper.Map<Faq>(inputData);
                    repository.Faq.Add(model);
                    response.Data = await repository.Faq.CommitAsync();
                }
                else
                {
                    response.Message = "Faq name already exists.";
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
                var data = await repository.Faq.GetAsync(a => a.FaqId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.Faq.Update(dataExists);
                    response.Data = await repository.Faq.CommitAsync();
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

        public void DetachEntity(VMFaq entityToDetach)
        {
            var model = mapper.Map<Faq>(entityToDetach);
            repository.Faq.DetachEntities(model);
        }

        public async Task<Response<List<VMFaq>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMFaq>>();
            try
            {
                var model = await repository.Faq.GetAsync();
                var vmData = mapper.Map<List<VMFaq>>(model);
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

        public async Task<Response<VMFaq>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMFaq>();
            try
            {
                var model = await repository.Faq.GetByIdAsync((decimal)id);
                var vmData = mapper.Map<VMFaq>(model);
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

        public async Task<Response<bool>> UpdateDataAsync(VMFaq updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.Faq.GetAsync(a => a.FaqId != Convert.ToInt32(updateData.FaqId) && (a.Question == updateData.Question ));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "Faq question already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.Faq.GetAsync(a => a.FaqId == Convert.ToInt32(updateData.FaqId));
                    var newData = newDatacheck.FirstOrDefault();
                    if(newData != null)
                    {
                        newData.FaqCategory = updateData.FaqCategory;
                        newData.Question = updateData.Question;
                        newData.Solution = updateData.Solution;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.Faq.Update(newData);
                        response.Data = await repository.Faq.CommitAsync();
                    }
                    else
                    {
                        response.Message = "Faq id not found.";
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

        public async Task<Response<List<VMFaq>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMFaq>>();
            try
            {
                var queryData = repository.Faq.GetAll();//.Where(s => s.Discontinued == false && (s.ProductName.Contains(request.search) || s.ProductNumber.Contains(request.search)));

                var Datas = (from Faq in queryData.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            select new VMFaq
                            {
                                FaqId = Faq.FaqId,
                                FaqCategory = Faq.FaqCategory,
                                Question = Faq.Question,
                                Solution = Faq.Solution
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

        public async Task<Response<List<VMFaqForUser>>> GetFaqList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMFaqForUser>>();
            try
            {
                var baseData = repository.Faq.GetAll().Where(a => a.Status == Status.Active.ToString()).OrderBy(a => a.Question).ToList();
                var Datas = mapper.Map<List<VMFaqForUser>>(baseData);
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

        public async Task<Response<List<VMFaq>>> AdminGetDataSearchWithPaginationAsync(RequestParam param)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMFaq>>();
            try
            {
                var orderby = param.orderBy == "desc" ? true : false;
                var ordercolumn = param.orderColumn == "" ? "Question" : param.orderColumn.ToUpperFirstLetter();

                var baseData = repository.Faq.GetAll();
                var queryData = baseData.Where(s => (s.Question.Contains(param.search) || s.Question.Contains(param.search)));

                var Datas = (from Faq in queryData
                             select new VMFaq
                             {
                                 FaqId = Faq.FaqId,
                                 FaqCategory = Faq.FaqCategory,
                                 Question = Faq.Question,
                                 Solution = Faq.Solution,
                                 Status = Faq.Status,
                                 CreatedBy = Faq.CreatedBy,
                                 CreatedDate = Faq.CreatedDate,
                                 UpdatedBy = Faq.UpdatedBy,
                                 UpdatedDate = Faq.UpdatedDate
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


