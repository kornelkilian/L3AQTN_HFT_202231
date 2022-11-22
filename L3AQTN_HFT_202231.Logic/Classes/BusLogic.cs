using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public class BusLogic : IBusLogic
    {
        IRepository<Bus> repo;

        public BusLogic(IRepository<Bus> repo)
        {
            this.repo = repo;
        }

        public void Create(Bus item)
        {
            if (item.Model.Length < 2)
            {
                throw new ArgumentException("Hibás modell név.");
            }
            if (item.Price==0)
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

        public double? GetAvaragePriceByBrand(Brand brand)
        {
            return this.repo
                .ReadAll()
                .Where(x => x.Brand == brand)
                .Average(x => x.Price);
        }

        public double? GetAvaragePriceByModel(string model)
        {
            return this.repo
                .ReadAll()
                .Where(x => x.Model == model)
                .Average(x => x.Price);
        }

        public double? HighestPriceByModel(string model)
        {
            return this.repo
                .ReadAll()
                .Where(x => x.Model == model)
                .Max(x => x.Price);
        }

        
    }
}
