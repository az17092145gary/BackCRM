using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetFactory _factory;
        public BudgetController(IConfiguration configuration)
        {
            _factory = new BudgetFactory(configuration);
        }
        // GET: api/<BudgetController>
        [HttpGet]
        public dynamic Get(string? user)
        {
            return Ok(_factory.getAll("SELECT * FROM BUDGET"));
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            return Ok(_factory.getOne("SELECT * FROM BUDGET where id = @id", id.ToString()));
        }

        [HttpPost("create")]
        public dynamic createMember(Budget budget,string user)
        {
            var result = _factory.create(budget);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic editMember(Budget budget,string user)
        {
            budget.u_sysdt = DateTime.Now;
            var result = _factory.edit(budget);
            return result > 0 ? Ok() : NotFound();

        }

        [HttpGet("delete{id}")]
        public dynamic delete(string id)
        {
            var result = _factory.delete("DELETE BUDGET WHERE ID = @id", id);
            return result > 0 ? Ok() : NotFound();
        }
    }
}
