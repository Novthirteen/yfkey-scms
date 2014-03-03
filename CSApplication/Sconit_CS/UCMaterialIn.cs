using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.ServiceModel;

namespace Sconit_CS
{
    public partial class UCMaterialIn : UCBase
    {
        public UCMaterialIn(User user, string moduleType)
            :base(user,moduleType)
        {
            InitializeComponent();
            this.btnConfirm.Text = "生产投料";
            this.gvList.Columns["OrderedQty"].HeaderText = "总回冲";
            this.gvList.Columns["Qty"].HeaderText = "总投数";
            this.gvList.Columns["CurrentQty"].HeaderText = "本次投料";
            this.gvList.Columns["LocationCode"].Visible = true;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["LocationFromCode"].Visible = false;
            this.gvList.Columns["LocationToCode"].Visible = false;
            this.gvList.Columns["UomCode"].Visible = false;
            this.gvList.Columns["BinCode"].Visible = false;
            this.gvList.Columns["LotNumber"].Visible = false;
            this.gvList.Columns["Qty"].Visible = true;
            this.gvList.Columns["OrderedQty"].Visible = true;
            this.gvHuList.Columns["StorageBinCode"].Visible = false;
            this.gvList.Columns["Cartons"].Visible = true;
            this.gvList.Columns["Operation"].Visible = true;
        }
    }
}
