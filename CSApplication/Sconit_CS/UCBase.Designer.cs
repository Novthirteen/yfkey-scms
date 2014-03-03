namespace Sconit_CS
{
    partial class UCBase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.CheckColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationFromCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationToCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentRejectQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvHuList = new System.Windows.Forms.DataGridView();
            this.HuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuUnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StorageBinCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPickUp = new System.Windows.Forms.GroupBox();
            this.btnQualified = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHuList)).BeginInit();
            this.gbPickUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbList
            // 
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.gvList);
            this.gbList.Controls.Add(this.gvHuList);
            this.gbList.Location = new System.Drawing.Point(12, 116);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(849, 376);
            this.gbList.TabIndex = 6;
            this.gbList.TabStop = false;
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.AllowUserToDeleteRows = false;
            this.gvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckColumn,
            this.Operation,
            this.OrderNo,
            this.ItemCode,
            this.ItemDescription,
            this.UomCode,
            this.UnitCount,
            this.LocationCode,
            this.LocationFromCode,
            this.LocationToCode,
            this.BinCode,
            this.LotNumber,
            this.Qty,
            this.OrderedQty,
            this.ReceivedQty,
            this.CurrentQty,
            this.CurrentRejectQty,
            this.Cartons,
            this.Id});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "0.########";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvList.Location = new System.Drawing.Point(18, 17);
            this.gvList.MultiSelect = false;
            this.gvList.Name = "gvList";
            this.gvList.RowHeadersVisible = false;
            this.gvList.RowTemplate.Height = 23;
            this.gvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvList.Size = new System.Drawing.Size(820, 342);
            this.gvList.TabIndex = 1;
            this.gvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellValueChanged);
            this.gvList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvList_CellMouseClick);
            this.gvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellEndEdit);
            this.gvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvList_EditingControlShowing);
            this.gvList.CurrentCellChanged += new System.EventHandler(this.gvList_CurrentCellChanged);
            this.gvList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvList_DataError);
            // 
            // CheckColumn
            // 
            this.CheckColumn.FillWeight = 50F;
            this.CheckColumn.HeaderText = "";
            this.CheckColumn.Name = "CheckColumn";
            this.CheckColumn.Visible = false;
            // 
            // Operation
            // 
            this.Operation.DataPropertyName = "Operation";
            this.Operation.FillWeight = 50F;
            this.Operation.HeaderText = "工序";
            this.Operation.Name = "Operation";
            this.Operation.ReadOnly = true;
            this.Operation.Visible = false;
            // 
            // OrderNo
            // 
            this.OrderNo.DataPropertyName = "OrderNo";
            this.OrderNo.FillWeight = 150F;
            this.OrderNo.HeaderText = "订单号";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.ReadOnly = true;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.FillWeight = 180F;
            this.ItemCode.HeaderText = "物料号";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemDescription
            // 
            this.ItemDescription.DataPropertyName = "ItemDescription";
            this.ItemDescription.FillWeight = 200F;
            this.ItemDescription.HeaderText = "物料描述";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            // 
            // UomCode
            // 
            this.UomCode.DataPropertyName = "UomCode";
            this.UomCode.FillWeight = 50F;
            this.UomCode.HeaderText = "单位";
            this.UomCode.Name = "UomCode";
            this.UomCode.ReadOnly = true;
            this.UomCode.Visible = false;
            // 
            // UnitCount
            // 
            this.UnitCount.DataPropertyName = "UnitCount";
            this.UnitCount.FillWeight = 80F;
            this.UnitCount.HeaderText = "单包装";
            this.UnitCount.Name = "UnitCount";
            this.UnitCount.ReadOnly = true;
            // 
            // LocationCode
            // 
            this.LocationCode.DataPropertyName = "LocationCode";
            this.LocationCode.HeaderText = "库位";
            this.LocationCode.Name = "LocationCode";
            this.LocationCode.ReadOnly = true;
            this.LocationCode.Visible = false;
            // 
            // LocationFromCode
            // 
            this.LocationFromCode.DataPropertyName = "LocationFromCode";
            this.LocationFromCode.HeaderText = "来源库位";
            this.LocationFromCode.Name = "LocationFromCode";
            this.LocationFromCode.ReadOnly = true;
            // 
            // LocationToCode
            // 
            this.LocationToCode.DataPropertyName = "LocationToCode";
            this.LocationToCode.HeaderText = "目的库位";
            this.LocationToCode.Name = "LocationToCode";
            this.LocationToCode.ReadOnly = true;
            // 
            // BinCode
            // 
            this.BinCode.DataPropertyName = "StorageBinCode";
            this.BinCode.FillWeight = 60F;
            this.BinCode.HeaderText = "库格";
            this.BinCode.Name = "BinCode";
            this.BinCode.ReadOnly = true;
            this.BinCode.Visible = false;
            // 
            // LotNumber
            // 
            this.LotNumber.DataPropertyName = "LotNo";
            this.LotNumber.FillWeight = 60F;
            this.LotNumber.HeaderText = "批号";
            this.LotNumber.Name = "LotNumber";
            this.LotNumber.ReadOnly = true;
            this.LotNumber.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.FillWeight = 80F;
            this.Qty.HeaderText = "数量";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // OrderedQty
            // 
            this.OrderedQty.DataPropertyName = "OrderedQty";
            this.OrderedQty.FillWeight = 80F;
            this.OrderedQty.HeaderText = "订单数";
            this.OrderedQty.Name = "OrderedQty";
            this.OrderedQty.ReadOnly = true;
            this.OrderedQty.Visible = false;
            // 
            // ReceivedQty
            // 
            this.ReceivedQty.DataPropertyName = "ReceivedQty";
            this.ReceivedQty.FillWeight = 80F;
            this.ReceivedQty.HeaderText = "已收数";
            this.ReceivedQty.Name = "ReceivedQty";
            this.ReceivedQty.ReadOnly = true;
            this.ReceivedQty.Visible = false;
            // 
            // CurrentQty
            // 
            this.CurrentQty.DataPropertyName = "CurrentQty";
            this.CurrentQty.FillWeight = 80F;
            this.CurrentQty.HeaderText = "当前数量";
            this.CurrentQty.Name = "CurrentQty";
            // 
            // CurrentRejectQty
            // 
            this.CurrentRejectQty.DataPropertyName = "CurrentRejectQty";
            this.CurrentRejectQty.FillWeight = 80F;
            this.CurrentRejectQty.HeaderText = "不合格";
            this.CurrentRejectQty.Name = "CurrentRejectQty";
            this.CurrentRejectQty.Visible = false;
            // 
            // Cartons
            // 
            this.Cartons.DataPropertyName = "Cartons";
            this.Cartons.FillWeight = 50F;
            this.Cartons.HeaderText = "箱数";
            this.Cartons.Name = "Cartons";
            this.Cartons.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "OrderLocTransId";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // gvHuList
            // 
            this.gvHuList.AllowUserToAddRows = false;
            this.gvHuList.AllowUserToDeleteRows = false;
            this.gvHuList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvHuList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHuList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvHuList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHuList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HuId,
            this.HuItemDescription,
            this.HuUnitCount,
            this.LotNo,
            this.StorageBinCode,
            this.HuQty});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Format = "0.########";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvHuList.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvHuList.Location = new System.Drawing.Point(10, 17);
            this.gvHuList.Name = "gvHuList";
            this.gvHuList.RowHeadersVisible = false;
            this.gvHuList.RowTemplate.Height = 23;
            this.gvHuList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHuList.Size = new System.Drawing.Size(822, 342);
            this.gvHuList.TabIndex = 0;
            this.gvHuList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvHuList_CellMouseClick);
            this.gvHuList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvHuList_CellEndEdit);
            this.gvHuList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvList_EditingControlShowing);
            this.gvHuList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvHuList_DataError);
            // 
            // HuId
            // 
            this.HuId.DataPropertyName = "HuId";
            this.HuId.FillWeight = 200F;
            this.HuId.HeaderText = "条码";
            this.HuId.Name = "HuId";
            this.HuId.ReadOnly = true;
            // 
            // HuItemDescription
            // 
            this.HuItemDescription.DataPropertyName = "ItemDescription";
            this.HuItemDescription.FillWeight = 150F;
            this.HuItemDescription.HeaderText = "物料描述";
            this.HuItemDescription.Name = "HuItemDescription";
            this.HuItemDescription.ReadOnly = true;
            // 
            // HuUnitCount
            // 
            this.HuUnitCount.DataPropertyName = "UnitCount";
            this.HuUnitCount.FillWeight = 70F;
            this.HuUnitCount.HeaderText = "单包装";
            this.HuUnitCount.Name = "HuUnitCount";
            this.HuUnitCount.ReadOnly = true;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.FillWeight = 80F;
            this.LotNo.HeaderText = "批号";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            // 
            // StorageBinCode
            // 
            this.StorageBinCode.DataPropertyName = "StorageBinCode";
            this.StorageBinCode.HeaderText = "库格";
            this.StorageBinCode.Name = "StorageBinCode";
            this.StorageBinCode.ReadOnly = true;
            // 
            // HuQty
            // 
            this.HuQty.DataPropertyName = "Qty";
            this.HuQty.FillWeight = 80F;
            this.HuQty.HeaderText = "数量";
            this.HuQty.Name = "HuQty";
            this.HuQty.ReadOnly = true;
            // 
            // gbPickUp
            // 
            this.gbPickUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPickUp.Controls.Add(this.btnQualified);
            this.gbPickUp.Controls.Add(this.btnConfirm);
            this.gbPickUp.Controls.Add(this.tbBarCode);
            this.gbPickUp.Controls.Add(this.lblResult);
            this.gbPickUp.Controls.Add(this.lblBarCode);
            this.gbPickUp.Controls.Add(this.lblMessage);
            this.gbPickUp.Location = new System.Drawing.Point(131, 9);
            this.gbPickUp.Name = "gbPickUp";
            this.gbPickUp.Size = new System.Drawing.Size(730, 101);
            this.gbPickUp.TabIndex = 5;
            this.gbPickUp.TabStop = false;
            // 
            // btnQualified
            // 
            this.btnQualified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQualified.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQualified.Location = new System.Drawing.Point(519, 17);
            this.btnQualified.Name = "btnQualified";
            this.btnQualified.Size = new System.Drawing.Size(76, 45);
            this.btnQualified.TabIndex = 2;
            this.btnQualified.Text = "合格";
            this.btnQualified.UseVisualStyleBackColor = true;
            this.btnQualified.Visible = false;
            this.btnQualified.Click += new System.EventHandler(this.btnQualified_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(601, 17);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(123, 45);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确  定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            this.btnConfirm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UCBase_KeyUp);
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("Arial", 20F);
            this.tbBarCode.Location = new System.Drawing.Point(80, 21);
            this.tbBarCode.MaxLength = 50;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(380, 38);
            this.tbBarCode.TabIndex = 1;
            this.tbBarCode.TextChanged += new System.EventHandler(this.tbBarCode_TextChanged);
            this.tbBarCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbBarCode_KeyUp);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(453, 28);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(66, 25);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "Result";
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBarCode.Location = new System.Drawing.Point(17, 24);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(60, 25);
            this.lblBarCode.TabIndex = 0;
            this.lblBarCode.Text = "条码:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(17, 67);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(93, 25);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            // 
            // UCBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.gbPickUp);
            this.Name = "UCBase";
            this.Size = new System.Drawing.Size(873, 500);
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHuList)).EndInit();
            this.gbPickUp.ResumeLayout(false);
            this.gbPickUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.GroupBox gbPickUp;
        protected System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblBarCode;
        protected System.Windows.Forms.DataGridView gvHuList;
        protected System.Windows.Forms.Button btnConfirm;
        protected System.Windows.Forms.TextBox tbBarCode;
        protected System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuUnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StorageBinCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuQty;
        protected System.Windows.Forms.Button btnQualified;
        protected System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationFromCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationToCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentRejectQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}
