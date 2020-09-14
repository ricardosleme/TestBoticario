using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Sales.Seller.CashBack.API.Repository.Data;
using System.Security.Policy;

namespace Sales.Seller.CashBack.API.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly IDbMongo _dbMongo;
        public SalesRepository(IDbMongo dbMongo)
        {
            _dbMongo = dbMongo;
        }
        public bool DeleteSale(Vendor vendor)
        {
            throw new System.NotImplementedException();
        }
        public Vendor GetVendorCpf(string cpf)
        {
            var database = _dbMongo.GetDatabase();
            var collection = database.GetCollection<Vendor>("Vendor");
            return collection.Find(x => x.CPF.Equals(cpf)).FirstOrDefault();
        }
        public Vendor GetVendorSenha(string cpf, string senha)
        {
            var database = _dbMongo.GetDatabase();
            var collection = database.GetCollection<Vendor>("Vendor");
            return collection.Find(x => x.Senha.Equals(senha) && x.CPF.Equals(cpf)).FirstOrDefault();
        }
        public Vendor GetVendorId(string id)
        {
            var database = _dbMongo.GetDatabase();
            var _id = new ObjectId(id);
            var collection = database.GetCollection<Vendor>("Vendor");
            return collection.Find(x => x._id.Equals(_id)).FirstOrDefault() ;
        }
        public Vendor PostVendor(Vendor vendor)
        {
            var database = _dbMongo.GetDatabase();
            var collection = database.GetCollection<Vendor>("Vendor");
            collection.InsertOne(vendor);
            return vendor;
        }

        public Vendor PutVendor(Vendor vendor)
        {
            var database = _dbMongo.GetDatabase();
            var collection = database.GetCollection<Vendor>("Vendor");
            collection.ReplaceOne(x => x._id.Equals(vendor._id), vendor);
            return vendor;
        }

    }
}
