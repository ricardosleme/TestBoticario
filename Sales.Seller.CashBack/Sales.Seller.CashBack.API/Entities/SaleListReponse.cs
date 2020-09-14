using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class SaleListReponse
    {
        public string Id { get; set; }
        public int Codigo { get; set; }
        public double Valor { get; set; }
        public DateTime DataCompra { get; set; }
        public double CashBackPer { get; set; }
        public double CashBackValor { get; set; }
        public string Status { get; set; }
    }
}
