using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using com.Sconit.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace com.Sconit.CodeSmith
{
    public partial class MappingPropertyEditorForm : Form
    {
        private DataSet ds;

        private IList _mappingInfoCollection;
        public IList MappingInfoCollection
        {
            get
            {
                return _mappingInfoCollection;
            }
            set
            {
                _mappingInfoCollection = value;
            }
        }

        public MappingPropertyEditorForm()
        {
            InitializeComponent();
            this.MappingInfoCollection = new ArrayList();
            this.ds = new DataSet();
        }

        public void Start(IWindowsFormsEditorService editorService, object value)
        {
            if (value is MappingProperty)
            {
                MappingProperty mappingProperty = value as MappingProperty;

                if (mappingProperty != null)
                {
                    this.MappingInfoCollection = mappingProperty.MappingInfoCollection;
                }
            }
        }

        private void MappingPropertyEditorForm_Load(object sender, EventArgs e)
        {
            this.tableCB.Text = "";
            lblMessage.Text = string.Empty;

            dgvMapping.Rows.Clear();

            ((DataGridViewComboBoxColumn)dgvMapping.Columns["colPKGenerator"]).Items.Clear();
            foreach (string generator in Enum.GetNames(typeof(Generator)))
            {
                ((DataGridViewComboBoxColumn)dgvMapping.Columns["colPKGenerator"]).Items.Add(generator);
            }

            ((DataGridViewComboBoxColumn)dgvMapping.Columns["colDataType"]).Items.Clear();
            foreach (string dataTypeName in Enum.GetNames(typeof(DataType)))
            {
                ((DataGridViewComboBoxColumn)dgvMapping.Columns["colDataType"]).Items.Add(dataTypeName);
            }

            foreach (Type type in typeof(EntityBase).Assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    ((DataGridViewComboBoxColumn)dgvMapping.Columns["colDataType"]).Items.Add(type.FullName);
                }
            }

            ((DataGridViewComboBoxColumn)dgvMapping.Columns["colPKMany2OnePropertyDataType"]).Items.Clear();
            foreach (string dataTypeName in Enum.GetNames(typeof(DataType)))
            {
                ((DataGridViewComboBoxColumn)dgvMapping.Columns["colPKMany2OnePropertyDataType"]).Items.Add(dataTypeName);
            }

            if (this.MappingInfoCollection != null)
            {
                foreach (MappingInfo mappingInfo in this.MappingInfoCollection)
                {
                    this.FillMappingInfo(mappingInfo);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IList newMappingInfoCollection = new ArrayList();

            foreach (DataGridViewRow row in dgvMapping.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                MappingInfo mappingInfo = new MappingInfo();

                //get input
                mappingInfo.IsPK = bool.Parse(row.Cells["colIsPK"].Value != null ? row.Cells["colIsPK"].Value.ToString() : "false");

                mappingInfo.IsUnique = bool.Parse(row.Cells["colIsUnique"].Value != null ? row.Cells["colIsUnique"].Value.ToString() : "false");

                mappingInfo.PKGenerator = row.Cells["colPKGenerator"].Value != null ? row.Cells["colPKGenerator"].Value.ToString() : "";

                if ((row.Cells["colClassPropertyName"].Value == null)
                    || (row.Cells["colClassPropertyName"].Value.ToString().Trim().Length == 0))
                {
                    lblMessage.Text = "Please input Class Property Name.";
                    return;
                }
                else
                {
                    mappingInfo.ClassPropertyName = row.Cells["colClassPropertyName"].Value.ToString();
                }

                if ((row.Cells["colTableColumnName"].Value == null)
                    || (row.Cells["colTableColumnName"].Value.ToString().Trim().Length == 0))
                {
                    lblMessage.Text = "Please input Table Column Name.";
                    return;
                }
                else
                {
                    mappingInfo.TableColumnName = row.Cells["colTableColumnName"].Value.ToString();
                }

                mappingInfo.DataType = row.Cells["colDataType"].Value.ToString();

                if ((row.Cells["colDataLength"].Value == null)
                    || (row.Cells["colDataLength"].Value.ToString().Trim().Length == 0))
                {
                    mappingInfo.DataLength = 0;
                }
                else
                {
                    try
                    {
                        mappingInfo.DataLength = int.Parse(row.Cells["colDataLength"].Value.ToString());
                    }
                    catch
                    {
                        lblMessage.Text = "Invalid Data Length.";
                        return;
                    }
                }

                mappingInfo.IsNullable = bool.Parse(row.Cells["colIsNullable"].Value != null ? row.Cells["colIsNullable"].Value.ToString() : "false");
                if (mappingInfo.DataType.ToString() == DataType.String.ToString())
                {
                    //String value can be null
                    mappingInfo.IsNullable = false;
                }

                if (!Enum.IsDefined(typeof(DataType), mappingInfo.DataType) && (mappingInfo.IsPK || mappingInfo.IsUnique))
                {
                    if ((row.Cells["colPKMany2OnePropertyName"].Value == null)
                    || (row.Cells["colPKMany2OnePropertyName"].Value.ToString().Trim().Length == 0))
                    {
                        lblMessage.Text = "Please input PK Many2One Property Name Table Name.";
                        return;
                    }
                    else
                    {
                        mappingInfo.PKMany2OnePropertyName = row.Cells["colPKMany2OnePropertyName"].Value.ToString();
                    }

                    mappingInfo.PKMany2OnePropertyDataType = row.Cells["colPKMany2OnePropertyDataType"].Value.ToString();
                }

                mappingInfo.IsOne2Many = bool.Parse(row.Cells["colIsOne2Many"].Value != null ? row.Cells["colIsOne2Many"].Value.ToString() : "false");

                if (mappingInfo.IsOne2Many)
                {
                    if ((row.Cells["colOne2ManyTable"].Value == null)
                    || (row.Cells["colOne2ManyTable"].Value.ToString().Trim().Length == 0))
                    {
                        lblMessage.Text = "Please input One2Many Table Name.";
                        return;
                    }
                    else
                    {
                        mappingInfo.One2ManyTable = row.Cells["colOne2ManyTable"].Value.ToString();
                    }

                    if ((row.Cells["colOne2ManyColumn"].Value == null)
                    || (row.Cells["colOne2ManyColumn"].Value.ToString().Trim().Length == 0))
                    {
                        lblMessage.Text = "Please inpu One2Many Column Name.";
                        return;
                    }
                    else
                    {
                        mappingInfo.One2ManyColumn = row.Cells["colOne2ManyColumn"].Value.ToString();
                    }

                    mappingInfo.One2ManyInverse = bool.Parse(row.Cells["colOne2ManyInverse"].Value != null ? row.Cells["colOne2ManyInverse"].Value.ToString() : "false");
                    mappingInfo.One2ManyLazy = bool.Parse(row.Cells["colOne2ManyLazy"].Value != null ? row.Cells["colOne2ManyLazy"].Value.ToString() : "false");
                }

                newMappingInfoCollection.Add(mappingInfo);
            }

            this.MappingInfoCollection = newMappingInfoCollection;

            lblMessage.Text = string.Empty;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void dgvMapping_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvMapping.Rows[e.RowIndex].IsNewRow)
            {
                dgvMapping.Rows[e.RowIndex].Cells["colPKGenerator"].Value = Generator.assigned.ToString();
                dgvMapping.Rows[e.RowIndex].Cells["colDataType"].Value = DataType.String.ToString();
                dgvMapping.Rows[e.RowIndex].Cells["colDataLength"].Value = "50";
                ((DataGridViewCheckBoxCell)dgvMapping.Rows[e.RowIndex].Cells["colOne2ManyInverse"]).Selected = true;
                ((DataGridViewCheckBoxCell)dgvMapping.Rows[e.RowIndex].Cells["colOne2ManyLazy"]).Selected = false;
                dgvMapping.Rows[e.RowIndex].Cells["colPKMany2OnePropertyDataType"].Value = DataType.String.ToString();
            }
        }

        private void dgvMapping_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private SqlDataAdapter SqlConn(string sql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(this.connTB.Text);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                return da;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return null;
            }
        }

        private void connBT_Click(object sender, EventArgs e)
        {
            string sql = "select name,id from sysobjects where xtype='U' or xtype='V' order by name ";

            SqlDataAdapter da = this.SqlConn(sql);
            ds.Clear();
            try
            {
                da.Fill(ds);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            if (ds.Tables.Count != 0)
            {
                this.tableCB.DataSource = ds.Tables[0];
                this.tableCB.DisplayMember = "name";
                this.tableCB.ValueMember = "id";
            }
        }

        private void tableCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvMapping.Rows.Clear();

            string tableName = this.tableCB.Text.Trim().ToLower();
            int tableID = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[0].ToString().Trim().ToLower().Equals(tableName))
                {
                    tableID = Convert.ToInt32(dr[1]);
                    break;
                }
            }
            DataTable dt = this.FindTableProperty(tableID);

            List<MappingInfo> lmi = new List<MappingInfo>();
            MappingInfo mi;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mi = new MappingInfo();

                DataTable PKcolid = this.FindPKorUQ(tableID, true);
                DataTable UQcolid = this.FindPKorUQ(tableID, false);
                if (PKcolid.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in PKcolid.Rows)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["ColID"]) == Convert.ToInt32(dr1[0]))
                        {
                            mi.IsPK = true;
                        }
                    }
                }
                if (UQcolid.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in UQcolid.Rows)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["ColID"]) == Convert.ToInt32(dr2[0]))
                        {
                            mi.IsUnique = true;
                        }
                    }
                }
                if (dt.Rows[i]["AutoVal"].ToString() == "")
                {
                    mi.PKGenerator = Generator.assigned.ToString();
                }
                else
                {
                    mi.PKGenerator = Generator.identity.ToString();
                }
                mi.TableColumnName = dt.Rows[i]["ColName"].ToString();
                mi.ClassPropertyName = mi.TableColumnName;
                mi.DataType = this.MappingDataType(dt.Rows[i]["DataType"].ToString());
                if (mi.DataType.Equals(DataType.String.ToString()))
                {
                    mi.DataLength = Convert.ToInt32(dt.Rows[i]["Lenth"]);
                }
                if (dt.Rows[i]["IsNullable"].ToString() == "1")
                {
                    mi.IsNullable = true;
                }
                else
                {
                    mi.IsNullable = false;
                }

                //用datasource存在问题,无法以编程方式直接清除行集合,故直接添加
                this.FillMappingInfo(mi);
            }

        }

        private DataTable FindTableProperty(int tableID)
        {
            string sql = "select c.name as ColName,t.name as DataType,c.colid as ColID,c.autoval as AutoVal,c.length as Lenth,c.isnullable as IsNullable from syscolumns c,systypes t where c.xtype=t.xtype and c.xusertype=t.xusertype and c.id=" + tableID.ToString() + " order by c.colid";

            SqlDataAdapter da = this.SqlConn(sql);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            return dt;
        }

        private string MappingDataType(string inType)
        {
            string outType;
            switch (inType.ToLower())
            {
                case "nvarchar":
                    outType = DataType.String.ToString();
                    break;
                case "varchar":
                    outType = DataType.String.ToString();
                    break;
                case "ntext":
                    outType = DataType.String.ToString();
                    break;
                case "text":
                    outType = DataType.String.ToString();
                    break;
                case "int":
                    outType = DataType.Int32.ToString();
                    break;
                case "tinyint":
                    outType = DataType.Int16.ToString();
                    break;
                case "double":
                    outType = DataType.Double.ToString();
                    break;
                case "float":
                    outType = DataType.Double.ToString();
                    break;
                case "decimal":
                    outType = DataType.Decimal.ToString();
                    break;
                case "numeric":
                    outType = DataType.Decimal.ToString();
                    break;
                case "money":
                    outType = DataType.Decimal.ToString();
                    break;
                case "datetime":
                    outType = DataType.DateTime.ToString();
                    break;
                case "timestamp":
                    outType = DataType.DateTime.ToString();
                    break;
                case "bit":
                    outType = DataType.Boolean.ToString();
                    break;
                case "image":
                    outType = DataType.BinaryBlob.ToString();
                    break;
                default:
                    outType = "";
                    break;
            }
            return outType;
        }

        private void FillMappingInfo(MappingInfo mi)
        {
            DataGridViewCheckBoxCell cell0 = new DataGridViewCheckBoxCell();
            cell0.Value = mi.IsPK;

            DataGridViewCheckBoxCell cell1 = new DataGridViewCheckBoxCell();
            cell1.Value = mi.IsUnique;

            DataGridViewComboBoxCell cell2 = new DataGridViewComboBoxCell();
            foreach (string generator in Enum.GetNames(typeof(Generator)))
            {
                cell2.Items.Add(generator);
            }
            cell2.Value = mi.PKGenerator;

            DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
            cell3.Value = mi.ClassPropertyName;

            DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
            cell4.Value = mi.TableColumnName;

            DataGridViewComboBoxCell cell5 = new DataGridViewComboBoxCell();
            foreach (string dataTypeName in Enum.GetNames(typeof(DataType)))
            {
                cell5.Items.Add(dataTypeName);
            }
            foreach (Type type in typeof(EntityBase).Assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    cell5.Items.Add(type.FullName);
                }
            }
            cell5.Value = mi.DataType;

            DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
            cell6.Value = mi.DataLength.ToString();

            DataGridViewCheckBoxCell cell7 = new DataGridViewCheckBoxCell();
            cell7.Value = mi.IsNullable;

            DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
            cell8.Value = mi.PKMany2OnePropertyName;

            DataGridViewComboBoxCell cell9 = new DataGridViewComboBoxCell();
            foreach (string dataTypeName in Enum.GetNames(typeof(DataType)))
            {
                cell9.Items.Add(dataTypeName);
            }
            cell9.Value = mi.PKMany2OnePropertyDataType;

            DataGridViewCheckBoxCell cell10 = new DataGridViewCheckBoxCell();
            cell10.Value = mi.IsOne2Many;

            DataGridViewTextBoxCell cell11 = new DataGridViewTextBoxCell();
            cell11.Value = mi.One2ManyTable;

            DataGridViewTextBoxCell cell12 = new DataGridViewTextBoxCell();
            cell12.Value = mi.One2ManyColumn;

            DataGridViewCheckBoxCell cell13 = new DataGridViewCheckBoxCell();
            cell13.Value = mi.One2ManyInverse;

            DataGridViewCheckBoxCell cell14 = new DataGridViewCheckBoxCell();
            cell14.Value = mi.One2ManyLazy;


            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(cell0);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            row.Cells.Add(cell7);
            row.Cells.Add(cell8);
            row.Cells.Add(cell9);
            row.Cells.Add(cell10);
            row.Cells.Add(cell11);
            row.Cells.Add(cell12);
            row.Cells.Add(cell13);
            row.Cells.Add(cell14);
            this.dgvMapping.Rows.Add(row);
        }

        //由于CodeSmith模板原因, unique只返回第一条记录
        private DataTable FindPKorUQ(int tableid, bool IsPK)
        {
            DataTable PKcolid = new DataTable();

            string para = IsPK ? "PK" : "UQ";
            string sql = "select a.name as Name,a.indid as IndexID,b.xtype as XType from sysindexes a,sysobjects b where a.id ='" + tableid + "' and b.parent_obj=a.id and a.name = b.name and b.xtype = '" + para + "' ";

            SqlDataAdapter da = this.SqlConn(sql);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int indid = Convert.ToInt32(dr["IndexID"]);
                    sql = "select colid from sysindexkeys where id = " + tableid + " and indid = " + indid;
                    da = this.SqlConn(sql);

                    try
                    {
                        da.Fill(PKcolid);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
            }

            return PKcolid;
        }
    }
}