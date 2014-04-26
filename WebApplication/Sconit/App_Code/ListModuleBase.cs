using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using NHibernate;
using com.Sconit.Utility;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for ListModuleBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class ListModuleBase : ModuleBase
    {
        public ListModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected ICriteriaMgr CriteriaMgr
        {
            get
            {
                return GetService<ICriteriaMgr>("CriteriaMgr.service");
            }
        }

        public void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria)
        {
            new SessionHelper(this.Page).AddUserSelectCriteria(this.TemplateControl.AppRelativeVirtualPath, SelectCriteria, SelectCountCriteria);
        }

        public void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria, IDictionary<string, string> alias)
        {
            new SessionHelper(this.Page).AddUserSelectCriteria(this.TemplateControl.AppRelativeVirtualPath, SelectCriteria, SelectCountCriteria, alias);
        }


        public abstract void UpdateView();

        public virtual void ExportXLS(com.Sconit.Control.GridView GV_List)
        {
            this.ExportXLS(GV_List, "");
        }

        public virtual void ExportXLS(com.Sconit.Control.GridView GV_List, string fileName)
        {

            if (GV_List.FindPager().RecordCount > 5000)
            {
                ShowWarningMessage("Common.Export.Warning.GreatThan5000", GV_List.FindPager().RecordCount.ToString());
            }

            if (GV_List.FindPager().RecordCount == 0)
            {
                this.ShowWarningMessage("Common.GridView.NoRecordFound");
            }
            else
            {
                GV_List.ExportXLS(fileName);
            }
        }

        public void GetView(string searchSql) { }


        #region Excel导出方法


        protected void ExportDOC(GridView gridview)
        {
            this.ExportDOC(gridview, null);
        }

        protected void ExportXLS(GridView gridview)
        {
            this.ExportXLS(gridview, null);
        }
        protected void ExportXLS(GridView gridview, String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "temp.xls";
            this.Export(gridview, "application/ms-excel", FileName);
        }

        protected void ExportDOC(GridView gridview, String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "temp.doc";
            this.Export(gridview, "application/ms-word", FileName);
        }

        /// <summary>  
        /// 导出数据函数  
        /// </summary>  
        /// <param name="FileType">导出文件MIME类型</param>  
        /// <param name="FileName">导出文件的名称</param>  
        protected void Export(GridView gridview, String FileType, String FileName)
        {
            /*
            gridview.AllowPaging = false;
            gridview.AllowSorting = false;
            */
            if (gridview.Rows.Count > 5000)
            {
                ShowWarningMessage("Common.Export.Warning.GreatThan5000", gridview.Rows.Count.ToString());
            }
            else if (gridview.Rows.Count == 0)
            {
                ShowWarningMessage("Common.GridView.NoRecordFound");
                return;
            }

            //gridview.DataBind();

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(cultureInfo);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            gridview.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);

            form.Controls.Add(gridview);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;

            //      Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.AppendHeader("Content-Disposition", "attachment;filename="
                    + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));

            //设置输出流HttpMiME类型(导出文件格式)  
            Response.ContentType = FileType;
            //Response.Charset = "UTF-8";
            //设定输出字符集  
            Response.Charset = "GB2312";
            //Response.ContentEncoding = Encoding.Default;
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string content = sw.ToString();
            if (CurrentUser != null && CurrentUser.UserLanguage != null && CurrentUser.UserLanguage != string.Empty)
            {
                content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.UserLanguage);
            }
            else
            {
                EntityPreference defaultLanguage = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                content = TheLanguageMgr.ProcessLanguage(content, defaultLanguage.Value);
            }

            Response.Write(content);
            Response.End();
            Response.Flush();
            /*
                        gridview.AllowPaging = true;
                        gridview.AllowSorting = true;
            */
            //gridview.DataBind();
        }


        #endregion
    }
}
