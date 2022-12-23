using AirBnb_Part_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb_Part_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("Get All Users")]
        public List<UserProfile> Get()
        {

            return UserProfile.Read();
         
        }

        // GET api/<UserController>/5
        [HttpGet("Get User/email/{email}/password/{password}")]
        public bool GetUser(string email, string password)
        {

            UserProfile user = new UserProfile();
            int temp = user.GetAccess(email,password);
            if (temp > 0)
            {
                return true;
            }
            else

            {
                return false;
            }

        }

        // POST api/<UserController>
        [HttpPost("Insert User")]
        public int Post([FromBody] UserProfile profile)
        {
           return UserProfile.Insert(profile);

        }

        // PUT api/<UserController>/5
        [HttpPut("Update User By {id}")]
        public IActionResult Put(int id, [FromBody] UserProfile user)
        {
                        
               user.UserId= id;
                int temp = UserProfile.UpdateUserProfile(user);
                if (temp != 0)
                {
                    return Ok();
                }
                else

                {
                    return NotFound("id " + id.ToString() + " was not update");
                }
      
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete User/email/{email}")]
        public IActionResult Delete(string email)
        {
           
            int temp = UserProfile.DeleteUserProfile(email);
            if (temp > 0)
            {
                return Ok();
            }
            else

            {
                return NotFound("user with this email :  " + email.ToString() + " , was not found");
            }



        }
    }
}
