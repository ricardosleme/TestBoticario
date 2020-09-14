using Sales.Seller.CashBack.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sales.Seller.CashBack.Test.Entities
{
    public class CreateEntitiesTest
    {
        [Fact]
        public void CreateBaseResponse()
        {
            var objeto = new BaseResponse() { Sucess = true };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateCashBackBoticarioResponse()
        {
            var objeto = new CashBackBoticarioResponse() { statusCode = 200};
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateCashBackResponse()
        {
            var objeto = new CashBackResponse() { Sucess = true, Credit = 10 };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateError()
        {
            var objeto = new Error() { Code = 1 , Descricao = "teste"};
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateLoginRequest()
        {
            var objeto = new LoginRequest() { CPF = "123", Senha = "123" };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateLoginResponse()
        {
            var objeto = new LoginResponse() { Sucess = true };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateSaleGetResponse()
        {
            var objeto = new SaleGetResponse() { Sucess = true };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateSalePostRequest()
        {
            var objeto = new SalePostRequest() { Codigo = 123, Valor = 123, DataCompra = DateTime.Now };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateSalePutRequest()
        {
            var objeto = new SalePutRequest() { Codigo = 123, Valor = 123, DataCompra = DateTime.Now };
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateSaleResponse()
        {
            var objeto = new SaleListReponse() { Id = "123" };
            Assert.NotNull(objeto);
        }
     
        [Fact]
        public void CreateSellerRequest()
        {
            var objeto = new SellerRequest() { CPF = "123" , Email = "teste", Senha = "123", Nome ="123"};
            Assert.NotNull(objeto);
        }
        [Fact]
        public void CreateSellerResponse()
        {
            var objeto = new SellerResponse() { Id = "123" };
            Assert.NotNull(objeto);
        }

    }
}
