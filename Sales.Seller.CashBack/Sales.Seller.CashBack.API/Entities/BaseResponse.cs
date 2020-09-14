using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class BaseResponse
    {
        public bool Sucess { get; set; }
        public List<Error> Error { get; set; }
    }
}
