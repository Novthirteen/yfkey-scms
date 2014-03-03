namespace Sconit_CS
{
    partial class UCModuleSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabModule = new System.Windows.Forms.TabControl();
            this.tabProcurement = new System.Windows.Forms.TabPage();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReceiveReturn = new System.Windows.Forms.Button();
            this.btnReceive = new System.Windows.Forms.Button();
            this.tabProduction = new System.Windows.Forms.TabPage();
            this.btnReuse = new System.Windows.Forms.Button();
            this.btnRepack = new System.Windows.Forms.Button();
            this.btnMaterialIn = new System.Windows.Forms.Button();
            this.btnOnline = new System.Windows.Forms.Button();
            this.btnFlushBack = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.tabDistribution = new System.Windows.Forms.TabPage();
            this.btnPickListOnline = new System.Windows.Forms.Button();
            this.btnShipConfirm = new System.Windows.Forms.Button();
            this.btnShipReturn = new System.Windows.Forms.Button();
            this.btnShip = new System.Windows.Forms.Button();
            this.btnPickList = new System.Windows.Forms.Button();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.btnHuStatus = new System.Windows.Forms.Button();
            this.btnInspection = new System.Windows.Forms.Button();
            this.btnInvTransfer = new System.Windows.Forms.Button();
            this.btnStockTaking = new System.Windows.Forms.Button();
            this.btnPickUp = new System.Windows.Forms.Button();
            this.btnPutAway = new System.Windows.Forms.Button();
            this.btnUnboxing = new System.Windows.Forms.Button();
            this.tabInspection = new System.Windows.Forms.TabPage();
            this.btnInspect = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.comboBoxPrint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnShipOrder = new System.Windows.Forms.Button();
            this.tabModule.SuspendLayout();
            this.tabProcurement.SuspendLayout();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.tabProduction.SuspendLayout();
            this.tabDistribution.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.tabInspection.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabModule
            // 
            this.tabModule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabModule.Controls.Add(this.tabProcurement);
            this.tabModule.Controls.Add(this.tabProduction);
            this.tabModule.Controls.Add(this.tabDistribution);
            this.tabModule.Controls.Add(this.tabInventory);
            this.tabModule.Controls.Add(this.tabInspection);
            this.tabModule.Controls.Add(this.tabSettings);
            this.tabModule.Font = new System.Drawing.Font("SimSun", 12F);
            this.tabModule.Location = new System.Drawing.Point(3, 3);
            this.tabModule.Name = "tabModule";
            this.tabModule.SelectedIndex = 0;
            this.tabModule.Size = new System.Drawing.Size(894, 494);
            this.tabModule.TabIndex = 0;
            this.tabModule.SelectedIndexChanged += new System.EventHandler(this.tabModule_SelectedIndexChanged);
            // 
            // tabProcurement
            // 
            this.tabProcurement.Controls.Add(this.gbList);
            this.tabProcurement.Controls.Add(this.btnReceiveReturn);
            this.tabProcurement.Controls.Add(this.btnReceive);
            this.tabProcurement.Location = new System.Drawing.Point(4, 25);
            this.tabProcurement.Name = "tabProcurement";
            this.tabProcurement.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcurement.Size = new System.Drawing.Size(886, 465);
            this.tabProcurement.TabIndex = 0;
            this.tabProcurement.Text = "供货管理";
            this.tabProcurement.UseVisualStyleBackColor = true;
            // 
            // gbList
            // 
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.dgList);
            this.gbList.Location = new System.Drawing.Point(6, 74);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(874, 385);
            this.gbList.TabIndex = 4;
            this.gbList.TabStop = false;
            this.gbList.Text = "收货单";
            // 
            // dgList
            // 
            this.dgList.AllowUserToAddRows = false;
            this.dgList.AllowUserToDeleteRows = false;
            this.dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sequence,
            this.HuId,
            this.ReceiptNo,
            this.IpNo,
            this.OrderNo,
            this.PartyFrom,
            this.PartyTo,
            this.Dock,
            this.ItemDescription,
            this.UnitCount,
            this.Qty,
            this.CreateDate,
            this.CreateUser});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgList.Location = new System.Drawing.Point(6, 25);
            this.dgList.Name = "dgList";
            this.dgList.ReadOnly = true;
            this.dgList.RowHeadersVisible = false;
            dataGridViewCellStyle4.NullValue = " ";
            this.dgList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgList.RowTemplate.Height = 23;
            this.dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgList.Size = new System.Drawing.Size(862, 354);
            this.dgList.TabIndex = 3;
            // 
            // Sequence
            // 
            this.Sequence.DataPropertyName = "Sequence";
            this.Sequence.FillWeight = 40F;
            this.Sequence.HeaderText = "序号";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            // 
            // HuId
            // 
            this.HuId.DataPropertyName = "HuId";
            this.HuId.FillWeight = 120F;
            this.HuId.HeaderText = "条码";
            this.HuId.Name = "HuId";
            this.HuId.ReadOnly = true;
            this.HuId.Visible = false;
            // 
            // ReceiptNo
            // 
            this.ReceiptNo.DataPropertyName = "ReceiptNo";
            this.ReceiptNo.HeaderText = "收货单";
            this.ReceiptNo.Name = "ReceiptNo";
            this.ReceiptNo.ReadOnly = true;
            // 
            // IpNo
            // 
            this.IpNo.DataPropertyName = "IpNo";
            this.IpNo.HeaderText = "ASN";
            this.IpNo.Name = "IpNo";
            this.IpNo.ReadOnly = true;
            // 
            // OrderNo
            // 
            this.OrderNo.DataPropertyName = "OrderNo";
            this.OrderNo.FillWeight = 80F;
            this.OrderNo.HeaderText = "工单";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.ReadOnly = true;
            // 
            // PartyFrom
            // 
            this.PartyFrom.DataPropertyName = "PartyFrom";
            this.PartyFrom.FillWeight = 150F;
            this.PartyFrom.HeaderText = "供应商";
            this.PartyFrom.Name = "PartyFrom";
            this.PartyFrom.ReadOnly = true;
            // 
            // PartyTo
            // 
            this.PartyTo.DataPropertyName = "PartyTo";
            this.PartyTo.FillWeight = 150F;
            this.PartyTo.HeaderText = "客户";
            this.PartyTo.Name = "PartyTo";
            this.PartyTo.ReadOnly = true;
            // 
            // Dock
            // 
            this.Dock.DataPropertyName = "Dock";
            this.Dock.FillWeight = 60F;
            this.Dock.HeaderText = "道口";
            this.Dock.Name = "Dock";
            this.Dock.ReadOnly = true;
            this.Dock.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ItemDescription
            // 
            this.ItemDescription.DataPropertyName = "ItemDescription";
            this.ItemDescription.FillWeight = 120F;
            this.ItemDescription.HeaderText = "描述";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            this.ItemDescription.Visible = false;
            // 
            // UnitCount
            // 
            this.UnitCount.DataPropertyName = "UnitCount";
            dataGridViewCellStyle1.Format = "0.########";
            dataGridViewCellStyle1.NullValue = null;
            this.UnitCount.DefaultCellStyle = dataGridViewCellStyle1;
            this.UnitCount.FillWeight = 40F;
            this.UnitCount.HeaderText = "单包装";
            this.UnitCount.Name = "UnitCount";
            this.UnitCount.ReadOnly = true;
            this.UnitCount.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Format = "0.########";
            dataGridViewCellStyle2.NullValue = null;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty.FillWeight = 50F;
            this.Qty.HeaderText = "数量";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Visible = false;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            this.CreateDate.FillWeight = 80F;
            this.CreateDate.HeaderText = "创建日期";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            // 
            // CreateUser
            // 
            this.CreateUser.DataPropertyName = "CreateUser";
            this.CreateUser.FillWeight = 80F;
            this.CreateUser.HeaderText = "创建人";
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.ReadOnly = true;
            // 
            // btnReceiveReturn
            // 
            this.btnReceiveReturn.Enabled = false;
            this.btnReceiveReturn.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReceiveReturn.Location = new System.Drawing.Point(190, 20);
            this.btnReceiveReturn.Name = "btnReceiveReturn";
            this.btnReceiveReturn.Size = new System.Drawing.Size(135, 36);
            this.btnReceiveReturn.TabIndex = 2;
            this.btnReceiveReturn.Text = "2. 要货退货";
            this.btnReceiveReturn.UseVisualStyleBackColor = true;
            this.btnReceiveReturn.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.Enabled = false;
            this.btnReceive.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReceive.Location = new System.Drawing.Point(25, 20);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(135, 36);
            this.btnReceive.TabIndex = 1;
            this.btnReceive.Text = "1. 收      货";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // tabProduction
            // 
            this.tabProduction.Controls.Add(this.btnReuse);
            this.tabProduction.Controls.Add(this.btnRepack);
            this.tabProduction.Controls.Add(this.btnMaterialIn);
            this.tabProduction.Controls.Add(this.btnOnline);
            this.tabProduction.Controls.Add(this.btnFlushBack);
            this.tabProduction.Controls.Add(this.btnOffline);
            this.tabProduction.Location = new System.Drawing.Point(4, 25);
            this.tabProduction.Name = "tabProduction";
            this.tabProduction.Padding = new System.Windows.Forms.Padding(3);
            this.tabProduction.Size = new System.Drawing.Size(886, 465);
            this.tabProduction.TabIndex = 1;
            this.tabProduction.Text = "生产管理";
            this.tabProduction.UseVisualStyleBackColor = true;
            // 
            // btnReuse
            // 
            this.btnReuse.Enabled = false;
            this.btnReuse.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReuse.Location = new System.Drawing.Point(729, 20);
            this.btnReuse.Name = "btnReuse";
            this.btnReuse.Size = new System.Drawing.Size(135, 36);
            this.btnReuse.TabIndex = 6;
            this.btnReuse.Text = "6.材料回用";
            this.btnReuse.UseVisualStyleBackColor = true;
            this.btnReuse.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnRepack
            // 
            this.btnRepack.Enabled = false;
            this.btnRepack.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRepack.Location = new System.Drawing.Point(588, 20);
            this.btnRepack.Name = "btnRepack";
            this.btnRepack.Size = new System.Drawing.Size(135, 36);
            this.btnRepack.TabIndex = 5;
            this.btnRepack.Text = "5. 翻      箱";
            this.btnRepack.UseVisualStyleBackColor = true;
            this.btnRepack.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnMaterialIn
            // 
            this.btnMaterialIn.Enabled = false;
            this.btnMaterialIn.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMaterialIn.Location = new System.Drawing.Point(307, 20);
            this.btnMaterialIn.Name = "btnMaterialIn";
            this.btnMaterialIn.Size = new System.Drawing.Size(135, 36);
            this.btnMaterialIn.TabIndex = 3;
            this.btnMaterialIn.Text = "3. 生产投料";
            this.btnMaterialIn.UseVisualStyleBackColor = true;
            this.btnMaterialIn.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnOnline
            // 
            this.btnOnline.Enabled = false;
            this.btnOnline.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOnline.Location = new System.Drawing.Point(25, 20);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(135, 36);
            this.btnOnline.TabIndex = 1;
            this.btnOnline.Text = "1. 生产上线";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnFlushBack
            // 
            this.btnFlushBack.Enabled = false;
            this.btnFlushBack.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFlushBack.Location = new System.Drawing.Point(448, 20);
            this.btnFlushBack.Name = "btnFlushBack";
            this.btnFlushBack.Size = new System.Drawing.Size(135, 36);
            this.btnFlushBack.TabIndex = 4;
            this.btnFlushBack.Text = "4. 投料回冲";
            this.btnFlushBack.UseVisualStyleBackColor = true;
            this.btnFlushBack.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Enabled = false;
            this.btnOffline.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOffline.Location = new System.Drawing.Point(166, 20);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(135, 36);
            this.btnOffline.TabIndex = 2;
            this.btnOffline.Text = "2. 生产下线";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // tabDistribution
            // 
            this.tabDistribution.Controls.Add(this.btnPickListOnline);
            this.tabDistribution.Controls.Add(this.btnShipConfirm);
            this.tabDistribution.Controls.Add(this.btnShipReturn);
            this.tabDistribution.Controls.Add(this.btnShipOrder);
            this.tabDistribution.Controls.Add(this.btnShip);
            this.tabDistribution.Controls.Add(this.btnPickList);
            this.tabDistribution.Location = new System.Drawing.Point(4, 25);
            this.tabDistribution.Name = "tabDistribution";
            this.tabDistribution.Padding = new System.Windows.Forms.Padding(3);
            this.tabDistribution.Size = new System.Drawing.Size(886, 465);
            this.tabDistribution.TabIndex = 2;
            this.tabDistribution.Text = "发货管理";
            this.tabDistribution.UseVisualStyleBackColor = true;
            // 
            // btnPickListOnline
            // 
            this.btnPickListOnline.Enabled = false;
            this.btnPickListOnline.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPickListOnline.Location = new System.Drawing.Point(25, 20);
            this.btnPickListOnline.Name = "btnPickListOnline";
            this.btnPickListOnline.Size = new System.Drawing.Size(135, 36);
            this.btnPickListOnline.TabIndex = 1;
            this.btnPickListOnline.Text = "1. 拣货上线";
            this.btnPickListOnline.UseVisualStyleBackColor = true;
            this.btnPickListOnline.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnShipConfirm
            // 
            this.btnShipConfirm.Enabled = false;
            this.btnShipConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShipConfirm.Location = new System.Drawing.Point(730, 20);
            this.btnShipConfirm.Name = "btnShipConfirm";
            this.btnShipConfirm.Size = new System.Drawing.Size(135, 36);
            this.btnShipConfirm.TabIndex = 6;
            this.btnShipConfirm.Text = "6. 发货确认";
            this.btnShipConfirm.UseVisualStyleBackColor = true;
            this.btnShipConfirm.Visible = false;
            this.btnShipConfirm.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnShipReturn
            // 
            this.btnShipReturn.Enabled = false;
            this.btnShipReturn.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShipReturn.Location = new System.Drawing.Point(589, 20);
            this.btnShipReturn.Name = "btnShipReturn";
            this.btnShipReturn.Size = new System.Drawing.Size(135, 36);
            this.btnShipReturn.TabIndex = 5;
            this.btnShipReturn.Text = "5. 发货退货";
            this.btnShipReturn.UseVisualStyleBackColor = true;
            this.btnShipReturn.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnShip
            // 
            this.btnShip.Enabled = false;
            this.btnShip.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShip.Location = new System.Drawing.Point(307, 20);
            this.btnShip.Name = "btnShip";
            this.btnShip.Size = new System.Drawing.Size(135, 36);
            this.btnShip.TabIndex = 3;
            this.btnShip.Text = "3. 拣货发货";
            this.btnShip.UseVisualStyleBackColor = true;
            this.btnShip.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnPickList
            // 
            this.btnPickList.Enabled = false;
            this.btnPickList.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPickList.Location = new System.Drawing.Point(166, 20);
            this.btnPickList.Name = "btnPickList";
            this.btnPickList.Size = new System.Drawing.Size(135, 36);
            this.btnPickList.TabIndex = 2;
            this.btnPickList.Text = "2. 拣      货";
            this.btnPickList.UseVisualStyleBackColor = true;
            this.btnPickList.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // tabInventory
            // 
            this.tabInventory.Controls.Add(this.btnHuStatus);
            this.tabInventory.Controls.Add(this.btnInspection);
            this.tabInventory.Controls.Add(this.btnInvTransfer);
            this.tabInventory.Controls.Add(this.btnStockTaking);
            this.tabInventory.Controls.Add(this.btnPickUp);
            this.tabInventory.Controls.Add(this.btnPutAway);
            this.tabInventory.Controls.Add(this.btnUnboxing);
            this.tabInventory.Location = new System.Drawing.Point(4, 25);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(886, 465);
            this.tabInventory.TabIndex = 3;
            this.tabInventory.Text = "库存管理";
            this.tabInventory.UseVisualStyleBackColor = true;
            // 
            // btnHuStatus
            // 
            this.btnHuStatus.Enabled = false;
            this.btnHuStatus.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHuStatus.Location = new System.Drawing.Point(635, 20);
            this.btnHuStatus.Name = "btnHuStatus";
            this.btnHuStatus.Size = new System.Drawing.Size(128, 36);
            this.btnHuStatus.TabIndex = 6;
            this.btnHuStatus.Text = "6. 条码状态";
            this.btnHuStatus.UseVisualStyleBackColor = true;
            this.btnHuStatus.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnInspection
            // 
            this.btnInspection.Enabled = false;
            this.btnInspection.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInspection.Location = new System.Drawing.Point(780, 20);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(90, 36);
            this.btnInspection.TabIndex = 0;
            this.btnInspection.Text = "0. 报验";
            this.btnInspection.UseVisualStyleBackColor = true;
            this.btnInspection.Visible = false;
            // 
            // btnInvTransfer
            // 
            this.btnInvTransfer.Enabled = false;
            this.btnInvTransfer.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInvTransfer.Location = new System.Drawing.Point(25, 20);
            this.btnInvTransfer.Name = "btnInvTransfer";
            this.btnInvTransfer.Size = new System.Drawing.Size(90, 36);
            this.btnInvTransfer.TabIndex = 1;
            this.btnInvTransfer.Text = "1. 移库";
            this.btnInvTransfer.UseVisualStyleBackColor = true;
            this.btnInvTransfer.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnStockTaking
            // 
            this.btnStockTaking.Enabled = false;
            this.btnStockTaking.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStockTaking.Location = new System.Drawing.Point(513, 20);
            this.btnStockTaking.Name = "btnStockTaking";
            this.btnStockTaking.Size = new System.Drawing.Size(90, 36);
            this.btnStockTaking.TabIndex = 5;
            this.btnStockTaking.Text = "5. 盘点";
            this.btnStockTaking.UseVisualStyleBackColor = true;
            this.btnStockTaking.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnPickUp
            // 
            this.btnPickUp.Enabled = false;
            this.btnPickUp.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPickUp.Location = new System.Drawing.Point(269, 20);
            this.btnPickUp.Name = "btnPickUp";
            this.btnPickUp.Size = new System.Drawing.Size(90, 36);
            this.btnPickUp.TabIndex = 3;
            this.btnPickUp.Text = "3. 下架";
            this.btnPickUp.UseVisualStyleBackColor = true;
            this.btnPickUp.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnPutAway
            // 
            this.btnPutAway.Enabled = false;
            this.btnPutAway.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPutAway.Location = new System.Drawing.Point(147, 20);
            this.btnPutAway.Name = "btnPutAway";
            this.btnPutAway.Size = new System.Drawing.Size(90, 36);
            this.btnPutAway.TabIndex = 2;
            this.btnPutAway.Text = "2. 上架";
            this.btnPutAway.UseVisualStyleBackColor = true;
            this.btnPutAway.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // btnUnboxing
            // 
            this.btnUnboxing.Enabled = false;
            this.btnUnboxing.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnboxing.Location = new System.Drawing.Point(391, 20);
            this.btnUnboxing.Name = "btnUnboxing";
            this.btnUnboxing.Size = new System.Drawing.Size(90, 36);
            this.btnUnboxing.TabIndex = 4;
            this.btnUnboxing.Text = "4. 拆箱";
            this.btnUnboxing.UseVisualStyleBackColor = true;
            this.btnUnboxing.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // tabInspection
            // 
            this.tabInspection.Controls.Add(this.btnInspect);
            this.tabInspection.Location = new System.Drawing.Point(4, 25);
            this.tabInspection.Name = "tabInspection";
            this.tabInspection.Padding = new System.Windows.Forms.Padding(3);
            this.tabInspection.Size = new System.Drawing.Size(886, 465);
            this.tabInspection.TabIndex = 5;
            this.tabInspection.Text = "检验管理";
            this.tabInspection.UseVisualStyleBackColor = true;
            // 
            // btnInspect
            // 
            this.btnInspect.Enabled = false;
            this.btnInspect.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInspect.Location = new System.Drawing.Point(25, 20);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(90, 36);
            this.btnInspect.TabIndex = 1;
            this.btnInspect.Text = "1. 检验";
            this.btnInspect.UseVisualStyleBackColor = true;
            this.btnInspect.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.comboBoxPrint);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.BtnSave);
            this.tabSettings.Location = new System.Drawing.Point(4, 25);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(886, 465);
            this.tabSettings.TabIndex = 4;
            this.tabSettings.Text = "用户设置";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // comboBoxPrint
            // 
            this.comboBoxPrint.Font = new System.Drawing.Font("SimHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxPrint.FormattingEnabled = true;
            this.comboBoxPrint.Location = new System.Drawing.Point(190, 36);
            this.comboBoxPrint.Name = "comboBoxPrint";
            this.comboBoxPrint.Size = new System.Drawing.Size(414, 28);
            this.comboBoxPrint.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(45, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择打印机:";
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("SimHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSave.Location = new System.Drawing.Point(625, 33);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(102, 33);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "保存设置";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.btnSavePrintSetting_Click);
            // 
            // btnShipOrder
            // 
            this.btnShipOrder.Enabled = false;
            this.btnShipOrder.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShipOrder.Location = new System.Drawing.Point(448, 20);
            this.btnShipOrder.Name = "btnShipOrder";
            this.btnShipOrder.Size = new System.Drawing.Size(135, 36);
            this.btnShipOrder.TabIndex = 4;
            this.btnShipOrder.Text = "4. 订单发货";
            this.btnShipOrder.UseVisualStyleBackColor = true;
            this.btnShipOrder.Click += new System.EventHandler(this.ModuleSelect_Click);
            // 
            // UCModuleSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabModule);
            this.Name = "UCModuleSelection";
            this.Size = new System.Drawing.Size(900, 500);
            this.tabModule.ResumeLayout(false);
            this.tabProcurement.ResumeLayout(false);
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.tabProduction.ResumeLayout(false);
            this.tabDistribution.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            this.tabInspection.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabModule;
        private System.Windows.Forms.TabPage tabProcurement;
        private System.Windows.Forms.TabPage tabProduction;
        private System.Windows.Forms.TabPage tabDistribution;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.Button btnStockTaking;
        private System.Windows.Forms.Button btnPickUp;
        private System.Windows.Forms.Button btnPutAway;
        private System.Windows.Forms.Button btnUnboxing;
        private System.Windows.Forms.Button btnReceiveReturn;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnRepack;
        private System.Windows.Forms.Button btnMaterialIn;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Button btnFlushBack;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnShipConfirm;
        private System.Windows.Forms.Button btnShipReturn;
        private System.Windows.Forms.Button btnShip;
        private System.Windows.Forms.Button btnPickList;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Button btnInvTransfer;
        private System.Windows.Forms.Button btnPickListOnline;
        private System.Windows.Forms.DataGridView dgList;
        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPrint;
        private System.Windows.Forms.TabPage tabInspection;
        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.Button btnHuStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn HuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dock;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateUser;
        private System.Windows.Forms.Button btnReuse;
        private System.Windows.Forms.Button btnShipOrder;
    }
}
