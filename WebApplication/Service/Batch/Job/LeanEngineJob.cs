using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Procurement;

namespace com.Sconit.Service.Batch.Job
{
    public class LeanEngineJob : IJob
    {
        private ILeanEngineMgr leanEngineMgr;

        #region 构造函数
        public LeanEngineJob(
            ILeanEngineMgr leanEngineMgr)
        {
            this.leanEngineMgr = leanEngineMgr;
        }
        #endregion

        public void Execute(JobRunContext context)
        {
            //leanEngineMgr.GenerateOrder();
            leanEngineMgr.OrderGenerate();
        }
    }
}
