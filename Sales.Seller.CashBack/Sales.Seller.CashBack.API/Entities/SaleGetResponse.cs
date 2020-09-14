using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class SaleGetResponse : BaseResponse
    {
        public SaleGetResponse() { }
        public SaleGetResponse(List<SaleListReponse> sales)
        {
            this.Sucess = true;
            this.Sales = sales;
        }
        public List<SaleListReponse> Sales { get; set; }
    }
}
