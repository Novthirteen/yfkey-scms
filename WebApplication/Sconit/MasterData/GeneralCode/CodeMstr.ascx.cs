using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

public partial class MasterData_GeneralCode_CodeMstr : ModuleBase
{
    public event EventHandler SearchEvent;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<CodeMaster> listCodeMaster = TheCodeMasterMgr.GetAllCodeMaster();
        List<CodeMaster> listTemp = new List<CodeMaster>();
        CodeMaster temp = new CodeMaster();
        foreach (CodeMaster codeMaster in listCodeMaster)
        {
            if (temp.Code != codeMaster.Code)
            {
                listTemp.Add(codeMaster);
            }
            temp = codeMaster;
        }
        this.ddlCode.DataSource = listTemp;
        this.ddlCode.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Do_Search();
    }

    public void Do_Search()
    {
        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(CodeMaster));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(CodeMaster)).SetProjection(Projections.Count("Code"));
            if (this.ddlCode != null && this.ddlCode.SelectedValue != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Code", this.ddlCode.SelectedValue));
                selectCountCriteria.Add(Expression.Eq("Code", this.ddlCode.SelectedValue));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

}
