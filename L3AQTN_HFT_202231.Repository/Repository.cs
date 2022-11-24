
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly BusDbContext context;

        public Repository(BusDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> ReadAll()
        {
            return context.Set<T>();
        }


        public abstract T Read(int i);
       

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int i)
        {
            context.Set<T>().Remove(Read(i));
            context.SaveChanges();
        }

        public abstract void Update(T item);

    }
}

