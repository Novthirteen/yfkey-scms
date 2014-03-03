using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    public class Bom : EntityBase
    {
        public string Item { get; set; }

        public decimal GrossQtyPer { get; set; }

        public int Op { get; set; }

        /// <summary>
        /// Scrap percentage, 5 instead of 5%
        /// </summary>
        public decimal ScrapPct { get; set; }

        public decimal QtyPer
        {
            get { return GrossQtyPer * (1 + ScrapPct / 100); }
        }
    }
}
