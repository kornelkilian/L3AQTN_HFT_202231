using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public   interface IBusLogic
    {
        List<Bus> BusesByZIPCode(int zip);
        List<Bus> BusesWithMustacheOwners();
        void Create(Bus item);
        void Delete(int id);
        double? GetAvaragePriceByBrandCountry(string country);
        double? GetAvaragePriceByOwner(string ownername);
        IEnumerable<OwnerBrandInfo> GetBusCountByMustache();
        IEnumerable<OwnerBrandInfo> GetBusCountByOwner();
        double? HighestPriceByBrand(string brandname);
        Bus Read(int id);
        IEnumerable<Bus> ReadAll();
        void Update(Bus item);



    }
}
