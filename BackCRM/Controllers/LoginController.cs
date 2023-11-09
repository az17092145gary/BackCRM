using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BackCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginFactory _loginFactory;
        public LoginController(IConfiguration configuratio)
        {
            _loginFactory = new LoginFactory(configuratio);
        }
        [HttpPost]
        public dynamic LoginCheckMember(Dictionary<string, string> account)
        {
            var result = _loginFactory.LoginCheckMember(account);
            return result != null ? Ok() : NotFound();
        }
    }
}
