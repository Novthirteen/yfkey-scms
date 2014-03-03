using System;
using com.Sconit.Entity.Batch;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch
{
    public interface IBatchTriggerParameterMgr : IBatchTriggerParameterBaseMgr
    {
        #region Customized Methods

        IList<BatchTriggerParameter> GetBatchTriggerParameter(int triggerId);

        #endregion Customized Methods
    }
}
