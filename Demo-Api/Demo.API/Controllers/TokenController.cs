using Azure.Core;
using Demo.Service;
using Demo.Service.Contracts;
using Demo.ViewModel.Request;
using Demo.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Demo.API.Service;

namespace Demo.API.Controllers
{
    public class TokenController : Controller
    {
        public ILogger<TokenController> Logger { get; }
        private IServiceWrapper service;
        private readonly TokenService tokenService;
        public TokenController(ILogger<TokenController> logger, IServiceWrapper _service, TokenService _tokenService)
        {
            service = _service;
            Logger = logger;
            tokenService = _tokenService;
        } 

        [HttpPost]
        [Route("api/User/token")]
        public async Task<Response<AuthResponse>> Post(AuthenticateRequest _userData)
        {
            var exceptions = new Dictionary<string, string>();
            var response = new Response<AuthResponse>();
            try
            {
                if (_userData != null)
                {
                    var userInDb = await service.User.GetUserTokenAsync(_userData);

                    if (userInDb is null)
                    {
                        response.Success = false;
                        response.Message = "Username or password is not valid";
                    }
                    else
                    {
                        var accessToken = tokenService.CreateToken(userInDb);

                        var data = new AuthResponse
                        {
                            Username = userInDb.Username,
                            Email = userInDb.EmailId,
                            Token = accessToken,
                        };
                        response.Data = data;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Something is wrong.";
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
    }
}
