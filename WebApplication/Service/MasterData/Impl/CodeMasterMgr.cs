using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class CodeMasterMgr : CodeMasterBaseMgr, ICodeMasterMgr
    {
        private static IDictionary<string, IList<CodeMaster>> cachedCodeMaster = new Dictionary<string, IList<CodeMaster>>();
        private ICriteriaMgr criteriaMgr;
        public CodeMasterMgr(ICodeMasterDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<CodeMaster> GetCachedCodeMaster(string code)
        {
            if (cachedCodeMaster.ContainsKey(code))
            {
                return cachedCodeMaster[code];
            }
            else
            {
                DetachedCriteria criteria = DetachedCriteria.For(typeof(CodeMaster)).Add(Expression.Eq("Code", code)).AddOrder(Order.Asc("Seq"));
                IList<CodeMaster> list = criteriaMgr.FindAll<CodeMaster>(criteria);
                if (list != null && list.Count > 0)
                {
                    cachedCodeMaster[code] = list;
                    return list;
                }
                else
                {
                    //throw new ArgumentException("Not find codemaster of code:" + code);
                    return null;
                }
            }
        }

        public CodeMaster GetCachedCodeMaster(string code, string value)
        {
            IList<CodeMaster> list = GetCachedCodeMaster(code);

            foreach(CodeMaster cm in list)
            {
                if (cm.Value == value)
                {
                    return cm;
                }
            }

            throw new ArgumentException("Not find codemaster of code:" + code + " and value:" + value);
        }

        public IList<CodeMaster> GetCodeMasterList(string code, object[] valueArray)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(CodeMaster)).Add(Expression.Eq("Code", code)).Add(Expression.In("Value",valueArray));
            IList<CodeMaster> list = criteriaMgr.FindAll<CodeMaster>(criteria);
            return list;
        }

        public CodeMaster GetDefaultCodeMaster(string code)
        {
            IList<CodeMaster> list = GetCachedCodeMaster(code);

            foreach (CodeMaster cm in list)
            {
                if (cm.IsDefault == true)
                {
                    return cm;
                }
            }

            throw new ArgumentException("Not find default codemaster of code:" + code );
        }

        public IList<CodeMaster> GetAllCodeMaster()
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(CodeMaster)).AddOrder(Order.Asc("Code")).AddOrder(Order.Asc("Seq"));
            //criteria.SetProjection(Projections.Distinct(()));

            return criteriaMgr.FindAll<CodeMaster>(criteria);

        }

        public string GetRandomTheme(string themeType)
        {
            IList<CodeMaster> iListCodeMaster = GetCachedCodeMaster(themeType);

            if (iListCodeMaster.Count != 0)
            {
                Random random = new Random();
                int i = 0;
                int r = random.Next(0, iListCodeMaster.Count - 1);
                CodeMaster codeMaster = (CodeMaster)iListCodeMaster[r];

                while (codeMaster.Value == "Random" && (i < 100))
                {
                    r = random.Next(0, iListCodeMaster.Count - 1);
                    codeMaster = (CodeMaster)iListCodeMaster[r];
                    i++;
                }
                return codeMaster.Value;
            }
            else
            {
                return null;
            }
        }


        #endregion Customized Methods
    }
}