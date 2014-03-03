using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Utility;

public partial class Item_FLowItem_Main : MainModuleBase
{
    public Item_FLowItem_Main()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

  

    protected void tbItem_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.tbItemCode.Text.Trim() != string.Empty)
            {
                DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
                criteria.CreateAlias("Flow","f");
                //criteria.CreateAlias("f.PartyFrom", "fpf");
                //criteria.CreateAlias("f.PartyTo", "ftf");

                criteria.Add(Expression.Eq("Item.Code", this.tbItemCode.Text.Trim()));
                criteria.Add(Expression.Eq("f.IsActive", true));

                //#region partyFrom
                //SecurityHelper.SetPartySearchCriteria(criteria, "fpf.Code", this.CurrentUser.Code);             
                //#endregion

                //#region partyTo
                //SecurityHelper.SetPartySearchCriteria(criteria, "ftf.Code", this.CurrentUser.Code);
                //#endregion

                IList<FlowDetail> flowDetailList = TheCriteriaMgr.FindAll<FlowDetail>(criteria);
                this.ucList.InitPageParameter(flowDetailList);
                this.ucList.Visible = true;
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
