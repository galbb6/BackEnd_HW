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
        [HttpGet]
        public List<UserProfile> Get()
        {

            return UserProfile.Read();
         
        }
        //--------------------------------------------------------------------------------------------------
        // # GET ACCESS FOT USER ACCOUNT                                
        //--------------------------------------------------------------------------------------------------

        // GET api/<UserController>/5
        [HttpGet("email/{email}")]
        public UserProfile GetUser(string email)
        {

            UserProfile user = new UserProfile();
            user = user.GetAccess(email);
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
        [HttpPost]
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
        [HttpPut]
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
        [HttpDelete("email/{email}")]
        public bool Delete(string email)
        {
           
            int temp = UserProfile.DeleteUserProfile(email);
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
