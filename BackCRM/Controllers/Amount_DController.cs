using BackCRM.Model;
using BackCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Amount_DController : ControllerBase
    {
        private readonly Amount_DFactory _factory;
        private readonly IConfiguration _config;
        public Amount_DController(IConfiguration configuration)
        {
            _config = configuration;
            _factory = new Amount_DFactory(_config);
        }
        // GET: api/<Amount_DController>
        [HttpGet]
        public dynamic Get(string? user)
        {
            var data = getAmount_DView();
            if (user != null)
                if (user != "A000")
                    data = data.Where(data => data.empid == user);
            string result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }

        // GET api/<Amount_DController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            return Ok(_factory.getOne("SELECT * FROM AMOUNT_D where id = @id", id.ToString()));
        }

        [HttpPost("create")]
        public dynamic create(Amount_D amount, string? user)
        {
            if (amount.payment != "P" && user != "A000")
                return Ok(JsonConvert.SerializeObject(new ErrorModel()
                {
                    ErrorMsg = "您沒有權限!!",
                    Status = "000"
                }));
            amount.a_user = user;
            amount.a_sysdt = DateTime.Now;
            amount.u_user = user;
            amount.u_sysdt = DateTime.Now;
            var result = _factory.create(amount);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic edit(Amount_D amount, string? user)
        {
            if (amount.payment != "P" && user != "A000")
                return Ok(JsonConvert.SerializeObject(new ErrorModel()
                {
                    ErrorMsg = "您沒有權限!!",
                    Status = "000"
                }));
            amount.u_user = user;
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
        private IEnumerable<Amount_DView> getAmount_DView()
        {
            List<Amount_D> amount_Ds = _factory.getAll();
            List<EMPL> empls = new EMPLFactory(_config).getAll();
            var data = from ad in amount_Ds
                       join emp in empls on ad.empid equals emp.EMPID
                       select new Amount_DView
                       {
                          id = ad.id,
                          empid = ad.empid,
                          empnm = emp.EMPNAME,
                          amount = ad.amount,
                          car = ad.car,
                          memo = ad.memo,
                          payment = ad.payment,
                       };
            return data.ToList();
        }
    }
}
