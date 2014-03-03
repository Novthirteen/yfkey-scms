namespace com.Sconit.CodeSmith
{
    partial class MappingPropertyEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvMapping = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connBT = new System.Windows.Forms.Button();
            this.tableCB = new System.Windows.Forms.ComboBox();
            this.connTB = new System.Windows.Forms.TextBox();
            this.colIsPK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsUnique = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPKGenerator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colClassPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTableColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDataLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPKMany2OnePropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPKMany2OnePropertyDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colIsOne2Many = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOne2ManyTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOne2ManyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOne2ManyInverse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOne2ManyLazy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(270, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(365, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvMapping
            // 
            this.dgvMapping.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIsPK,
            this.colIsUnique,
            this.colPKGenerator,
            this.colClassPropertyName,
            this.colTableColumnName,
            this.colDataType,
            this.colDataLength,
            this.colIsNullable,
            this.colPKMany2OnePropertyName,
            this.colPKMany2OnePropertyDataType,
            this.colIsOne2Many,
            this.colOne2ManyTable,
            this.colOne2ManyColumn,
            this.colOne2ManyInverse,
            this.colOne2ManyLazy});
            this.dgvMapping.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMapping.Location = new System.Drawing.Point(0, 162);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMapping.RowTemplate.Height = 23;
            this.dgvMapping.Size = new System.Drawing.Size(1284, 567);
            this.dgvMapping.TabIndex = 3;
            this.dgvMapping.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvMapping_RowsAdded);
            this.dgvMapping.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMapping_DataError);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(51, 137);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(64, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Connection:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Table:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connBT);
            this.groupBox1.Controls.Add(this.tableCB);
            this.groupBox1.Location = new System.Drawing.Point(43, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // connBT
            // 
            this.connBT.Location = new System.Drawing.Point(592, 17);
            this.connBT.Name = "connBT";
            this.connBT.Size = new System.Drawing.Size(69, 23);
            this.connBT.TabIndex = 9;
            this.connBT.Text = "Connect";
            this.connBT.UseVisualStyleBackColor = true;
            this.connBT.Click += new System.EventHandler(this.connBT_Click);
            // 
            // tableCB
            // 
            this.tableCB.FormattingEnabled = true;
            this.tableCB.Location = new System.Drawing.Point(93, 49);
            this.tableCB.Name = "tableCB";
            this.tableCB.Size = new System.Drawing.Size(209, 20);
            this.tableCB.TabIndex = 0;
            this.tableCB.SelectedIndexChanged += new System.EventHandler(this.tableCB_SelectedIndexChanged);
            // 
            // connTB
            // 
            this.connTB.Location = new System.Drawing.Point(136, 22);
            this.connTB.Name = "connTB";
            this.connTB.Size = new System.Drawing.Size(491, 21);
            this.connTB.TabIndex = 8;
            this.connTB.Text = "data source=10.20.96.66;user id=sconittest;password=sconittest;database=sconit_tes" +
                "t";
            // 
            // colIsPK
            // 
            this.colIsPK.DataPropertyName = "IsPK";
            this.colIsPK.HeaderText = "Is PK";
            this.colIsPK.Name = "colIsPK";
            this.colIsPK.Width = 41;
            // 
            // colIsUnique
            // 
            this.colIsUnique.HeaderText = "Is Unique";
            this.colIsUnique.Name = "colIsUnique";
            this.colIsUnique.Width = 65;
            // 
            // colPKGenerator
            // 
            this.colPKGenerator.HeaderText = "PK Generator";
            this.colPKGenerator.Name = "colPKGenerator";
            this.colPKGenerator.Width = 75;
            // 
            // colClassPropertyName
            // 
            this.colClassPropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colClassPropertyName.DataPropertyName = "ClassPropertyName";
            this.colClassPropertyName.HeaderText = "Class Property Name";
            this.colClassPropertyName.Name = "colClassPropertyName";
            this.colClassPropertyName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colClassPropertyName.Width = 150;
            // 
            // colTableColumnName
            // 
            this.colTableColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTableColumnName.DataPropertyName = "TableColumnName";
            this.colTableColumnName.FillWeight = 200F;
            this.colTableColumnName.HeaderText = "Table Column Name";
            this.colTableColumnName.Name = "colTableColumnName";
            this.colTableColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTableColumnName.Width = 150;
            // 
            // colDataType
            // 
            this.colDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDataType.DataPropertyName = "DataType";
            this.colDataType.FillWeight = 350F;
            this.colDataType.HeaderText = "Data Type";
            this.colDataType.Name = "colDataType";
            this.colDataType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDataType.Width = 350;
            // 
            // colDataLength
            // 
            this.colDataLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDataLength.DataPropertyName = "DataLength";
            this.colDataLength.HeaderText = "DataLength";
            this.colDataLength.Name = "colDataLength";
            this.colDataLength.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDataLength.Width = 80;
            // 
            // colIsNullable
            // 
            this.colIsNullable.DataPropertyName = "IsNullable";
            this.colIsNullable.HeaderText = "Is Nullable";
            this.colIsNullable.Name = "colIsNullable";
            this.colIsNullable.Width = 69;
            // 
            // colPKMany2OnePropertyName
            // 
            this.colPKMany2OnePropertyName.HeaderText = "PK Many 2 One Property Name";
            this.colPKMany2OnePropertyName.Name = "colPKMany2OnePropertyName";
            this.colPKMany2OnePropertyName.Width = 153;
            // 
            // colPKMany2OnePropertyDataType
            // 
            this.colPKMany2OnePropertyDataType.HeaderText = "PK Many 2 One Property Data Type";
            this.colPKMany2OnePropertyDataType.Name = "colPKMany2OnePropertyDataType";
            this.colPKMany2OnePropertyDataType.Width = 134;
            // 
            // colIsOne2Many
            // 
            this.colIsOne2Many.DataPropertyName = "IsOne2Many";
            this.colIsOne2Many.HeaderText = "Is One2Many";
            this.colIsOne2Many.Name = "colIsOne2Many";
            this.colIsOne2Many.Width = 69;
            // 
            // colOne2ManyTable
            // 
            this.colOne2ManyTable.HeaderText = "One2Many Table Name";
            this.colOne2ManyTable.Name = "colOne2ManyTable";
            this.colOne2ManyTable.Width = 110;
            // 
            // colOne2ManyColumn
            // 
            this.colOne2ManyColumn.HeaderText = "One2Many Column Name";
            this.colOne2ManyColumn.Name = "colOne2ManyColumn";
            this.colOne2ManyColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colOne2ManyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOne2ManyColumn.Width = 96;
            // 
            // colOne2ManyInverse
            // 
            this.colOne2ManyInverse.HeaderText = "One2Many Inverse";
            this.colOne2ManyInverse.Name = "colOne2ManyInverse";
            this.colOne2ManyInverse.Width = 59;
            // 
            // colOne2ManyLazy
            // 
            this.colOne2ManyLazy.HeaderText = "One2Many Lazy";
            this.colOne2ManyLazy.Name = "colOne2ManyLazy";
            this.colOne2ManyLazy.Width = 59;
            // 
            // MappingPropertyEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 729);
            this.Controls.Add(this.connTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvMapping);
            this.Name = "MappingPropertyEditorForm";
            this.Text = "MappingPropertyForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MappingPropertyEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvMapping;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox tableCB;
        private System.Windows.Forms.TextBox connTB;
        private System.Windows.Forms.Button connBT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPK;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsUnique;
        private System.Windows.Forms.DataGridViewComboBoxColumn colPKGenerator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassPropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsNullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPKMany2OnePropertyName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colPKMany2OnePropertyDataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsOne2Many;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOne2ManyTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOne2ManyColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colOne2ManyInverse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colOne2ManyLazy;
    }
}