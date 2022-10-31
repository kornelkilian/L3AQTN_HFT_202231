
using L3AQTN_HFT_202231.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IDbEntity
    {
        private readonly BusDbContext context;

        public Repository(BusDbContext context)
        {
            this.context = context;
        }

        public abstract IEnumerable<T> ReadAll();


        public T Read(int i)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == i);
        }

        public void Create(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int i)
        {
            var item = Read(i);
            context.Remove(item);
            context.SaveChanges();
        }

        public abstract bool Update(T item);

    }
}

