using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    public class Orders : EntityBase
    {
        public Flow Flow { get; set; }

        public List<ItemFlow> ItemFlows { get; set; }

        public bool IsEmergency { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime WindowTime { get; set; }
    }
}
