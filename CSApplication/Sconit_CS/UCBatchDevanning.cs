using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.Drawing.Printing;
using Sconit_CS.Properties;

namespace Sconit_CS
{
    public partial class UCBatchDevanning : UserControl
    {
        private ClientMgrWSSoapClient TheClientMgr;
        private Resolver resolver;
        private List<ReceiptNote> cacheReceiptNotes;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public UCBatchDevanning(User user, string moduleType)
        {
            InitializeComponent();
            this.resolver = new Resolver();
            this.resolver.UserCode = user.Code;
            this.resolver.ModuleType = moduleType;
            this.TheClientMgr = new ClientMgrWSSoapClient();
            this.dataGridView1.AutoGenerateColumns = false;

            this.resolver.Transformers = null;
            this.resolver.Result = string.Empty;
            this.resolver.BinCode = string.Empty;
            this.resolver.Code = string.Empty;
            this.resolver.CodePrefix = string.Empty;
            this.cacheReceiptNotes = new List<ReceiptNote>();
        }


        private void Databind()
        {
            cacheReceiptNotes.AddRange(resolver.ReceiptNotes);

            var query = (from t in this.cacheReceiptNotes orderby t.CreateDate descending select t).Take(100);
            List<ReceiptNote> SelectReceiptNote = query.ToList();
            this.dataGridView1.DataSource = new BindingList<ReceiptNote>(SelectReceiptNote);
        }

      

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE + this.txtHuId.Text.Trim();
            resolver = TheClientMgr.ScanBarcode(resolver);
            this.txtHuId.Text = string.Empty;
            Databind();
        }

      
    }
}
