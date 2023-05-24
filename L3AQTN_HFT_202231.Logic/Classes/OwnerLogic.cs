using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public class OwnerLogic : IOwnerLogic
    {
        IRepository<Owner> repo;
        IRepository<Bus> busrepo;


        public OwnerLogic(IRepository<Owner> repo)
        {
            this.repo = repo;
        }

        public void Create(Owner item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
           this.repo.Delete(id);
           

        }

        public Owner Read(int id)
        {
            return this.repo.Read(id);
        }

        public IEnumerable<Owner> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Owner item)
        {
            this.repo.Update(item);
        }
    }
}
