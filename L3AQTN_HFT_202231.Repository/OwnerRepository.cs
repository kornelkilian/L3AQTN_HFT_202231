using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Repository
{

    public class OwnerRepository : Repository<Owner>, IRepository<Owner>
    {
        private readonly BusDbContext context;

        public OwnerRepository(BusDbContext context) : base(context)
        {
            this.context = context;
        }

        public override Owner Read(int i)
        {
            return context.Owners.FirstOrDefault(o => o.Id == i);
        }

        public override void Update(Owner owner)
        {
            var sourceItem = Read(owner.Id);
            foreach (var prop in sourceItem.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(sourceItem, prop.GetValue(owner));
                }

            }

            context.SaveChanges();

        }


    }


}

