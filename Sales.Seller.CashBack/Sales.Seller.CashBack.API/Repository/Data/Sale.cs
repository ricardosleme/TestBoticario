using System;
using MongoDB.Bson;

namespace Sales.Seller.CashBack.API.Repository.Data
{
    public class Sale
    {
        public ObjectId _id { get; set; }
        public int Codigo { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public double CashBackPer { get; set; }
        public double CashBackValor { get; set; }
        public string Status { get; set; }
    }
}
