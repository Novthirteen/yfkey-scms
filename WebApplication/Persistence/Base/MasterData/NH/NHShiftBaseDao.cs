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
    public class NHShiftBaseDao : NHDaoBase, IShiftBaseDao
    {
        public NHShiftBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateShift(Shift entity)
        {
            Create(entity);
        }

        public virtual IList<Shift> GetAllShift()
        {
            return FindAll<Shift>();
        }

        public virtual Shift LoadShift(String code)
        {
            return FindById<Shift>(code);
        }

        public virtual void UpdateShift(Shift entity)
        {
            Update(entity);
        }

        public virtual void DeleteShift(String code)
        {
            string hql = @"from Shift entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteShift(Shift entity)
        {
            Delete(entity);
        }

        public virtual void DeleteShift(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Shift entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteShift(IList<Shift> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Shift entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteShift(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
