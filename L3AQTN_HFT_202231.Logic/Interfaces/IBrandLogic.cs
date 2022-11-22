using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public interface IBrandLogic
    {
        void Create(Brand item);
        void Delete(int id);

        Brand Read(int id);

        IEnumerable<Brand> ReadAll();

        void Update(Brand item);
    }
}
