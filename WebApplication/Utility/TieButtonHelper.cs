using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace com.Sconit.Utility
{
    public static class TieButtonHelper
    {
        public static void TieButton(Page page, Control TextBoxToTie, Control ButtonToTie)
        {
            // 初始化Jscript，实现原理是向客户端发送特定Jscript
            string jsString = "";
            // 检查输入框对应的事件按纽
            if (ButtonToTie is LinkButton)
            {
                jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {" + page.ClientScript.GetPostBackEventReference(ButtonToTie, "").Replace(":", "$") + ";return false;} else return true;";
            }
            else if (ButtonToTie is ImageButton)
            {
                jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {" + page.ClientScript.GetPostBackEventReference(ButtonToTie, "").Replace(":", "$") + ";return false;} else return true;";
            }
            else
            {
                jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document." + "forms[0].elements['" + ButtonToTie.UniqueID.Replace(":", "_") + "'].click();return false;} else return true; ";
            }
            // 把 jscript 附加到输入框的onkeydown属性

            if (TextBoxToTie is HtmlControl)
            {
                ((HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            }
            else if (TextBoxToTie is WebControl)
            {
                ((WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            }
        }
    }
}
