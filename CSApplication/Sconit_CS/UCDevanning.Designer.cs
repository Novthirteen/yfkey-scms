namespace Sconit_CS
{
    partial class UCDevanning
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
            this.outHuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPickUp = new System.Windows.Forms.GroupBox();
            this.btnDevanning = new System.Windows.Forms.Button();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgOutList = new System.Windows.Forms.DataGridView();
            this.outItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outUomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outUnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbOut = new System.Windows.Forms.GroupBox();
            this.inUnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inUomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbIn = new System.Windows.Forms.GroupBox();
            this.dgInList = new System.Windows.Forms.DataGridView();
            this.inHuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPickUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOutList)).BeginInit();
            this.gbOut.SuspendLayout();
            this.gbIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInList)).BeginInit();
            this.SuspendLayout();
            // 
            // outHuId
            // 
            this.outHuId.DataPropertyName = "HuId";
            this.outHuId.FillWeight = 50F;
            this.outHuId.HeaderText = "条码";
            this.outHuId.Name = "outHuId";
            this.outHuId.ReadOnly = true;
            // 
            // gbPickUp
            // 
            this.gbPickUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPickUp.Controls.Add(this.btnDevanning);
            this.gbPickUp.Controls.Add(this.tbBarCode);
            this.gbPickUp.Controls.Add(this.lblBarCode);
            this.gbPickUp.Controls.Add(this.lblMessage);
            this.gbPickUp.Location = new System.Drawing.Point(151, 1);
            this.gbPickUp.Name = "gbPickUp";
            this.gbPickUp.Size = new System.Drawing.Size(737, 78);
            this.gbPickUp.TabIndex = 2;
            this.gbPickUp.TabStop = false;
            // 
            // btnDevanning
            // 
            this.btnDevanning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevanning.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDevanning.Location = new System.Drawing.Point(632, 17);
            this.btnDevanning.Name = "btnDevanning";
            this.btnDevanning.Size = new System.Drawing.Size(88, 45);
            this.btnDevanning.TabIndex = 2;
            this.btnDevanning.Text = "拆  箱";
            this.btnDevanning.UseVisualStyleBackColor = true;
            this.btnDevanning.Click += new System.EventHandler(this.btnDevanning_Click);
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("Arial", 20F);
            this.tbBarCode.Location = new System.Drawing.Point(135, 23);
            this.tbBarCode.MaxLength = 50;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(380, 38);
            this.tbBarCode.TabIndex = 1;
            this.tbBarCode.TextChanged += new System.EventHandler(this.tbBarCode_TextChanged);
            this.tbBarCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbBarCode_KeyUp);
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBarCode.Location = new System.Drawing.Point(17, 29);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(122, 28);
            this.lblBarCode.TabIndex = 0;
            this.lblBarCode.Text = "拆箱前条码:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(513, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(96, 27);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            // 
            // dgOutList
            // 
            this.dgOutList.AllowUserToAddRows = false;
            this.dgOutList.AllowUserToDeleteRows = false;
            this.dgOutList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgOutList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgOutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOutList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.outHuId,
            this.outItemDescription,
            this.outUomCode,
            this.outUnitCount,
            this.outQty});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Format = "0.########";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOutList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgOutList.Location = new System.Drawing.Point(6, 28);
            this.dgOutList.Name = "dgOutList";
            this.dgOutList.ReadOnly = true;
            this.dgOutList.RowHeadersVisible = false;
            this.dgOutList.RowTemplate.Height = 23;
            this.dgOutList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOutList.Size = new System.Drawing.Size(861, 259);
            this.dgOutList.TabIndex = 0;
            // 
            // outItemDescription
            // 
            this.outItemDescription.DataPropertyName = "ItemDescription";
            this.outItemDescription.FillWeight = 50F;
            this.outItemDescription.HeaderText = "物料描述";
            this.outItemDescription.Name = "outItemDescription";
            this.outItemDescription.ReadOnly = true;
            // 
            // outUomCode
            // 
            this.outUomCode.DataPropertyName = "UomCode";
            this.outUomCode.FillWeight = 25F;
            this.outUomCode.HeaderText = "单位";
            this.outUomCode.Name = "outUomCode";
            this.outUomCode.ReadOnly = true;
            // 
            // outUnitCount
            // 
            this.outUnitCount.DataPropertyName = "UnitCount";
            this.outUnitCount.FillWeight = 30F;
            this.outUnitCount.HeaderText = "单包装";
            this.outUnitCount.Name = "outUnitCount";
            this.outUnitCount.ReadOnly = true;
            // 
            // outQty
            // 
            this.outQty.DataPropertyName = "Qty";
            this.outQty.FillWeight = 40F;
            this.outQty.HeaderText = "数量";
            this.outQty.Name = "outQty";
            this.outQty.ReadOnly = true;
            // 
            // inQty
            // 
            this.inQty.DataPropertyName = "Qty";
            this.inQty.FillWeight = 40F;
            this.inQty.HeaderText = "数量";
            this.inQty.Name = "inQty";
            this.inQty.ReadOnly = true;
            // 
            // gbOut
            // 
            this.gbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOut.Controls.Add(this.dgOutList);
            this.gbOut.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbOut.Location = new System.Drawing.Point(15, 204);
            this.gbOut.Name = "gbOut";
            this.gbOut.Size = new System.Drawing.Size(873, 293);
            this.gbOut.TabIndex = 0;
            this.gbOut.TabStop = false;
            this.gbOut.Text = "拆箱后";
            // 
            // inUnitCount
            // 
            this.inUnitCount.DataPropertyName = "UnitCount";
            this.inUnitCount.FillWeight = 30F;
            this.inUnitCount.HeaderText = "单包装";
            this.inUnitCount.Name = "inUnitCount";
            this.inUnitCount.ReadOnly = true;
            // 
            // inUomCode
            // 
            this.inUomCode.DataPropertyName = "UomCode";
            this.inUomCode.FillWeight = 25F;
            this.inUomCode.HeaderText = "单位";
            this.inUomCode.Name = "inUomCode";
            this.inUomCode.ReadOnly = true;
            // 
            // inItemDescription
            // 
            this.inItemDescription.DataPropertyName = "ItemDescription";
            this.inItemDescription.FillWeight = 50F;
            this.inItemDescription.HeaderText = "物料描述";
            this.inItemDescription.Name = "inItemDescription";
            this.inItemDescription.ReadOnly = true;
            // 
            // gbIn
            // 
            this.gbIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbIn.Controls.Add(this.dgInList);
            this.gbIn.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbIn.Location = new System.Drawing.Point(15, 85);
            this.gbIn.Name = "gbIn";
            this.gbIn.Size = new System.Drawing.Size(873, 99);
            this.gbIn.TabIndex = 0;
            this.gbIn.TabStop = false;
            this.gbIn.Text = "拆箱前";
            // 
            // dgInList
            // 
            this.dgInList.AllowUserToAddRows = false;
            this.dgInList.AllowUserToDeleteRows = false;
            this.dgInList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgInList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.inHuId,
            this.inItemDescription,
            this.inUomCode,
            this.inUnitCount,
            this.inQty});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "0.########";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgInList.Location = new System.Drawing.Point(6, 28);
            this.dgInList.Name = "dgInList";
            this.dgInList.ReadOnly = true;
            this.dgInList.RowHeadersVisible = false;
            this.dgInList.RowTemplate.Height = 23;
            this.dgInList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInList.Size = new System.Drawing.Size(861, 65);
            this.dgInList.TabIndex = 0;
            // 
            // inHuId
            // 
            this.inHuId.DataPropertyName = "HuId";
            this.inHuId.FillWeight = 50F;
            this.inHuId.HeaderText = "条码";
            this.inHuId.Name = "inHuId";
            this.inHuId.ReadOnly = true;
            // 
            // UCDevanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOut);
            this.Controls.Add(this.gbIn);
            this.Controls.Add(this.gbPickUp);
            this.Name = "UCDevanning";
            this.Size = new System.Drawing.Size(900, 500);
            this.gbPickUp.ResumeLayout(false);
            this.gbPickUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOutList)).EndInit();
            this.gbOut.ResumeLayout(false);
            this.gbIn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn outHuId;
        private System.Windows.Forms.GroupBox gbPickUp;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnDevanning;
        private System.Windows.Forms.TextBox tbBarCode;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.DataGridView dgOutList;
        private System.Windows.Forms.DataGridViewTextBoxColumn outItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn outUomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn outUnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn outQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn inQty;
        private System.Windows.Forms.GroupBox gbOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn inUnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn inUomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn inItemDescription;
        private System.Windows.Forms.GroupBox gbIn;
        private System.Windows.Forms.DataGridView dgInList;
        private System.Windows.Forms.DataGridViewTextBoxColumn inHuId;
    }
}
