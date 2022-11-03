using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using Microsoft.EntityFrameworkCore;

namespace L3AQTN_HFT_202231.Repository
{
    public class BusRepository : Repository<Bus>, IRepository<Bus>
    {



        private readonly BusDbContext context;

        public BusRepository(BusDbContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<Bus> ReadAll()
        {
            {
                var returnValues = context.Set<Bus>().Include(_ => _.Brand);

                return returnValues;
            }
        }

        public override bool Update(Bus bus)
        {
            var sourceItem = Read(bus.Id);
            if (sourceItem == null)
            {
                return false;
            }

            //context.Remove(sourceItem);
            //context.Add(car);

            sourceItem.CopyFrom(bus);
            context.SaveChanges();

            return true;

        }
    }
}
