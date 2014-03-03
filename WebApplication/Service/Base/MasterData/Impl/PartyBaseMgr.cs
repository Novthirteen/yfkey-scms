using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PartyBaseMgr : SessionBase, IPartyBaseMgr
    {
        protected IPartyDao entityDao;
        
        public PartyBaseMgr(IPartyDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateParty(Party entity)
        {
            entityDao.CreateParty(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Party LoadParty(String code)
        {
            return entityDao.LoadParty(code);
        }

		[Transaction(TransactionMode.Requires)]
        public virtual IList<Party> GetAllParty()
        {
			return entityDao.GetAllParty(false);
        }
		
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Party> GetAllParty(bool includeInactive)
        {
            return entityDao.GetAllParty(includeInactive);
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateParty(Party entity)
        {
            entityDao.UpdateParty(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteParty(String code)
        {
            entityDao.DeleteParty(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteParty(Party entity)
        {
            entityDao.DeleteParty(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteParty(IList<String> pkList)
        {
            entityDao.DeleteParty(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteParty(IList<Party> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteParty(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
