using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Service;

namespace Sales.Seller.CashBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellerController : ControllerBase
    {
       

        private readonly ILogger<SellerController> _logger;
        private readonly ISellerService _sellerService;

        public SellerController(ILogger<SellerController> logger, ISellerService sellerService)
        {
            _logger = logger;
            _sellerService = sellerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] SellerRequest request)
        {
            var result = _sellerService.PostSeller(request);
            if (result.Sucess)
                return new ObjectResult(result);
            else
            {
                var resultError = new ObjectResult(result);
                resultError.StatusCode = result.Error.FirstOrDefault().Code;
                return resultError;
            }
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CashBackResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CashBackResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CashBackResponse), StatusCodes.Status500InternalServerError)]
        [Route("CashBack")]
        public IActionResult GetCashBack()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
            var result = _sellerService.GetCashBack(id);
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
