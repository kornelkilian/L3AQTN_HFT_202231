using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public class BusLogic : IBusLogic
    {
        IRepository<Bus> repo;
        IRepository<Owner> ownerRepo;
        IRepository<Brand> brandRepo;

        public BusLogic(IRepository<Bus> repo, IRepository<Owner> ownerRepo, IRepository<Brand> brandRepo)
        {
            this.repo = repo;
            this.ownerRepo = ownerRepo;
            this.brandRepo = brandRepo;
        }

        public void Create(Bus item)
        {
            if (item.Model.Length < 2)
            {
                throw new ArgumentException("Hibás modell név.");
            }
            if (item.Price == 0)
            {
                throw new ArgumentException("Busz ára nem lehet nulla");
            }
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Bus Read(int id)
        {
            var b = repo.Read(id);
            if (b == null)
            {
                throw new ArgumentException("Nincs ilyen busz");
            }
            return b;
        }

        public IEnumerable<Bus> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Bus item)
        {
            repo.Update(item);
        }

        //non crud

        public double? GetAvaragePriceByBrandCountry(string country)
        {
            return this.repo
                .ReadAll()
                .Where(x => x.Brand.Country == country)
                .Average(x => x.Price);
        }
        //Buszok száma tulajonként, márka szerint
        public IEnumerable<OwnerBrandInfo> GetBusCountByOwner()
        {
            var owners = ownerRepo.ReadAll();

            List<OwnerBrandInfo> list = new List<OwnerBrandInfo>();

            foreach (var owner in owners)
            {
                OwnerBrandInfo info = new OwnerBrandInfo()
                {
                    OwnerName = owner.Name
                };

                info.Brands = (from x in owner.Buses
                               group x by x.Brand into g
                               select new BrandCount
                               {
                                   Name = g.Key.Name,
                                   Count = g.Key.Buses.Count()

                               });
                list.Add(info);

            }
            return list;
        }

        public List<Bus> BusesByZIPCode(int zip)
        {
            return this.repo.ReadAll().Where(bus => bus.Owner.ZIPCode == zip).ToList();
        }

        public IEnumerable<OwnerBrandInfo> GetBusCountByMustache()
        {
            var owners = ownerRepo.ReadAll();

            List<OwnerBrandInfo> list = new List<OwnerBrandInfo>();

            foreach (var owner in owners)
            {
                if ((bool)(owner.HasMustache))
                {
                    OwnerBrandInfo info = new OwnerBrandInfo()
                    {
                        OwnerName = owner.Name
                    };

                    info.Brands = (from x in owner.Buses
                                   group x by x.Brand into g
                                   select new BrandCount
                                   {
                                       Name = g.Key.Name,
                                       Count = g.Key.Buses.Count()

                                   });
                    list.Add(info);
                }


            }
            return list;
        }

        public double? GetAvaragePriceByOwner(string ownername)
        {

            return this.repo
                .ReadAll()
                .Where(x => x.Owner.Name == ownername)
                .Average(x => x.Price);
        }

        public double? HighestPriceByBrand(string brandname)
        {
            return this.repo
                .ReadAll()
                .Where(x => x.Brand.Name == brandname)
                .Max(x => x.Price);
        }

        public List<Bus> BusesWithMustacheOwners()
        {
            return this.repo.ReadAll().Where(bus => bus.Owner.HasMustache == true).ToList();


        }



    }

    public class OwnerBrandInfo
    {
        public string OwnerName { get; set; }
        public IEnumerable<BrandCount> Brands { get; set; }

        
    }



    public class BrandCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
