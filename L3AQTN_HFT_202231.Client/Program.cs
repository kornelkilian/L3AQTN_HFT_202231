using L3AQTN_HFT_202231.Repository;
using System;

namespace L3AQTN_HFT_202231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            BusDbContext context = new BusDbContext();
            var buses = context.Buses.AsQueryable();    
        }
    }
}

