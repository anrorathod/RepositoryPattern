using AutoMapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.StoreProcedure;
using Demo.Data.Contracts;
using Demo.Data.Repositories;
using Demo.Service.Contracts;
using Demo.ViewModel.Response;
using Demo.ViewModel.StoreProcedure;
using Demo.ViewModel.Tables;

namespace Demo.Service.Services
{
    public class StoreProcedureService : IStoreProcedureService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public StoreProcedureService(IRepositoryWrapper _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<List<VMSPDestination>>> GetDestinationList()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMSPDestination>>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>
                {
                    //new SqlParameter { ParameterName = "@CategoryName", Value = "1" },
                    //new SqlParameter { ParameterName = "@CategoryCode", Value = "1" }
                };

                //var model = await repository.SP.ExecuteSP("EXEC sp_GetDestination @CategoryName, @CategoryCode", param);
                var model = await repository.SP.ExecuteSP("EXEC sp_GetDestination", param);
                var vmData = mapper.Map<List<VMSPDestination>>(model);
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
        public async Task<Response<List<VMSPDestination>>> GetDestinationList2()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMSPDestination>>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>
                {
                };

                var model = await repository.SP.ExecuteSP("EXEC sp_GetDestination2", param);
                var vmData = mapper.Map<List<VMSPDestination>>(model);
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

         

        #region default not required to implement
        public Task<Response<List<VMSPData>>> GetDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<VMSPData>> GetDatabyIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<VMSPData>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> CreateDataAsync(VMSPData inputData, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateDataAsync(VMSPData updateData, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteDataAsync(object id, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public void DetachEntity(VMSPData entityToDetach)
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
