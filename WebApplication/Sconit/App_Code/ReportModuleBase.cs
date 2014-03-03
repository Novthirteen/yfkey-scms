using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using System.Collections;
using com.Sconit.Utility;

/// <summary>
/// Summary description for ReportModuleBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class ReportModuleBase : ListModuleBase
    {
        public ReportModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public CriteriaParam _criteriaParam
        {
            get { return ViewState["CriteriaParam"] != null ? (CriteriaParam)ViewState["CriteriaParam"] : new CriteriaParam(); }
            set { ViewState["CriteriaParam"] = value; }
        }

        public abstract void InitPageParameter(object sender);

        protected abstract void SetCriteria();

        public virtual void PostProcess(IList list)
        {
        }
    }
}
