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
    public class NHUomBaseDao : NHDaoBase, IUomBaseDao
    {
        public NHUomBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUom(Uom entity)
        {
            Create(entity);
        }

        public virtual IList<Uom> GetAllUom()
        {
            return FindAll<Uom>();
        }

        public virtual Uom LoadUom(String code)
        {
            return FindById<Uom>(code);
        }

        public virtual void UpdateUom(Uom entity)
        {
            Update(entity);
        }

        public virtual void DeleteUom(String code)
        {
            string hql = @"from Uom entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteUom(Uom entity)
        {
            Delete(entity);
        }

        public virtual void DeleteUom(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Uom entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteUom(IList<Uom> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Uom entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteUom(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
