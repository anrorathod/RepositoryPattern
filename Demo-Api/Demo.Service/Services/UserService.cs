using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Abstractions;
using System.Security.Cryptography;
using System.Text;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Data.Repositories;
using Demo.Service.Contracts;
using Demo.ViewModel;
using Demo.ViewModel.Enums;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Demo.ViewModel.Tables;

namespace Demo.Service.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        string key = "ENg0aPpiNob009X!A9Hh8Bs2D";
        public UserService(IRepositoryWrapper _repository, IMapper _mapper)
        {  
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<Response<bool>> CreateDataAsync(VMUser inputData, int LoggedInUserId = 0)
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

                var model = mapper.Map<User>(inputData);
                repository.User.Add(model);
                response.Data = await repository.User.CommitAsync();
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
                var data = await repository.User.GetAsync(a => a.UserId == Convert.ToInt32(id));
                var dataExists = data.FirstOrDefault();
                if (dataExists != null)
                {
                    dataExists.Status = Status.Deleted.ToString();
                    dataExists.UpdatedBy = LoggedInUserId;
                    dataExists.UpdatedDate = DateTime.Now;
                    repository.User.Update(dataExists);
                    response.Data = await repository.User.CommitAsync();
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

        public void DetachEntity(VMUser entityToDetach)
        {
            var model = mapper.Map<User>(entityToDetach);
            repository.User.DetachEntities(model);
        }

        public async Task<Response<List<VMUser>>> GetDataAsync()
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMUser>>();
            try
            {
                var model = await repository.User.GetAsync();
                var vmData = mapper.Map<List<VMUser>>(model);
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

        public async Task<Response<VMUser>> GetDatabyIdAsync(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMUser>();
            try
            {
                var model = await repository.User.GetByIdAsync((int)id);
                var vmData = mapper.Map<VMUser>(model);
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
                var User = await repository.User.GetAsync(g => g.EmailId == name);
                response.Data = User.Any();
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
           
        }

        public async Task<Response<bool>> UpdateDataAsync(VMUser updateData, int LoggedInUserId = 0)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var data = await repository.User.GetAsync(a => a.UserId != Convert.ToInt32(updateData.UserId) && (a.EmailId == updateData.EmailId || a.MobileNumber == updateData.MobileNumber));
                var exstingData = data.FirstOrDefault();
                if (exstingData != null)
                {
                    response.Message = "Email id or Mobile number already exists.";
                    response.Success = false;
                }
                else
                {
                    var newDatacheck = await repository.User.GetAsync(a => a.UserId == Convert.ToInt32(updateData.UserId));
                    var newData = newDatacheck.FirstOrDefault();
                    if (newData != null)
                    {
                        newData.FirstName = updateData.FirstName;
                        newData.LastName = updateData.LastName;
                        newData.EmailId = updateData.EmailId;
                        newData.MobileNumber = updateData.MobileNumber;
                        newData.Status = updateData.Status;
                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = LoggedInUserId;
                        repository.User.Update(newData);
                        response.Data = await repository.User.CommitAsync();
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

        public async Task<Response<List<VMUser>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMUser>>();
            try
            {
                var model = await repository.User.GetAsync(null, a => a.OrderBy(s => s.EmailId), pageNumber, pageSize);
                var vmData = mapper.Map<List<VMUser>>(model);
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

        public async Task<VMUser> GetUserTokenAsync(AuthenticateRequest userData)
        {            
            var model = await repository.User.GetSingleDataAsync(a => a.EmailId == userData.Username && string.Equals(a.Password,userData.Password) == true && a.usertype == userData.UType);

            if(model != null)
            {
                var IsPasswordMatched = string.Equals(model.Password, userData.Password);
                if (!IsPasswordMatched)
                    model = null;
            }            

            var vmData = mapper.Map<VMUser>(model);
            return vmData;
        }

        public async Task<Response<VMMyProfile>> GetMyProfile(object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMMyProfile>();
            try
            {
                decimal decimalVal = decimal.Parse(id.ToString());
                var model = await repository.User.GetByIdAsync(decimalVal);
                var vmData = mapper.Map<VMMyProfile>(model);
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
        public async Task<Response<VMRegister>> RegisterUserAsync(VMRegister inputData)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMRegister>();
            try
            {
                var model = mapper.Map<User>(inputData);

                var existingUser = await repository.User.GetAsync(g => g.EmailId == inputData.EmailId);
                var data = existingUser.FirstOrDefault();
                if (data == null)
                {
                    model.Username = model.EmailId;
                    model.Status = Status.Register.ToString();
                    model.usertype = ViewModel.Enums.Role.User.ToString();
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 1;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = 1;
                    repository.User.Add(model);
                    var insertdata = await repository.User.CommitAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Email id is alredy exists.";
                }

                inputData.Password = "";
                response.Data = inputData;

            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<VMMyProfile>> UpdateProfile(VMMyProfile inputData, object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMMyProfile>();
            try
            {
                var existingData = await repository.User.GetByIdAsync(id);
                if (existingData != null)
                {
                    existingData.FirstName = inputData.FirstName;
                    existingData.LastName = inputData.LastName;
                    existingData.EmailId = inputData.EmailId;
                    existingData.MobileNumber = inputData.MobileNumber; 
                    repository.User.Update(existingData);
                    await repository.User.CommitAsync();
                }
                response.Data = inputData;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<bool>> ChangePassword(VMChangePassword updateData, object id)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<bool>();
            try
            {
                var existingData = await repository.User.GetByIdAsync(id);
                if (existingData != null)
                {
                    if (updateData.NewPassword != updateData.NewPasswordRetype)
                    {
                        response.Success = false;
                        response.Message = "New Password and Retype Password is not mached.";
                    }
                    else if (existingData.Password != updateData.OldPassword)
                    {
                        response.Success = false;
                        response.Message = "Existing Password is not matched.";
                    }
                    else
                    {
                        existingData.Password = updateData.NewPassword;
                        existingData.UpdatedDate = DateTime.Now;
                        repository.User.Update(existingData);
                        await repository.User.CommitAsync();
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
        public async Task<Response<VMUpdatePassword>> ChangePasswordAsync(string EmailID, string Password)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMUpdatePassword>();
            try
            {
                var existingData = await repository.User.GetAsync(e => e.EmailId == EmailID);
                var retdata = mapper.Map<VMUpdatePassword>(existingData.FirstOrDefault());
                var data = existingData.FirstOrDefault();
                if (data != null)
                {
                    data.Password = Password;
                    repository.User.Update(data);
                    await repository.User.CommitAsync();
                }
                response.Data = retdata;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<VMResetPassword>> GetResetPasswordDetailsAsync(string request)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMResetPassword>();
            VMResetPassword vMRestPassword = new VMResetPassword();
            try
            {
                //string strDecrypt = Decrypt(request);
                //string[] arrObj = strDecrypt.Split('&');
                //vMRestPassword.EmailId = arrObj[0];
                response.Data = vMRestPassword;
                response.Success = true;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Exceptions = exceptions;
                response.Success = false;
            }
            return response;
        }

        public async Task<Response<VMResetPassword>> ForgotPasswordByUserName(object userName, string template, string strURL)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<VMResetPassword>();
            try
            {
                var model = await repository.User.GetAsync(a => a.Username == (string)userName.ToString().Replace("-", " ") || a.EmailId == (string)userName.ToString().Replace("-", " "));
                var data = mapper.Map<VMUser>(model.FirstOrDefault());
                VMResetPassword vMRestPassword = new VMResetPassword();
                if (data == null)
                {
                    response.Success = false;
                    response.Message = "User name does not exists.";
                }
                else
                {
                    vMRestPassword.EmailId = data.EmailId;
                    string strQ = Encrypt(data.EmailId + "&" + DateTime.Now);
                    ICommonService commonService = new CommonService(repository, mapper);
                    template = template.Replace("{activationlink}", strURL + "resetpassword?p=" + strQ);
                    template = template.Replace("{firstname}", data.FirstName);
                    template = template.Replace("{lastname}", data.LastName);
                    await commonService.SentEmail(userName.ToString(), data.EmailId, "", "", template, "Reset Password", "");
                    response.Data = vMRestPassword;
                    response.Success = true;
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
        public async Task<Response<List<VMUserForList>>> GetOnAdminSearchWithPaginationAsync(RequestParam request)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<List<VMUserForList>>();

            try
            {
                var orderby = request.orderBy == "desc" ? true : false;
                var ordercolumn = request.orderColumn == "" ? "FirstName" : request.orderColumn.ToUpperFirstLetter();

                var searchQuery = repository.User.GetAll();
                int totalRecords = searchQuery.Count();
                searchQuery = searchQuery.Where(s => s.usertype == "user" && (s.FirstName.Contains(request.search) || s.LastName.Contains(request.search) || s.EmailId.Contains(request.search)));
                int totalFilteredRecords = searchQuery.Count();

                var Datas = (from user in searchQuery
                             select new VMUserForList
                             {
                                 UserId = user.UserId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 EmailId = user.EmailId,
                                 MobileNumber = user.MobileNumber,
                                 Status = user.Status,
                                 CreatedDate = user.CreatedDate,
                                 UpdatedDate = user.UpdatedDate
                             }).OrderBy(ordercolumn, orderby).Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

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

        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}

