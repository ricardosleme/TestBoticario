

namespace Sales.Seller.CashBack.API.Entities
{
    public class SellerResponse : BaseResponse
    {
        public SellerResponse() { 
        }
        public SellerResponse(string id) {
            this.Id = id;
            Sucess = true;
        }

        public string Id { get; set; }
    }
}
