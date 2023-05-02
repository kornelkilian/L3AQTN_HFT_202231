using L3AQTN_HFT_202231.Logic;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT202231.Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace L3AQTN_HFT202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        IBusLogic logic;
        IHubContext<SignalRHub> hub;
        public BusController(IBusLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("BusCreated",value);
        }

        // PUT api/<BusController>/5
        [HttpPut]
        public void Update([FromBody] Bus value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("BusUpdated", value);

        }

        // DELETE api/<BusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var busToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("BusDeleted",busToDelete);


        }
    }
}
