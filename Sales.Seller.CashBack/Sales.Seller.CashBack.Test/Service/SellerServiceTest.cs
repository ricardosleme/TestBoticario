using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NSubstitute;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.External;
using Sales.Seller.CashBack.API.Mapper;
using Sales.Seller.CashBack.API.Repository;
using Sales.Seller.CashBack.API.Repository.Data;
using Sales.Seller.CashBack.API.Service;
using Xunit;
namespace Sales.Seller.CashBack.Test.Service
{
    public class SellerServiceTest
    {
        private SellerService sellerService;
        private SellerRequest sellerRequestSucess;
        private string idsucess;
        public SellerServiceTest()
        {

            var logger = Substitute.For<ILogger<SellerService>>();
            var salesRepository = Substitute.For<ISalesRepository>();
            var cashBackBoticarioExternal = Substitute.For<ICashBackBoticarioExternal>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SaleProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var id = ObjectId.GenerateNewId();
            idsucess = id.ToString();
            sellerRequestSucess = new SellerRequest() { CPF = "1234567", Senha = "1234", Email = "aaa", Nome = "aaa" };
            Vendor vendor = new Vendor() { _id = id, CPF = sellerRequestSucess.CPF, Senha = sellerRequestSucess.Senha };
            salesRepository.GetVendorCpf("123").Returns(vendor);
            salesRepository.GetVendorId(idsucess).Returns(vendor);
            salesRepository.PostVendor(vendor).Returns(vendor);
            cashBackBoticarioExternal.GetCashBack("1234567").Returns(10);
            sellerService = new SellerService(logger, salesRepository,  mapper,cashBackBoticarioExternal);
        }
        [Fact]
        public void PostSellerSucessTest()
        {
            
            var resultado = sellerService.PostSeller(sellerRequestSucess);
            Assert.True(resultado.Sucess);
        }
        [Fact]
        public void PostSellerErrorNotFoundTest()
        {
            SellerRequest sellerRequest = new SellerRequest() { CPF = "123" };
            var resultado = sellerService.PostSeller(sellerRequest);
            Assert.False(resultado.Sucess);
        }
        [Fact]
        public void GetCashBackTest()
        {
            var resultado = sellerService.GetCashBack(idsucess);
            Assert.True(resultado.Sucess);
        }
        [Fact]
        public void GetCashBackNotFoundTest()
        {
            var resultado = sellerService.GetCashBack(ObjectId.GenerateNewId().ToString());
            Assert.False(resultado.Sucess);
        }
    }
}
