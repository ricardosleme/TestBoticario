using Sales.Seller.CashBack.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Service
{
    public interface ISellerService
    {
        public SellerResponse PostSeller(SellerRequest sellerRequest);
        public CashBackResponse GetCashBack(string id);
    }
}
