using Sales.Seller.CashBack.API.Repository.Data;
namespace Sales.Seller.CashBack.API.Repository
{
    public interface ISalesRepository
    {
        public Vendor GetVendorCpf(string cpf);
        public Vendor GetVendorSenha(string cpf, string senha);
        public Vendor GetVendorId(string id);
        public Vendor PostVendor(Vendor vendor);
        public Vendor PutVendor(Vendor vendor);
    }
}
