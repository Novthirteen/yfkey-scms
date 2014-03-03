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
    public partial class UCPrintMonitor : UserControl
    {
        private ClientMgrWSSoapClient TheClientMgr;
        private Resolver resolver;
        private List<ReceiptNote> cacheReceiptNotes;
        public static object locker = new object();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.P))
                {
                    if (this.dataGridView1 != null && this.dataGridView1.CurrentCell != null)
                    {
                        int currentRowIndex = this.dataGridView1.CurrentCell.RowIndex;
                        if (currentRowIndex > -1)
                        {
                            DoPrint(this.dataGridView1["PrintUrl", currentRowIndex].Value.ToString(), null);
                        }
                    }
                    return true;
                }

            }
            catch (Exception)
            {

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public UCPrintMonitor(User user, string moduleType)
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

            //timer1_Tick(this, null);

            #region Printer
            foreach (string fPrinterName in LocalPrinter.GetLocalPrinters())
            {
                this.comboBoxPrint1.Items.Add(fPrinterName);
                this.comboBoxPrint2.Items.Add(fPrinterName);
                this.comboBoxPrint3.Items.Add(fPrinterName);
                this.comboBoxPrint4.Items.Add(fPrinterName);
                this.comboBoxPrint5.Items.Add(fPrinterName);
            }
            //this.comboBoxPrint1.SelectedItem = Settings.Default.DefaultPrintName1;
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lock (locker)
                {
                    string regionCode = this.txtRegion1.Text.Trim();
                    if (regionCode != null && regionCode.Trim() != string.Empty)
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;

                        if (this.cbRec1.Checked)
                        {
                            this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_RECEIPT;
                        }

                        if (this.cbASN1.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                        }

                        if (this.cbPL1.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                        }

                        if (this.cbInp1.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                        }

                        if (this.cbOrd1.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                        }

                        if (this.resolver.Input.Length > 2 && regionCode != string.Empty)
                        {
                            this.resolver.Input += "&" + regionCode;
                            resolver = TheClientMgr.ScanBarcode(resolver);
                            if (resolver.ReceiptNotes != null)
                            {
                                foreach (ReceiptNote receiptNote in resolver.ReceiptNotes)
                                {
                                    DoPrint(receiptNote.PrintUrl, (this.comboBoxPrint1.SelectedItem == null ? string.Empty : this.comboBoxPrint1.SelectedItem.ToString()));
                                    receiptNote.Dock = this.comboBoxPrint1.SelectedItem == null ? string.Empty : this.comboBoxPrint1.SelectedItem.ToString();

                                }
                            }
                            Databind();
                        }
                    }

                    string regionCode2 = this.txtRegion2.Text.Trim();
                    if (regionCode2 != null && regionCode2.Trim() != string.Empty)
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;

                        if (this.cbRec2.Checked)
                        {
                            this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_RECEIPT;
                        }

                        if (this.cbASN2.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                        }

                        if (this.cbPL2.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                        }

                        if (this.cbInp2.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                        }

                        if (this.cbOrd2.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                        }

                        if (this.resolver.Input.Length > 2 && regionCode2 != string.Empty)
                        {
                            this.resolver.Input += "&" + regionCode2;
                            resolver = TheClientMgr.ScanBarcode(resolver);
                            if (resolver.ReceiptNotes != null)
                            {
                                foreach (ReceiptNote receiptNote in resolver.ReceiptNotes)
                                {
                                    DoPrint(receiptNote.PrintUrl, (this.comboBoxPrint2.SelectedItem == null ? string.Empty : this.comboBoxPrint2.SelectedItem.ToString()));
                                    receiptNote.Dock = this.comboBoxPrint2.SelectedItem == null ? string.Empty : this.comboBoxPrint2.SelectedItem.ToString();
                                }
                            }
                            Databind();
                        }
                    }

                    string regionCode3 = this.txtRegion3.Text.Trim();
                    if (regionCode3 != null && regionCode3.Trim() != string.Empty)
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;

                        if (this.cbRec3.Checked)
                        {
                            this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_RECEIPT;
                        }

                        if (this.cbASN3.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                        }

                        if (this.cbPL3.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                        }

                        if (this.cbInp3.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                        }

                        if (this.cbOrd3.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                        }

                        if (this.resolver.Input.Length > 2 && regionCode3 != string.Empty)
                        {
                            this.resolver.Input += "&" + regionCode3;
                            resolver = TheClientMgr.ScanBarcode(resolver);
                            if (resolver.ReceiptNotes != null)
                            {
                                foreach (ReceiptNote receiptNote in resolver.ReceiptNotes)
                                {
                                    DoPrint(receiptNote.PrintUrl, (this.comboBoxPrint3.SelectedItem == null ? string.Empty : this.comboBoxPrint3.SelectedItem.ToString()));
                                    receiptNote.Dock = this.comboBoxPrint3.SelectedItem == null ? string.Empty : this.comboBoxPrint3.SelectedItem.ToString();

                                }
                            }
                            Databind();
                        }
                    }

                    string regionCode4 = this.txtRegion4.Text.Trim();
                    if (regionCode4 != null && regionCode4.Trim() != string.Empty)
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;

                        if (this.cbRec4.Checked)
                        {
                            this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_RECEIPT;
                        }

                        if (this.cbASN4.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                        }

                        if (this.cbPL4.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                        }

                        if (this.cbInp4.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                        }

                        if (this.cbOrd4.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                        }

                        if (this.resolver.Input.Length > 2 && regionCode4 != string.Empty)
                        {
                            this.resolver.Input += "&" + regionCode4;
                            resolver = TheClientMgr.ScanBarcode(resolver);
                            if (resolver.ReceiptNotes != null)
                            {
                                foreach (ReceiptNote receiptNote in resolver.ReceiptNotes)
                                {
                                    DoPrint(receiptNote.PrintUrl, (this.comboBoxPrint4.SelectedItem == null ? string.Empty : this.comboBoxPrint4.SelectedItem.ToString()));
                                    receiptNote.Dock = this.comboBoxPrint4.SelectedItem == null ? string.Empty : this.comboBoxPrint4.SelectedItem.ToString();

                                }
                            }
                            Databind();
                        }
                    }

                    string regionCode5 = this.txtRegion5.Text.Trim();
                    if (regionCode5 != null && regionCode5.Trim() != string.Empty)
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;

                        if (this.cbRec5.Checked)
                        {
                            this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_RECEIPT;
                        }

                        if (this.cbASN5.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ASN;
                            }
                        }

                        if (this.cbPL5.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_PICKLIST;
                            }
                        }

                        if (this.cbInp5.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_INSPECT;
                            }
                        }

                        if (this.cbOrd5.Checked)
                        {
                            if (this.resolver.Input.Length > 2)
                            {
                                this.resolver.Input += "|" + BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                            else
                            {
                                this.resolver.Input += BusinessConstants.PRINT_ORDER_TYPE_ORDER;
                            }
                        }

                        if (this.resolver.Input.Length > 2 && regionCode5 != string.Empty)
                        {
                            this.resolver.Input += "&" + regionCode5;
                            resolver = TheClientMgr.ScanBarcode(resolver);
                            if (resolver.ReceiptNotes != null)
                            {
                                foreach (ReceiptNote receiptNote in resolver.ReceiptNotes)
                                {
                                    DoPrint(receiptNote.PrintUrl, (this.comboBoxPrint5.SelectedItem == null ? string.Empty : this.comboBoxPrint5.SelectedItem.ToString()));
                                    receiptNote.Dock = this.comboBoxPrint5.SelectedItem == null ? string.Empty : this.comboBoxPrint5.SelectedItem.ToString();
                                }
                            }
                            Databind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.Log("Error:UCPrintMonitor:" + ex.Message);
            }
        }

        private void Databind()
        {
            cacheReceiptNotes.AddRange(resolver.ReceiptNotes);

            var query = (from t in this.cacheReceiptNotes orderby t.CreateDate descending select t).Take(100);
            List<ReceiptNote> SelectReceiptNote = query.ToList();
            this.dataGridView1.DataSource = new BindingList<ReceiptNote>(SelectReceiptNote);
        }

        private void DoPrint(string printUrl, string printer)
        {
            Utility.PrintOrder(printUrl, this, printer);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //点击后启动,启动后不可以可以修改间隔时间
            if (this.tbInterval.Enabled)
            {
                this.tbInterval.Enabled = false;
                this.BtnStart1.Text = "暂停";

                if (this.tbInterval.Text.Trim() != string.Empty && int.Parse(this.tbInterval.Text) != 0)
                {
                    this.timer1.Interval = int.Parse(this.tbInterval.Text) * 1000;
                }
                else
                {
                    this.tbInterval.Text = (this.timer1.Interval / 1000).ToString();
                }
                this.timer1.Enabled = true;
                timer1_Tick(this, null);
            }
            //点击后暂停,暂停后可以修改间隔时间
            else
            {
                this.tbInterval.Enabled = true;
                this.timer1.Enabled = false;
                this.BtnStart1.Text = "开始";
            }
        }

        private void tbInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.TextBoxIntFilter(sender, e);
            try
            {
                int.Parse(this.tbInterval.Text);
            }
            catch (Exception)
            {
                //this.tbInterval.Text = (this.timer1.Interval / 1000).ToString();
                //throw;
            }
        }
    }


    public class LocalPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        /// <summary>
        /// 获取本机默认打印机名称
        /// </summary>
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }
        /// <summary>
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。
        /// </summary>
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }
    }
}
