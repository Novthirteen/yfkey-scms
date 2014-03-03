using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whidsoft.WebControls;

namespace com.Sconit.Control
{
    public class MyOrgNode : OrgNode
    {
        //public string ID;
        public string Code;
        public string Name;
        public string Memo1;
        public string Memo2;

        public string CodeTooltip;
        public string NameTooltip;
        public string Memo1Tooltip;
        public string Memo2Tooltip;

        public override string OrgNodeHtml(OrgNode node)
        {
            string str = "<div style='margin-left:10px;' >";

            str += "<table align='top' border='1' bordercolor='Silver' cellspacing='0' style='border-collapse:collapse;'>";
            str += "<tr><td class='GVRow'><div style='overflow:hidden;white-space: nowrap; width:120px;' title='" + this.CodeTooltip + "'>" + this.Code + "</div></td></tr>";
            str += "<tr><td class='GVAlternatingRow'><div style='overflow:hidden;white-space: nowrap; width:120px;' title='" + this.NameTooltip + "'>" + this.Name + "</div></td></tr>";
            str += "<tr><td class='GVRow'><div style='overflow:hidden;white-space: nowrap; width:120px;' title='" + this.Memo1Tooltip + "'>" + this.Memo1 + "</div></td></tr>";
            str += "<tr><td class='GVAlternatingRow'><div style='overflow:hidden;white-space: nowrap; width:120px;' title='" + this.Memo2Tooltip + "'>" + this.Memo2 + "</div></td></tr>";
            str += "</table>";
            str += "</div>";

            return str;
        }

        static int ImageRnd;
        /// <summary>
        /// 根据用户编号获得图片
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetImageFile(string ID)
        {
            //这里要你自己读数据库或其他处理，返回图片的显示文件名。

            if (ImageRnd > 13) //我的目录只有13个图
                ImageRnd = 0;

            string src = "images/" + ImageRnd + ".gif";

            ImageRnd++;
            return src;
        }
    }
}
