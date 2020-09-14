using Microsoft.Extensions.Hosting;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class CashBackResponse :BaseResponse
    {
        public CashBackResponse() { 
        }
        public CashBackResponse(double credit)
        {
            this.Sucess = true;
            this.Credit = credit;
        }
        public double Credit { get; set; }
    }
}
