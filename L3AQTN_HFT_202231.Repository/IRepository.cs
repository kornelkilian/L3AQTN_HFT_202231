using L3AQTN_HFT_202231.Models;
using System.Collections.Generic;
using System.Security.Principal;

namespace L3AQTN_HFT_202231.Repo
{
    public interface IRepository<T> where T : class, IDbEntity
    {
        void Create(T entity);
        void Delete(int i);
        T Read(int i);
        IEnumerable<T> ReadAll();

        void Update(T item);
    }
}
