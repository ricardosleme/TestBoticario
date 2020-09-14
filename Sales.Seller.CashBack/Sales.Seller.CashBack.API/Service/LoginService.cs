using Microsoft.Extensions.Logging;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Repository;
using System.IdentityModel.Tokens.Jwt;
using System;
using Sales.Seller.CashBack.API.Repository.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Collections.Generic;

namespace Sales.Seller.CashBack.API.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly ISalesRepository _salesRepository;
        private readonly IConfiguration _configuration;
        public LoginService(ILogger<LoginService> logger, ISalesRepository salesRepository, IConfiguration configuration)
        {
            _salesRepository = salesRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public LoginResponse GetLogin(LoginRequest loginRequest)
        {
            _logger.LogInformation("GetLogin", loginRequest);
            try
            {
                _logger.LogInformation("GetLogin Databese");
                var vendor = _salesRepository.GetVendorSenha(loginRequest.CPF, loginRequest.Senha);
                if (vendor == null) {
                    _logger.LogInformation("GetLogin Senha e Cpf invalido");
                    return new LoginResponse() { Sucess = false, Error = new System.Collections.Generic.List<Error>() { new Error() { Code = 400, Descricao = "senha ou cpf invalido" } } };
                }
                return BuildToken(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError("PostSeller.Error - {0}", ex);
                return new LoginResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
          
        }
        private LoginResponse BuildToken(Vendor vendor)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, vendor._id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new LoginResponse()
            {
                Sucess = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

