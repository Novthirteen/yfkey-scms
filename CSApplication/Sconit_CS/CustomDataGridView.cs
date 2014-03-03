using System;
using System.Windows.Forms;

namespace Sconit_CS
{
    public class CustomDataGridView : DataGridView
    {

        [System.Security.Permissions.UIPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //设定Enter键被按时和Tab键被按时一样
            if ((keyData & Keys.KeyCode) == Keys.Enter)
            {
                if (this.CurrentCell != null
                    && this.CurrentCell.ColumnIndex + 1 == this.ColumnCount
                    && this.CurrentCell.RowIndex + 1 < this.RowCount)
                {
                    this.CurrentCell = this[6, this.CurrentCell.RowIndex + 1];
                    return true;
                }
                else if (this.CurrentCell != null
                    && this.CurrentCell.ColumnIndex + 1 == this.ColumnCount
                    && this.CurrentCell.RowIndex + 1 == this.RowCount)
                {
                    this.EndEdit();
                    this.ClearSelection();
                    return true;
                }
                //else if ((string)this.CurrentCell.EditedFormattedValue == string.Empty)
                //{
                //    return this.ProcessZeroKey(keyData);
                //}
                else
                {
                    return this.ProcessTabKey(keyData);
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            //设定Enter键被按时和Tab键被按时一样
            if (e.KeyCode == Keys.Enter)
            {
                if (this.CurrentCell != null
                    && this.CurrentCell.ColumnIndex + 1 == this.ColumnCount
                    && this.CurrentCell.RowIndex + 1 < this.RowCount)
                {
                    this.CurrentCell = this[this.ColumnCount - 1, this.CurrentCell.RowIndex + 1];
                    return true;
                }
                else if (this.CurrentCell != null
                    && this.CurrentCell.ColumnIndex + 1 == this.ColumnCount
                    && this.CurrentCell.RowIndex + 1 < this.RowCount)
                {
                    this.ClearSelection();
                    this.EndEdit();
                    return true;
                }
                //else if ((string)this.CurrentCell.EditedFormattedValue == string.Empty)
                //{
                //    return this.ProcessZeroKey(e.KeyCode);
                //}
                else
                {
                    return this.ProcessTabKey(e.KeyCode);
                }
            }

            return base.ProcessDataGridViewKey(e);
        }

    }
}
