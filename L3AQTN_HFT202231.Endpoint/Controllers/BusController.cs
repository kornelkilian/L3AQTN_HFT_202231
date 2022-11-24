using L3AQTN_HFT_202231.Logic;
using L3AQTN_HFT_202231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace L3AQTN_HFT202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        IBusLogic logic;

        public BusController(IBusLogic logic)
        {
            this.logic = logic;
        }



        // GET: api/<BusController>
        [HttpGet]
        public IEnumerable<Bus> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<BusController>/5
        [HttpGet("{id}")]
        public Bus Read(int id)
        {
            return logic.Read(id);
        }

        // POST api/<BusController>
        [HttpPost]
        public void Create([FromBody] Bus value)
        {
            this.logic.Create(value);
        }

        // PUT api/<BusController>/5
        [HttpPut]
        public void Update([FromBody] Bus value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<BusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
