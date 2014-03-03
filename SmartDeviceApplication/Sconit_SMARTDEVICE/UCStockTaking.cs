using Sconit_SMARTDEVICE.SconitWS;
using System;

namespace Sconit_SD
{
    public partial class UCStockTaking : UCBase
    {
        public UCStockTaking(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "盘点";
            columnStorageBinCode.Width = 30;
        }

        protected override void BarCodeScan()
        {
            base.BarCodeScan();
            string message = HelpMessage();
            Utility.ShowMessageBox(message);
        }

        protected override string HelpMessage()
        {
            /*test
                        string[] s1 = new string[] { "a", "b", "c", "d" };
                        string[] s2 = new string[] { "1", "2" };
                        List<string[]> s = new List<string[]>();
                        s.Add(s1);
                        s.Add(s2);
                        resolver.WorkingHours = s.ToArray();
                    */
            string message = string.Empty;
            if (resolver.WorkingHours != null && resolver.WorkingHours.Length == 2
                && resolver.BarcodeHead == BusinessConstants.CODE_PREFIX_CYCCNT)
            {
                if (resolver.WorkingHours[0].Length > 0)
                {
                    message = "你要盘点的物料有:";
                    int i = 0;
                    foreach (var item in resolver.WorkingHours[0])
                    {
                        message += item;
                        i++;
                        if (i < resolver.WorkingHours[0].Length)
                        {
                            message += ",";
                        }
                    }
                }
                if (message != string.Empty)
                {
                    message += ";\n";//\n\n按F1显示此信息";
                }
                if (resolver.WorkingHours[1].Length > 0)
                {
                    int j = 0;
                    message += "你要盘点的库格有:";
                    foreach (var bin in resolver.WorkingHours[1])
                    {
                        message += bin;
                        j++;
                        if (j < resolver.WorkingHours[1].Length)
                        {
                            message += ",";
                        }
                    }
                }
                if (message != string.Empty)
                {
                    message += ";";//\n\n按F1显示此信息";
                }
                else
                {
                    return null;
                }

                return message;
            }
            else
            {
                //Utility.ShowMessageBox("盘点出错");
            }
            return null;
        }
    }
}
