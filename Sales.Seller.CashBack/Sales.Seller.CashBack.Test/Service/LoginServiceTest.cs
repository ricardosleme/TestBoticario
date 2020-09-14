using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Repository;
using Sales.Seller.CashBack.API.Repository.Data;
using Sales.Seller.CashBack.API.Service;
using Xunit;

namespace Sales.Seller.CashBack.Test.Service
{
    public class LoginServiceTest
    {
        LoginService loginService;
        string cpfValido;
        string senhaValido; 
        public LoginServiceTest() {

            var Logger = Substitute.For<ILogger<LoginService>>();
            var SalesRepository = Substitute.For<ISalesRepository>();
            cpfValido = "123456789";
            senhaValido = "123456789";
            Vendor vendor = new Vendor() { CPF = cpfValido, Senha = senhaValido };
            SalesRepository.GetVendorSenha(cpfValido, senhaValido).Returns(vendor);
            var configuration =  new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build(); 
            loginService = new LoginService(Logger, SalesRepository, configuration);
        }
        [Fact]
        public void GetLoginSucessTest()
        {
            LoginRequest loginRequest = new LoginRequest() { CPF = cpfValido, Senha = senhaValido };
            var resultado = loginService.GetLogin(loginRequest);
            Assert.True(resultado.Sucess);
        }
        [Fact]
        public void GetLoginFalseTest()
        {
            LoginRequest loginRequest = new LoginRequest() { CPF = "123", Senha = "123" };
            var resultado = loginService.GetLogin(loginRequest);
            Assert.False(resultado.Sucess);
        }
    }
}
