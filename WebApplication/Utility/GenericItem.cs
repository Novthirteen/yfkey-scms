using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.Sconit.Utility
{

    /// <summary>
    /// Summary description for GenericItem.
    /// </summary>
    public class GenericItem : ITemplate
    {
        private string column;
        //private bool validate;
        public GenericItem(string column)
        {
            this.column = column;
        }
        public void InstantiateIn(Control container)
        {
            TextBox tb = new TextBox();
            tb.DataBinding += new EventHandler(this.BindData);
            tb.ID = column;
            tb.ReadOnly = true;
            tb.CssClass = "tbmps";
            container.Controls.Add(tb);
        }

        public void BindData(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DataGridItem container = (DataGridItem)tb.NamingContainer;
            tb.Text = ((DataRowView)container.DataItem)[column].ToString();
        }
    }

}
