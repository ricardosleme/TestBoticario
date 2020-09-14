using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Sales.Seller.CashBack.API.Repository
{
    public class DbMongo : IDbMongo
    {
        private IMongoDatabase _MongoDatabase;
        private IConfiguration _configuration;
        public DbMongo(IConfiguration configuration) {
            _configuration = configuration;
            MongoClient client = new MongoClient(_configuration.GetConnectionString("Conexao"));
            _MongoDatabase = client.GetDatabase("Sales");
        }

        public IMongoDatabase GetDatabase()
        {
            return _MongoDatabase;
        }
    }
}
