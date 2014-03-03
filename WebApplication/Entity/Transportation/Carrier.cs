using System;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public class Carrier : Party
    {
        public string ReferenceSupplier { get; set; }
    }
}
