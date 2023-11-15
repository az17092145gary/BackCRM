using BackCRM.Base;
using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetFactory _factory;
        private readonly IConfiguration _config;
        public BudgetController(IConfiguration configuration)
        {
            _config = configuration;
            _factory = new BudgetFactory(_config);
        }
        // GET: api/<BudgetController>
        [HttpGet]
        public dynamic Get(string? user)
        {
            List<Budget> budgets = _factory.getAll("SELECT * FROM BUDGET");
            List<EMPL> empls = new EMPLFactory(_config).getAll("SELECT * FROM EMPL");
            var data = from budget in budgets
                       join emp in empls on budget.empid equals emp.EMPID
                       select new
                       {
                           budget.empid,
                           emp.EMPNAME,
                           budget.budget
                       };
            string result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            return Ok(_factory.getOne("SELECT * FROM BUDGET where id = @id", id.ToString()));
        }

        [HttpPost("create")]
        public dynamic create(Budget budget,string? user)
        {
            budget.a_user = user;
            budget.a_sysdt = DateTime.Now;
            budget.u_user = user;
            budget.u_sysdt = DateTime.Now;
            var result = _factory.create(budget);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic edit(Budget budget,string user)
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
