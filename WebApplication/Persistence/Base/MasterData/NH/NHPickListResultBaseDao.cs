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
    public class NHPickListResultBaseDao : NHDaoBase, IPickListResultBaseDao
    {
        public NHPickListResultBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePickListResult(PickListResult entity)
        {
            Create(entity);
        }

        public virtual IList<PickListResult> GetAllPickListResult()
        {
            return FindAll<PickListResult>();
        }

        public virtual PickListResult LoadPickListResult(Int32 id)
        {
            return FindById<PickListResult>(id);
        }

        public virtual void UpdatePickListResult(PickListResult entity)
        {
            Update(entity);
        }

        public virtual void DeletePickListResult(Int32 id)
        {
            string hql = @"from PickListResult entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePickListResult(PickListResult entity)
        {
            Delete(entity);
        }

        public virtual void DeletePickListResult(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PickListResult entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePickListResult(IList<PickListResult> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PickListResult entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePickListResult(pkList);
        }


        public virtual PickListResult LoadPickListResult(com.Sconit.Entity.MasterData.PickListDetail pickListDetail, com.Sconit.Entity.MasterData.LocationLotDetail locationLotDetail)
        {
            string hql = @"from PickListResult entity where entity.PickListDetail.Id = ? and entity.LocationLotDetail.Id = ?";
            IList<PickListResult> result = FindAllWithCustomQuery<PickListResult>(hql, new object[] { pickListDetail.Id, locationLotDetail.Id }, new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeletePickListResult(Int32 pickListDetailId, Int32 locationLotDetailId)
        {
            string hql = @"from PickListResult entity where entity.PickListDetail.Id = ? and entity.LocationLotDetail.Id = ?";
            Delete(hql, new object[] { pickListDetailId, locationLotDetailId }, new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 });
        }

        public virtual PickListResult LoadPickListResult(Int32 pickListDetailId, Int32 locationLotDetailId)
        {
            string hql = @"from PickListResult entity where entity.PickListDetail.Id = ? and entity.LocationLotDetail.Id = ?";
            IList<PickListResult> result = FindAllWithCustomQuery<PickListResult>(hql, new object[] { pickListDetailId, locationLotDetailId }, new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Method Created By CodeSmith
    }
}
