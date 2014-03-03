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
    public class NHMiscOrderBaseDao : NHDaoBase, IMiscOrderBaseDao
    {
        public NHMiscOrderBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateMiscOrder(MiscOrder entity)
        {
            Create(entity);
        }

        public virtual IList<MiscOrder> GetAllMiscOrder()
        {
            return FindAll<MiscOrder>();
        }

        public virtual MiscOrder LoadMiscOrder(String orderNo)
        {
            return FindById<MiscOrder>(orderNo);
        }

        public virtual void UpdateMiscOrder(MiscOrder entity)
        {
            Update(entity);
        }

        public virtual void DeleteMiscOrder(String orderNo)
        {
            string hql = @"from MiscOrder entity where entity.OrderNo = ?";
            Delete(hql, new object[] { orderNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteMiscOrder(MiscOrder entity)
        {
            Delete(entity);
        }

        public virtual void DeleteMiscOrder(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MiscOrder entity where entity.OrderNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteMiscOrder(IList<MiscOrder> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (MiscOrder entity in entityList)
            {
                pkList.Add(entity.OrderNo);
            }

            DeleteMiscOrder(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
