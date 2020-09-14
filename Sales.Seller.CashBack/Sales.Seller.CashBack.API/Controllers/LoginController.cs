using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Service;
using System.Linq;

namespace Sales.Seller.CashBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
       

        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger , ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult PostLogin([FromBody] LoginRequest loginRequest)
        {
            var result =  _loginService.GetLogin(loginRequest);
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
