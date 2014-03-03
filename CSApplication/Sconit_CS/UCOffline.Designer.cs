namespace Sconit_CS
{
    partial class UCOffline
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbWO = new System.Windows.Forms.GroupBox();
            this.btnOffline = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tbWO = new System.Windows.Forms.TextBox();
            this.lblWO = new System.Windows.Forms.Label();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.gvWODetail = new Sconit_CS.CustomDataGridView();
            this.gbEmployee = new System.Windows.Forms.GroupBox();
            this.lblEmployeeMessage = new System.Windows.Forms.Label();
            this.gvEmployee = new System.Windows.Forms.DataGridView();
            this.EmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeWorkingHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lblHours = new System.Windows.Forms.Label();
            this.tbEmployee = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScrapQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RejectedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbWO.SuspendLayout();
            this.gbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWODetail)).BeginInit();
            this.gbEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // gbWO
            // 
            this.gbWO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWO.Controls.Add(this.btnOffline);
            this.gbWO.Controls.Add(this.lblMessage);
            this.gbWO.Controls.Add(this.tbWO);
            this.gbWO.Controls.Add(this.lblWO);
            this.gbWO.Location = new System.Drawing.Point(130, 14);
            this.gbWO.Name = "gbWO";
            this.gbWO.Size = new System.Drawing.Size(755, 64);
            this.gbWO.TabIndex = 0;
            this.gbWO.TabStop = false;
            // 
            // btnOffline
            // 
            this.btnOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOffline.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOffline.Location = new System.Drawing.Point(652, 14);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(88, 45);
            this.btnOffline.TabIndex = 5;
            this.btnOffline.Text = "下  线";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(473, 24);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(95, 27);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "message";
            // 
            // tbWO
            // 
            this.tbWO.Font = new System.Drawing.Font("Arial", 20F);
            this.tbWO.Location = new System.Drawing.Point(89, 17);
            this.tbWO.MaxLength = 50;
            this.tbWO.Name = "tbWO";
            this.tbWO.Size = new System.Drawing.Size(380, 38);
            this.tbWO.TabIndex = 1;
            // 
            // lblWO
            // 
            this.lblWO.AutoSize = true;
            this.lblWO.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWO.Location = new System.Drawing.Point(8, 24);
            this.lblWO.Name = "lblWO";
            this.lblWO.Size = new System.Drawing.Size(77, 27);
            this.lblWO.TabIndex = 0;
            this.lblWO.Text = "订单号:";
            // 
            // gbDetail
            // 
            this.gbDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetail.Controls.Add(this.gvWODetail);
            this.gbDetail.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbDetail.Location = new System.Drawing.Point(16, 84);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(869, 398);
            this.gbDetail.TabIndex = 0;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "产品信息";
            // 
            // gvWODetail
            // 
            this.gvWODetail.AllowUserToAddRows = false;
            this.gvWODetail.AllowUserToDeleteRows = false;
            this.gvWODetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvWODetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWODetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemDescription,
            this.UomCode,
            this.UnitCount,
            this.OrderedQty,
            this.ReceivedQty,
            this.ScrapQty,
            this.RejectedQty,
            this.CurrentQty});
            this.gvWODetail.Location = new System.Drawing.Point(17, 28);
            this.gvWODetail.MultiSelect = false;
            this.gvWODetail.Name = "gvWODetail";
            this.gvWODetail.ReadOnly = true;
            this.gvWODetail.RowHeadersVisible = false;
            dataGridViewCellStyle1.Format = "0.########";
            dataGridViewCellStyle1.NullValue = null;
            this.gvWODetail.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvWODetail.RowTemplate.Height = 23;
            this.gvWODetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvWODetail.Size = new System.Drawing.Size(837, 354);
            this.gvWODetail.TabIndex = 2;
            this.gvWODetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvWODetail_EditingControlShowing);
            this.gvWODetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvWODetail_KeyDown);
            // 
            // gbEmployee
            // 
            this.gbEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEmployee.Controls.Add(this.lblEmployeeMessage);
            this.gbEmployee.Controls.Add(this.gvEmployee);
            this.gbEmployee.Controls.Add(this.tbHours);
            this.gbEmployee.Controls.Add(this.lblHours);
            this.gbEmployee.Controls.Add(this.tbEmployee);
            this.gbEmployee.Controls.Add(this.lblEmployee);
            this.gbEmployee.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbEmployee.Location = new System.Drawing.Point(16, 333);
            this.gbEmployee.Name = "gbEmployee";
            this.gbEmployee.Size = new System.Drawing.Size(869, 149);
            this.gbEmployee.TabIndex = 0;
            this.gbEmployee.TabStop = false;
            this.gbEmployee.Text = "人员信息";
            this.gbEmployee.Visible = false;
            // 
            // lblEmployeeMessage
            // 
            this.lblEmployeeMessage.AutoSize = true;
            this.lblEmployeeMessage.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployeeMessage.ForeColor = System.Drawing.Color.Black;
            this.lblEmployeeMessage.Location = new System.Drawing.Point(18, 102);
            this.lblEmployeeMessage.Name = "lblEmployeeMessage";
            this.lblEmployeeMessage.Size = new System.Drawing.Size(95, 27);
            this.lblEmployeeMessage.TabIndex = 5;
            this.lblEmployeeMessage.Text = "message";
            // 
            // gvEmployee
            // 
            this.gvEmployee.AllowUserToAddRows = false;
            this.gvEmployee.AllowUserToDeleteRows = false;
            this.gvEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeCode,
            this.EmployeeName,
            this.EmployeeWorkingHours,
            this.ScanTime});
            this.gvEmployee.Location = new System.Drawing.Point(540, 23);
            this.gvEmployee.Name = "gvEmployee";
            this.gvEmployee.RowHeadersVisible = false;
            this.gvEmployee.RowTemplate.Height = 23;
            this.gvEmployee.Size = new System.Drawing.Size(314, 120);
            this.gvEmployee.TabIndex = 0;
            // 
            // EmployeeCode
            // 
            this.EmployeeCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeCode.DataPropertyName = "EmployeeCode";
            this.EmployeeCode.HeaderText = "工号";
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.ReadOnly = true;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "姓名";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            // 
            // EmployeeWorkingHours
            // 
            this.EmployeeWorkingHours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeWorkingHours.DataPropertyName = "EmployeeWorkingHours";
            this.EmployeeWorkingHours.HeaderText = "工时";
            this.EmployeeWorkingHours.Name = "EmployeeWorkingHours";
            this.EmployeeWorkingHours.ReadOnly = true;
            // 
            // ScanTime
            // 
            this.ScanTime.DataPropertyName = "ScanTime";
            this.ScanTime.HeaderText = "ScanTime";
            this.ScanTime.Name = "ScanTime";
            this.ScanTime.ReadOnly = true;
            this.ScanTime.Visible = false;
            // 
            // tbHours
            // 
            this.tbHours.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHours.Location = new System.Drawing.Point(335, 40);
            this.tbHours.MaxLength = 10;
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(170, 45);
            this.tbHours.TabIndex = 4;
            this.tbHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbHours_KeyPress);
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHours.Location = new System.Drawing.Point(282, 49);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(57, 27);
            this.lblHours.TabIndex = 0;
            this.lblHours.Text = "工时:";
            // 
            // tbEmployee
            // 
            this.tbEmployee.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmployee.Location = new System.Drawing.Point(70, 40);
            this.tbEmployee.MaxLength = 20;
            this.tbEmployee.Name = "tbEmployee";
            this.tbEmployee.Size = new System.Drawing.Size(175, 45);
            this.tbEmployee.TabIndex = 3;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployee.Location = new System.Drawing.Point(7, 49);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(57, 27);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "工号:";
            // 
            // ItemCode
            // 
            this.ItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.FillWeight = 110F;
            this.ItemCode.HeaderText = "物料号";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemDescription
            // 
            this.ItemDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemDescription.DataPropertyName = "ItemDescription";
            this.ItemDescription.FillWeight = 180F;
            this.ItemDescription.HeaderText = "描述";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            this.ItemDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UomCode
            // 
            this.UomCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UomCode.DataPropertyName = "UomCode";
            this.UomCode.FillWeight = 50F;
            this.UomCode.HeaderText = "单位";
            this.UomCode.Name = "UomCode";
            this.UomCode.ReadOnly = true;
            this.UomCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitCount
            // 
            this.UnitCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitCount.DataPropertyName = "UnitCount";
            this.UnitCount.FillWeight = 70F;
            this.UnitCount.HeaderText = "单包装";
            this.UnitCount.Name = "UnitCount";
            this.UnitCount.ReadOnly = true;
            this.UnitCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OrderedQty
            // 
            this.OrderedQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OrderedQty.DataPropertyName = "OrderedQty";
            this.OrderedQty.FillWeight = 80F;
            this.OrderedQty.HeaderText = "订单数";
            this.OrderedQty.Name = "OrderedQty";
            this.OrderedQty.ReadOnly = true;
            this.OrderedQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ReceivedQty
            // 
            this.ReceivedQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReceivedQty.DataPropertyName = "ReceivedQty";
            this.ReceivedQty.FillWeight = 80F;
            this.ReceivedQty.HeaderText = "已收数";
            this.ReceivedQty.Name = "ReceivedQty";
            this.ReceivedQty.ReadOnly = true;
            this.ReceivedQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScrapQty
            // 
            this.ScrapQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ScrapQty.DataPropertyName = "ScrapQty";
            this.ScrapQty.FillWeight = 60F;
            this.ScrapQty.HeaderText = "废品数";
            this.ScrapQty.Name = "ScrapQty";
            this.ScrapQty.ReadOnly = true;
            this.ScrapQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ScrapQty.Visible = false;
            // 
            // RejectedQty
            // 
            this.RejectedQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RejectedQty.DataPropertyName = "RejectedQty";
            this.RejectedQty.FillWeight = 60F;
            this.RejectedQty.HeaderText = "次品数";
            this.RejectedQty.Name = "RejectedQty";
            this.RejectedQty.ReadOnly = true;
            this.RejectedQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RejectedQty.Visible = false;
            // 
            // CurrentQty
            // 
            this.CurrentQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CurrentQty.DataPropertyName = "CurrentQty";
            this.CurrentQty.FillWeight = 80F;
            this.CurrentQty.HeaderText = "收货数";
            this.CurrentQty.Name = "CurrentQty";
            this.CurrentQty.ReadOnly = true;
            this.CurrentQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UCOffline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDetail);
            this.Controls.Add(this.gbWO);
            this.Controls.Add(this.gbEmployee);
            this.Name = "UCOffline";
            this.Size = new System.Drawing.Size(900, 500);
            this.Load += new System.EventHandler(this.UCWOScanOffline_Load);
            this.gbWO.ResumeLayout(false);
            this.gbWO.PerformLayout();
            this.gbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWODetail)).EndInit();
            this.gbEmployee.ResumeLayout(false);
            this.gbEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbWO;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox tbWO;
        private System.Windows.Forms.Label lblWO;
        private System.Windows.Forms.GroupBox gbDetail;
        private System.Windows.Forms.GroupBox gbEmployee;
        private System.Windows.Forms.TextBox tbEmployee;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.DataGridView gvEmployee;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblEmployeeMessage;
        private CustomDataGridView gvWODetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeWorkingHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanTime;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScrapQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn RejectedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentQty;
    }
}
