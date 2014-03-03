using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace com.Sconit.Utility
{

    /// <summary>
    /// Summary description for ValidateEditItem.
    /// </summary>
    public class ValidateEditItem : ITemplate
    {
        private string column;
        public ValidateEditItem(string column)
        {
            this.column = column;
        }

        public void InstantiateIn(Control container)
        {
            TextBox tb = new TextBox();
            tb.DataBinding += new EventHandler(this.BindData);
          //  container.Controls.Add(tb);
            tb.ID = column;

          //  RequiredFieldValidator rfv = new RequiredFieldValidator();
          //  rfv.Text = "Please Answer";
          //  rfv.ControlToValidate = tb.ID;
          //  rfv.Display = ValidatorDisplay.Dynamic;
          //  rfv.ID = "validate" + tb.ID;
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
