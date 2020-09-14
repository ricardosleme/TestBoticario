using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Service;
using System.Linq;

namespace Sales.Seller.CashBack.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
       

        private readonly ILogger<SalesController> _logger;
        private readonly ISaleService _saleService;

        public SalesController(ILogger<SalesController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult PostSale([FromBody] SalePostRequest request)
        {
            var id = HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
            var result =  _saleService.PostSales(id, request);
            if (result.Sucess)
                return new ObjectResult(result);
            else
            {
                var resultError = new ObjectResult(result);
                resultError.StatusCode = result.Error.FirstOrDefault().Code;
                return resultError;
            }
        }
        [HttpPut]
        [Route("{code}")]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult PutSale( string code , [FromBody] SalePutRequest request)
        {
            var id = HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
            var result =  _saleService.PutSales(id, code, request);
            if (result.Sucess)
                return new ObjectResult(result);
            else
            {
                var resultError = new ObjectResult(result);
                resultError.StatusCode = result.Error.FirstOrDefault().Code;
                return resultError;
            }
        }
        [HttpDelete]
        [Route("{code}")]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSale(string code)
        {
            var id = HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
            var result = _saleService.DeleteSales(id, code);
            if (result.Sucess)
                return new ObjectResult(result);
            else
            {
                var resultError = new ObjectResult(result);
                resultError.StatusCode = result.Error.FirstOrDefault().Code;
                return resultError;
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(SaleGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SaleGetResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaleGetResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetSale()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
            var result =  _saleService.GetSales(id);
            if (result.Sucess)
                return new ObjectResult(result);
            else
            {
                var resultError = new ObjectResult(result);
                resultError.StatusCode = result.Error.FirstOrDefault().Code;
                return resultError;
            }
        }
    }
}
