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
        //[HttpGet]
        //public IEnumerable<Amount_D> Get()
        //{
        //    List<Amount_D> datalist = new List<Amount_D>();
        //    datalist = _factory;
        //    return List<Amount_D>;
        //}

        // GET api/<Amount_DController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Amount_DController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Amount_DController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Amount_DController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
