using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;

namespace L3AQTN_HFT_202231.Logic
{
    public class BusLogic : IBusLogic
    {
        private readonly IRepository<Bus> busRepo;
        private readonly IRepository<Brand> brandRepo;

        public BusLogic(IRepository<Bus> busRepo, IRepository<Brand> brandRepo)
        {
            this.busRepo = busRepo;
            this.brandRepo = brandRepo;
        }

        public IEnumerable<Bus> GetAllCars()
        {
            return busRepo.ReadAll();
        }
        public IEnumerable<AveragePriceByBrand> GetAverageByBrand()
        {
            var result =
                GetAllCars().GroupBy(_ => _.BrandId)
                    .Select(group => new AveragePriceByBrand()
                    {

                        BrandId = group.Key,
                        AveragePrice = group.Average(_ => _.Price).Value
                    });

            return result;
        }

        public void Update(Bus newItem, int id)
        {
            newItem.Id = id;
            busRepo.Update(newItem);
        }

        public void Update2(Bus newItem, int id)
        {
            var item = busRepo.Read(id);

            item.CopyFrom(newItem);

            busRepo.Update(item);
        }
    }
}
