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
    public class NHUomConversionBaseDao : NHDaoBase, IUomConversionBaseDao
    {
        public NHUomConversionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUomConversion(UomConversion entity)
        {
            Create(entity);
        }

        public virtual IList<UomConversion> GetAllUomConversion()
        {
            return FindAll<UomConversion>();
        }

        public virtual UomConversion LoadUomConversion(Int32 id)
        {
            return FindById<UomConversion>(id);
        }

        public virtual UomConversion LoadUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom)
        {
            string hql = @"from UomConversion entity where entity.Item.Code = ? and entity.AlterUom.Code = ? and entity.BaseUom.Code = ?";
            IList<UomConversion> result = FindAllWithCustomQuery<UomConversion>(hql, new object[] { item.Code, alterUom.Code, baseUom.Code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual UomConversion LoadUomConversion(String itemCode, String alterUomCode, String baseUomCode)
        {
            string hql = @"from UomConversion entity where entity.Item.Code = ? and entity.AlterUom.Code = ? and entity.BaseUom.Code = ?";
            IList<UomConversion> result = FindAllWithCustomQuery<UomConversion>(hql, new object[] { itemCode, alterUomCode, baseUomCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateUomConversion(UomConversion entity)
        {
            Update(entity);
        }

        public virtual void DeleteUomConversion(Int32 id)
        {
            string hql = @"from UomConversion entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom)
        {
            string hql = @"from UomConversion entity where entity.Item.Code = ? and entity.AlterUom.Code = ? and entity.BaseUom.Code = ?";
            Delete(hql, new object[] { item.Code, alterUom.Code, baseUom.Code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteUomConversion(String itemCode, String alterUomCode, String baseUomCode)
        {
            string hql = @"from UomConversion entity where entity.Item.Code = ? and entity.AlterUom.Code = ? and entity.BaseUom.Code = ?";
            Delete(hql, new object[] { itemCode, alterUomCode, baseUomCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteUomConversion(UomConversion entity)
        {
            Delete(entity);
        }

        public virtual void DeleteUomConversion(IList<UomConversion> entityList)
        {
            foreach (UomConversion entity in entityList)
            {
                DeleteUomConversion(entity);
            }
        }

        public virtual void DeleteUomConversion(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from UomConversion entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }


        #endregion Method Created By CodeSmith
    }
}
