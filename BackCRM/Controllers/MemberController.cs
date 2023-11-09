using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace BackCRM.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class MemberController : ControllerBase
    {
        private MemberService _memberService;

        public MemberController(MemberService service)
        {
            _memberService = service;
        }
        [HttpGet("getAllMember")]
        public dynamic getAllMember()
        {
            using (SqlConnection connection = new SqlConnection(_memberService.connectionString))
            {
                var data = _memberService.getAllMember(connection);
                var result = JsonConvert.SerializeObject(data);
                return Ok(result);
            }
        }
        [HttpGet("getMember{id}")]
        public dynamic getMember(string id)
        {
            using (SqlConnection connection = new SqlConnection(_memberService.connectionString))
            {
                var data = _memberService.getMember(id, connection);
                var result = JsonConvert.SerializeObject(data);
                return Ok(result);
            }
        }
        [HttpPost("createMember")]
        public dynamic createMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(_memberService.connectionString))
            {
                _memberService.createMember(member, connection);
                return Ok();
            }

        }
        [HttpPost("editMember")]
        public dynamic editMember(Dictionary<string,string> member)
        {
            using (SqlConnection connection = new SqlConnection(_memberService.connectionString))
            {
                _memberService.editMember(member, connection);
                return Ok();
            }
        }
        [HttpGet("deleteMember{id}")]
        public dynamic deleteMember(string id)
        {
            using (SqlConnection connection = new SqlConnection(_memberService.connectionString))
            {
                _memberService.deleteMember(id, connection);
                return Ok();
            }
        }
    }
}
