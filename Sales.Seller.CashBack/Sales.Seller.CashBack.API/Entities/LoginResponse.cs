using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
