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
    public class OwnerController : ControllerBase
    {
        IOwnerLogic logic;
        IHubContext<SignalRHub> hub;

        public OwnerController(IOwnerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: api/<OwnerController>
        [HttpGet]
        public IEnumerable<Owner> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public Owner Read(int id)
        {
            return logic.Read(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        public void Create([FromBody] Owner value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("OwnerCreated", value);
        }

        // PUT api/<OwnerController>/5
        [HttpPut]
        public void Update([FromBody] Owner value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OwnerUpdated", value);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var ownerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("OwnerDeleted", ownerToDelete);
        }
    }
}
