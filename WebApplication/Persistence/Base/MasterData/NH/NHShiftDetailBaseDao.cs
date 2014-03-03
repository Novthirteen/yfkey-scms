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
    public class NHShiftDetailBaseDao : NHDaoBase, IShiftDetailBaseDao
    {
        public NHShiftDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateShiftDetail(ShiftDetail entity)
        {
            Create(entity);
        }

        public virtual IList<ShiftDetail> GetAllShiftDetail()
        {
            return FindAll<ShiftDetail>();
        }

        public virtual ShiftDetail LoadShiftDetail(Int32 id)
        {
            return FindById<ShiftDetail>(id);
        }

        public virtual void UpdateShiftDetail(ShiftDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteShiftDetail(Int32 id)
        {
            string hql = @"from ShiftDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteShiftDetail(ShiftDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteShiftDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ShiftDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteShiftDetail(IList<ShiftDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ShiftDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteShiftDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
