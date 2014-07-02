using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Web;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using System.Data.SqlClient;

public partial class NewMrp_ShipPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
            this.StatusSelect.Items.Clear();
            this.StatusSelect.Items.Add(new ListItem("", ""));
            this.StatusSelect.Items.Add(new ListItem("已创建", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            this.StatusSelect.Items.Add(new ListItem("已释放", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        }

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblDateType.SelectedValue));
        //    var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);

        //    this.ListTable(customerPlanList);
        //}
        //catch (BusinessErrorException ex)
        //{
        //    ShowErrorMessage(ex);
        //}
    }

    #region   明细查询

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"select det.Flow,det.Item,i.Desc1,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,det.ShipQty,det.OrgShipQty,m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,det.ReqQty,l.InitStock,l.SafeStock, l.InTransitQty,det.UUID
from  MRP_ShipPlanDet as det 
 inner join MRP_ShipPlanMstr as m on det.ShipPlanId=m.Id 
 inner join Item as i on i.Code=det.Item 
 left join MRP_ShipPlanInitLocationDet as l on det.ShipPlanId=l.ShipPlanId and det.Item=l.Item and det.LocTo=l.Location where 1=1 ";
        if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        {
            searchSql += string.Format(" and det.Flow ='{0}' ", this.tbFlow.Text.Trim());
        }
        
        DateTime startTime = DateTime.Today;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            searchSql += string.Format(" and det.WindowTime>='{0}' ", startTime.Date);
        }
        else
        {
            this.list.InnerHtml = "开始日期不能为空。";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        if (!string.IsNullOrEmpty(this.tbEndDate.Text.Trim()))
        {
            DateTime endTime = DateTime.Today;
            DateTime.TryParse(this.tbEndDate.Text.Trim(), out endTime);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }
        else
        {
            DateTime endTime = startTime.AddDays(14);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }
       
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(this.tbReleaseNo.Text.Trim()))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", this.tbReleaseNo.Text.Trim());
        }                                                       

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var shipPlanDetList = new List<ShipPlanDet>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //det.Flow,det.Item,i.Desc1,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,det.ShipQty,
            //det.OrgShipQty,m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,det.ReqQty,l.InitStock,l.SafeStock, l.InTransitQty
            shipPlanDetList.Add(new ShipPlanDet {
                Flow = row[0].ToString(),
                Item = row[1].ToString(),
                ItemDesc = row[2].ToString(),
                RefItemCode = row[3].ToString(),
                LocFrom = row[4].ToString(),
                LocTo = row[5].ToString(),
                WindowTime = Convert.ToDateTime(row[6]),
                Version = Convert.ToInt32(row[7]),
                ShipQty = Convert.ToDecimal(row[8]),
                OrgShipQty = Convert.ToDecimal(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                Id = Convert.ToInt32(row[14].ToString()),
                ReqQty = Convert.ToDecimal(row[15]),
                InitStock = Convert.ToDecimal(row[16]),
                SafeStock = Convert.ToDecimal(row[17]),
                InTransitQty = Convert.ToDecimal(row[18]),
                UUID = row[19].ToString(),
            });
        }
        if (string.IsNullOrEmpty(this.tbReleaseNo.Text.Trim()) && shipPlanDetList.Count>0)
        {
            var activePlanList = new List<ShipPlanDet>();
            var groupByFlowVersion = shipPlanDetList.GroupBy(d => new { d.Flow}).ToDictionary(d => d.Key, d => d.OrderByDescending(g=>g.ReleaseNo).ToList());
            foreach (var g in groupByFlowVersion)
            {
                activePlanList.AddRange(shipPlanDetList.Where(s => s.Flow == g.Key.Flow && s.ReleaseNo == g.Value.Max(m=>m.ReleaseNo)));
            }
            ListTable(activePlanList);
        }
        else
        {
            ListTable(shipPlanDetList);
        }
    }

    private void ListTable(IList<ShipPlanDet> shipPlanDetList)
    {
        if (shipPlanDetList == null || shipPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        IList<ShipPlanDetTrace> traceList = new List<ShipPlanDetTrace>();
        traceList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.UUID in ('{0}') ",string.Join("','", shipPlanDetList.Select(d=>d.UUID).Distinct().ToArray())));

        if (traceList.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count>0)
                {
                    showText = "<table><thead><tr><th>销售路线</th><th>物料</th><th>需求日期</th><th>需求数</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.DistributionFlow + "</td><td>" + c.Item + "</td><td>" + c.ReqDate + "</td><td>" + c.ReqQty + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }

        var planByDateIndexs = shipPlanDetList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
        var planByFlowItems = shipPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item,p.LocFrom,p.LocTo });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>行数</th><th rowspan='2'>发运计划版本</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>安全库存</th><th rowspan='2'>期初库存</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='3'>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >需求数</th><th >发货数</th><th >期末</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
         if (ii > 14)
        {
            widths = "240%";
        }
        else if (ii > 10)
        {
            widths = "200%";
        }
        else if (ii > 6)
        {
            widths = "150%";
        }
      
        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d=>d.WindowTime).ToDictionary(d => d.Key, d => d.Sum(q=>q.ShipQty));
            l++;
            if (l % 2 == 0)
            {
                str.Append("<tr class='GVAlternatingRow'>");
            }
            else
            {
                str.Append("<tr class='GVRow'>");
            }
            str.Append("<td>");
            str.Append(l);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ReleaseNo);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Flow);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append((firstPlan.InTransitQty + firstPlan.InitStock).ToString("0.##"));
            str.Append("</td>");

            var InitStockQty = firstPlan.InTransitQty + firstPlan.InitStock;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                //str.Append("<th >需求数</th><th >发货数</th><th >期末</th>");
                //var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
                var curenPlan = planByFlowItem.Where(p => p.WindowTime == planByDateIndex.Key);
                var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", shipPlanDet.Logs));
                str.Append(shipPlanDet.ReqQty.ToString("0.##"));
                str.Append("</td>");
                if (firstPlan.Status ==BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + shipPlanDet.Id + "'value='" + shipPlanDet.ShipQty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "' style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' />");
                    str.Append("</td>");
                }                   
                else
                {
                    str.Append("<td>");
                    str.Append(shipPlanDet.ShipQty.ToString("0.##"));
                    str.Append("</td>");
                }
                InitStockQty = InitStockQty + shipPlanDet.ShipQty - shipPlanDet.ReqQty;
                str.Append("<td>");
                str.Append(InitStockQty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr+str.ToString();
    }


    //public void btnReplace_Click(object sender, EventArgs e)
    //{
    //    var allHtml = this.list.InnerHtml;
    //    var dd = btHidden.Value;

    //    int startIndex = dd.IndexOf("flow='");
    //    dd = dd.Substring(startIndex + 6);
    //    int endIndex = dd.IndexOf("'");
    //    string flow=allHtml.Substring(0, endIndex);

    //    startIndex = allHtml.IndexOf("item='");
    //    allHtml = allHtml.Substring(startIndex + 6);
    //    endIndex = allHtml.IndexOf("'");
    //    itemList.Add(allHtml.Substring(0, endIndex));

    //    startIndex = allHtml.IndexOf("id='");
    //    allHtml = allHtml.Substring(startIndex + 4);
    //    endIndex = allHtml.IndexOf("'");
    //    idList.Add(allHtml.Substring(0, endIndex));

    //    startIndex = allHtml.IndexOf("value='");
    //    allHtml = allHtml.Substring(startIndex + 7);
    //    endIndex = allHtml.IndexOf("'");
    //    qtyList.Add(allHtml.Substring(0, endIndex));

    //    startIndex = allHtml.IndexOf("releaseNo='");
    //    allHtml = allHtml.Substring(startIndex + 11);
    //    endIndex = allHtml.IndexOf("'");
    //    releaseNoList.Add(allHtml.Substring(0, endIndex));

    //    startIndex = allHtml.IndexOf("dateFrom='");
    //    allHtml = allHtml.Substring(startIndex + 10);
    //    endIndex = allHtml.IndexOf("'");
    //    dateFromList.Add(allHtml.Substring(0, endIndex));

    //    startIndex = allHtml.IndexOf("'"+seq+"'");
    //    var prevHtml = allHtml.Substring(0, startIndex - 1);
    //    startIndex = allHtml.LastIndexOf("value='");



    //}
    #endregion


    #region    头查询
    protected void btnMstrSearch_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"select m.ReleaseNo,m.BatchNo,m.Status,m.LastModifyDate,m.LastModifyUser,l.Item,i.desc1,l.Flow,l.Msg from MRP_ShipPlanMstr as  m 
