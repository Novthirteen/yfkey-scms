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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Distribution;

public partial class Inventory_InspectOrder_List : ListModuleBase
{
    public EventHandler EditEvent;
    public bool isExport
    {
        get { return ViewState["isExport"] == null ? false : (bool)ViewState["isExport"]; }
        set { ViewState["isExport"] = value; }
    }
    public bool isGroup
    {
        get { return ViewState["isGroup"] == null ? false : (bool)ViewState["isGroup"]; }
        set { ViewState["isGroup"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        if (isGroup)
        {
            this.GV_List.Visible = true;
            this.gp.Visible = true;
            this.GV_List_Detail.Visible = false;
            this.gp_Detail.Visible = false;

            if (!isExport)
            {
                this.GV_List.Execute();
            }
            else
            {
                //if (GV_List.Rows.Count > 0)
                //{
                //    GV_List.Columns.RemoveAt(GV_List.Columns.Count - 1);
                //}
                string dateTime = DateTime.Now.ToString("ddhhmmss");
                this.ExportXLS(this.GV_List, "InspectGroup" + dateTime + ".xls");
            }
        }
        else
        {
            this.GV_List.Visible = false;
            this.gp.Visible = false;
            this.GV_List_Detail.Visible = true;
            this.gp_Detail.Visible = true;
            if (isExport)
            {
                string dateTime = DateTime.Now.ToString("ddhhmmss");
                this.ExportXLS(this.GV_List_Detail, "InspectGroup" + dateTime + ".xls");
            }
            else
            {
                this.GV_List_Detail.Execute();
            }
        }
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InspectOrder inspectOrder = (InspectOrder)e.Row.DataItem;
        //    if (inspectOrder.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        //    {
        //        if ((LinkButton)e.Row.FindControl("lbtnDelete") != null)
        //            ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = true;
        //    }
        //}
    }

    protected void GV_List_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (this.isExport)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                com.Sconit.Control.CodeMstrLabel lblDefectClassification = ((com.Sconit.Control.CodeMstrLabel)e.Row.FindControl("lblDefectClassification"));
                com.Sconit.Control.CodeMstrLabel lblDisposition = ((com.Sconit.Control.CodeMstrLabel)e.Row.FindControl("lblDisposition"));
                com.Sconit.Control.CodeMstrLabel lblDefectFactor = ((com.Sconit.Control.CodeMstrLabel)e.Row.FindControl("lblDefectFactor"));
                
                if (lblDefectClassification.Value != string.Empty)
                {
                    CodeMaster cmDefectClassification = TheCodeMasterMgr.LoadCodeMaster(BusinessConstants.CODE_MASTER_INSPECT_DEFECTCLASSIFICATION, lblDefectClassification.Value);
                    if (cmDefectClassification != null)
                    {
                        lblDefectClassification.Text = cmDefectClassification.Description;
                    }
                }
                if (lblDisposition.Value != string.Empty)
                {
                    CodeMaster cmDisposition = TheCodeMasterMgr.LoadCodeMaster(BusinessConstants.CODE_MASTER_INSPECT_DISPOSITION, lblDisposition.Value);
                    if (cmDisposition != null)
                    {
                        lblDisposition.Text = cmDisposition.Description;
                    }
                }
                if (lblDefectFactor.Value != string.Empty)
                {
                    CodeMaster cmlblDefectFactor = TheCodeMasterMgr.LoadCodeMaster(BusinessConstants.CODE_MASTER_INSPECT_DEFECTFACTOR, lblDefectFactor.Value);
                    if (cmlblDefectFactor != null)
                    {
                        lblDefectFactor.Text = cmlblDefectFactor.Description;
                    }
                }
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string inspectNo = ((LinkButton)sender).CommandArgument;
            EditEvent(inspectNo, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {

    }
}
