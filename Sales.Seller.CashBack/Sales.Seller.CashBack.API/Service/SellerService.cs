using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.External;
using Sales.Seller.CashBack.API.Repository;
using Sales.Seller.CashBack.API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Service
{
    public class SellerService : ISellerService
    {

        private readonly ILogger<SellerService> _logger;
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly ICashBackBoticarioExternal  _cashBackBoticarioExternal;
        public SellerService(ILogger<SellerService> logger, ISalesRepository salesRepository, IMapper mapper, ICashBackBoticarioExternal cashBackBoticarioExternal)
        {
            _salesRepository = salesRepository;
            _cashBackBoticarioExternal = cashBackBoticarioExternal;
            _mapper = mapper;
            _logger = logger;
        }
  
        public CashBackResponse GetCashBack(string id)
        {
            _logger.LogInformation("GetCashBack: Start");
            try
            {
                _logger.LogInformation("GetCashBack: buscar Id");
                var vendor = _salesRepository.GetVendorId(id);
                _logger.LogInformation("GetCashBack: buscar External");
                var retorno = _cashBackBoticarioExternal.GetCashBack(vendor.CPF).Result;
                return new CashBackResponse(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetCashBack:error - {0}",ex);
                return new CashBackResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
  
            
        }

        public SellerResponse PostSeller(SellerRequest sellerRequest)
        {
            _logger.LogInformation("PostSeller: Start");
            var vendor = _mapper.Map<Vendor>(sellerRequest);
            vendor._id = ObjectId.GenerateNewId();
            _logger.LogInformation("PostSeller: Send DataBase");
            try
            {
                 if(_salesRepository.GetVendorCpf(vendor.CPF) != null)
                    return new SellerResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Vendedor já cadastrado." } } };
                _salesRepository.PostVendor(vendor);
                _logger.LogInformation("PostSeller: Return Database");
                return new SellerResponse(vendor._id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("PostSeller.Error - {0}", ex);
                return new SellerResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500  , Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
          
        }
    }
}
