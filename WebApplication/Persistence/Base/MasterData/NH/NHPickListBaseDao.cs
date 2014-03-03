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
    public class NHPickListBaseDao : NHDaoBase, IPickListBaseDao
    {
        public NHPickListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePickList(PickList entity)
        {
            Create(entity);
        }

        public virtual IList<PickList> GetAllPickList()
        {
            return FindAll<PickList>();
        }

        public virtual PickList LoadPickList(String pickListNo)
        {
            return FindById<PickList>(pickListNo);
        }

        public virtual void UpdatePickList(PickList entity)
        {
            Update(entity);
        }

        public virtual void DeletePickList(String pickListNo)
        {
            string hql = @"from PickList entity where entity.PickListNo = ?";
            Delete(hql, new object[] { pickListNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeletePickList(PickList entity)
        {
            Delete(entity);
        }

        public virtual void DeletePickList(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PickList entity where entity.PickListNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePickList(IList<PickList> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (PickList entity in entityList)
            {
                pkList.Add(entity.PickListNo);
            }

            DeletePickList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
