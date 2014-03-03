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
    public class NHNumberControlBaseDao : NHDaoBase, INumberControlBaseDao
    {
        public NHNumberControlBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateNumberControl(NumberControl entity)
        {
            Create(entity);
        }

        public virtual IList<NumberControl> GetAllNumberControl()
        {
            return FindAll<NumberControl>();
        }

        public virtual NumberControl LoadNumberControl(String code)
        {
            return FindById<NumberControl>(code);
        }

        public virtual void UpdateNumberControl(NumberControl entity)
        {
            Update(entity);
        }

        public virtual void DeleteNumberControl(String code)
        {
            string hql = @"from NumberControl entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteNumberControl(NumberControl entity)
        {
            Delete(entity);
        }

        public virtual void DeleteNumberControl(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from NumberControl entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteNumberControl(IList<NumberControl> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (NumberControl entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteNumberControl(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
