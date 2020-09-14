using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.External
{
    public interface ICashBackBoticarioExternal
    {
        public Task<double> GetCashBack(string CPF); 
    }
}
