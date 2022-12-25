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
        // # GET flat by ID IF NOT FOUND RETURN NULL                              
        //--------------------------------------------------------------------------------------------------
        // GET api/<FlatController>/5
        [HttpGet("id/{id}")]
        public Flat Get(int id)
        {
             List< Flat > F = Flat.Read();
            foreach (var item in F)
            {
                if (item.FlatId == id)
                {
                    return item;
                }
            }
            return null;
        }

        //--------------------------------------------------------------------------------------------------
        // # GET ALL FLATS WHERE CITY == CITY AND PRICE <= PRICE                                    
        //--------------------------------------------------------------------------------------------------
        [HttpGet("city/{city}/price/{price}")]
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
        public bool Post([FromBody] Flat flat)
        {
            int temp = flat.InsertFlat(flat);
            if (temp > 0)
            {
                return true;
            }
            else

            {
                return false;
            }

        }
        //--------------------------------------------------------------------------------------------------
        // # UPDATE FLAT                       
        //--------------------------------------------------------------------------------------------------

        // PUT api/<FlatController>/5
        [HttpPut]
        public bool Put([FromBody] Flat flat)
        {
            int temp = flat.UpdateFlat(flat);
            if (temp > 0)
            {
                return true;
            }
            else

            {
                return false;
            }

        }

        //--------------------------------------------------------------------------------------------------
        // # DELETE FLAT                            
        //--------------------------------------------------------------------------------------------------

        [HttpDelete("{flatId}")]
        public bool Delete(int flatId)
        {
            
            Flat f = new Flat();
            int temp = f.DeleteFlat(flatId);
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
