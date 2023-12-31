﻿using BackCRM.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteFactory _factory;
        public NoteController(IConfiguration configuration)
        {
            _factory = new NoteFactory(configuration);
        }
        // GET: api/<NoteController>
        [HttpGet]
        public dynamic Get(string user)
        {
            string sql = @"SELECT EMPL.EMPNAME, NOTE.* FROM NOTE LEFT JOIN EMPL ON NOTE.EMPLID = EMPL.EMPID";
            if (user != "A000")
                sql += " WHERE EMPLID = '" + user + "'";
            return Ok(_factory.getAll(sql));
        }

        // GET api/<NoteController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            return Ok(_factory.getOne("SELECT * FROM NOTE where id = @id", id.ToString()));
        }

        [HttpPost("create")]
        public dynamic createMember(Note note, string user)
        {
            note.a_user = user;
            note.a_sysdt = DateTime.Now;
            note.u_user = user;
            note.u_sysdt = DateTime.Now;
            var result = _factory.create(note);
            return result > 0 ? Ok() : NotFound();
        }
        [HttpPost("edit")]
        public dynamic editMember(Note note, string user)
        {
            note.u_user = user;
            note.u_sysdt = DateTime.Now;
            var result = _factory.edit(note);
            return result > 0 ? Ok() : NotFound();

        }

        [HttpGet("delete")]
        public dynamic delete(string id)
        {
            var result = _factory.delete("DELETE NOTE WHERE ID = @id", id);
            return result > 0 ? Ok() : NotFound();
        }
    }
}
