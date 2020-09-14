using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class SalePutRequest
    {
        public int? Codigo { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public double? Valor { get; set; }
        public DateTime? DataCompra { get; set; }
    }
}
