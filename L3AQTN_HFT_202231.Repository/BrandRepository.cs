using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Repository
{
    
        public class BrandRepository : Repository<Brand>, IRepository<Brand>
        {
            private readonly BusDbContext context;

            public BrandRepository(BusDbContext context) : base(context)
            {
                this.context = context;
            }

            public override IEnumerable<Brand> ReadAll()
            {
                {
                    var returnValues = context.Set<Brand>();

                    return returnValues;
                }
            }
            public override bool Update(Brand brand)
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
