using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackCRM.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class EMPLController : ControllerBase
    {
        private EMPLFactory _memberService;

        public EMPLController(IConfiguration configuratio)
        {
            _memberService = new EMPLFactory(configuratio);
        }
        [HttpGet("getAllEMPL")]
        public dynamic getAllEMPL()
        {
            var data = _memberService.getAll("SELECT * FROM EMPL");
            var result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }
        [HttpGet("getEMPL")]
        public dynamic getEMPL(string EMPID)
        {
            if (EMPID == "A000")
            {
                var data = _memberService.getAll("select * from empl");
                var result = JsonConvert.SerializeObject(data);
                return Ok(result);
            }
            else
            {
                var data = _memberService.getOne("select * from empl where EMPID = @EMPID", EMPID);
                var list = new List<EMPL>
            {
                data
            };
                var result = JsonConvert.SerializeObject(list);
                return Ok(result);
            }

        }
        [HttpPost("createEMPL")]
        public dynamic createEMPL(EMPL empl)
        {
            var result = _memberService.create(empl);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("editEMPL")]
        public dynamic editEMPL(EMPL empl)
        {
            var result = _memberService.edit(empl);
            return result > 0 ? Ok() : NotFound();

        }
        [HttpGet("deleteEMPL")]
        public dynamic deleteEMPL(string id)
        {
            var result = _memberService.delete("DELETE EMPL WHERE ID = @id", id);
            return result > 0 ? Ok() : NotFound();
        }
    }
}
