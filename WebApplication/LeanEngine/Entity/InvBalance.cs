using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Utility;

namespace LeanEngine.Entity
{
    /// <summary>
    /// Inventory details
    /// </summary>
    public class InvBalance : EntityBase
    {
        public string Item { get; set; }

        public string Loc { get; set; }

        public decimal Qty { get; set; }

        public Enumerators.InvType InvType { get; set; }
    }
}
