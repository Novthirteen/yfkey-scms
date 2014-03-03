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
    public class NHFileUploadBaseDao : NHDaoBase, IFileUploadBaseDao
    {
        public NHFileUploadBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFileUpload(FileUpload entity)
        {
            Create(entity);
        }

        public virtual FileUpload LoadFileUpload(Int32 id)
        {
			return FindById<FileUpload>(id);
        }

        public virtual void UpdateFileUpload(FileUpload entity)
        {
            Update(entity);
        }

        public virtual void DeleteFileUpload(Int32 id)
		{
            string hql = @"from FileUpload entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
		}

		public virtual void DeleteFileUpload(FileUpload entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFileUpload(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FileUpload entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFileUpload(IList<FileUpload> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FileUpload entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFileUpload(pkList);
        }

        #endregion Method Created By CodeSmith
    }
}
