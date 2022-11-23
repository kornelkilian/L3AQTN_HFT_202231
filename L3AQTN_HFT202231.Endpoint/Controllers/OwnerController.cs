﻿using L3AQTN_HFT_202231.Logic;
using L3AQTN_HFT_202231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L3AQTN_HFT202231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerLogic logic;

        public OwnerController(IOwnerLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Owner value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
