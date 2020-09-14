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
using System;
using Xunit;

namespace Sales.Seller.CashBack.Test.Service
{
    public class SaleServiceTest
    {
        private SaleService saleService;
        private SellerRequest sellerRequestSucess;
        private string idsucess;
        private string idsucess1;
        private string idsucessSales;
        public SaleServiceTest()
        {

            var logger = Substitute.For<ILogger<SaleService>>();
            var salesRepository = Substitute.For<ISalesRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SaleProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var id = ObjectId.GenerateNewId();
            idsucess = id.ToString();
            var id1 = ObjectId.GenerateNewId();
            idsucess1 = id1.ToString();
            var idSales = ObjectId.GenerateNewId();
            idsucessSales = idSales.ToString();

            sellerRequestSucess = new SellerRequest() { CPF = "1234567", Senha = "1234", Email = "aaa", Nome = "aaa" };
            Sale sale = new Sale() { _id = idSales , Data = DateTime.Now , Valor = 1000, CashBackPer = 10 , CashBackValor = 100 , Codigo = 10, Status = "em validação" };
            Vendor vendor = new Vendor() { _id = id, CPF = sellerRequestSucess.CPF, Senha = sellerRequestSucess.Senha, Sales = new System.Collections.Generic.List<Sale>() { sale} };
            Sale sale1 = new Sale() { _id = idSales, Data = DateTime.Now, Valor = 1000, CashBackPer = 10, CashBackValor = 100, Codigo = 10, Status = "Aprovado" };
            Vendor vendor1 = new Vendor() { _id = id1, CPF = "15350946056", Senha = sellerRequestSucess.Senha , Sales = new System.Collections.Generic.List<Sale>() { sale1} };
            
            
            
            salesRepository.GetVendorId(idsucess).Returns(vendor);
            salesRepository.GetVendorId(idsucess1).Returns(vendor1);
            salesRepository.PutVendor(vendor).Returns(vendor);
            salesRepository.PostVendor(vendor).Returns(vendor);
            saleService = new SaleService(logger,  mapper, salesRepository);
        }
        [Fact]
        public void DeleteSalesSucessTest()
        {
            var resultado = saleService.DeleteSales(idsucess, idsucessSales);
            Assert.True(resultado.Sucess);
            
        }
        [Fact]
        public void DeleteSalesErrorStatusTest()
        {
            var resultado = saleService.DeleteSales(idsucess1, idsucessSales);
            Assert.False(resultado.Sucess);

        }

        [Fact]
        public void GetSalesSucessTest()
        {
            var resultado = saleService.GetSales(idsucess);
            Assert.True(resultado.Sucess);

        }
        [Fact]
        public void PostSalesSucessTest()
        {
            SalePostRequest salePostRequest = new SalePostRequest() { Codigo = 10, DataCompra = DateTime.Now, Valor = 900 };
            var resultado = saleService.PostSales(idsucess, salePostRequest);
            Assert.True(resultado.Sucess);
        }
        [Fact]
        public void PostSales10SucessTest()
        {
            var resultado = saleService.CashBackCalc(900);
            Assert.Equal(10, resultado.Item1);
        }
        [Fact]
        public void PostSales15SucessTest()
        {
            var resultado = saleService.CashBackCalc(1400);
            Assert.Equal(15, resultado.Item1);
        }
        [Fact]
        public void PostSales20SucessTest()
        {
            var resultado = saleService.CashBackCalc(2000);
            Assert.Equal(20, resultado.Item1);
        }
        [Fact]
        public void PutSalesSucessTest()
        {
            SalePutRequest salePutRequest = new SalePutRequest() { Codigo = 20, DataCompra = DateTime.Now, Valor = 1000 };
               var resultado = saleService.PutSales(idsucess,idsucessSales, salePutRequest);
            Assert.True(resultado.Sucess);

        }
        [Fact]
        public void PutSalesErrorStatusTest()
        {

            SalePutRequest salePutRequest = new SalePutRequest() { Codigo = 20, DataCompra = DateTime.Now, Valor = 1000 };
            var resultado = saleService.PutSales(idsucess1, idsucessSales, salePutRequest);
            Assert.False(resultado.Sucess);
        }


    }
}
