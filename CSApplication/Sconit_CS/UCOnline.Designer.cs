namespace Sconit_CS
{
    partial class UCOnline
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
            this.lblWO = new System.Windows.Forms.Label();
            this.gbWO = new System.Windows.Forms.GroupBox();
            this.btnOnline = new System.Windows.Forms.Button();
            this.lblmessage = new System.Windows.Forms.Label();
            this.tbWO = new System.Windows.Forms.TextBox();
            this.gbWODetail = new System.Windows.Forms.GroupBox();
            this.gvWODetail = new System.Windows.Forms.DataGridView();
            this.WONo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnlineDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbWO.SuspendLayout();
            this.gbWODetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWODetail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWO
            // 
            this.lblWO.AutoSize = true;
            this.lblWO.Font = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWO.Location = new System.Drawing.Point(8, 17);
            this.lblWO.Name = "lblWO";
            this.lblWO.Size = new System.Drawing.Size(111, 38);
            this.lblWO.TabIndex = 0;
            this.lblWO.Text = "工单号:";
            // 
            // gbWO
            // 
            this.gbWO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWO.Controls.Add(this.btnOnline);
            this.gbWO.Controls.Add(this.lblmessage);
            this.gbWO.Controls.Add(this.tbWO);
            this.gbWO.Controls.Add(this.lblWO);
            this.gbWO.Location = new System.Drawing.Point(121, 7);
            this.gbWO.Name = "gbWO";
            this.gbWO.Size = new System.Drawing.Size(890, 70);
            this.gbWO.TabIndex = 0;
            this.gbWO.TabStop = false;
            // 
            // btnOnline
            // 
            this.btnOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOnline.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOnline.Location = new System.Drawing.Point(712, 16);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(163, 45);
            this.btnOnline.TabIndex = 2;
            this.btnOnline.Text = "上  线";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblmessage.ForeColor = System.Drawing.Color.Black;
            this.lblmessage.Location = new System.Drawing.Point(507, 26);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(95, 27);
            this.lblmessage.TabIndex = 0;
            this.lblmessage.Text = "message";
            this.lblmessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbWO
            // 
            this.tbWO.Font = new System.Drawing.Font("Arial", 20F);
            this.tbWO.Location = new System.Drawing.Point(123, 19);
            this.tbWO.MaxLength = 50;
            this.tbWO.Name = "tbWO";
            this.tbWO.Size = new System.Drawing.Size(380, 38);
            this.tbWO.TabIndex = 1;
            this.tbWO.TextChanged += new System.EventHandler(this.tbWO_TextChanged);
            this.tbWO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWO_KeyPress);
            // 
            // gbWODetail
            // 
            this.gbWODetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWODetail.Controls.Add(this.gvWODetail);
            this.gbWODetail.Location = new System.Drawing.Point(17, 83);
            this.gbWODetail.Name = "gbWODetail";
            this.gbWODetail.Size = new System.Drawing.Size(994, 501);
            this.gbWODetail.TabIndex = 0;
            this.gbWODetail.TabStop = false;
            // 
            // gvWODetail
            // 
            this.gvWODetail.AllowUserToAddRows = false;
            this.gvWODetail.AllowUserToDeleteRows = false;
            this.gvWODetail.AllowUserToResizeRows = false;
            this.gvWODetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWODetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvWODetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWODetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WONo,
            this.OnlineDate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvWODetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvWODetail.Location = new System.Drawing.Point(14, 20);
            this.gvWODetail.Name = "gvWODetail";
            this.gvWODetail.ReadOnly = true;
            this.gvWODetail.RowHeadersVisible = false;
            this.gvWODetail.RowTemplate.Height = 40;
            this.gvWODetail.Size = new System.Drawing.Size(965, 464);
            this.gvWODetail.TabIndex = 0;
            // 
            // WONo
            // 
            this.WONo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WONo.DataPropertyName = "WONo";
            this.WONo.HeaderText = "工单号";
            this.WONo.MinimumWidth = 40;
            this.WONo.Name = "WONo";
            this.WONo.ReadOnly = true;
            this.WONo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OnlineDate
            // 
            this.OnlineDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OnlineDate.DataPropertyName = "OnlineDate";
            this.OnlineDate.HeaderText = "上线时间";
            this.OnlineDate.MinimumWidth = 400;
            this.OnlineDate.Name = "OnlineDate";
            this.OnlineDate.ReadOnly = true;
            this.OnlineDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UCOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbWODetail);
            this.Controls.Add(this.gbWO);
            this.Name = "UCOnline";
            this.Size = new System.Drawing.Size(1024, 600);
            this.Load += new System.EventHandler(this.UCWOScanOnline_Load);
            this.gbWO.ResumeLayout(false);
            this.gbWO.PerformLayout();
            this.gbWODetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWODetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWO;
        private System.Windows.Forms.GroupBox gbWO;
        private System.Windows.Forms.TextBox tbWO;
        private System.Windows.Forms.GroupBox gbWODetail;
        private System.Windows.Forms.DataGridView gvWODetail;
        private System.Windows.Forms.Label lblmessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn WONo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnlineDate;
        private System.Windows.Forms.Button btnOnline;
    }
}
