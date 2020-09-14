using Sales.Seller.CashBack.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Service
{
    public interface ILoginService
    {
        public LoginResponse GetLogin(LoginRequest loginRequest);
    }
}
