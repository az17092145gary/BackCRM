using BackCRM.Base;
using BackCRM.Model;
using BackCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            var data = getBudgetView();
            if (user != null)
                if (user != "A000")
                    data = data.Where(data => data.empid == user);
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
        public dynamic create(Budget budget, string? user)
        {
            if (user != "A000")
                return Ok(JsonConvert.SerializeObject(new ErrorModel()
                {
                    ErrorMsg = "您沒有權限!!",
                    Status = "000"
                }));
            var data = new EMPLFactory(_config).getAll().Where(x => x.EMPID == budget.empid);
            if (data.Count() == 0)
                return Ok(JsonConvert.SerializeObject(new ErrorModel()
                {
                    ErrorMsg = "沒有這個員工!!",
                    Status = "000"
                }));
            budget.a_user = user;
            budget.a_sysdt = DateTime.Now;
            budget.u_user = user;
            budget.u_sysdt = DateTime.Now;
            var result = _factory.create(budget);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic edit(Budget budget, string? user)
        {
            if (user != "A000")
                return Ok(JsonConvert.SerializeObject(new ErrorModel()
                {
                    ErrorMsg = "您沒有權限!!",
                    Status = "000"
                }));
            budget.u_sysdt = DateTime.Now;
            budget.u_user = user;
            var result = _factory.edit(budget);
            return result > 0 ? Ok() : NotFound();

        }

        [HttpGet("delete{id}")]
        public dynamic delete(string id)
        {
            var result = _factory.delete("DELETE BUDGET WHERE ID = @id", id);
            return result > 0 ? Ok() : NotFound();
        }
        private IEnumerable<BudgetView> getBudgetView()
        {
            List<Budget> budgets = _factory.getAll();
            List<EMPL> empls = new EMPLFactory(_config).getAll();
            var data = from bud in budgets
                       join emp in empls on bud.empid equals emp.EMPID
                       select new BudgetView
                       {
                           id = bud.id,
                           empname = emp.EMPNAME,
                           empid = bud.empid,
                           budget = bud.budget
                       };
            return data.ToList();
        }
    }
}
