using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.Drawing;
using System.IO;
using Sconit_CS.Properties;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sconit_CS
{
    public class Utility
    {
       

        public static string GetBarcodeCode128BByStr(string str)
        {
            int total = 104;
            int a = 0;
            int endAsc = 0;
            char endChar = new char();
            for (int i = 0; i < str.Length; i++)
            {
                //转换ASCII数值
                a = Convert.ToInt32(Convert.ToChar(str.Substring(i, 1)));

                //Code 128 SET B 字符集
                if (a >= 32)
                {
                    total += (a - 32) * (i + 1);
                }
                else
                {
                    total += (a + 64) * (i + 1);
                }
            }
            endAsc = total % 103;
            //字符集大于95直接赋值，其它转换后获得
            if (endAsc >= 95)
            {
                switch (endAsc)
                {
                    case 95:
                        endChar = Convert.ToChar("Ã");
                        break;
                    case 96:
                        endChar = Convert.ToChar("Ä");
                        break;
                    case 97:
                        endChar = Convert.ToChar("Å");
                        break;
                    case 98:
                        endChar = Convert.ToChar("Æ");
                        break;
                    case 99:
                        endChar = Convert.ToChar("Ç");
                        break;
                    case 100:
                        endChar = Convert.ToChar("È");
                        break;
                    case 101:
                        endChar = Convert.ToChar("É");
                        break;
                    case 102:
                        endChar = Convert.ToChar("Ê");
                        break;
                    default:
                        endChar = Convert.ToChar("");
                        break;
                }
            }
            else
            {
                endAsc += 32;
                endChar = Convert.ToChar(endAsc);
            }
            //生成Code 128B条码字符串
            string result = "Ì" + str + endChar.ToString() + "Î";
            return result;
        }

        public static string Md5(string originalPassword)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(originalPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static bool IsDecimal(string str)
        {
            try
            {
                Convert.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string FormatExMessage(string message)
        {
            try
            {
                if (message.StartsWith("System.Web.Services.Protocols.SoapException"))
                {
                    message = message.Remove(0, 44);
                    message = message.Remove(message.IndexOf("\n"), message.Length - message.IndexOf("\n"));
                }
                message = message.Replace("\\n", "\n\n");
            }
            catch (Exception ex)
            {
                return message;
            }
            return message;
        }

        public static Transformer[] SumCurrentQty(Transformer[] transformerArray)
        {
            foreach (Transformer transformer in transformerArray)
            {
                transformer.CurrentQty = 0;
                transformer.Cartons = 0;
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    transformer.CurrentQty += transformerDetail.CurrentQty;
                    if (transformerDetail.CurrentQty > 0)
                    {
                        transformer.Cartons++;
                    }
                }
            }
            return transformerArray;
        }

        public static DataGridView RenderDataGridViewBackColor(DataGridView dataGrid)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                decimal CurrentQty = Convert.ToDecimal(row.Cells["CurrentQty"].Value.ToString());
                decimal CurrentRejectQty = 0;
                try { CurrentRejectQty = Convert.ToDecimal(row.Cells["CurrentRejectQty"].Value.ToString()); }
                catch (Exception) { }
                decimal Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString());
                if (CurrentQty + CurrentRejectQty == Qty)
                {
                    row.DefaultCellStyle.ForeColor = Color.Green;
                }
                else if (CurrentQty + CurrentRejectQty > Qty)
                {
                    row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                }
                else if (CurrentQty + CurrentRejectQty < Qty)
                {
                    //row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            return dataGrid;
        }

        public static bool IsHasTransformer(Resolver resolver)
        {
            if (resolver != null && resolver.Transformers != null)
            {
                //decimal CurrentQty = 0;
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        //CurrentQty += transformer.CurrentQty;
                        if (transformer.CurrentQty != 0 || transformer.CurrentRejectQty != 0)
                        {
                            return true;
                        }
                        if (transformer.TransformerDetails != null)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                //CurrentQty += transformerDetail.CurrentQty;
                                if (transformerDetail.CurrentQty != 0 || transformerDetail.CurrentRejectQty != 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsHasTransformerDetail(Resolver resolver)
        {
            if (resolver != null && resolver.Transformers != null)
            {
                //decimal CurrentQty = 0;
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        if (transformer.TransformerDetails != null)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                //CurrentQty += transformerDetail.CurrentQty;
                                if (transformerDetail.CurrentQty != 0 || transformerDetail.CurrentRejectQty != 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 得到当前行号,没有匹配上返回-1
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public static int GetRowIndex(Resolver resolver, string compareString)
        {
            bool isMatch = false;
            int rowIndex = 0;
            if (resolver.Transformers == null || (resolver.Transformers != null && resolver.Transformers.Length != 1))
            {
                return -1;
            }
            if (resolver.Transformers[0].TransformerDetails != null)
            {
                foreach (TransformerDetail transformerDetail in resolver.Transformers[0].TransformerDetails)
                {
                    if (resolver.IsScanHu)
                    {
                        if (transformerDetail.HuId != null && transformerDetail.HuId.Trim().ToUpper() != compareString.Trim().ToUpper())
                        {
                            rowIndex++;
                        }
                        else if (transformerDetail.HuId != null && transformerDetail.HuId.Trim().ToUpper() == compareString.Trim().ToUpper())
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    else
                    {
                        if (transformerDetail.ItemCode != null && transformerDetail.ItemCode.Trim().ToUpper() != compareString.Trim().ToUpper())
                        {
                            rowIndex++;
                        }
                        else if (transformerDetail.ItemCode != null && transformerDetail.ItemCode.Trim().ToUpper() == compareString.Trim().ToUpper())
                        {
                            isMatch = true;
                            break;
                        }
                    }

                }
            }

            rowIndex = isMatch ? rowIndex : -1;
            return rowIndex;
        }

        public static void RemoveLot(Resolver resolver)
        {
            if (resolver.PickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO && resolver != null && resolver.Transformers != null)
            {
                for (int i = 0; i < resolver.Transformers.Length; i++)
                {
                    if (resolver.Transformers[i].TransformerDetails != null)
                    {
                        for (int j = 0; j < resolver.Transformers[i].TransformerDetails.Length; j++)
                        {
                            if (resolver.Transformers[i].TransformerDetails[j].HuId == null || resolver.Transformers[i].TransformerDetails[j].HuId == string.Empty)
                            {
                                resolver.Transformers[i].TransformerDetails[j] = null;
                            }
                        }
                    }
                }
            }

        }

        public static void PrintOrder(string fileUrl, IWin32Window win32Window, string printer)
        {
            KillProcess("EXCEL");
            Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook myBook = null;
            Microsoft.Office.Interop.Excel.Worksheet mySheet1 = null;

            Object missing = System.Reflection.Missing.Value;
            Object defaultPrint = missing;

            //string print = Settings.Default.DefaultPrintName1;
            if (printer != null && printer != string.Empty)
            {
                defaultPrint = printer;
            }

            try
            {
                
                myBook = myExcel.Workbooks.Open(fileUrl, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //handle sheets
                mySheet1 = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Sheets[1];
                mySheet1.PrintOut(missing, missing, missing, missing, defaultPrint, missing, missing, missing);
            }
            catch (Exception e)
            {
                string errorMsg = "打印失败,重打请按CTRL+P!错误信息:" + e.Message;
                MessageBox.Show(win32Window, errorMsg, "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (myBook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(myBook);
                }
                if (mySheet1 != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mySheet1);
                }
                myBook = null;
                mySheet1 = null;
                myExcel.Quit();
                GC.Collect();
            }
        }

        #region 杀死进程
        private static void KillProcess(string processName)
        {
            //获得进程对象，以用来操作   
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程    
            try
            {
                //获得需要杀死的进程名   
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    //立即杀死进程   
                    thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                //throw new Exception("", Exc);
            }
        }
        #endregion

        public static void DataGridViewDecimalFilter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b" || e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "-")
            {
                string str;
                if (e.KeyChar.ToString() == "\b")
                {
                    e.Handled = false;
                    return;
                }
                else
                {
                    str = ((DataGridViewTextBoxEditingControl)sender).Text + e.KeyChar.ToString();
                }

                if (Utility.IsDecimal(str) || str == "-")
                {
                    e.Handled = false;
                    return;
                }
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void DataGridViewIntFilter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b" || e.KeyChar.ToString() == "-")
            {
                string str;
                if (e.KeyChar.ToString() == "\b")
                {
                    e.Handled = false;
                    return;
                }
                else
                {
                    str = ((DataGridViewTextBoxEditingControl)sender).Text + e.KeyChar.ToString();
                }

                if (Utility.IsDecimal(str) || str == "-")
                {
                    e.Handled = false;
                    return;
                }
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void TextBoxDecimalFilter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b" || e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "-")
            {
                string str;
                if (e.KeyChar.ToString() == "\b")
                {
                    e.Handled = false;
                    return;
                }
                else
                {
                    str = ((TextBox)sender).Text.Trim() + e.KeyChar.ToString();
                }
                if (Utility.IsDecimal(str) || str == "-")
                {
                    e.Handled = false;
                    return;
                }
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void TextBoxIntFilter(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void Log(string logstr)
        {
            FileStream fs = new FileStream("C:\\SconitTemp\\Sconit_CSLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(logstr + " " + DateTime.Now.ToString() + "\n");
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }

        //===============//
        public static Resolver ProcessOriginalResolver(Resolver resolver, Resolver originalResolver)
        {
            CheckMatchHuExist(resolver);
            DeepCopyResolver(resolver, originalResolver);
            if (originalResolver.Transformers != null)
            {
                foreach (Transformer t in originalResolver.Transformers)
                {
                    if (t.TransformerDetails != null)
                    {
                        t.TransformerDetails = null;
                    }
                }
            }

            return originalResolver;
        }

        public static Resolver MergeResolver(Resolver resolver, Resolver originalResolver)
        {
            if (originalResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT
                && resolver.Transformers != null)
            {
                foreach (Transformer t in originalResolver.Transformers)
                {
                    if (t.TransformerDetails != null)
                    {
                        foreach (TransformerDetail td in t.TransformerDetails)
                        {
                            if (td.CurrentQty > 0)
                            {
                                TransformerDetail transformerDetail = new TransformerDetail();
                                CopyProperty(td, transformerDetail);
                                AddTransformerDetail(resolver, transformerDetail);
                            }
                        }
                    }
                }
                //Transformer[] transformers = resolver.Transformers;
                //DeepCopyResolver(originalResolver, resolver);
                //resolver.Transformers = transformers;
            }
            else if (originalResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_LOCATION
                || originalResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
            {
                Transformer[] transformers = resolver.Transformers;
                DeepCopyResolver(originalResolver, resolver);
                resolver.Transformers = transformers;
            }
            else
            {
                DeepCopyResolver(originalResolver, resolver);
            }
            return resolver;
        }

        /// <summary>
        /// 分步取消
        /// </summary>
        /// <param name="resolver"></param>
        public static void CancelOperation(Resolver resolver)
        {
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
            {
                CancelRepackOperation(resolver);
                return;
            }

            if (AccumlationCurrentQty(resolver) == 0)
            {
                resolver.Transformers = null;
                resolver.BinCode = null;
                resolver.Code = null;
                resolver.CodePrefix = null;
                resolver.Description = null;
                resolver.OrderType = null;
                resolver.PickBy = null;
                resolver.BarcodeHead = null;
                resolver.LocationCode = null;
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                resolver.Result = string.Empty;
                return;
            }

            if (FindMaxSeq(resolver.Transformers) >= 0)
            {
                //最后一条记录的当前数设置CurrentQty为0 设置序号Sequence为-1 //为了兼容带条码发货
                int[] indexArray = FindMaxSeqTransformerDetailRowAndColumnIndex(resolver.Transformers);
                if (resolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver.Transformers[indexArray[0]].Qty -= resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].Qty;
                }
                resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].CurrentQty = 0;
                resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].Sequence = -1;
                //设置Bin为最后一库格
                indexArray = FindMaxSeqTransformerDetailRowAndColumnIndex(resolver.Transformers);
                if (indexArray != null)
                {
                    resolver.BinCode = resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].StorageBinCode;
                }
            }

            ProcessTransformer(resolver.Transformers);
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP
                || (IsHasTransformerDetail(resolver) && resolver.ModuleType != BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
                )
            {
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
                if (resolver.BinCode != null && resolver.BinCode != string.Empty)
                {
                    resolver.Result = "库格:" + resolver.BinCode;
                }
            }
            else
            {
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            }
            resolver.BarcodeHead = string.Empty;
        }


        #region 私有方法
        /// <summary>
        /// 翻箱取消操作
        /// </summary>
        /// <param name="resolver"></param>
        private static void CancelRepackOperation(Resolver resolver)
        {
            if (resolver.Transformers.Length == 2)
            {
                if (resolver.Transformers[1].TransformerDetails != null && resolver.Transformers[1].TransformerDetails.Length > 0)
                {
                    //int maxSeq = FindMaxSeq(resolver.Transformers[1]);
                    //resolver.Transformers[1].TransformerDetails.RemoveAt(maxSeq);
                    resolver.Transformers[1].TransformerDetails.OrderBy(t => t.Sequence);
                    resolver.Transformers[1].TransformerDetails.Take(resolver.Transformers[1].TransformerDetails.Length - 1);
                    resolver.IOType = BusinessConstants.IO_TYPE_OUT;
                }
                else if ((resolver.Transformers[1].TransformerDetails == null || resolver.Transformers[1].TransformerDetails.Length == 0)
                    && resolver.IOType == BusinessConstants.IO_TYPE_OUT && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
                {
                    resolver.IOType = BusinessConstants.IO_TYPE_IN;
                }
                else if (resolver.Transformers[0].TransformerDetails != null && resolver.Transformers[0].TransformerDetails.Length > 0)
                {
                    resolver.Transformers[0].TransformerDetails.OrderBy(t => t.Sequence);
                    resolver.Transformers[0].TransformerDetails.Take(resolver.Transformers[1].TransformerDetails.Length - 1);
                    resolver.IOType = BusinessConstants.IO_TYPE_IN;
                }
            }
        }

        private static decimal AccumlationCurrentQty(Resolver resolver)
        {
            decimal totalQty = 0;
            if (resolver != null && resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            totalQty += transformerDetail.CurrentQty;
                        }
                    }
                }
            }
            return totalQty;
        }

        private static decimal AccumlationCurrentQty(TransformerDetail[] transformerDetails)
        {
            if (transformerDetails == null)
            {
                return 0m;
            }
            var q = (from td in transformerDetails
                     select td.CurrentQty).Sum();
            return q;
        }

        private static int FindMaxSeq(Transformer[] transformers)
        {
            int maxSeq = 0;
            if (transformers != null)
            {
                foreach (Transformer transformer in transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.Sequence > maxSeq)
                            {
                                maxSeq = transformerDetail.Sequence;
                            }
                        }
                    }
                }
            }
            return maxSeq;
        }

        /// <summary>
        /// 查找最大序号的TransformerDetail所在的行和列
        /// </summary>
        /// <param name="transformerList"></param>
        /// <returns></returns>
        private static int[] FindMaxSeqTransformerDetailRowAndColumnIndex(Transformer[] transformerList)
        {
            int maxSeq = FindMaxSeq(transformerList);
            if (transformerList != null)
            {
                for (int i = 0; i < transformerList.Length; i++)
                {
                    if (transformerList[i].TransformerDetails != null)
                    {
                        for (int j = 0; j < transformerList[i].TransformerDetails.Length; j++)
                        {
                            if (transformerList[i].TransformerDetails[j].Sequence == maxSeq)
                            {
                                return new int[] { i, j };
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static void ProcessTransformer(Transformer[] transformerList)
        {
            if (transformerList != null && transformerList.Length > 0)
            {
                foreach (Transformer transformer in transformerList)
                {
                    if (transformer != null && transformer.TransformerDetails != null)
                    {
                        transformer.CurrentQty = AccumlationCurrentQty(transformer.TransformerDetails);
                        transformer.Cartons = GetSumCartons(transformer.TransformerDetails);
                    }
                }
            }
        }

        private static int GetSumCartons(TransformerDetail[] transformerDetails)
        {
            var q = (from td in transformerDetails
                     where td.CurrentQty != 0 && td.HuId != null && td.HuId.Trim() != string.Empty
                     select td).Count();
            return q;
        }

        private static void DeepCopyResolver(Resolver sourceResolver, Resolver targetResolver)
        {
            CopyProperty(sourceResolver, targetResolver);
            List<Transformer> transformers = new List<Transformer>();
            if (sourceResolver != null && sourceResolver.Transformers != null)
            {
                foreach (var st in sourceResolver.Transformers)
                {
                    Transformer transformer = new Transformer();
                    CopyProperty(st, transformer);

                    #region TransformerDetail
                    List<TransformerDetail> transformerDetails = new List<TransformerDetail>();
                    if (st.TransformerDetails != null && st.TransformerDetails.Length > 0)
                    {
                        foreach (var std in st.TransformerDetails)
                        {
                            TransformerDetail transformerDetail = new TransformerDetail();
                            CopyProperty(std, transformerDetail);
                            transformerDetails.Add(transformerDetail);
                        }
                    }
                    if (transformerDetails.Count > 0)
                    {
                        transformer.TransformerDetails = transformerDetails.ToArray();
                    }
                    #endregion

                    transformers.Add(transformer);
                }
                if (transformers.Count > 0)
                {
                    targetResolver.Transformers = transformers.ToArray();
                }
            }
        }

        private static void AddTransformerDetail(Resolver resolver, TransformerDetail transformerDetail)
        {
            if (transformerDetail != null && transformerDetail.CurrentQty > 0)
            {
                List<Transformer> transformers = new List<Transformer>();
                if (resolver.Transformers != null)
                {
                    transformers = resolver.Transformers.ToList();
                }
                //检查重复扫描
                //CheckMatchHuExist(resolver, transformerDetail.HuId);

                //自动生成序号
                int seq = FindMaxSeq(transformers.ToArray());
                transformerDetail.Sequence = seq + 1;
                //匹配:严格匹配ItemCode/UomCode/UnitCount/StorageBinCode/LotNo
                var query = transformers.Where
                    (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
                           && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
                           && (t.UnitCount == transformerDetail.UnitCount || t.UnitCount == transformerDetail.Qty)
                           && string.Equals(t.StorageBinCode, transformerDetail.StorageBinCode, StringComparison.OrdinalIgnoreCase)
                           && (t.LotNo == null || t.LotNo.Trim() == string.Empty || (string.Equals(t.LotNo, transformerDetail.LotNo, StringComparison.OrdinalIgnoreCase)))
                          ));
                //匹配:如果没有匹配上,降低条件,匹配ItemCode/UomCode/UnitCount
                if (query.Count() == 0)
                {
                    query = transformers.Where
                    (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
                           && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
                           && (t.UnitCount == transformerDetail.UnitCount || t.UnitCount == transformerDetail.Qty)
                           ));
                }
                //匹配:如果还是没有匹配上,再降低条件,匹配ItemCode/UomCode
                if (query.Count() == 0)
                {
                    query = transformers.Where
                    (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
                           && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
                           ));
                }
                if (query.Count() > 0)
                {
                    List<Transformer> transformerList = query.ToList();
                    foreach (var transformer in transformerList)
                    {
                        if (transformer.Qty > transformer.CurrentQty || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING)
                        {
                            List<TransformerDetail> transformerDetails = new List<TransformerDetail>();
                            foreach (var t in transformers)
                            {
                                if (t.TransformerDetails != null && t.TransformerDetails.Length > 0)
                                {
                                    foreach (var td in t.TransformerDetails)
                                    {
                                        if (td.HuId != null && td.HuId.ToLower() == transformerDetail.HuId.ToLower())
                                        {
                                            transformerDetails.Add(td);
                                        }
                                    }
                                }
                            }
                            if (transformerDetails.Count() == 1 && transformerDetails.Single().CurrentQty == 0M)
                            {
                                transformerDetails.Single().CurrentQty = transformerDetail.CurrentQty;
                                transformerDetails.Single().Sequence = transformerDetail.Sequence;
                                transformer.CurrentQty += transformerDetail.CurrentQty;
                                transformer.Cartons++;
                            }
                            else
                            {
                                AddTransformerDetail(transformer, transformerDetail);
                            }
                            break;
                        }
                    }
                }
                else if (query.Count() == 0)
                {
                    Transformer transformer = new Transformer();
                    transformer.ItemCode = transformerDetail.ItemCode;
                    transformer.ItemDescription = transformerDetail.ItemDescription;
                    transformer.UomCode = transformerDetail.UomCode;
                    transformer.UnitCount = transformerDetail.UnitCount;
                    transformer.CurrentQty = transformerDetail.CurrentQty;
                    transformer.Qty = transformerDetail.Qty;
                    transformer.LocationCode = transformerDetail.LocationCode;
                    transformer.LotNo = transformerDetail.LotNo;
                    transformer.StorageBinCode = transformerDetail.StorageBinCode;
                    transformer.Cartons = 1;
                    transformer.Sequence = transformers.Count > 0 ? transformers.Max(t => t.Sequence) + 1 : 0;

                    AddTransformerDetail(transformer, transformerDetail);
                    transformers.Add(transformer);
                }
                //else
                //{
                //    //throw new Exception("Error on: Sconit_CS.Utility");
                //}
                resolver.Transformers = transformers.ToArray();
            }
            ProcessTransformer(resolver.Transformers);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        private static void AddTransformerDetail(Transformer transformer, TransformerDetail transformerDetail)
        {
            if (transformer == null || transformerDetail.CurrentQty == 0)
            {
                return;
            }
            if (transformerDetail != null)
            {
                List<TransformerDetail> transformerDetails = new List<TransformerDetail>();
                if (transformer.TransformerDetails != null)
                {
                    transformerDetails = transformer.TransformerDetails.ToList();
                }
                transformerDetails.Add(transformerDetail);
                transformer.TransformerDetails = transformerDetails.ToArray();
                transformer.CurrentQty += transformerDetail.CurrentQty;
                transformer.Cartons++;
            }
        }

        private static void CheckMatchHuExist(Resolver resolver)
        {
            CheckMatchHuExist(resolver, resolver.Input);
        }

        private static void CheckMatchHuExist(Resolver resolver, string huId)
        {
            if (resolver != null && resolver.Transformers != null && resolver.Transformers.Length > 0)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null && transformer.TransformerDetails.Length > 0)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.HuId != null
                                && transformerDetail.HuId.Trim().ToUpper() == huId.ToUpper()
                                && transformerDetail.CurrentQty != 0)
                            {
                                throw new FaultException("不能重复扫描,条码:" + huId);
                            }
                        }
                    }
                }
            }
        }

        private static void CopyProperty(object sourceObj, object targetObj)
        {
            PropertyInfo[] sourcePropertyInfoAry = sourceObj.GetType().GetProperties();
            PropertyInfo[] targetPropertyInfoAry = targetObj.GetType().GetProperties();

            foreach (PropertyInfo sourcePropertyInfo in sourcePropertyInfoAry)
            {
                foreach (PropertyInfo targetPropertyInfo in targetPropertyInfoAry)
                {
                    if (sourcePropertyInfo.Name == targetPropertyInfo.Name)
                    {
                        if (targetPropertyInfo.CanWrite && sourcePropertyInfo.CanRead)
                        {
                            targetPropertyInfo.SetValue(targetObj, sourcePropertyInfo.GetValue(sourceObj, null), null);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
