using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackCRM.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class MemberController : ControllerBase
    {
        private MemberFactory _memberService;

        public MemberController()
        {
            _memberService = new MemberFactory();
        }
        [HttpGet("getAllMember")]
        public dynamic getAllMember()
        {
            var data = _memberService.getAllMember();
            var result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }
        [HttpGet("getMember{id}")]
        public dynamic getMember(string id)
        {
            var data = _memberService.getMember(id);
            var result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }
        [HttpPost("createMember")]
        public dynamic createMember(Member member)
        {

            var result = _memberService.createMember(member);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("editMember")]
        public dynamic editMember(Dictionary<string, string> member)
        {
            var result = _memberService.editMember(member);
            return result > 0 ? Ok() : NotFound();

        }
        [HttpGet("deleteMember{id}")]
        public dynamic deleteMember(string id)
        {
            var result = _memberService.deleteMember(id);
            return result > 0 ? Ok() : NotFound();
        }
    }
}
