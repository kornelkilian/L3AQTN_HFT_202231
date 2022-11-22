using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using L3AQTN_HFT_202231.Models;

namespace L3AQTN_HFT_202231.Logic
{
    
        public interface IBusLogic
        {
            IEnumerable<Bus> GetAllCars();
            IEnumerable<AveragePriceByBrand> GetAverageByBrand();

        }
    
}

