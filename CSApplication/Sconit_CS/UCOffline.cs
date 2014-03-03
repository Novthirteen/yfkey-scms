using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using Sconit_CS.SconitWS;
//待解决:离线上线的工单可以收货;离线多次收货默认收货数显示;无工单收货(界面类似移库);
namespace Sconit_CS
{
    public partial class UCOffline : UserControl
    {
        private User user;
        private int currentRowIndex;
        private string orderNo;
        private ClientMgrWSSoapClient TheClientMgr;
        private Resolver resolver;
        private DataTable dt;
        private Employee employee;
        private string moduleType;

        public UCOffline(User user, string moduleType)
        {
            InitializeComponent();
            this.user = user;
            this.gvWODetail.AutoGenerateColumns = false;
            this.gvEmployee.AutoGenerateColumns = false;
            this.moduleType = moduleType;
        }

        private void UCWOScanOffline_Load(object sender, EventArgs e)
        {
            this.InitialAll();
            this.TheClientMgr = new ClientMgrWSSoapClient();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter))
            {
                ReceiveOrder();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                if (tbWO.Focused)
                {
                    this.lblMessage.Text = string.Empty;
                    this.orderNo = this.tbWO.Text.Trim();
                    this.WorkOrderScan();
                    return true;
                }
                else if (tbEmployee.Focused)
                {
                    this.EmployeeScan();
                    return true;
                }
                else if (tbHours.Focused)
                {
                    this.HoursScan();
                    return true;
                }
                else if (this.gvWODetail.Focused)
                {
                    if (this.gvWODetail.CurrentCell.RowIndex + 1 == this.gvWODetail.Rows.Count
                        && this.gvWODetail.CurrentCell.ColumnIndex == 8)
                    {
                        this.btnOffline.Focus();
                        //this.tbEmployee.Focus();
                        //this.gvWODetail.ClearSelection();
                        return true;
                    }
                }
                else if (this.btnOffline.Focused)
                {
                    this.gbDetail.Focus();
                    ReceiveOrder();
                }
                if (this.gvWODetail.CurrentCell != null && (string)this.gvWODetail.CurrentCell.EditedFormattedValue == string.Empty)
                {
                    SendKeys.Send("0");
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                if (this.gvWODetail.CurrentCell != null && (string)this.gvWODetail.CurrentCell.EditedFormattedValue == string.Empty)
                {
                    SendKeys.Send("0{ESC}");
                    return true;
                }
                else
                {
                    this.resolver.UserCode = this.user.Code;
                    this.resolver.ModuleType = this.moduleType;
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
                    this.resolver = this.TheClientMgr.ScanBarcode(this.resolver);
                    this.gvOrderDataBind();
                    this.tbWO.Text = string.Empty;
                    this.tbWO.Focus();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void gvEmployeeDataBind()
        {
            this.gvEmployee.ClearSelection();
            if (this.gvEmployee.Rows.Count > 0)
            {
                this.gvEmployee.Rows[0].Selected = true;
            }
        }

        private void WorkOrderScan()
        {
            try
            {
                this.InitialAll();
                if (orderNo.Length > 5)
                {
                    this.resolver.Input = this.orderNo;
                    this.resolver.UserCode = this.user.Code;
                    this.resolver.ModuleType = this.moduleType;

                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);

                    this.lblMessage.Text = resolver.Result;
                    this.lblMessage.ForeColor = Color.Black;
                    //this.resolver = TheClientMgr.GetTransformer(this.resolver);

                    this.gvOrderDataBind();
                    if (this.gvWODetail.Rows.Count > 0)
                    {
                        this.gvWODetail.Rows[0].Cells[this.gvWODetail.ColumnCount - 1].Selected = true;
                    }
                    this.gvWODetail.Focus();
                    //SendKeys.Send("{Tab}");
                    this.tbWO.Text = this.orderNo;
                    this.tbWO.ForeColor = Color.Black;
                }
                else
                {
                    this.tbWO.Text = string.Empty;
                    this.lblMessage.Text = "请输入正确的工单号!";
                    this.lblMessage.ForeColor = Color.Red;
                    this.tbWO.Focus();
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
                this.tbWO.Text = string.Empty;
                this.tbWO.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbWO.Text = string.Empty;
                this.tbWO.Focus();
            }
        }

        private void gvOrderDataBind()
        {
            if (this.resolver.Transformers == null)
            {
                this.resolver.Transformers = new Transformer[] { };
            }
            this.gvWODetail.DataSource = resolver.Transformers;
            this.gvWODetail.ClearSelection();
        }

        private void EmployeeScan()
        {
            try
            {
                this.tbEmployee.Text = this.tbEmployee.Text.Trim().ToUpper();
                if (this.tbEmployee.Text != string.Empty)
                {
                    this.employee = TheClientMgr.LoadEmployee(this.tbEmployee.Text.Trim());
                    if (employee != null)
                    {
                        this.ShowEmployee(employee);
                    }
                    else
                    {
                        this.tbEmployee.Text = string.Empty;
                        this.lblEmployeeMessage.Text = "此雇员信息不存在!";
                        this.lblEmployeeMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    this.btnOffline.Focus();
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
                this.tbWO.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "获取雇员信息失败!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbEmployee.Text = string.Empty;
                this.tbEmployee.Focus();
            }
        }

        private void ShowEmployee(Employee employee)
        {
            string name = employee.Name.Trim() == string.Empty ? string.Empty : "姓名:" + employee.Name;
            string department = employee.Department.Trim() == string.Empty ? string.Empty : "  部门:" + employee.Department;
            string workGroup = employee.WorkGroup.Trim() == string.Empty ? string.Empty : "  班组:" + employee.WorkGroup;
            string post = employee.Post.Trim() == string.Empty ? string.Empty : "  岗位:" + employee.Post;
            this.lblEmployeeMessage.Text = name + department + workGroup + post;
            this.lblEmployeeMessage.ForeColor = Color.Black;
            this.tbHours.Focus();
        }

        private void HoursScan()
        {
            this.tbHours.Text = this.tbHours.Text.Trim() == string.Empty ? "0" : this.tbHours.Text.Trim();
            decimal CurrentWorkingHours = Convert.ToDecimal(this.tbHours.Text);
            bool isNewRow = true;
            foreach (DataRow dr in this.dt.Rows)
            {
                if (dr[0].ToString().Trim().ToLower() == this.tbEmployee.Text.Trim().ToLower())
                {
                    DialogResult dialogResult = MessageBox.Show(this, "该雇员工时已经录入,继续操作数量将累加!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            this.currentRowIndex = this.dt.Rows.IndexOf(dr);
                            this.dt.Rows[this.currentRowIndex][2] = (CurrentWorkingHours + Convert.ToDecimal(this.dt.Rows[currentRowIndex][2].ToString())).ToString("0.########");
                            this.dt.Rows[this.currentRowIndex][3] = DateTime.Now;
                            isNewRow = false;
                            break;
                        case DialogResult.No:
                            this.tbEmployee.Text = string.Empty;
                            this.tbHours.Text = string.Empty;
                            this.tbEmployee.Focus();
                            return;
                        default:
                            break;
                    }
                    break;
                }
            }
            if (isNewRow && CurrentWorkingHours > 0 && this.employee != null)
            {
                DataRow dr = this.dt.NewRow();
                dr[0] = this.employee.Code;
                dr[1] = this.employee.Name;
                dr[2] = this.tbHours.Text;
                dr[3] = DateTime.Now;
                this.dt.Rows.InsertAt(dr, dt.Rows.Count);
                this.lblEmployeeMessage.Text = "新增雇员工时成功!";
                this.lblEmployeeMessage.ForeColor = Color.Green;
            }
            else if (!isNewRow && Convert.ToDecimal(this.dt.Rows[this.currentRowIndex][2]) == 0)
            {
                this.dt.Rows.RemoveAt(this.currentRowIndex);
                this.lblEmployeeMessage.Text = "删除雇员" + this.employee.Name + "工时成功!";
                this.lblEmployeeMessage.ForeColor = Color.Black;
            }
            else
            {
                this.lblEmployeeMessage.Text = "累加雇员工时成功!";
                this.lblEmployeeMessage.ForeColor = Color.Green;
            }

            this.gvEmployee.ClearSelection();
            this.gvEmployee.Rows[0].Selected = true;
            this.tbEmployee.Text = string.Empty;
            this.tbHours.Text = string.Empty;
            this.tbEmployee.Focus();
        }

        private void gvWODetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.gvWODetail.CurrentCell != null
                    && this.gvWODetail.CurrentCell.ColumnIndex + 1 == this.gvWODetail.ColumnCount
                    && this.gvWODetail.CurrentCell.RowIndex + 1 == this.gvWODetail.RowCount)
                {
                    SendKeys.Send("{Tab}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "不可预知的错误!请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveOrder()
        {
            try
            {
                if (this.resolver.Transformers != null && this.resolver.Transformers.Length > 0)
                {
                    List<string[]> workingHoursList = new List<string[]>();
                    foreach (DataRow dr in this.dt.Rows)
                    {
                        string[] stringArray = new string[2];
                        stringArray[0] = dr[0].ToString();
                        stringArray[1] = dr[2].ToString();
                        workingHoursList.Add(stringArray);
                    }
                    this.resolver.WorkingHours = workingHoursList.ToArray();
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                    if (this.resolver.AutoPrintHu)
                    {
                        Utility.PrintOrder(this.resolver.PrintUrl,this, null);
                    }
                    this.InitialAll();
                    this.lblMessage.Text = "收货成功!";
                    this.lblMessage.ForeColor = Color.Green;

                }
                else
                {
                    MessageBox.Show(this, "没有可收的货物!", "收货失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
            }
            catch (Exception ex)
            {
                this.InitialAll();
                MessageBox.Show(this, ex.Message, "收货失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitialAll()
        {
            this.dt = new DataTable();
            this.employee = new Employee();
            this.resolver = new Resolver();
            this.dt.Columns.Add(new DataColumn("EmployeeCode", Type.GetType("System.String")));
            this.dt.Columns.Add(new DataColumn("EmployeeName", Type.GetType("System.String")));
            this.dt.Columns.Add(new DataColumn("EmployeeWorkingHours", Type.GetType("System.String")));
            this.dt.Columns.Add(new DataColumn("ScanTime", Type.GetType("System.DateTime")));
            this.gvEmployee.DataSource = this.dt;
            this.dt.DefaultView.Sort = "ScanTime DESC";

            this.lblMessage.Text = string.Empty;
            this.lblEmployeeMessage.Text = string.Empty;
            this.tbWO.Text = string.Empty;
            this.currentRowIndex = -1;
            this.gvOrderDataBind();
            this.tbWO.Focus();
        }

        #region 过滤输入
        private void gvWODetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl EditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            EditingControl.KeyPress += new KeyPressEventHandler(Utility.DataGridViewDecimalFilter);
        }

        private void tbHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.TextBoxDecimalFilter(sender, e);
        }
        #endregion

        private void btnOffline_Click(object sender, EventArgs e)
        {
            ReceiveOrder();
        }

    }
}
