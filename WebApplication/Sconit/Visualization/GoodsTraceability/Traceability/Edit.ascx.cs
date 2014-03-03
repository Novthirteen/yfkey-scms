using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using System.Collections;

public partial class Visualization_GoodsTraceability_Traceability_Edit : EditModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {
        this.ODS_FV.SelectParameters["code"].DefaultValue = code;
        this.FV_Edit.DataBind();
        TextBox tbBin = (TextBox)(this.FV_Edit.FindControl("tbBin"));
        if (tbBin != null)
        {
            tbBin.Text = (this.GetBin(code) == null || this.GetBin(code).StorageBin == null) ? string.Empty : this.GetBin(code).StorageBin.Code;
        }
    }

    protected void FV_DataBound(object sender, EventArgs e)
    {
    }

    private LocationLotDetail GetBin(string huId)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationLotDetail));
        selectCriteria.Add(Expression.Eq("Hu.HuId", huId));

        decimal qty = 0;
        selectCriteria.Add(Expression.Gt("Qty",qty));
        //selectCriteria.SetProjection(Projections.GroupProperty("StorageBin.Code"));

        IList<LocationLotDetail> result = TheCriteriaMgr.FindAll<LocationLotDetail>(selectCriteria);
        if (result != null && result.Count > 0)
        {
            return result[0];
        }
        else
        {
            return null;
        }
    }
}
