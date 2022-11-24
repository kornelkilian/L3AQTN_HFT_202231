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

        public override Brand Read(int i)
        {
            return context.Brands.FirstOrDefault(b => b.Id == i);
        }

        public override void Update(Brand  brand)
        {
            var sourceItem = Read(brand.Id);
            foreach (var prop in sourceItem.GetType().GetProperties())
            {

                if (prop.GetAccessors().FirstOrDefault(t=>t.IsVirtual)==null)
                {
                    prop.SetValue(sourceItem, prop.GetValue(brand));
                }
       
            }

            context.SaveChanges();

        }


    }

    
}
