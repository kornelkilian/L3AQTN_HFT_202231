using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Repository;
using System;
using System.Linq;

namespace L3AQTN_HFT_202231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            BusDbContext context = new BusDbContext();
            var buses = context.Buses.AsQueryable();
            ;

            var b = context.Buses.Where(t => t.Brand.Name.Equals("Citrom"));


            IRepository<Bus> repo = new BusRepository(new BusDbContext());

            Bus c = new Bus()
            {
                Model = "BMW"
            };
            repo.Create(c);

            var another = repo.Read(1);
            another.Model = "Mercedes";
            repo.Update(another);

            var items = repo.ReadAll().ToArray();
            
            
        }
    }
}

