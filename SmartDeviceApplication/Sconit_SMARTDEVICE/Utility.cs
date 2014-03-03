using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;
using Sconit_SMARTDEVICE.SconitWS;
using System.IO;
using Sconit_SMARTDEVICE.Properties;
using System.Reflection;
using System.Web.Services.Protocols;

namespace Sconit_SD
{
    public class Utility
    {
        public static string FormatExMessage(string message)
        {
            try
            {
                if (message.StartsWith("System.Web.Services.Protocols.SoapException"))
                {
                    message = message.Remove(0, 44);
                    int index = message.IndexOf("\n");
                    if (index > 0)
                    {
                        message = message.Remove(index, message.Length - index);
                    }
                    index = message.IndexOf("\r\n");
                    if (index > 0)
                    {
                        message = message.Remove(index, message.Length - index);
                    }
                }
                message = message.Replace("\\n", "\r\n");
                return message;
            }
            catch (Exception ex)
            {
                return message;
            }
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

        public static Transformer[] SumCurrentQty(Transformer[] transformerArray)
        {
            foreach (Transformer transformer in transformerArray)
            {
                transformer.CurrentQty = 0;
                transformer.Cartons = 0;
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    transformer.CurrentQty += transformerDetail.CurrentQty;
                    if (transformerDetail.CurrentQty != 0)
                    {
                        transformer.Cartons++;
                    }
                }

            }
            return transformerArray;
        }

        public static void ValidatorDecimal(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b" || e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "-")
            {
                string str = string.Empty;
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

        public static void ShowMessageBox(string message)
        {
            if (message != null && message != string.Empty)
            {
                message = FormatExMessage(message);
                Sound sound = new Sound(Resources.Error);
                sound.Play();
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            }
        }

        public static bool IsHasTransformerDetail(Resolver resolver)
        {
            if (resolver != null && resolver.Transformers != null)
            {
                decimal CurrentQty = 0;
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        CurrentQty += transformer.CurrentQty;
                        if (transformer.TransformerDetails != null)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                CurrentQty += transformerDetail.CurrentQty;
                            }
                        }
                    }
                }
                if (CurrentQty > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static DataGrid RenderDataGridBackColor(DataGrid dataGrid)
        {
            //foreach (DataGridViewRow row in dataGrid.)
            //{
            //    decimal CurrentQty = Convert.ToDecimal(row.Cells["CurrentQty"].Value.ToString());
            //    decimal Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString());
            //    if (CurrentQty == Qty)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.YellowGreen;
            //    }
            //    else if (CurrentQty > Qty)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.OrangeRed;
            //    }
            //    else if (CurrentQty < Qty)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Yellow;
            //    }
            //}
            return dataGrid;
        }
        /////ADD 本地缓存   throw new SoapException("此物料已经匹配满了:" + transformerDetail.ItemCode, SoapException.ServerFaultCode, string.Empty);
        //===============//
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
            resolver.BarcodeHead = originalResolver.BarcodeHead;
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
                                throw new SoapException("不能重复扫描,条码:" + huId, SoapException.ServerFaultCode, string.Empty);
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

        ///////////////////
    }

    public class Sound
    {
        private byte[] m_soundBytes;

        private string m_fileName;

        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySound(string szSound, IntPtr hMod, int flags);

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySoundBytes(byte[] szSound, IntPtr hMod, int flags);

        /// <summary>
        /// Construct the Sound object to play sound data from the specified file.
        /// </summary>
        public Sound(string fileName)
        {
            m_fileName = fileName;
        }

        /// <summary>
        /// Construct the Sound object to play sound data from the specified stream.
        /// </summary>
        public Sound(Stream stream)
        {
            // read the data from the stream
            m_soundBytes = new byte[stream.Length];
            stream.Read(m_soundBytes, 0, (int)stream.Length);
        }

        /// <summary>
        /// Construct the Sound object to play sound data from the specified byte.
        /// </summary>
        public Sound(byte[] m_soundBytes)
        {
            this.m_soundBytes = m_soundBytes;
        }

        /// <summary>
        /// Play the sound
        /// </summary>
        public void Play()
        {
            // if a file name has been registered, call WCE_PlaySound,
            //  otherwise call WCE_PlaySoundBytes
            try
            {
                if (m_fileName != null)
                {
                    WCE_PlaySound(m_fileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
                }
                else
                {
                    WCE_PlaySoundBytes(m_soundBytes, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_MEMORY));
                }

            }
            catch (Exception)
            {
                //throw;
            }
        }
        

    }
}
