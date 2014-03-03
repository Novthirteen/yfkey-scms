using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Expression;
using System.Reflection;
using System.Collections;
using System.Web.Compilation;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using System.IO;
using System.Web.UI.HtmlControls;
using com.Sconit.Entity.MasterData;


namespace com.Sconit.Control
{
    [ToolboxData("<{0}:GridView runat=server></{0}:GridView>")]
    public class GridView : System.Web.UI.WebControls.GridView
    {
        #region Property

        #region 序号
        [Category("序号"),
        Browsable(true),
        Description("显示序号的位置。"),
        DefaultValue("0")
        ]
        public int SeqNo
        {
            get
            {
                return (ViewState["SeqNo"] != null) ? (int)ViewState["SeqNo"] : 0;
            }
            set
            {
                ViewState["SeqNo"] = value;
            }
        }


        [Category("序号"),
        Browsable(true),
        Description("显示序号的文本。")
        ]
        public string SeqText
        {
            get
            {
                return "No.";//(ViewState["SeqText"] != null) ? (string)ViewState["SeqText"] : "序号";
            }
            //set
            //{
            //    ViewState["SeqText"] = value;
            //}
        }



        [Category("序号"),
        Browsable(true),
        Description("是否显示序号。"),
        DefaultValue(false)
        ]
        public bool ShowSeqNo
        {
            get
            {
                return (ViewState["ShowSeqNo"] != null) ? (bool)ViewState["ShowSeqNo"] : false;
            }
            set
            {
                ViewState["ShowSeqNo"] = value;
            }
        }

        private string snWidth = "3%";
        [Category("序号"),
        Browsable(true),
        Description("序号列的宽度,用百分比."),
        DefaultValue("3%")
        ]
        public string SNWidth
        {
            get
            {
                return this.snWidth;
            }
            set
            {
                this.snWidth = value;
            }
        }
        #endregion

