using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public override Bus Read(int i)
        {
            return context.Buses.FirstOrDefault(t => t.Id == i);
        }

        public override void Update(Bus bus)
        {
            var sourceItem = Read(bus.Id);
            foreach (var prop in sourceItem.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(sourceItem, prop.GetValue(bus));
                }

               
            }

            context.SaveChanges();

        }
    }
}
