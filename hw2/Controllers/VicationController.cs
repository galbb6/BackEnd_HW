using AirBnb_Part_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirBnb_Part_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VicationController : ControllerBase
    {
        // GET: api/<VixationController>
        [HttpGet]
        public List<Vication> Get()
        {
            return Vication.Read();
        }

        // GET api/<VixationController>/5
        [HttpGet("{id}")]
        public bool Get(int id)
        {
            List<Vication> V = Vication.Read();
            foreach (var item in V)
            {
                if (item.Id == id)
                {
                    return true;
                }

            }
            return false;
        }

        [HttpGet("getByDates/startDate/{startDate}/endDate/{endDate}")]
        public List<Vication> getByDates(DateTime startDate, DateTime endDate)
        {
            List<Vication> VList = Vication.getByDatesOrders(startDate, endDate);
            
                if (VList.Count>=0)
                {

                    return VList;
                }

            
            return null;
        }
        // POST api/<VixationController>
        [HttpPost]
        public bool Post([FromBody] Vication V)
        {
            return V.Insert();
        }

        // PUT api/<VixationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VixationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
       






    }
}
