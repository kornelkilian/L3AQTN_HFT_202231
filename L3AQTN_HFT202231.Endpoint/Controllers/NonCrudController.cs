using L3AQTN_HFT_202231.Logic;
using L3AQTN_HFT_202231.Models;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;


namespace L3AQTN_HFT202231.Endpoint.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IBusLogic logic;

        public StatController(IBusLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{brandname}")]
        public double? HighestPriceByBrand(string brandname)
        {
            return this.logic.HighestPriceByBrand(brandname);
        }

        [HttpGet("{country}")]
        public double? GetAvarageByCountry(string country)
        {
            return this.logic.GetAvaragePriceByBrandCountry(country);
        }

        [HttpGet("{zip}")]
        public List<Bus> BusesByZIPCode(int zip)
        {
            return this.logic.BusesByZIPCode(zip);
        }

        [HttpGet("{name}")]
        public double? AvaragePriceByOwner(string name)
        {
            return this.logic.GetAvaragePriceByOwner(name);
        }

        [HttpGet]

        public List<Bus> BusesWithMustacheOwners()
        {
            return this.logic.BusesWithMustacheOwners();


        }




    }
}




