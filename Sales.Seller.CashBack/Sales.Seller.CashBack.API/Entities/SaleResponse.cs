using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class SaleResponse : BaseResponse
    {
        public SaleResponse()
        {

        }
        public SaleResponse(string id)
        {
            this.Sucess = true;
            this.Id = id;
        }
        public string Id { get; set; }
    }
}
