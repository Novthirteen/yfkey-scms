using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;

namespace com.Sconit.Utility
{
    public static class GridViewHelper
    {
        //GridView合并单元格
        //根据相邻二行判断,如果text相同,则合并单元格
        //colIndex为合并列索引
        public static void GV_MergeTableCell(GridView GV, int[] colIndex)
        {
            GV_MergeTableCell(GV, colIndex, false);
        }
        public static void GV_MergeTableCell(GridView GV, int[] colIndex, bool switchStyle)
        {
            if (GV == null || GV.Rows.Count == 0)
                return;

            foreach (int k in colIndex)
            {
                bool GVAlternatingRow = false;
                TableCell oldTc = GV.Rows[0].Cells[k];
                for (int i = 1; i < GV.Rows.Count; i++)
                {
                    TableCell tc = GV.Rows[i].Cells[k];
                    if (oldTc.Text != "&nbsp;" && tc.Text != "&nbsp;"
                        && oldTc.Text == tc.Text)
                    {
                        tc.Visible = false;
                        if (oldTc.RowSpan == 0)
                        {
                            oldTc.RowSpan = 1;
                        }
                        oldTc.RowSpan++;
                    }
                    else
                    {
                        oldTc = tc;

                        #region 切换Style
                        GVAlternatingRow = !GVAlternatingRow;
                        if (switchStyle)
                        {
                            if (GVAlternatingRow)
                                GV.Rows[i].Cells[k].Attributes.Add("class", "GVAlternatingRow");
                            else
                                GV.Rows[i].Cells[k].Attributes.Add("class", "GVRow");
                        }
                        #endregion
                    }
                }
            }
        }

        //GridView合并单元格
        //根据相邻二行判断,如果text相同,则合并单元格
        //dicIndex的key为待合并列索引,value为同级合并列索引
        public static void GV_MergeTableCell(GridView GV, IDictionary<int, int[]> dicIndex)
        {
            if (GV == null || GV.Rows.Count == 0)
                return;

            foreach (KeyValuePair<int, int[]> index in dicIndex)
            {
                int oldTcRow = 0;
                TableCell oldTc = GV.Rows[oldTcRow].Cells[index.Key];
                for (int i = 1; i < GV.Rows.Count; i++)
                {
                    TableCell tc = GV.Rows[i].Cells[index.Key];
                    if (oldTc.Text == tc.Text)
                    {
                        foreach (int k in index.Value)
                        {
                            TableCell oldTc1 = GV.Rows[oldTcRow].Cells[k];
                            TableCell tc1 = GV.Rows[i].Cells[k];
                            tc1.Visible = false;
                            if (oldTc1.RowSpan == 0)
                            {
                                oldTc1.RowSpan = 1;
                            }
                            oldTc1.RowSpan++;
                        }
                    }
                    else
                    {
                        oldTc = tc;
                        oldTcRow = i;
                    }
                }
            }
        }

        //GridView合并单元格
        //根据相邻二行判断,如果text相同,则合并单元格
        //colIndex为合并列索引
        public static void GV_MergeTableCell<T>(GridView GV, int[] colIndex, IList<T> Comparer)
        {
            if (GV == null || GV.Rows.Count == 0)
                return;

            foreach (int k in colIndex)
            {
                TableCell oldTc = GV.Rows[0].Cells[k];
                T oldValue = Comparer[0];

                for (int i = 1; i < GV.Rows.Count; i++)
                {
                    TableCell tc = GV.Rows[i].Cells[k];
                    T newValue = Comparer[i];
                    if (oldValue.Equals(newValue))
                    {
                        tc.Visible = false;
                        if (oldTc.RowSpan == 0)
                        {
                            oldTc.RowSpan = 1;
                        }
                        oldTc.RowSpan++;
                    }
                    else
                    {
                        oldTc = tc;
                        oldValue = newValue;
                    }
                }
            }
        }

        /// <summary>
        /// 返回GridView分页起始行
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static int GetStartRow(int pageSize, int pageIndex)
        {
            return pageSize * (pageIndex - 1);
        }

        /// <summary>
        /// 返回GridView分页结束行
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public static int GetEndRow(int pageSize, int pageIndex, int maxCount)
        {
            return maxCount < pageSize * pageIndex ? maxCount : (pageSize * pageIndex - 1);
        }

        /// <summary>
        /// 隐藏数量为0的列
        /// </summary>
        /// <param name="alwaysVisibleColumns">保留列,数量为0也不隐藏</param>
        public static void HiddenColumns(GridView GV, int[] alwaysVisibleColumns)
        {
            Dictionary<int, bool> dics = new Dictionary<int, bool>();
            foreach (GridViewRow row in GV.Rows)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (!dics.ContainsKey(j))
                    {
                        dics.Add(j, false);
                    }
                    TableCell tc = row.Cells[j];
                    if (!(tc.Text.Trim() == "0" || (!tc.HasControls() && tc.Text.Trim() == string.Empty))
                        || alwaysVisibleColumns.Contains(j))
                    {
                        dics[j] = true;
                    }
                }
            }
            foreach (var dic in dics)
            {
                GV.Columns[dic.Key].Visible = dic.Value;
            }
            GV.DataBind();
        }
    }
}
