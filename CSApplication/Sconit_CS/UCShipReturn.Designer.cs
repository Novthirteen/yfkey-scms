namespace Sconit_CS
{
    partial class UCShipReturn
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
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.gvHuList = new System.Windows.Forms.DataGridView();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.gbFlow = new System.Windows.Forms.GroupBox();
            this.btnShipReturn = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceHuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuUomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuUnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvHuList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.gbFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDetail
            // 
            this.gbDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetail.Controls.Add(this.gvHuList);
            this.gbDetail.Controls.Add(this.gvList);
            this.gbDetail.Location = new System.Drawing.Point(12, 100);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(849, 392);
            this.gbDetail.TabIndex = 3;
            this.gbDetail.TabStop = false;
            // 
            // gvHuList
            // 
            this.gvHuList.AllowUserToAddRows = false;
            this.gvHuList.AllowUserToDeleteRows = false;
            this.gvHuList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvHuList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHuList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvHuList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHuList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HuId,
            this.ReferenceHuId,
            this.HuItemCode,
            this.HuItemDescription,
            this.HuUomCode,
            this.HuUnitCount,
            this.LotNo,
            this.HuQty});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "0.########";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvHuList.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvHuList.Location = new System.Drawing.Point(17, 14);
            this.gvHuList.Name = "gvHuList";
            this.gvHuList.RowHeadersVisible = false;
            this.gvHuList.RowTemplate.Height = 23;
            this.gvHuList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHuList.Size = new System.Drawing.Size(803, 365);
            this.gvHuList.TabIndex = 2;
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.AllowUserToDeleteRows = false;
            this.gvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemDescription,
            this.ReferenceItemCode,
            this.UomCode,
            this.UnitCount,
            this.LocationFrom,
            this.LocationTo,
            this.CurrentQty,
            this.Cartons});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Format = "0.########";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvList.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvList.Location = new System.Drawing.Point(17, 14);
            this.gvList.Name = "gvList";
            this.gvList.RowHeadersVisible = false;
            this.gvList.RowTemplate.Height = 23;
            this.gvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvList.Size = new System.Drawing.Size(814, 365);
            this.gvList.TabIndex = 1;
            // 
            // gbFlow
            // 
            this.gbFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFlow.Controls.Add(this.btnShipReturn);
            this.gbFlow.Controls.Add(this.lblMessage);
            this.gbFlow.Controls.Add(this.tbBarCode);
            this.gbFlow.Controls.Add(this.lblBarCode);
            this.gbFlow.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbFlow.Location = new System.Drawing.Point(131, 8);
            this.gbFlow.Name = "gbFlow";
            this.gbFlow.Size = new System.Drawing.Size(730, 86);
            this.gbFlow.TabIndex = 2;
            this.gbFlow.TabStop = false;
            // 
            // btnShipReturn
            // 
            this.btnShipReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShipReturn.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShipReturn.Location = new System.Drawing.Point(596, 25);
            this.btnShipReturn.Name = "btnShipReturn";
            this.btnShipReturn.Size = new System.Drawing.Size(118, 45);
            this.btnShipReturn.TabIndex = 3;
            this.btnShipReturn.Text = "发货退货";
            this.btnShipReturn.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(367, 36);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(96, 27);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBarCode.Location = new System.Drawing.Point(61, 25);
            this.tbBarCode.MaxLength = 20;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(300, 45);
            this.tbBarCode.TabIndex = 1;
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Location = new System.Drawing.Point(6, 36);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(57, 27);
            this.lblBarCode.TabIndex = 0;
            this.lblBarCode.Text = "条码:";
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "物料号";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemDescription
            // 
            this.ItemDescription.DataPropertyName = "ItemDescription";
            this.ItemDescription.FillWeight = 150F;
            this.ItemDescription.HeaderText = "物料描述";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            // 
            // ReferenceItemCode
            // 
            this.ReferenceItemCode.DataPropertyName = "ReferenceItemCode";
            this.ReferenceItemCode.HeaderText = "参考物料";
            this.ReferenceItemCode.Name = "ReferenceItemCode";
            this.ReferenceItemCode.ReadOnly = true;
            // 
            // UomCode
            // 
            this.UomCode.DataPropertyName = "UomCode";
            this.UomCode.FillWeight = 60F;
            this.UomCode.HeaderText = "单位";
            this.UomCode.Name = "UomCode";
            this.UomCode.ReadOnly = true;
            // 
            // UnitCount
            // 
            this.UnitCount.DataPropertyName = "UnitCount";
            this.UnitCount.FillWeight = 80F;
            this.UnitCount.HeaderText = "单包装";
            this.UnitCount.Name = "UnitCount";
            this.UnitCount.ReadOnly = true;
            // 
            // LocationFrom
            // 
            this.LocationFrom.DataPropertyName = "LocationFrom";
            this.LocationFrom.HeaderText = "来源库位";
            this.LocationFrom.Name = "LocationFrom";
            this.LocationFrom.ReadOnly = true;
            // 
            // LocationTo
            // 
            this.LocationTo.DataPropertyName = "LocationTo";
            this.LocationTo.HeaderText = "目的库位";
            this.LocationTo.Name = "LocationTo";
            this.LocationTo.ReadOnly = true;
            // 
            // CurrentQty
            // 
            this.CurrentQty.DataPropertyName = "CurrentQty";
            this.CurrentQty.HeaderText = "退货数";
            this.CurrentQty.Name = "CurrentQty";
            this.CurrentQty.ReadOnly = true;
            // 
            // Cartons
            // 
            this.Cartons.DataPropertyName = "Cartons";
            this.Cartons.FillWeight = 60F;
            this.Cartons.HeaderText = "箱数";
            this.Cartons.Name = "Cartons";
            this.Cartons.ReadOnly = true;
            // 
            // HuId
            // 
            this.HuId.DataPropertyName = "HuId";
            this.HuId.FillWeight = 150F;
            this.HuId.HeaderText = "条码";
            this.HuId.Name = "HuId";
            this.HuId.ReadOnly = true;
            // 
            // ReferenceHuId
            // 
            this.ReferenceHuId.DataPropertyName = "ReferenceHuId";
            this.ReferenceHuId.FillWeight = 150F;
            this.ReferenceHuId.HeaderText = "参考条码";
            this.ReferenceHuId.Name = "ReferenceHuId";
            this.ReferenceHuId.ReadOnly = true;
            // 
            // HuItemCode
            // 
            this.HuItemCode.DataPropertyName = "ItemCode";
            this.HuItemCode.HeaderText = "物料号";
            this.HuItemCode.Name = "HuItemCode";
            this.HuItemCode.ReadOnly = true;
            // 
            // HuItemDescription
            // 
            this.HuItemDescription.DataPropertyName = "ItemDescription";
            this.HuItemDescription.FillWeight = 120F;
            this.HuItemDescription.HeaderText = "物料描述";
            this.HuItemDescription.Name = "HuItemDescription";
            this.HuItemDescription.ReadOnly = true;
            // 
            // HuUomCode
            // 
            this.HuUomCode.DataPropertyName = "UomCode";
            this.HuUomCode.FillWeight = 50F;
            this.HuUomCode.HeaderText = "单位";
            this.HuUomCode.Name = "HuUomCode";
            this.HuUomCode.ReadOnly = true;
            // 
            // HuUnitCount
            // 
            this.HuUnitCount.DataPropertyName = "UnitCount";
            this.HuUnitCount.FillWeight = 80F;
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
            // HuQty
            // 
            this.HuQty.DataPropertyName = "CurrentQty";
            this.HuQty.FillWeight = 70F;
            this.HuQty.HeaderText = "数量";
            this.HuQty.Name = "HuQty";
            this.HuQty.ReadOnly = true;
            // 
            // UCShipReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDetail);
            this.Controls.Add(this.gbFlow);
            this.Name = "UCShipReturn";
            this.Size = new System.Drawing.Size(873, 500);
            this.gbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvHuList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.gbFlow.ResumeLayout(false);
            this.gbFlow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDetail;
        private System.Windows.Forms.GroupBox gbFlow;
        private System.Windows.Forms.Button btnShipReturn;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox tbBarCode;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.DataGridView gvHuList;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceHuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuUomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuUnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuQty;
    }
}
