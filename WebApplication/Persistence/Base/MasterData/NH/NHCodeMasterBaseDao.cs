using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHCodeMasterBaseDao : NHDaoBase, ICodeMasterBaseDao
    {
        public NHCodeMasterBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCodeMaster(CodeMaster entity)
        {
            Create(entity);
        }

        public virtual CodeMaster LoadCodeMaster(String code, String value)
        {
            string hql = @"from CodeMaster entity where entity.Code = ? and entity.Value = ?";
            IList<CodeMaster> result = FindAllWithCustomQuery<CodeMaster>(hql, new object[] { code, value }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
			if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateCodeMaster(CodeMaster entity)
        {
            Update(entity);
        }

		public virtual void DeleteCodeMaster(String code, String value)
		{
			string hql = @"from CodeMaster entity where entity.Code = ? and entity.Value = ?";
            Delete(hql, new object[] { code, value }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
		}

		public virtual void DeleteCodeMaster(CodeMaster entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCodeMaster(IList<CodeMaster> entityList)
        {
            foreach (CodeMaster entity in entityList)
            {
                DeleteCodeMaster(entity);
            }
        }

        #endregion Method Created By CodeSmith
    }
}