        #region 排序
        [Description("是否启用多列排序功能")]
        [Category("排序")]
        [DefaultValue("false")]
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["EnableMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                ViewState["EnableMultiColumnSorting"] = value;
            }

        }
        [Description("升序时显示的图标")]
        [Category("排序")]
        [DefaultValue("~/Images/Icon/asc.gif")]
        public String SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? o.ToString() : "~/Images/Icon/asc.gif");
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }

        }
        [Description("降序时显示的图标")]
        [Category("排序")]
        [DefaultValue("~/Images/Icon/desc.gif")]
        public String SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? o.ToString() : "~/Images/Icon/desc.gif");
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }

        }
        [Description("当前排序顺序")]
        [Browsable(false)]
        [Category("排序")]
        [DefaultValue(SortDirection.Ascending)]
        public SortDirection CurrentSortDirection
        {
            get
            {
                if (ViewState["CurrentSortDirection"] == null)
                {
                    ViewState["CurrentSortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["CurrentSortDirection"];
            }
            set
            {
                ViewState["CurrentSortDirection"] = value;
            }
        }
        [Description("当前排序表达式")]
        [Browsable(false)]
        [Category("排序")]
        [DefaultValue("Descending")]
        public new String SortExpression
        {
            get
            {
                return ViewState["SortExpression"] == null ? "Descending" : ViewState["SortExpression"].ToString();
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        [Description("默认排序字段")]
        [Browsable(false)]
        [Category("排序")]
        [DefaultValue("")]
        public new String DefaultSortExpression
        {
            get
            {
                return ViewState["DefaultSortExpression"] == null ? "" : ViewState["DefaultSortExpression"].ToString();
            }
            set
            {
                ViewState["DefaultSortExpression"] = value;
            }
        }

        [Description("默认排序方向")]
        [Browsable(false)]
        [Category("排序")]
        [DefaultValue("Descending")]
        public new String DefaultSortDirection
        {
            get
            {
                return ViewState["DefaultSortDirection"] == null ? "" : ViewState["DefaultSortDirection"].ToString();
            }
            set
            {
                ViewState["DefaultSortDirection"] = value;
            }
        }
        #endregion

        #region CSS
        [Description("是否自动加载CSS")]
        [Category("CSS")]
        [DefaultValue("true")]
        public bool AutoLoadStyle
        {
            get
            {
                object o = ViewState["EnabledAutoLoadStyle"];
                return (o != null ? (bool)o : true);
            }
            set
            {
                ViewState["EnabledAutoLoadStyle"] = value;
            }

        }
        #endregion

        #region 分页
        [Category("分页"),
        Browsable(true),
        Description("分页控件ID，此属性必须设置。")]
        public string PagerID
        {
            get
            {
                if (ViewState["GridPagerID"] != null)
                {
                    return (string)ViewState["GridPagerID"];
                }
                else
                {
                    return "gp";
                    // throw new TechnicalException("GridPagerID not specified");
                }
            }
            set
            {
                ViewState["GridPagerID"] = value;
            }
        }

        private bool pagerVisible = false;
        [Category("分页"),
        Browsable(true),
        Description("PagerVisible."),
        DefaultValue(false)
        ]
        public bool PagerVisible
        {
            get
            {
                return this.pagerVisible;
            }
            set
            {
                this.pagerVisible = value;
            }
        }

        [Category("CSS"),
        Browsable(true),
        Description("是否控制GridViewCell的最大显示字符数"),
        DefaultValue(false),
        ]
        public bool FixCellMaxLength
        {
            get
            {
                return (ViewState["FixCellMaxLength"] != null) ? Convert.ToBoolean(ViewState["FixCellMaxLength"]) : false;
            }
            set
            {
                ViewState["FixCellMaxLength"] = value;
            }
        }
        [Category("CSS"),
        Browsable(true),
        Description("GridViewCell的最大显示字符数"),
        ]
        public int CellMaxLength
        {
            get
            {
                if (ViewState["CellMaxLength"] != null)
                {
                    return Convert.ToInt32(ViewState["CellMaxLength"]);
                }
                else
                {
                    return 10;
                }
            }
            set
            {
                ViewState["CellMaxLength"] = value;
            }
        }
        #endregion

        #endregion

        #region override && relier

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.SelectCriteriaExpire = false;
            this.PageSize = GetPageSize(FindPager());
            //if (!this.AutoLoadStyle)
            //{
            this.CssClass = "GV";
            this.HeaderStyle.CssClass = "GVHeader";
            this.RowStyle.CssClass = "GVRow";
            this.AlternatingRowStyle.CssClass = "GVAlternatingRow";
            this.SelectedRowStyle.CssClass = "GVSelectedRow";
            this.PagerStyle.CssClass = "GVPager";
            //this.Attributes.Add("BorderColor", "#ffffff");
            //this.Attributes.Add("Border", "0px");
            this.CellPadding = 4;
            this.CellSpacing = 0;
            //}
            if (this.Columns.Count > 0)
            {
                if (this.AllowSorting)
                {
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (this.Columns[i] is BoundField)
                        {
                            this.Columns[i].SortExpression = ((BoundField)this.Columns[i]).DataField;
                        }
                        else if (this.Columns[i] is HyperLinkField)
                        {
                            this.Columns[i].SortExpression = ((HyperLinkField)this.Columns[i]).DataTextField;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (this.Columns[i] is BoundField)
                        {
                            this.Columns[i].SortExpression = null;
                        }
                        else if (this.Columns[i] is HyperLinkField)
                        {
                            this.Columns[i].SortExpression = null;
                        }
                    }
                }
                bool bHaved = (this.Columns[this.SeqNo].HeaderText == this.SeqText);
                if (ShowSeqNo)//显示序号
                {
                    if (!bHaved)
                    {
                        BoundField bf = new BoundField();
                        bf.HeaderText = this.SeqText;
                        bf.HeaderStyle.Width = new Unit(this.snWidth);
                        bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bf.HeaderStyle.Wrap = false;
                        bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        bf.ItemStyle.Wrap = false;
                        bf.ReadOnly = true;

                        this.Columns.Insert(this.SeqNo, bf);
                    }
                    this.RowDataBound += new GridViewRowEventHandler(CustDataGrid_RowDataBound);
                }
                else//不显示序号
                {
                    if (bHaved)
                    {
                        this.Columns.RemoveAt(this.SeqNo);
                    }
                }
            }
            this.CreateRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
        }

        private IList<IDictionary<string, string>> SortingFields
        {
            get
            {
                if (ViewState["SortingFields"] != null)
                {
                    return ViewState["SortingFields"] as IList<IDictionary<string, string>>;
                }
                else
                {
                    return new List<IDictionary<string, string>>();
                }
            }
            set
            {
                ViewState["SortingFields"] = value;
            }
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            ExecuteCriteria(AddSortingField(e.SortExpression, e.SortDirection), this.SelectCountCriteria);
        }

        private DetachedCriteria AddSortingField(string currentSortingExpression,
            SortDirection currentSortDirection)
        {
            if (!AllowMultiColumnSorting)
            {
                //todo 清空排序字段
                SortingFields = new List<IDictionary<string, string>>();
            }

            SortExpression = currentSortingExpression;
            if (CurrentSortDirection == SortDirection.Ascending)
            {
                currentSortDirection = SortDirection.Ascending;
                CurrentSortDirection = SortDirection.Descending;
            }
            else
            {
                currentSortDirection = SortDirection.Descending;
                CurrentSortDirection = SortDirection.Ascending;
            }

            IList<IDictionary<string, string>> newSortingFields = new List<IDictionary<string, string>>();
            for (int i = SortingFields.Count - 3; i < SortingFields.Count; i++)
            {
                if (i < 0) continue;
                IDictionary<string, string> field = SortingFields[i];

                //用户指定排序字段和之前旧的排序字段重复的话，旧的排序字段不添加到排序列中
                if (!field.ContainsKey(SortExpression))
                {
                    newSortingFields.Add(field);
                }
            }
            IDictionary<string, string> newField = new Dictionary<string, string>();
            newField.Add(SortExpression, CurrentSortDirection.ToString());
            newSortingFields.Add(newField);
            SortingFields = newSortingFields;
            if (newSortingFields.Count > 3)
            {
                newSortingFields.RemoveAt(0);
            }

            return this.AddSortingField();
        }

        private DetachedCriteria AddSortingField()
        {
            if (this.SortExpression == "Descending" || this.SortingFields.Count == 0)
            {
                return this.SelectCriteria;
            }
            else 
            {
                //由于不提供Remove Order方法，只能一直Add Order，如果出现对同一字段多次会出错。
                //现在每次排序前克隆一个新的selectCriteria，手工过滤同一字段多次排序问题
                DetachedCriteria selectCriteria = CloneHelper.DeepClone<DetachedCriteria>(this.SelectCriteria);

                int fieldCount = 0;
                //存放相同别名
                Dictionary<string, string> aliasDict = CloneHelper.DeepClone<Dictionary<string, string>>(this.Alias as Dictionary<string, string>);
                if (aliasDict == null)
                {
                    aliasDict = new Dictionary<string, string>();
                }

                for (int i = (SortingFields.Count - 1); i >= 0; i--)
                {
                    IDictionary<string, string> field = SortingFields[i];
                    IEnumerator<KeyValuePair<string, string>> fieldEnum = field.GetEnumerator();
                    fieldEnum.MoveNext();
                    string sortExpression = fieldEnum.Current.Key;
                    string sortDirection = fieldEnum.Current.Value;

                    if (sortExpression.LastIndexOf(".") != -1)
                    {
                        string alias = sortExpression.Substring(0, sortExpression.LastIndexOf("."));
                        string orderField = sortExpression.Substring(sortExpression.LastIndexOf("."));
                        if (CurrentSortDirection == SortDirection.Ascending)
                        {
                            if (aliasDict.ContainsKey(alias))
                            {
                                selectCriteria.AddOrder(Order.Asc(aliasDict[alias] + orderField));
                            }
                            else
                            {
                                selectCriteria.CreateAlias(alias, "A" + fieldCount).AddOrder(Order.Asc("A" + fieldCount + orderField));
                                aliasDict[alias] = "A" + fieldCount;
                            }
                        }
                        else
                        {
                            if (aliasDict.ContainsKey(alias))
                            {
                                selectCriteria.AddOrder(Order.Desc(aliasDict[alias] + orderField));
                            }
                            else
                            {
                                selectCriteria.CreateAlias(alias, "A" + fieldCount).AddOrder(Order.Desc("A" + fieldCount + orderField));
                                aliasDict[alias] = "A" + fieldCount;
                            }
                        }
                        fieldCount++;
                    }
                    else
                    {
                        if (CurrentSortDirection == SortDirection.Ascending)
                        {
                            selectCriteria.AddOrder(Order.Asc(sortExpression));
                        }
                        else
                        {
                            selectCriteria.AddOrder(Order.Desc(sortExpression));
                        }
                    }
                }
                return selectCriteria;
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDireciton)
        {
            string m_SortDirection = String.Empty;

            switch (sortDireciton)
            {
                case SortDirection.Ascending:
                    m_SortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    m_SortDirection = "DESC";
                    break;
            }

            return m_SortDirection;
        }



        //// <summary>
        ///  获取排序表达式
        /// </summary>
        protected string GetSortExpression(GridViewSortEventArgs e)
        {
            string[] sortColumns = null;
            string sortAttribute = SortExpression;

            if (sortAttribute != String.Empty)
            {
                sortColumns = sortAttribute.Split(",".ToCharArray());
            }
            if (sortAttribute.IndexOf(e.SortExpression) > 0 || sortAttribute.StartsWith(e.SortExpression))
            {
                sortAttribute = ModifySortExpression(sortColumns, e.SortExpression);
            }
            else
            {
                sortAttribute += String.Concat(",", e.SortExpression, " ASC ");
            }
            return sortAttribute.TrimStart(",".ToCharArray()).TrimEnd(",".ToCharArray());

        }

        //// <summary>
        ///  修改排序顺序
        /// </summary>
        protected string ModifySortExpression(string[] sortColumns, string sortExpression)
        {
            string ascSortExpression = String.Concat(sortExpression, " ASC ");
            string descSortExpression = String.Concat(sortExpression, " DESC ");

            for (int i = 0; i < sortColumns.Length; i++)
            {

                if (ascSortExpression.Equals(sortColumns[i]))
                {
                    sortColumns[i] = descSortExpression;
                }

                else if (descSortExpression.Equals(sortColumns[i]))
                {
                    Array.Clear(sortColumns, i, 1);
                }
            }

            return String.Join(",", sortColumns).Replace(",,", ",").TrimStart(",".ToCharArray());

        }

        //// <summary>
        ///  获取当前的表达式对所选列进行排序
        /// </summary>
        protected void SearchSortExpression(string[] sortColumns, string sortColumn, out string sortOrder, out int sortOrderNo)
        {
            sortOrder = "";
            sortOrderNo = -1;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                if (sortColumns[i].StartsWith(sortColumn))
                {
                    sortOrderNo = i + 1;
                    if (AllowMultiColumnSorting)
                    {
                        sortOrder = sortColumns[i].Substring(sortColumn.Length).Trim();
                    }
                    else
                    {
                        sortOrder = ((CurrentSortDirection == SortDirection.Ascending) ? "ASC" : "DESC");
                    }
                }
            }
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("onMouseOver", "e=this.style.backgroundColor; this.style.backgroundColor=this.style.borderColor");
            //    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=e");
            //}
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (SortExpression != String.Empty)
                {
                    DisplaySortOrderImages(SortExpression, e.Row);
                    //this.CreateRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                }
            }
            base.OnRowCreated(e);
        }
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (FixCellMaxLength)
            {
                if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text.Trim().Length > CellMaxLength)
                        {
                            e.Row.Cells[i].Attributes.Add("Title", e.Row.Cells[i].Text.Trim());
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Trim().Substring(0, CellMaxLength) + "...";
                        }
                    }
                }
            }
            base.OnRowDataBound(e);
        }
        //// <summary>
        ///  绘制升序降序的图片
        /// </summary>
        protected void DisplaySortOrderImages(string sortExpression, GridViewRow dgItem)
        {

            for (int i = 0; i < dgItem.Cells.Count; i++)
            {
                if (dgItem.Cells[i].Controls.Count > 0 && dgItem.Cells[i].Controls[0] is LinkButton)
                {

                    string column = ((LinkButton)dgItem.Cells[i].Controls[0]).CommandArgument;

                    if (this.SortingFields != null)
                    {

                        for (int j = 0; j < this.SortingFields.Count; j++)
                        {
                            IDictionary<string, string> sortingField = this.SortingFields[j];
                            if (sortingField.ContainsKey(column))
                            {
                                string sortImgLoc =
                                    (sortingField[column] == SortDirection.Ascending.ToString()) ? SortAscImageUrl : SortDescImageUrl;

                                if (sortImgLoc != String.Empty)
                                {
                                    Image imgSortDirection = new Image();
                                    imgSortDirection.ImageUrl = sortImgLoc;
                                    dgItem.Cells[i].Controls.Add(imgSortDirection);
                                }

                                if (AllowMultiColumnSorting)
                                {
                                    Literal litSortSeq = new Literal();
                                    litSortSeq.Text = (this.SortingFields.Count - j).ToString();
                                    dgItem.Cells[i].Controls.Add(litSortSeq);

                                }
                            }
                        }
                    }
                }
            }
        }
        private void CustDataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex > -1)
            {
                int pageSize = GetPageSize(FindPager());
                int pageIndex = GetCurrentPageIndex(FindPager());
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int i = pageSize * (pageIndex - 1) + e.Row.RowIndex + 1;
                    e.Row.Cells[this.SeqNo].Text = i.ToString();
                }
            }
        }
        #endregion


        #region 查询封装

        #region 查询方式
        [Category("查询方式"),
        Browsable(true),
        Description("查询方式，Criteria或者Customize，默认为Criteria"),
        ]
        public String SearchMode
        {
            get
            {
                if (ViewState["SearchMode"] != null)
                {
                    return (string)ViewState["SearchMode"];
                }
                else
                {
                    return BusinessConstants.SEARCH_MODE_CRITERIA;
                }
            }
            set
            {
                ViewState["SearchMode"] = value;
            }
        }
        #endregion

        #region 查询条件
        [Category("查询条件"),
        Browsable(true),
        Description("查询条件封装对象。"),
        ]
        public DetachedCriteria SelectCriteria
        {
            get
            {
                SessionHelper sessionHelper = new SessionHelper(this.Page);
                return sessionHelper.GetUserSelectCriteria(this.NamingContainer.TemplateControl.AppRelativeVirtualPath);
            }
        }
        #endregion

        #region 查询Count的条件
        [Category("查询Count的条件"),
        Browsable(true),
        Description("查询Count的条件封装对象。"),
        ]
        public DetachedCriteria SelectCountCriteria
        {
            get
            {
                SessionHelper sessionHelper = new SessionHelper(this.Page);
                return sessionHelper.GetUserSelectCountCriteria(this.NamingContainer.TemplateControl.AppRelativeVirtualPath);
            }
        }
        #endregion

        #region 别名
        [Category("别名"),
        Browsable(true),
        Description("别名封装对象。"),
        ]
        public IDictionary<string, string> Alias
        {
            get
            {
                SessionHelper sessionHelper = new SessionHelper(this.Page);
                return sessionHelper.GetUserAlias(this.NamingContainer.TemplateControl.AppRelativeVirtualPath);
            }
        }
        #endregion

        #region 查询类的名称
        [Category("查询类的名称"),
        Browsable(true),
        Description("获取或设置查询类的名称。")
        ]
        public string TypeName
        {
            get
            {
                if (ViewState["TypeName"] != null)
                {
                    return (string)ViewState["TypeName"];
                }
                else
                {
                    return "com.Sconit.Web.CriteriaMgrProxy";
                    //throw new TechnicalException("TypeName not specified");
                }
            }
            set
            {
                ViewState["TypeName"] = value;
            }
        }
        #endregion

        #region 查询方法名称
        [Category("查询方法名称"),
        Browsable(true),
        Description("获取或设置查询方法名称。")
        ]
        public string SelectMethod
        {
            get
            {
                if (ViewState["SelectMethod"] != null)
                {
                    return (string)ViewState["SelectMethod"];
                }
                else
                {
                    return "FindAll";
                    //throw new TechnicalException("SelectMethod not specified");
                }
            }
            set
            {
                ViewState["SelectMethod"] = value;
            }
        }
        #endregion

        #region 查询记录数方法名称
        [Category("查询记录数方法名称"),
        Browsable(true),
        Description("获取或设置查询记录数方法名称。")
        ]
        public string SelectCountMethod
        {
            get
            {
                if (ViewState["SelectCountMethod"] != null)
                {
                    return (string)ViewState["SelectCountMethod"];
                }
                else
                {
                    return "FindCount";
                    //throw new TechnicalException("SelectCountMethod not specified");
                }
            }
            set
            {
                ViewState["SelectCountMethod"] = value;
            }
        }

        public bool SelectCriteriaExpire = false;
        #endregion

        #region 执行查询
        public void Execute()
        {
            if (this.SearchMode == BusinessConstants.SEARCH_MODE_CRITERIA)
            {
                if (this.DefaultSortDirection != string.Empty && this.SortingFields.Count == 0)
                {
                    if (this.SelectCriteria == null || this.SelectCountCriteria == null)
                    {
                        this.SelectCriteriaExpire = true;
                        return;
                    }

                    //按默认排序
                    SortDirection sortDirection = SortDirection.Descending;
                    if (this.DefaultSortDirection == SortDirection.Ascending.ToString())
                    {
                        sortDirection = SortDirection.Ascending;
                        this.CurrentSortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Descending;
                        this.CurrentSortDirection = SortDirection.Ascending;
                    }
                    ExecuteCriteria(AddSortingField(DefaultSortExpression, sortDirection), this.SelectCountCriteria);
                }
                else
                {
                    //ExecuteCriteria(this.SelectCriteria, this.SelectCountCriteria);
                    //当前如果有选择排序字段,就加到SelectCriteria中.
                    ExecuteCriteria(AddSortingField(), this.SelectCountCriteria);
                }
            }
            else if (this.SearchMode == BusinessConstants.SEARCH_MODE_CUSTOMIZE)
            {
                ExecuteCustomize();
            }
            else
            {
                throw new TechnicalException("SearchMode: " + this.SearchMode + " is not valided");
            }
        }

        private void ExecuteCriteria(DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria)
        {
            if (selectCriteria == null || selectCountCriteria == null)
            {
                this.SelectCriteriaExpire = true;
                return;
            }
            IList list = null;
            int recordCount = 0;
            MethodInfo selectMethodInfo = BuildManager.GetType(TypeName, true).GetMethod(SelectMethod);
            MethodInfo selectCountMethodInfo = BuildManager.GetType(TypeName, true).GetMethod(SelectCountMethod);
            ConstructorInfo constructorInfo = BuildManager.GetType(TypeName, true).GetConstructor(System.Type.EmptyTypes);
            GridPager pager = FindPager();

            #region 反射获得列表总记录数
            if (selectCountMethodInfo != null)
            {
                if (selectCountMethodInfo.IsStatic)
                {
                    recordCount = int.Parse(selectCountMethodInfo.Invoke(null, new object[] { selectCountCriteria }).ToString());
                }
                else
                {
                    if (constructorInfo != null)
                    {
                        if (constructorInfo.IsStatic)
                        {
                            recordCount = int.Parse(selectCountMethodInfo.Invoke(null, new object[] { selectCountCriteria }).ToString());
                        }
                        else
                        {
                            object obj = constructorInfo.Invoke(null);
                            recordCount = int.Parse(selectCountMethodInfo.Invoke(obj, new object[] { selectCountCriteria }).ToString());
                        }
                    }
                    else
                    {
                        throw new TechnicalException(TypeName + " specified has not zero parameter constructor");
                    }
                }
            }
            else
            {
                throw new TechnicalException("no specified method:" + selectCountMethodInfo);
            }

            pager.RecordCount = recordCount;
            #endregion

            #region 反射获得查询列表
            if (selectMethodInfo != null)
            {
                int pageSize = GetPageSize(FindPager());
                int pageIndex = GetCurrentPageIndex(FindPager());
                int firstRow = (pageIndex - 1) * pageSize;
                int maxRows = pageSize;
                this.PageSize = pageSize;
                if (selectMethodInfo.IsStatic)
                {
                    list = selectMethodInfo.Invoke(null, new object[] { selectCriteria, firstRow, maxRows }) as IList;
                }
                else
                {
                    if (constructorInfo != null)
                    {
                        if (constructorInfo.IsStatic)
                        {
                            list = selectMethodInfo.Invoke(null, new object[] { selectCriteria, firstRow, maxRows }) as IList;
                        }
                        else
                        {
                            object obj = constructorInfo.Invoke(null);
                            list = selectMethodInfo.Invoke(obj, new object[] { selectCriteria, firstRow, maxRows }) as IList;
                        }
                    }
                    else
                    {
                        throw new TechnicalException(TypeName + " specified has not zero parameter constructor");
                    }
                }
            }
            else
            {
                throw new TechnicalException("no specified method:" + SelectMethod);
            }

            #region 后处理
            Type[] types = new Type[] { typeof(IList) };
            object[] values = new object[] { list };
            MethodInfo postProcess = this.Parent.GetType().GetMethod("PostProcess", types);
            if (postProcess != null)
            {
                postProcess.Invoke(this.Parent, values);
            }
            #endregion

            this.DataSource = list;
            this.DataBind();
            #endregion
        }

        private void ExecuteCustomize()
        {
            #region 反射获得列表总记录数
            MethodInfo getDataCountMethod = this.Parent.GetType().GetMethod("GetDataCount");
            int count = (int)getDataCountMethod.Invoke(this.Parent, null);
            FindPager().RecordCount = count;
            #endregion

            #region 反射获得查询列表
            Type[] types = new Type[] { typeof(int), typeof(int) };
            object[] values = new object[] { GetPageSize(FindPager()), GetCurrentPageIndex(FindPager()) };
            MethodInfo getGetDataSource = this.Parent.GetType().GetMethod("GetDataSource", types);
            IList result = (IList)getGetDataSource.Invoke(this.Parent, values);
            this.DataSource = result;
            this.DataBind();
            #endregion
        }
        #endregion

        #endregion

        #region 导出封装
        public void ExportDOC()
        {
            this.ExportDOC("");
        }

        public void ExportXLS()
        {
            this.ExportXLS("");
        }
        public void ExportXLS(String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "export.xls";
            this.Export("application/ms-excel", FileName);
        }

        public void ExportDOC(String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "export.doc";
            this.Export("application/ms-word", FileName);
        }

        /// <summary>  
        /// 导出数据函数  
        /// </summary>  
        /// <param name="FileType">导出文件MIME类型</param>  
        /// <param name="FileName">导出文件的名称</param>  
        /// this.Export(GV_List, "application/ms-excel", "ShiftProd.xls");
        /// this.Export(GV_List, "application/ms-word", "ShiftProd.doc");  
        protected void Export(String FileType, String FileName)
        {

            int maxResult = 5000;

            this.AllowPaging = false;
            this.AllowSorting = false;
            if (this.FindPager().RecordCount > this.FindPager().PageSize)
            {
                if (this.FindPager().RecordCount > maxResult)
                {
                    this.FindPager().PageSize = maxResult;
                }
                else
                {
                    this.FindPager().PageSize = this.FindPager().RecordCount;
                }
            }

            this.Execute();

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(cultureInfo);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            System.Web.UI.Control parent = this.Parent;

            MethodInfo getResponseMethodInfo = this.Parent.GetType().GetMethod("GetResponse");
            HttpResponse Response = (HttpResponse)getResponseMethodInfo.Invoke(this.Parent, null);
            MethodInfo renderMethodInfo = this.Parent.GetType().GetMethod("Render");


            Page page = new Page();
            HtmlForm form = new HtmlForm();

            this.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);

            form.Controls.Add(this);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;

            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.AppendHeader("Content-Disposition", "attachment;filename="
                    + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));

            //设置输出流HttpMiME类型(导出文件格式)  
            Response.ContentType = FileType;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            //Response.Charset = "UTF-8";
            //设定输出字符集  
            Response.Charset = "GB2312";
            //Response.ContentEncoding = Encoding.Default;
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string content = sw.ToString();
            content = renderMethodInfo.Invoke(parent, new object[] { content }) as string;

            Response.Write(content);

            
            Response.Flush();
            Response.End();

            this.AllowPaging = true;
            this.AllowSorting = true;
            //this.PageSize = 10;
            //this.Execute();
        }
        #endregion

        public GridPager FindPager()
        {
            GridPager pager = (GridPager)this.Parent.FindControl(this.PagerID);
            if (pager != null)
            {
                return pager;
            }
            else
            {
                throw new TechnicalException("PagerId:" + PagerID + " specified is not valided");
            }
        }

        private int GetPageSize(GridPager pager)
        {
            return pager.PageSize;
        }

        private int GetCurrentPageIndex(GridPager pager)
        {
            return pager.CurrentPageIndex;
        }

        //#region PageShowingEventHandler Delegate
        //public delegate void PageShowingEventHandler(object src, PageShowingEventArgs e);
        //#endregion

        //#region Event
        //public event PageShowingEventHandler PageShowing;

        //public sealed class PageShowingEventArgs : EventArgs
        //{
        //    private readonly  _newpageindex;

        //    public PageShowingEventArgs(int newPageIndex)
        //    {
        //        this._newpageindex = newPageIndex;
        //    }
        //}
        //#endregion  
    }
}
