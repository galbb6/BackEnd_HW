using Microsoft.AspNetCore.Mvc;
using AirBnb_Part_2.Models;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb_Part_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------
        // # GET FLATS                                
        //--------------------------------------------------------------------------------------------------
        // GET: api/<FlatController>
        [HttpGet]
        public List<Flat> Get()
        {
            return Flat.Read();
        }


        //--------------------------------------------------------------------------------------------------
        // # GET TRUE IF WHERE FLAT WITH THIS ID                               
        //--------------------------------------------------------------------------------------------------
        // GET api/<FlatController>/5
        [HttpGet("{id}")]
        public bool Get(int id)
        {
             List< Flat > F = Flat.Read();
            foreach (var item in F)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        //--------------------------------------------------------------------------------------------------
        // # GET ALL FLATS WHERE CITY == CITY AND PRICE <= PRICE                                    
        //--------------------------------------------------------------------------------------------------
        [HttpGet("city/price")]
        public  List<Flat> GetByCityAndPrice(string city, double price)
        {
            List<Flat> FList =Flat.getCityPrice(city,price);

                if (FList.Count()>0)
                {
                    return FList;
                }
            
            return null;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT FLAT                              
        //--------------------------------------------------------------------------------------------------

        // POST api/<FlatController>
        [HttpPost]
        public IActionResult Post([FromBody] Flat flat)
        {
            int temp = flat.InsertFlat(flat);
            if (temp > 0)
            {
                return Ok();
            }
            else

            {
                return NotFound("Insert faild");
            }

        }
        //--------------------------------------------------------------------------------------------------
        // # UPDATE FLAT                       
        //--------------------------------------------------------------------------------------------------

        // PUT api/<FlatController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Flat flat)
        {
            flat.Id = id;
            int temp = flat.UpdateFlat(flat);
            if (temp > 0)
            {
                return Ok();
            }
            else

            {
                return NotFound("id " + id.ToString() + " was not update");
            }

        }

        //--------------------------------------------------------------------------------------------------
        // # DELETE FLAT                            
        //--------------------------------------------------------------------------------------------------

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Flat f = new Flat();
            int temp = f.DeleteFlat(id);
            if (temp > 0)
            {
                return Ok();
            }
            else

            {
                return NotFound("id "+id.ToString()+" was not found");
            }
        }



    }
}
