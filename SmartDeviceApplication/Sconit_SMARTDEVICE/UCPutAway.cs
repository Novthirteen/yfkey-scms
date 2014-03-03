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
    public partial class UCPutAway : UCBase
    {
        public UCPutAway(User user,string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "上架";
            columnStorageBinCode.Width = 30;
            columnStorageBinCode.NullText = string.Empty;
            this.resolver.IsScanHu = true;
        }        
    }
}
