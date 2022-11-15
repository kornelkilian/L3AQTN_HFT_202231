using System;
namespace L3AQTN_HFT_202231.Logic
{
        public struct AveragePriceByBrand
        {
            public int BrandId { get; set; }
            public double AveragePrice { get; set; }

            public override string ToString()
            {
                return $"BrandId: {BrandId} | AveragePrice : {AveragePrice}";
            }
        }
    }



