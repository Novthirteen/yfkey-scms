namespace Sconit_CS
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.plMain = new System.Windows.Forms.Panel();
            this.lbCountDown = new System.Windows.Forms.Label();
            this.ucLogin = new Sconit_CS.UCLogin();
            this.autoLogoutTimer = new System.Windows.Forms.Timer(this.components);
            this.plMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMain
            // 
            this.plMain.AutoSize = true;
            this.plMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plMain.BackColor = System.Drawing.SystemColors.Control;
            this.plMain.Controls.Add(this.lbCountDown);
            this.plMain.Controls.Add(this.ucLogin);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(401, 292);
            this.plMain.TabIndex = 0;
            // 
            // lbCountDown
            // 
            this.lbCountDown.AutoSize = true;
            this.lbCountDown.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountDown.Location = new System.Drawing.Point(-3, 15);
            this.lbCountDown.Name = "lbCountDown";
            this.lbCountDown.Size = new System.Drawing.Size(51, 56);
            this.lbCountDown.TabIndex = 4;
            this.lbCountDown.Text = "0";
            this.lbCountDown.Visible = false;
            // 
            // ucLogin
            // 
            this.ucLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ucLogin.Location = new System.Drawing.Point(17, 28);
            this.ucLogin.Name = "ucLogin";
            this.ucLogin.Size = new System.Drawing.Size(372, 226);
            this.ucLogin.TabIndex = 0;
            // 
            // autoLogoutTimer
            // 
            this.autoLogoutTimer.Enabled = true;
            this.autoLogoutTimer.Interval = 1000;
            this.autoLogoutTimer.Tick += new System.EventHandler(this.autoLogoutTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 292);
            this.ControlBox = false;
            this.Controls.Add(this.plMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(409, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCMS CS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.plMain.ResumeLayout(false);
            this.plMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plMain;
        private System.Windows.Forms.Timer autoLogoutTimer;
        private System.Windows.Forms.Label lbCountDown;
        private UCLogin ucLogin;
    }
}

