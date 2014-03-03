using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using com.Sconit.Service;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using System.Text;
using System.IO;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using System.Threading;
using System.Globalization;

namespace com.Sconit.Web
{

    /// <summary>
    /// Summary description for ModuleBase
    /// </summary>
    public abstract class ModuleBase : ControlBase, IMessage
    {
        #region 构造方法
        public ModuleBase()
        {
        }
        #endregion

        #region 页面事件
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Error += new EventHandler(PageErrorHandler);
        }

        //protected virtual void Page_Load(object sender, EventArgs e)
        //{            
        //    if (!IsPostBack)
        //    {
        //        CleanMessage();
        //    }
        //}
        #endregion

        #region 方法
        public void ShowSuccessMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowSuccessMessage(message);
            }
        }

        public void ShowSuccessMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowSuccessMessage(message, parameters);
            }
        }

        public void ShowWarningMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowWarningMessage(message);
            }
        }

        public void ShowWarningMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowWarningMessage(message, parameters);
            }
        }

        public void ShowErrorMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowErrorMessage(message);
            }
        }

        public void ShowErrorMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowErrorMessage(message, parameters);
            }
        }

        public void ShowErrorMessage(BusinessErrorException ex)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                if (ex.MessageParams != null && ex.MessageParams.Length > 0)
                {
                    ucMessage.ShowErrorMessage(ex.Message, ex.MessageParams);
                }
                else
                {
                    ucMessage.ShowErrorMessage(ex.Message);
                }
            }
        }

        public void CleanMessage()
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.CleanMessage();
            }
        }

        protected IList<int> GetSelectIdList(GridView gv)
        {
            IList<int> idList = new List<int>();

            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
                }
            }

            return idList;
        }

        private void PageErrorHandler(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is BusinessErrorException )
            {
                //todo 通用业务异常处理页面
                //添加返回按钮，返回原页面history.back(-1);
                Response.Write(ex.Message);
                Server.ClearError();
            }
        }
        #endregion

    }

}