using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

/// <summary>
/// Summary description for BusinessModuleBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class BusinessModuleBase : ModuleBase
    {
        public BusinessModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Resolver CacheResolver
        {
            get { return (Resolver)ViewState["CacheResolver"]; }
            set { ViewState["CacheResolver"] = value; }
        }

        protected virtual void InitialResolver(string userCode, string moduleType)
        {
            CacheResolver = new Resolver();
            CacheResolver.UserCode = userCode;
            CacheResolver.ModuleType = moduleType;
        }

        protected void ResolveInput(string input)
        {
            ResolveInput(input, true);
        }

        protected void ResolveInput(string input, bool bind)
        {
            CacheResolver.Input = input;
            CacheResolver = TheResolverMgr.Resolve(this.CacheResolver);

            if (bind) Bind();
        }

        protected virtual void Bind()
        {
            if (CacheResolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMER)
            {
                BindTransformer();
            }
            else if (CacheResolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL)
            {
                BindTransformerDetail();
            }
        }

        protected List<TransformerDetail> GetTransformerDetailList()
        {
            if (CacheResolver.Transformers == null || CacheResolver.Transformers.Count == 0)
                return null;

            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            foreach (Transformer transformer in CacheResolver.Transformers)
            {
                if (transformer.TransformerDetails != null)
                {
                    transformerDetailList.AddRange(transformer.TransformerDetails);
                }
            }

            return transformerDetailList;
        }

        protected void ExecuteSubmit()
        {
            this.CacheResolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
            this.CacheResolver = TheResolverMgr.Resolve(CacheResolver);
        }

        protected void ExecuteCancel()
        {
            this.CacheResolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
            this.CacheResolver = TheResolverMgr.Resolve(CacheResolver);
        }

        protected abstract void BindTransformer();
        protected abstract void BindTransformerDetail();
    }
}
