using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using System.Collections.Generic;
using com.Sconit.Entity.EDI;

public partial class EDI_FordPlan_DetailList : ListModuleBase
{
    public static int totalItem = 0;
    public static IList<EDIFordPlan> allList = new List<EDIFordPlan>();
    public List<DateTime> forecastDateList = new List<DateTime>();
    public static string control_num = string.Empty;

    public event EventHandler BackEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        control_num = string.Empty;
    }

    public void GetView(string searchSql)
    {
        allList = new List<EDIFordPlan>();
        IList<EDIFordPlan> eDIFordPlanList = TheGenericMgr.FindAllWithCustomQuery<EDIFordPlan>(searchSql);

        if (eDIFordPlanList != null && eDIFordPlanList.Count > 0)
        {
            IList<FlowDetail> flowdets = TheGenericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where  d.ReferenceItemCode in('{0}') ", string.Join("','", eDIFordPlanList.Select(w => w.RefItem).Distinct().ToArray())));
            
            control_num = eDIFordPlanList.First().Control_Num;
            var groups = (from tak in eDIFordPlanList
                          group tak by new
                          {
                              tak.Control_Num,
                              tak.RefItem
                          }
                              into result
                              select new
                              {
                                  RefItem = result.Key.RefItem,
                                  Control_Num = result.Key.Control_Num,
                                  List = result.ToList()
                              }).ToList();

           forecastDateList= groups.First().List.OrderBy(g => g.ForecastDate).Select(g => g.ForecastDate).ToList();

            foreach (var g in groups)
            {
                EDIFordPlan newPlan = g.List.First();
                Dictionary<DateTime, decimal[]> planDateDic = new Dictionary<DateTime, decimal[]>();
                List<decimal[]> planQtyArr = new List<decimal[]>();
                foreach (var f in g.List)
                {
                    //forecastDateList.Add(f.ForecastDate);
                    decimal[] dicArr = new decimal[] { f.ForecastQty, f.ForecastCumQty };
                    planQtyArr.Add(dicArr);
                }
                newPlan.PlanQtyArr = planQtyArr;
                var flowDet = flowdets.Where(f => f.ReferenceItemCode == newPlan.RefItem);
                if (flowDet != null || flowDet.Count()> 0)
                {
                    newPlan.Item = flowDet.First().Item.Code;
                    newPlan.ItemDesc = flowDet.First().Item.Description;
                }
                allList.Add(newPlan);
            }
            totalItem = allList.Count;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }



    public override void UpdateView()
    {
        //dic.Add("123", "321");
        //this.GV_List.Execute();
    }



    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

}
