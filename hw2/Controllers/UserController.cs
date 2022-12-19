using AirBnb_Part_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb_Part_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("Get All Users")]
        public List<User> Get()
        {

            return User.Read();
         
        }

        // GET api/<UserController>/5
        [HttpGet("Get User By {id}")]
        public IEnumerable<string> Get(int id)
        {
            return Ok();
        }

        // POST api/<UserController>
        [HttpPost("Insert User")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("Update User By {id}")]
        public void Put(int id, [FromBody] User user)
        {


        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete User By{id}")]
        public void Delete(int id)
        {
        }
    }
}
