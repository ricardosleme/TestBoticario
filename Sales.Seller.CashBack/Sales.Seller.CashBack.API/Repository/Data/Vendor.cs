using MongoDB.Bson;
using System.Collections.Generic;

namespace Sales.Seller.CashBack.API.Repository.Data
{
    public class Vendor
    {
        public ObjectId _id { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
