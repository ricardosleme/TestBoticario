using Sales.Seller.CashBack.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Service
{
    public interface ISaleService 
    {
        public SaleGetResponse GetSales(string id);
        public SaleResponse PostSales(string id, SalePostRequest salePostRequest);
        public SaleResponse PutSales(string id, string idSales, SalePutRequest salePutRequest);
        public SaleResponse DeleteSales(string id, string idSales);
    }
}
