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
        //--------------------------------------------------------------------------------------------------
        // # GET ALL USERS                                
        //--------------------------------------------------------------------------------------------------
        // GET: api/<UserController>
        [HttpGet("Get All Users")]
        public List<UserProfile> Get()
        {

            return UserProfile.Read();
         
        }
        //--------------------------------------------------------------------------------------------------
        // # GET ACCESS FOT USER ACCOUNT                                
        //--------------------------------------------------------------------------------------------------

        // GET api/<UserController>/5
        [HttpGet("Get User/email/{email}/password/{password}")]
        public UserProfile GetUser(string email, string password)
        {

            UserProfile user = new UserProfile();
            user = user.GetAccess(email,password);
            if (user.email != null)
            {
                return user;
            }
            else

            {
                return null;
            }

        }
        //--------------------------------------------------------------------------------------------------
        // # INSERT USER                                
        //--------------------------------------------------------------------------------------------------
        // POST api/<UserController>
        [HttpPost("Insert User")]
        public bool Post([FromBody] UserProfile profile)
        {

           int tmp=UserProfile.Insert(profile);
            if (tmp!=null)
            {
                return true;
            }
            return false;

        }
        //--------------------------------------------------------------------------------------------------
        // # UPDATE USER                                
        //--------------------------------------------------------------------------------------------------
        // PUT api/<UserController>/5
        [HttpPut("Update User")]
        public bool Put( [FromBody] UserProfile user)
        {
                        
                int temp = UserProfile.UpdateUserProfile(user);
                if (temp != 0)
                {
                    return true;
                }
                else

                {
                    return false;
                }
      
        }
        //--------------------------------------------------------------------------------------------------
        // # DELETE USER                                
        //--------------------------------------------------------------------------------------------------
        // DELETE api/<UserController>/5
        [HttpDelete("Delete User/email/{email}/userId/{userId}")]
        public bool Delete(string email, int userId)
        {
           
            int temp = UserProfile.DeleteUserProfile(email,userId);
            if (temp > 0)
            {
                return true;
            }
            else

            {
                return false;
            }
 
        }
    }
}
