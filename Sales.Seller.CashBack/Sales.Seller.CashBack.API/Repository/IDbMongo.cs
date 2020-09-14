using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Repository
{
    public interface IDbMongo
    {
        public IMongoDatabase GetDatabase();
    }
}
