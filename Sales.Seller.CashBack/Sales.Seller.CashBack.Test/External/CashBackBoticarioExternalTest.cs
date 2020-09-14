using Microsoft.Extensions.Configuration;
using Sales.Seller.CashBack.API.External;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Sales.Seller.CashBack.Test.External
{
    public class CashBackBoticarioExternalTest
    {
        public CashBackBoticarioExternal cashBackBoticarioExternal;
        public CashBackBoticarioExternalTest() {
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();
            cashBackBoticarioExternal = new CashBackBoticarioExternal(configuration);
        }
        [Fact]
        public async void TestIntegracao() {
            var result =  await cashBackBoticarioExternal.GetCashBack("12312312323");
            Assert.NotNull(result);
        }

    }
}
