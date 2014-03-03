namespace Sconit_SD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBase));
            this.tbQty = new System.Windows.Forms.TextBox();
            this.dgList = new System.Windows.Forms.DataGrid();
            this.btnOrder = new System.Windows.Forms.Button();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnHidden = new System.Windows.Forms.Button();
            this.btnQualified = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pbQuestion = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // tbQty
            // 
            this.tbQty.Location = new System.Drawing.Point(195, 49);
            this.tbQty.MaxLength = 10;
            this.tbQty.Name = "tbQty";
            this.tbQty.Size = new System.Drawing.Size(40, 23);
            this.tbQty.TabIndex = 2;
            this.tbQty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbQty_KeyUp);
            this.tbQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQty_KeyPress);
            this.tbQty.LostFocus += new System.EventHandler(this.tbQty_LostFocus);
            // 
            // dgList
            // 
            this.dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgList.Location = new System.Drawing.Point(0, 49);
            this.dgList.Name = "dgList";
            this.dgList.RowHeadersVisible = false;
            this.dgList.Size = new System.Drawing.Size(238, 270);
            this.dgList.TabIndex = 0;
            this.dgList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UCBase_KeyUp);
            this.dgList.Click += new System.EventHandler(this.dgList_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.Location = new System.Drawing.Point(195, 4);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(40, 20);
            this.btnOrder.TabIndex = 3;
            this.btnOrder.Text = "确定";
            this.btnOrder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnOrder_KeyUp);
            // 
            // tbBarCode
            // 
            this.tbBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBarCode.Location = new System.Drawing.Point(38, 4);
            this.tbBarCode.MaxLength = 50;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(151, 23);
            this.tbBarCode.TabIndex = 1;
            this.tbBarCode.TextChanged += new System.EventHandler(this.tbBarCode_TextChanged);
            this.tbBarCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbBarCode_KeyUp);
            // 
            // lblBarCode
            // 
            this.lblBarCode.Location = new System.Drawing.Point(1, 8);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(43, 20);
            this.lblBarCode.Text = "条码:";
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(0, 26);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(238, 20);
            // 
            // btnHidden
            // 
            this.btnHidden.Location = new System.Drawing.Point(149, 3);
            this.btnHidden.Name = "btnHidden";
            this.btnHidden.Size = new System.Drawing.Size(0, 0);
            this.btnHidden.TabIndex = 14;
            this.btnHidden.Visible = false;
            this.btnHidden.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnHidden_KeyUp);
            // 
            // btnQualified
            // 
            this.btnQualified.Location = new System.Drawing.Point(197, 23);
            this.btnQualified.Name = "btnQualified";
            this.btnQualified.Size = new System.Drawing.Size(38, 20);
            this.btnQualified.TabIndex = 15;
            this.btnQualified.Text = "合格";
            this.btnQualified.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(6, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 20);
            this.lblMessage.Text = "Message";
            this.lblMessage.Visible = false;
            // 
            // pbQuestion
            // 
            this.pbQuestion.Image = ((System.Drawing.Image)(resources.GetObject("pbQuestion.Image")));
            this.pbQuestion.Location = new System.Drawing.Point(218, 29);
            this.pbQuestion.Name = "pbQuestion";
            this.pbQuestion.Size = new System.Drawing.Size(16, 17);
            this.pbQuestion.Click += new System.EventHandler(this.pbQuestion_Click);
            // 
            // UCBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pbQuestion);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnQualified);
            this.Controls.Add(this.tbQty);
            this.Controls.Add(this.dgList);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.tbBarCode);
            this.Controls.Add(this.lblBarCode);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnHidden);
            this.Name = "UCBase";
            this.Size = new System.Drawing.Size(238, 320);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UCBase_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbQty;
        protected System.Windows.Forms.DataGrid dgList;
        protected System.Windows.Forms.Button btnOrder;
        protected System.Windows.Forms.TextBox tbBarCode;
        protected System.Windows.Forms.Label lblBarCode;
        protected System.Windows.Forms.Label lblResult;
        protected System.Windows.Forms.Button btnHidden;
        protected System.Windows.Forms.Button btnQualified;
        protected System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pbQuestion;
    }
}
