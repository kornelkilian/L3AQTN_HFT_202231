using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public interface IOwnerLogic
    {
        void Create(Owner item);
        void Delete(int id);

        Owner Read(int id);

        IEnumerable<Owner> ReadAll();

        void Update(Owner item);
    }
}
