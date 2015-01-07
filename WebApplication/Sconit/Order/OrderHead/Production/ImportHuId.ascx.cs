using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using System.Collections;
using NPOI.SS.UserModel;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Dss;
using com.Sconit.Entity.Exception;


public partial class Order_OrderHead_Production_ImportHuId : ModuleBase
{
    public event EventHandler ImportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            HSSFWorkbook excel = new HSSFWorkbook(fileUpload.PostedFile.InputStream);
            Sheet sheet = excel.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();
            ImportHelper.JumpRows(rows, 10);
            //生产线	物料号	物料描述	条码	数量	上线日期	上线时间	生产单号	


            #region 列定义
            int colProdLine = 1;//供货路线
            int colItemCode = 2;//物料号
            int colHuId = 4;// 条码号
            int colQty = 5;//订单数
            int colOfflineDateStr = 6;//上线日期
            int colOfflineTimeStr = 7;//上线时间
            int colOrderNo = 8;//生产单号
            #endregion
            int rowCount = 10;
            //IList<Exception> exceptionList = new List<Exception>();
            //Exception exceptio = new Exception();
            string errorMessage = string.Empty;
            DateTime nowTime = DateTime.Now;
            DssInboundControl control=TheGenericMgr.FindById<DssInboundControl>(9);
            IList<DssImportHistory> importList = new List<DssImportHistory>();
            while (rows.MoveNext())
            {
                rowCount++;
                HSSFRow row = (HSSFRow)rows.Current;
                if (!TheImportMgr.CheckValidDataRow(row, 1, 4))
                {
                    break;//边界
                }
                string prodLineCode = string.Empty;
                Item Item =null;
                string huId = string.Empty;
                decimal qty = 0;
                string offlineDateStr = string.Empty;
                string offlineTimeStr = string.Empty;
                string orderNo = string.Empty;

                #region 读取数据
                #region 生产线
                prodLineCode = row.GetCell(colProdLine) != null ? row.GetCell(colProdLine).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(prodLineCode))
                {
                    //ShowErrorMessage(string.Format("第{0}行:供货路线不能为空。", rowCount));
                    errorMessage += string.Format("第{0}行:生产线不能为空。<br/>", rowCount);
                    continue;
                }
                #endregion

                #region 读取物料代码
                string itemCode = row.GetCell(colItemCode) != null ? row.GetCell(colItemCode).StringCellValue : string.Empty;
                if (itemCode == null || itemCode.Trim() == string.Empty)
                {
                    errorMessage += string.Format("第{0}行:物料代码不能为空。<br/>", rowCount);
                    //ShowErrorMessage(string.Format("第{0}行:物料代码不能为空。", rowCount));
                    continue;
                }
                else
                {
                    Item = this.TheGenericMgr.FindById<Item>(itemCode);
                    if (Item==null)
                    {
                        errorMessage += string.Format("第{0}行:物料代码{1}不存在。<br/>", rowCount, itemCode);
                        continue;
                    }
                }

                #endregion

                #region 条码
                huId = row.GetCell(colHuId) != null ? row.GetCell(colHuId).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(huId))
                {
                    errorMessage += string.Format("第{0}行:条码不能为空。<br/>", rowCount);
                    continue;
                }
                else
                {
                    if (huId.Length < 9)
                    {
                        errorMessage += string.Format("第{0}行:条码长度不能小于9。<br/>", rowCount);
                        continue;
                    }
                    var yearCodeArr =new string[]{"1","2","3","4","5","6","7","8","9","A","B","C","D","E","F","G","H","J","K","L","M","N","P","Q","S","T","V","W","X","Y"};
                    var monthCodeArr =new string[]{"1","2","3","4","5","6","7","8","9","A","B","C"};
                    var yearCode = huId.Substring(huId.Length - 8, 1);
                    if (yearCodeArr.Where(a => a == yearCode).Count() == 0)
                    {
                        errorMessage += string.Format("第{0}行:批号的年份格式不正确。。<br/>", rowCount);
                        continue;
                    }

                    var monthCode = huId.Substring(huId.Length - 7, 1);
                    if (monthCodeArr.Where(a => a == monthCode).Count() == 0)
                    {
                        errorMessage += string.Format("第{0}行:批号的月份格式不正确。。。<br/>", rowCount);
                        continue;
                    }

                    var dayCode =int.Parse( huId.Substring(huId.Length - 6, 2));
                    if (dayCode<1 || dayCode>31)
                    {
                        errorMessage += string.Format("第{0}行:批号的日期格式不正确。<br/>", rowCount);
                        continue;
                    }
                }
                #endregion

                #region 读取数量
                try
                {
                    qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                }
                catch
                {
                    errorMessage += string.Format("第{0}行:数量填写有误。<br/>", rowCount);
                    continue;
                }
                #endregion

                #region 上线日期
                offlineDateStr = row.GetCell(colOfflineDateStr) != null ? row.GetCell(colOfflineDateStr).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(offlineDateStr))
                {
                    errorMessage += string.Format("第{0}行:上线日期不能为空。<br/>", rowCount);
                    continue;
                }
                #endregion

                #region 上线时间
                offlineTimeStr = row.GetCell(colOfflineTimeStr) != null ? row.GetCell(colOfflineTimeStr).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(offlineTimeStr))
                {
                    errorMessage += string.Format("第{0}行:上线时间不能为空。<br/>", rowCount);
                    continue;
                }
                else
                {
                    try
                    {
                        var offlineDateTime = DateTime.Parse(offlineDateStr + " " + offlineTimeStr);
                    }
                    catch (Exception ex)
                    {
                        errorMessage += string.Format("第{0}行:[上线日期{1}+上线时间{2}]不符合要求。<br/>", rowCount, offlineDateStr, offlineTimeStr);
                        continue;
                    }
                }
                #endregion
               


                #region 生产线
                orderNo = row.GetCell(colOrderNo) != null ? row.GetCell(colOrderNo).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(orderNo))
                {
                    errorMessage += string.Format("第{0}行:生产单号不能为空。<br/>", rowCount);
                    continue;
                }
                #endregion
                #endregion

                #region 填充数据
                DssImportHistory dssImportHistory = new DssImportHistory { 
                    data0=prodLineCode,
                    data1=Item.Code,
                    data2=huId,
                    data3=qty.ToString(),
                    data7=offlineDateStr,
                    data8=offlineTimeStr,
                    data12=orderNo,
                    IsActive=true,
                    ErrorCount=0,
                    CreateDate = nowTime,
                    LastModifyDate = nowTime,
                    LastModifyUser=CurrentUser.Code,
                    DssInboundCtrl = control,
                    EventCode = "CREATE",
                    KeyCode="EXCEL",
                };
                importList.Add(dssImportHistory);
               
                #endregion
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }
            if (importList.Count == 0)
            {
                throw new Exception("导入的有效数据为0.");
            }

            //try
            //{
            //    foreach (var dssImpHis in importList)
            //    {
            //        TheGenericMgr.Create(dssImpHis);
            //    }
            //}
            //catch (Exception ex)
            //{
                
            //    throw ex;
            //}


            TheMaterialFlushBackMgr.ImportProdItemHuId(importList);

            ShowSuccessMessage("导入成功。");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
            return;
        }

    }


}