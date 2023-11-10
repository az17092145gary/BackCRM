using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Amount_DController : ControllerBase
    {
        private readonly Amount_DFactory _factory;
        public Amount_DController(IConfiguration configuration)
        {
            _factory = new Amount_DFactory(configuration);
        }
        // GET: api/<Amount_DController>
        [HttpGet]
        public dynamic Get()
        {
            return Ok(_factory.getAll("SELECT * FROM AMOUNT_D"));
        }

        // GET api/<Amount_DController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            return Ok(_factory.getOne("SELECT * FROM AMOUNT_D where id = @id", id.ToString()));
        }

        [HttpPost("create")]
        public dynamic createMember(Amount_D amount, string user)
        {
            var result = _factory.create(amount);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic editMember(Amount_D amount, string user)
        {
            amount.u_sysdt = DateTime.Now;
            var result = _factory.edit(amount);
            return result > 0 ? Ok() : NotFound();

        }

        [HttpGet("delete{id}")]
        public dynamic delete(string id)
        {
            var result = _factory.delete("DELETE AMOUNT_D WHERE ID = @id", id);
            return result > 0 ? Ok() : NotFound();
        }
    }
}
