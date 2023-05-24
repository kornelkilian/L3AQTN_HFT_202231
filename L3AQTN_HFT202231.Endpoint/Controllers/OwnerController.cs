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
        IBusLogic buslogic;
        IHubContext<SignalRHub> hub;

        public OwnerController(IOwnerLogic logic, IHubContext<SignalRHub> hub, IBusLogic buslogic)
        {
            this.logic = logic;
            this.hub = hub;
            this.buslogic = buslogic;
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
            var cascadedellist = this.buslogic.ReadAll();
            foreach (var bus in cascadedellist)
            {
                if (bus.OwnerId == id)
                {
                    buslogic.Delete(bus.Id);
                    this.hub.Clients.All.SendAsync("BusDeleted", bus);

                }

            }
            this.hub.Clients.All.SendAsync("OwnerDeleted", ownerToDelete);
        }
    }
}
