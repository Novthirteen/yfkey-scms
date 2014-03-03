using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCPickUp : UCBase
    {
        public UCPickUp(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "下架";
            columnStorageBinCode.Width = 30;
            columnStorageBinCode.NullText = string.Empty;
            this.resolver.IsScanHu = true;
        }

    }
}
