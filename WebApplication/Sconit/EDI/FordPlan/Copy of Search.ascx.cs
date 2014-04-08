using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using Geekees.Common.Controls;
using com.Sconit.Entity.EDI;
using NHibernate.Expression;

public partial class EDI_FordPlan_Search : SearchModuleBase
{



    public event EventHandler SearchEvent;
    //private List<string> StatusList
    //{
    //    get { return this.astvMyTree.GetCheckedNodes().Select(a => a.NodeValue).ToList(); }
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
       
        if (!IsPostBack)
        {

            #region
            IList<EDIFordPlan> eDIFordPlanList = this.TheGenericMgr.FindAllWithCustomQuery<EDIFordPlan>("select e from EDIFordPlan as e where CreateDate>=? order by CreateDate desc",System.DateTime.Now.AddDays(-100));
            if (eDIFordPlanList != null && eDIFordPlanList.Count > 0)
            {
                var Control_NumMs = eDIFordPlanList.Where(d => d.Type != "D").Select(d => d.Control_Num).Distinct().ToArray();
                var Control_NumDs = eDIFordPlanList.Where(d => d.Type == "D").Select(d => d.Control_Num).Distinct().ToArray();
                for (int i = 0; i < 100; i++)
                {
                    if (i == Control_NumMs.Length) break;
                    ASTreeViewNode control_NumM = new ASTreeViewNode(Control_NumMs[i], Control_NumMs[i]);
                    if (i == 0)
                    {
                        control_NumM.CheckedState = ASTreeViewCheckboxState.Checked;
                    }
                    this.controlNumM.RootNode.AppendChild(control_NumM);
                }

                for (int i = 0; i < 100; i++)
                {
                    if (i == Control_NumDs.Length) break;
                    ASTreeViewNode control_NumD = new ASTreeViewNode(Control_NumDs[i], Control_NumDs[i]);
                    if (i == 0)
                    {
                        control_NumD.CheckedState = ASTreeViewCheckboxState.Checked;
                    }
                    this.controlNumD.RootNode.AppendChild(control_NumD);
                }
                this.controlNumM.InitialDropdownText = Control_NumMs.First();
                this.controlNumM.DropdownText = Control_NumMs.First();
                this.controlNumD.InitialDropdownText = Control_NumDs.First();
                this.controlNumD.DropdownText = Control_NumDs.First();
            }
            #endregion



            //this.tbStartDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            //this.tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        IList<string> control_Nums = this.controlNumM.GetCheckedNodes().Select(a => a.NodeValue).ToList();
       
        //string control_Num = this.Control_Num.Text.Trim();
        string itemCode = this.tbItem.Text.Trim();
        //string type = type == "D" ? type : "W,M";
        string type = this.rblListFormat.SelectedValue;
        if (type == "D")
        {
            control_Nums = this.controlNumD.GetCheckedNodes().Select(a => a.NodeValue).ToList();
        }
        //DateTime? startDate = null;
        //DateTime? endDate = null;
        //if (!string.IsNullOrEmpty(this.tbStartDate.Text))
        //{
        //    startDate = DateTime.Parse(this.tbStartDate.Text);
        //}
        //if (!string.IsNullOrEmpty(this.tbEndDate.Text))
        //{
        //    endDate = DateTime.Parse(this.tbEndDate.Text);
        //}

        if (SearchEvent != null)
        {
            #region

            string searchSql =string.Format( " select e from EDIFordPlan as e  where Type in('{0}') ",type);
            if (control_Nums.Count > 0)
            {
                searchSql += string.Format(" and Control_Num in('{0}')", string.Join("','", control_Nums.ToArray()));
            }
            //if (!string.IsNullOrEmpty(control_Num))
            //{
            //    searchSql += string.Format(" and Control_Num ='{0}' ", control_Num);
            //}
            if (itemCode != string.Empty)
            {
                searchSql += string.Format(" and e.Item = '{0}' ", itemCode);
            }

            //if (startDate.HasValue)
            //{
            //    searchSql += string.Format(" and CreateDate >='{0}'", startDate.Value);
            //}
            //if (endDate.HasValue)
            //{
            //    searchSql += string.Format(" and CreateDate <='{0}'", endDate.Value);
            //}
            SearchEvent((new object[] { searchSql + " order by Control_Num Desc,Id asc " }), null);
            #endregion
            #region DetachedCriteria

            //DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(EDIFordPlan));
            //DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(EDIFordPlan)).SetProjection(Projections.Count("Id"));
            //if (control_Nums.Count > 0)
            //{
            //    selectCriteria.Add(Expression.In("Control_Num", control_Nums.ToList()));
            //    selectCountCriteria.Add(Expression.In("Control_Num", control_Nums.ToList()));
            //}
            //if (itemCode != string.Empty)
            //{
            //    selectCriteria.Add(Expression.Eq("Item", itemCode));
            //    selectCountCriteria.Add(Expression.Eq("Item", itemCode));
            //}

            //if (startDate.HasValue)
            //{
            //    selectCriteria.Add(Expression.Ge("CreateDate", startDate.Value));
            //    selectCountCriteria.Add(Expression.Ge("CreateDate", startDate.Value));
            //}
            //if (endDate.HasValue)
            //{
            //    selectCriteria.Add(Expression.Lt("CreateDate", endDate.Value));
            //    selectCountCriteria.Add(Expression.Lt("CreateDate", endDate.Value));
            //}

            //SearchEvent((new object[] { selectCriteria, selectCountCriteria}), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

}
