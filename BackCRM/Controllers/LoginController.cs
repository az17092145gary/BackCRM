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
            var EMPID = account["EMPID"];
            var EMPPWD = account["EMPPWD"];
            var result = _loginFactory.LoginEMPLCheck("select * from EMPL where EMPID = @EMPID and EMPPWD = @EMPPWD", EMPID, EMPPWD);
            if (result != null)
            {
                return Ok(result.id);
            }
            return NotFound();
        }
    }
}
