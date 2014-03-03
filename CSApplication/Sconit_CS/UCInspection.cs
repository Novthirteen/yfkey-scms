using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Sconit_CS.SconitWS;

namespace Sconit_CS
{
    public partial class UCInspection : UCBase
    {
        public UCInspection(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
        }
    }
}
