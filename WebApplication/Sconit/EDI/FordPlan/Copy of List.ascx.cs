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

public partial class EDI_FordPlan_List : ListModuleBase
{
    public static bool isMaxPage = false;
    public static bool isMinPage = false;
    public static int CurrenPage = 1;
    public static int totalItem = 0;
    public static int currentItem01 = 0;
    public static int currentItem02 = 0;
    public static IList<EDIFordPlan> returnList = new List<EDIFordPlan>();
    public static IList<EDIFordPlan> allList = new List<EDIFordPlan>();
   //public IDictionary<string, string> dic = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void GetView(string searchSql)
    {
        CurrenPage = 1;
        totalItem = 0;
        allList = new List<EDIFordPlan>();
        IList<EDIFordPlan> eDIFordPlanList = TheGenericMgr.FindAllWithCustomQuery<EDIFordPlan>(searchSql);
        //IList<EDIFordPlan> getList = new List<EDIFordPlan>(); ;

        if (eDIFordPlanList != null && eDIFordPlanList.Count > 0)
        {
            string control_Num = string.Empty;
            var groups = (from tak in eDIFordPlanList
                          group tak by new
                          {
                              tak.Control_Num,
                              tak.Item
                          }
                              into result
                              select new
                              {
                                  Item = result.Key.Item,
                                  Control_Num = result.Key.Control_Num,
                                  List = result.ToList()
                              }).ToList();

            foreach (var g in groups)
            {
                EDIFordPlan newPlan = g.List.First();
                Dictionary<DateTime, decimal[]> planDateDic = new Dictionary<DateTime, decimal[]>();
                foreach (var f in g.List)
                {
                    //Dictionary<decimal, decimal> dic = new Dictionary<decimal, decimal>();
                    decimal[] dic = new decimal[] { f.ForecastQty, f.ForecastCumQty };
                    planDateDic.Add(f.ForecastDate, dic);
                }
                newPlan.PlanDateDic = planDateDic;
                allList.Add(newPlan);
            }
            totalItem = allList.Count;
            currentItem01 = (CurrenPage - 1) * 10 + 1;
            currentItem02 = CurrenPage * 10 > totalItem ? totalItem : CurrenPage * 10;
            returnList = allList.Skip((CurrenPage - 1) * 10).Take(10).ToList();
            isMaxPage = currentItem02 == totalItem;
            isMinPage = currentItem01 == 1;
        }
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        this.GetView2("first");
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        this.GetView2("Prev");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.GetView2("next");
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        this.GetView2("last");
    }





    public void GetView2(string clickType)
    {
        if (clickType == "first")
        {
            CurrenPage = 1;
        }
        else if (clickType == "Prev")
        {
            if (CurrenPage == 1)
            {
            }
            else
            {
                CurrenPage -= 1;
            }
        }
        else if (clickType == "next")
        {
            int maxPage = allList.Count % 10 == 0 ? allList.Count / 10 : allList.Count / 10 + 1;
            if (CurrenPage == maxPage)
            {

            }
            else
            {
                CurrenPage += 1;
            }
        }
        else if (clickType == "last")
        {
            CurrenPage = allList.Count % 10 == 0 ? allList.Count / 10 : allList.Count / 10+1;
        }
        totalItem = allList.Count;
        currentItem01 = (CurrenPage - 1) * 10 + 1;
        currentItem02 = CurrenPage * 10 > totalItem ? totalItem : CurrenPage * 10;
        returnList = allList.Skip((CurrenPage - 1) * 10).Take(10).ToList();

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
