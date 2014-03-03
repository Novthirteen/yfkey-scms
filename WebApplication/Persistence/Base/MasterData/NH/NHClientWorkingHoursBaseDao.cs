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
    public class NHClientWorkingHoursBaseDao : NHDaoBase, IClientWorkingHoursBaseDao
    {
        public NHClientWorkingHoursBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClientWorkingHours(ClientWorkingHours entity)
        {
            Create(entity);
        }

        public virtual IList<ClientWorkingHours> GetAllClientWorkingHours()
        {
            return FindAll<ClientWorkingHours>();
        }

        public virtual ClientWorkingHours LoadClientWorkingHours(Int32 id)
        {
            return FindById<ClientWorkingHours>(id);
        }

        public virtual void UpdateClientWorkingHours(ClientWorkingHours entity)
        {
            Update(entity);
        }

        public virtual void DeleteClientWorkingHours(Int32 id)
        {
            string hql = @"from ClientWorkingHours entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteClientWorkingHours(ClientWorkingHours entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClientWorkingHours(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ClientWorkingHours entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClientWorkingHours(IList<ClientWorkingHours> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ClientWorkingHours entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteClientWorkingHours(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
