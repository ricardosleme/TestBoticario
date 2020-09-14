using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class CashBackBoticarioResponse
    {
        public int statusCode { get; set; }
        public CashBackBoticarioBodyResponse body { get; set; }
    }
    public class CashBackBoticarioBodyResponse
    {
        public int credit { get; set; }
    }
}
