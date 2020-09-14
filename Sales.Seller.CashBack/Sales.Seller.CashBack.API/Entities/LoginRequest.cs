using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Entities
{
    public class LoginRequest
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
