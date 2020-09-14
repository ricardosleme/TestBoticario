using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Repository;
using Sales.Seller.CashBack.API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Seller.CashBack.API.Service
{
    public class SaleService : ISaleService
    {
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesRepository;
        public SaleService(ILogger<SaleService> logger, IMapper mapper, ISalesRepository salesRepository)
        {
            _mapper = mapper;
            _salesRepository = salesRepository;
            _logger = logger;
        }
        public SaleResponse DeleteSales(string id, string idSales)
        {
            _logger.LogInformation("DeleteSales: Start");
            try
            {
                _logger.LogInformation("DeleteSales: buscar Id");
                var vendor = _salesRepository.GetVendorId(id);
                _logger.LogInformation("DeleteSales: Verifica Existencia de vendas");
                if (vendor.Sales == null) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Venda não encontrada." } } };
                _logger.LogInformation("DeleteSales: Verifica Existencia de vendas do Id informado");
                var sale = vendor.Sales.Where(x => x._id.ToString().Equals(idSales)).FirstOrDefault();
                if (sale == null) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Venda não encontrada." } } };
                _logger.LogInformation("DeleteSales: Validação Status");
                if (sale.Status.Equals("Aprovado")) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Não pode ser deletado." } } };
                vendor.Sales.Remove(sale);
               _logger.LogInformation("DeleteSales: Gravar venda");
                _salesRepository.PutVendor(vendor);
                return new SaleResponse(sale._id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("DeleteSales:error - {0}", ex);
                return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
        }

        public SaleGetResponse GetSales(string id)
        {
            _logger.LogInformation("GetSales: Start");
            try
            {
                _logger.LogInformation("GetSales: buscar Id");
                var vendor = _salesRepository.GetVendorId(id);
                var Sales = _mapper.Map<List<SaleListReponse>>(vendor.Sales);
                _logger.LogInformation("GetSales: Verifica Existencia de vendas");
                return new SaleGetResponse(Sales);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("DeleteSales:error - {0}", ex);
                return new SaleGetResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
        }

        public SaleResponse PostSales(string id, SalePostRequest salePostRequest)
        {
            _logger.LogInformation("PostSales: Start");
            try
            {
                _logger.LogInformation("PostSales: buscar Id");
                var vendor = _salesRepository.GetVendorId(id);
                
                if (vendor.Sales == null) vendor.Sales = new List<Sale>();
                var sale = _mapper.Map<Sale>(salePostRequest);
                _logger.LogInformation("PostSales: Efetua Calculo de Cashback");
                var cashback = this.CashBackCalc(sale.Valor);
                sale.CashBackPer = cashback.Item1;
                sale.CashBackValor = cashback.Item2;
                sale._id = ObjectId.GenerateNewId();
                sale.Status = this.getStatus(vendor.CPF);
                vendor.Sales.Add(sale);
                _logger.LogInformation("PostSales: Grava Venda");
                _salesRepository.PutVendor(vendor);
                return new SaleResponse(sale._id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("PostSales:error - {0}", ex);
                return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
        }

        public SaleResponse PutSales(string id, string idSales, SalePutRequest salePutRequest)
        {
            _logger.LogInformation("PutSales: Start");
            try
            {
                _logger.LogInformation("PutSales: buscar Id");
                var vendor = _salesRepository.GetVendorId(id);
                _logger.LogInformation("PutSales: Verifica Existencia de vendas");
                if (vendor.Sales == null) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Venda não encontrada." } } };
                _logger.LogInformation("PutSales: Verifica Existencia de vendas do Id informado");
                var sale = vendor.Sales.Where(x => x._id.ToString().Equals(idSales)).FirstOrDefault();
                if (sale == null) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Venda não encontrada." } } };
                _logger.LogInformation("PutSales: Validação Status");
                if (sale.Status.Equals("Aprovado")) return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 400, Descricao = "Não pode ser editado." } } };
                if (salePutRequest.DataCompra != null) sale.Data = salePutRequest.DataCompra.Value;
                if (salePutRequest.Codigo != null) sale.Codigo = salePutRequest.Codigo.Value;
                if (salePutRequest.Valor != null) {
                    sale.Valor = salePutRequest.Valor.Value;
                    _logger.LogInformation("PutSales: Executa calculos de Cashback");
                    var cashback = this.CashBackCalc(sale.Valor);
                    sale.CashBackPer = cashback.Item1;
                    sale.CashBackValor = cashback.Item2;
                }
                _logger.LogInformation("PutSales: Gravar venda");
                _salesRepository.PutVendor(vendor);
                return new SaleResponse(sale._id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("PutSales:error - {0}", ex);
                return new SaleResponse() { Sucess = false, Error = new List<Error>() { new Error() { Code = 500, Descricao = "Ocorreu um erro interno favor tentar novamente mais tarde" } } };
            }
        }
        public Tuple<double, double> CashBackCalc(double valor)
        {
            double percentualRetorno = 0;
            double valorRetorno = 0;
            if (valor <= 1000)
            {
                percentualRetorno = 10;
                valorRetorno = valor * 0.10;
            }
            else if (valor <= 1500)
            {
                percentualRetorno = 15;
                valorRetorno = valor * 0.15;
            }
            else
            {
                percentualRetorno = 20;
                valorRetorno = valor * 0.20;
            }
            return Tuple.Create(percentualRetorno, valorRetorno);
        }
        private string getStatus(string cpf)
        {
            if( cpf.Equals("15350946056"))
                return "Aprovado";
            return "Em validação";

        }
    }
}
