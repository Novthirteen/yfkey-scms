using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public class TEMP_FORD_EDI_830 : TEMP_FORD_EDI_830Base
    {
        #region Non O/R Mapping Properties

        public string TempFlow { get; set; }

        #endregion
    }
}
