using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3AQTN_HFT_202231.Logic
{
    public   interface IBusLogic
    {
        void Create(Bus item);
        void Delete(int id);

        Bus Read(int id);

        IEnumerable<Bus> ReadAll();

        void Update(Bus item);

        

    }
}
