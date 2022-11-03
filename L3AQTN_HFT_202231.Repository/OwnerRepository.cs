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

        public override IEnumerable<Owner> ReadAll()
        {
            {
                var returnValues = context.Set<Owner>();

                return returnValues;
            }
        }
        public override bool Update(Owner brand)
        {
            var sourceItem = Read(brand.Id);
            if (sourceItem == null)
            {
                return false;
            }

            //context.Remove(sourceItem);
            //context.Add(car);

            sourceItem.CopyFrom(brand);
            context.SaveChanges();

            return true;
        }


    }


}