left join MRP_RunShipPlanLog as l on m.BatchNo=l.BatchNo
left join Item as i on l.Item=i.Code where 1=1  ";
       

        if (!string.IsNullOrEmpty(this.StatusSelect.Value))
        {
            searchSql += string.Format(" and m.Status ='{0}' ", this.StatusSelect.Value);
        }

        if (!string.IsNullOrEmpty(this.tbMstrReleaseNo.Text.Trim()))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", this.tbMstrReleaseNo.Text.Trim());
        }

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql+" order by m.ReleaseNo desc ").Tables[0];
        var shipPlanMstrList = new List<ShipPlanMstr>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.ReleaseNo,m.BatchNo,m.Status,m.LastModifyDate,m.LastModifyUser,l.Item,i.desc1,l.Flow,l.Msg
            shipPlanMstrList.Add(new ShipPlanMstr
            {
                ReleaseNo =int.Parse( row[0].ToString()),
                BatchNo = int.Parse(row[1].ToString()),
                Status = row[2].ToString(),
                LastModifyDate = DateTime.Parse( row[3].ToString()),
                LastModifyUser = row[4].ToString(),
                Item = row[5].ToString(),
                ItemDesc =row[6].ToString(),
                Flow =row[7].ToString(),
                Msg =row[8].ToString(),
            });
        }
        ListTable(shipPlanMstrList);
    }

    private void ListTable(IList<ShipPlanMstr> shipPlanMstrList)
    {
        if (shipPlanMstrList == null || shipPlanMstrList.Count == 0)
        {
            this.mstrList.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        var planByReleaseNo = shipPlanMstrList.GroupBy(p => p.ReleaseNo);

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th>行数</th><th>发运计划版本</th><th>状态</th><th>最后修改用户</th><th>最后修改时间</th><th>路线</th><th>物料号</th><th>物料描述</th><th>错误消息</th>");
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        foreach (var g in planByReleaseNo)
        {
            var firstPlan = g.First();
            int s = 0;
            foreach (var r in g)
            {
                l++;
                if (l % 2 == 0)
                {
                    str.Append("<tr class='GVAlternatingRow'>");
                }
                else
                {
                    str.Append("<tr class='GVRow'>");
                }
                if (s == 0)
                {
                    str.Append("<td>");
                    str.Append(l);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.ReleaseNo);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.Status);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.LastModifyUser);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.LastModifyUser);
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td></td><td></td><td></td><td></td><td></td>");
                }
                str.Append("<td>");
                str.Append(r.Flow);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(r.Item);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(r.ItemDesc);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(r.Msg);
                str.Append("</td>");
                s++;
            }
        }
        str.Append("</tbody></table>");
        this.mstrList.InnerHtml = headStr + str.ToString();
    }
    #endregion

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        if (this.rblAction.SelectedIndex == 0)
        {
            this.tblImport.Visible = false;
            this.mstrList.Visible = false;
            this.tblSearch.Visible = true;
            this.list.Visible = true;
        }
        else
        {
            this.tblImport.Visible = true;
            this.mstrList.Visible = true;
            this.tblSearch.Visible = false;
            this.list.Visible = false;
        }
    }

    private string CopyString()
    {
                 //底色黄色重新导入无改动,橙色重新导入并有改动

        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>
                <script type='text/javascript'>
                    function copyHtml()
                    {
                        window.clipboardData.setData('Text', $('#ctl01_list')[0].innerHTML);
                    }
                </script>";
    }

    protected void btnRunProdPlan_Click(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunProductionPlan(this.CurrentUser);
            ShowSuccessMessage("生成成功。");
        }
        catch (SqlException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception ee)
        {
            ShowErrorMessage(ee.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //}
        //foreach (Control c in this.list.Controls)
        //{
        //    if (c is TextBox)
        //    {
        //        TextBox currentBox = (TextBox)c;
        //    }
        //}

        //str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + shipPlanDet.Id + "'value='" + shipPlanDet.ShipQty.ToString("0.##") + "' style='width:70px' />");
        try
        {

            var allSeqArr = string.IsNullOrEmpty(this.btSeqHidden.Value)?new string[0]:this.btSeqHidden.Value.Split(',');
            var allShipQty = string.IsNullOrEmpty(this.btQtyHidden.Value) ? new string[0] : this.btQtyHidden.Value.Split(',');
            string allHtml = this.list.InnerHtml;
            IList<string> flowList = new List<string>();
            IList<string> itemList = new List<string>();
            IList<string> qtyList = new List<string>();
            IList<string> idList = new List<string>();
            IList<string> releaseNoList = new List<string>();
            IList<string> dateFromList = new List<string>();

            string flow = string.Empty;
            string item = string.Empty;
            string qty = string.Empty;
            string id = string.Empty;
            string releaseNo = string.Empty;
            string dateFrom = string.Empty;
            string seq = string.Empty;
            while (allHtml.Length > 0)
            {
                int startIndex = allHtml.IndexOf("flow='");
                if (startIndex == -1) { allHtml = string.Empty; break; }
                allHtml = allHtml.Substring(startIndex + 6);
                int endIndex = allHtml.IndexOf("'");
                flow = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("item='");
                allHtml = allHtml.Substring(startIndex + 6);
                endIndex = allHtml.IndexOf("'");
                item = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("id='");
                allHtml = allHtml.Substring(startIndex + 4);
                endIndex = allHtml.IndexOf("'");
                id = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("value='");
                allHtml = allHtml.Substring(startIndex + 7);
                endIndex = allHtml.IndexOf("'");
                qty = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("releaseNo='");
                allHtml = allHtml.Substring(startIndex + 11);
                endIndex = allHtml.IndexOf("'");
                releaseNo = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("dateFrom='");
                allHtml = allHtml.Substring(startIndex + 10);
                endIndex = allHtml.IndexOf("'");
                dateFrom = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("seq='");
                allHtml = allHtml.Substring(startIndex + 5);
                endIndex = allHtml.IndexOf("'");
                seq = allHtml.Substring(0, endIndex);
                if (allSeqArr.Contains(seq))
                {
                    int i = 0;
                    foreach (var s in allSeqArr)
                    {
                        i++;
                        if (s == seq) break;
                    }
                    if (allShipQty[i-1] == qty)
                    { }
                    else
                    {
                        flowList.Add(flow);
                        itemList.Add(item);
                        idList.Add(id);
                        qtyList.Add(allShipQty[i-1]);
                        releaseNoList.Add(releaseNo);
                        dateFromList.Add(dateFrom);
                    }
                }

                
            }
            IList<decimal> shipQtyList = new List<decimal>();
            foreach (var q in qtyList)
            {
                try
                {
                    shipQtyList.Add(Convert.ToDecimal(q));
                }
                catch (Exception exc)
                {
                    ShowErrorMessage("数量" + q + "填写错误");
                }
            }
            if (flowList.Count == 0)
            {
                ShowErrorMessage("没有要修改的计划。");
            }
            TheMrpMgr.UpdateShipPlanQty(flowList, itemList, idList, shipQtyList, releaseNoList,dateFromList, this.CurrentUser);
            ShowSuccessMessage("修改成功。");
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IList<ShipPlanMstr> mstr = TheGenericMgr.FindAllWithCustomQuery<ShipPlanMstr>(string.Format(" select m from ShipPlanMstr as m where m.Status='{0}' ",BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        if (mstr != null && mstr.Count > 0)
        {
            ShipPlanMstr m = mstr.First();
            DateTime dateNow = System.DateTime.Now;
            m.LastModifyUser = this.CurrentUser.Code;
            m.LastModifyDate = dateNow;
            m.ReleaseDate = dateNow;
            m.ReleaseUser = this.CurrentUser.Code;
            m.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            m.Version += 1;
            TheGenericMgr.Update(m);
            ShowSuccessMessage("释放成功。");
        }
        else
        {
            ShowErrorMessage("没有需要释放的发运计划。");
        }
    }
}