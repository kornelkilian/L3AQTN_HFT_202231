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
    public class BrandController : ControllerBase
    {
        IBrandLogic logic;
        IBusLogic buslogic;

        IHubContext<SignalRHub> hub;
        public BrandController(IBrandLogic logic, IHubContext<SignalRHub> hub, IBusLogic busLogi)
        {
            this.logic = logic;
            this.hub = hub;
            this.buslogic = busLogi;
        }



        // GET: api/<BrandController>
        [HttpGet]
        public IEnumerable<Brand> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public Brand Read(int id)
        {
            return logic.Read(id);
        }

        // POST api/<BrandController>
        [HttpPost]
        public void Create([FromBody] Brand value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("BrandCreated", value);
        }

        // PUT api/<BrandController>/5
        [HttpPut]
        public void Update([FromBody] Brand value)
        {
            this.logic.Update(value);

            this.hub.Clients.All.SendAsync("BrandUpdated", value);

        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            var cascadedellist = this.buslogic.ReadAll();
            foreach (var bus in cascadedellist)
            {
                if (bus.BrandId==id)
                {
                    buslogic.Delete(bus.Id);
                    this.hub.Clients.All.SendAsync("BusDeleted", bus);

                }

            }
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }
    }
}
