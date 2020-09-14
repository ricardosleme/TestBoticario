using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sales.Seller.CashBack.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.External
{
    public class CashBackBoticarioExternal : ICashBackBoticarioExternal
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CashBackBoticarioExternal(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        public async Task<double> GetCashBack(string CPF)
        {
            string url = string.Format("?cpf={0}", CPF);

            _httpClient.DefaultRequestHeaders.Add("token", _configuration["CashBackBoticario:Token"]);
            using (var response = await _httpClient.GetAsync(_configuration["CashBackBoticario:Url"] + url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseUrl = JsonConvert.DeserializeObject<CashBackBoticarioResponse>(apiResponse);
                return responseUrl.body.credit;

            }
        }
    }
}
