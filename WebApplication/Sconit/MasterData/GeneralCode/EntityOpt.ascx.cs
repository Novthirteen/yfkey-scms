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
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class MasterData_GeneralCode_EntityOpt : ModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ODS_GV_EntityOpt_OnUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {
        EntityPreference entityPreference = (EntityPreference)e.InputParameters[0];
        
        if (entityPreference.Value == null || entityPreference.Value == string.Empty)
        {
            e.Cancel = true;
            ShowErrorMessage("Common.String.Empty");
        }
        else
        {
            entityPreference.Value = entityPreference.Value.Trim();
        }
    }

    protected void GV_EntityOpt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Seq = ((TextBox)GV_EntityOpt.Rows[e.RowIndex].Cells[0].Controls[1]).Text.ToString().Trim();
        try
        {
            Convert.ToInt16(Seq);
        }
        catch (Exception)
        {
            e.Cancel = true;
            ShowErrorMessage("Common.Int.Error", Seq);
        }

        string Code = GV_EntityOpt.Rows[e.RowIndex].Cells[1].Text;
        string Value = ((TextBox)GV_EntityOpt.Rows[e.RowIndex].Cells[3].Controls[1]).Text.ToString().Trim();


        if (Value == null || Value == string.Empty)
        {
            e.Cancel = true;
            ShowErrorMessage("Common.String.Empty");
        }
        else
        {
            switch (Code)
            {
                case "MailTo":
                case "SMTPEmailAddr":
                    if (!IsValidEmail(Value))
                    {
                        e.Cancel = true;
                        ShowErrorMessage("Common.EmailFormat.Error");
                    }
                    return;
                case "OrderLength":
                    try
                    {
                        Convert.ToInt16(Value);
                    }
                    catch (Exception)
                    {
                        e.Cancel = true;
                        ShowErrorMessage("Common.Int.Error", Code);
                    }
                    return;

                default:
                    break;
            }

            IList<CodeMaster> list = TheCodeMasterMgr.GetCachedCodeMaster(Code);

            if (list != null)
            {
                bool flag = false;
                foreach (CodeMaster ep in list)
                {
                    if (ep.Value == Value)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    e.Cancel = true;
                    ShowErrorMessage("MasterData.EntityOption.Update.Fail", Code);
                }
                else
                {
                    ShowSuccessMessage("MasterData.EntityOption.Update.Successfully", Code);
                }
            }
            else
            {
                ShowSuccessMessage("MasterData.EntityOption.Update.Successfully", Code);
            }
        }

    }

    private bool IsValidEmail(string strIn)
    {
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
}
