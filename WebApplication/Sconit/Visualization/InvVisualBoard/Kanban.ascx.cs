using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.View;
using com.Sconit.Entity.MasterData;

public partial class Visualization_InvVisualBoard_Kanban : System.Web.UI.UserControl
{
    #region Image
    private const string MaxStockImage = "~/Images/Icon/MaxStock.gif";
    private const string SafeStockImage = "~/Images/Icon/SafeStock.gif";
    private const string InvImage = "~/Images/Icon/Inv.gif";
    private const string ToBeIn = "~/Images/Icon/ToBeIn.gif";
    private const string InTransit = "~/Images/Icon/InTransit.gif";
    private const string ToBeOut = "~/Images/Icon/ToBeOut.gif";
    private const string MinusAlert = "~/Images/Icon/MinusAlert.gif";
    private const string oddInvImage = "~/Images/Icon/odd_Inv.gif";
    private const string oddToBeIn = "~/Images/Icon/odd_ToBeIn.gif";
    private const string oddInTransit = "~/Images/Icon/odd_InTransit.gif";
    private const string oddToBeOut = "~/Images/Icon/odd_ToBeOut.gif";
    private const string oddMinusAlert = "~/Images/Icon/odd_MinusAlert.gif";
    private const string dot = "~/Images/Icon/dot.gif";
    #endregion

    #region ViewState
    private decimal SafeStock
    {
        get { return ViewState["SafeStock"] != null ? (decimal)ViewState["SafeStock"] : 0; }
        set { ViewState["SafeStock"] = value; }
    }
    private decimal MaxStock
    {
        get { return ViewState["MaxStock"] != null ? (decimal)ViewState["MaxStock"] : 0; }
        set { ViewState["MaxStock"] = value; }
    }
    private decimal UC
    {
        get { return ViewState["UC"] != null ? (decimal)ViewState["UC"] : 0; }
        set { ViewState["UC"] = value; }
    }
    private decimal InvQty
    {
        get { return ViewState["InvQty"] != null ? (decimal)ViewState["InvQty"] : 0; }
        set { ViewState["InvQty"] = value; }
    }
    private decimal QtyToBeIn
    {
        get { return ViewState["QtyToBeIn"] != null ? (decimal)ViewState["QtyToBeIn"] : 0; }
        set { ViewState["QtyToBeIn"] = value; }
    }
    private decimal InTransitQty
    {
        get { return ViewState["InTransitQty"] != null ? (decimal)ViewState["InTransitQty"] : 0; }
        set { ViewState["InTransitQty"] = value; }
    }
    private decimal QtyToBeOut
    {
        get { return ViewState["QtyToBeOut"] != null ? (decimal)ViewState["QtyToBeOut"] : 0; }
        set { ViewState["QtyToBeOut"] = value; }
    }
    private decimal LimitInv
    {
        get { return InvQty - QtyToBeOut; }
    }
    private decimal RemainQtyToBeIn
    {
        get { return QtyToBeIn > InTransitQty ? QtyToBeIn - InTransitQty : 0; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            this.GenKanbanCard();
        }
    }

    public void InitPageParameter(FlowView flowView)
    {
        this.SafeStock = flowView.FlowDetail.SafeStock.HasValue ? flowView.FlowDetail.SafeStock.Value : 0;
        this.MaxStock = flowView.FlowDetail.MaxStock.HasValue ? flowView.FlowDetail.MaxStock.Value : 0;
        this.UC = flowView.FlowDetail.UnitCount;
        this.InvQty = flowView.LocationDetail.Qty;
        this.QtyToBeIn = flowView.LocationDetail.QtyToBeIn;
        this.InTransitQty = flowView.LocationDetail.InTransitQty;
        this.QtyToBeOut = flowView.LocationDetail.QtyToBeOut;

        this.GenKanbanCard();
    }

    private void GenKanbanCard()
    {
        int maxSize = 1;

        decimal safeStock = this.SafeStock;
        decimal maxStock = this.MaxStock;

        if (UC <= 0)
            return;

        int safeStockIndex = (int)Math.Ceiling(safeStock / UC);
        int maxStockIndex = (int)Math.Ceiling(maxStock / UC);
        int minusUC = 0;

        List<Image> board = new List<Image>();
        if (LimitInv < 0)
        {
            this.FillList(board, MinusAlert, oddMinusAlert, -LimitInv);
            this.FillList(board, ToBeOut, oddToBeOut, InvQty);

            minusUC = (int)Math.Ceiling(-LimitInv / UC);
            safeStockIndex += minusUC;
            maxStockIndex += minusUC;
        }
        else
        {
            this.FillList(board, InvImage, oddInvImage, LimitInv);
            this.FillList(board, ToBeOut, oddToBeOut, QtyToBeOut);
        }

        this.FillList(board, InTransit, oddInTransit, InTransitQty);
        this.FillList(board, ToBeIn, oddToBeIn, RemainQtyToBeIn);

        safeStockIndex = safeStockIndex < board.Count ? safeStockIndex : board.Count;
        board.Insert(safeStockIndex, GetImage(SafeStockImage, safeStock.ToString("0.###")));

        if (maxStock > 0)
        {
            maxStockIndex = maxStockIndex < board.Count ? maxStockIndex + 1 : board.Count;
            board.Insert(maxStockIndex, GetImage(MaxStockImage, maxStock.ToString("0.###")));
        }

        string image = string.Empty;
        int count = 1;
        bool isAdd = false;
        for (int i = 0; i < board.Count; i++)
        {
            if (image != board[i].ImageUrl)
            {
                image = board[i].ImageUrl;
                count = 1;
                isAdd = false;
                continue;
            }

            if (count > maxSize)
            {
                if (!isAdd)
                {
                    board[i - 1] = GetImage(dot);
                    isAdd = true;
                }
                else
                {
                    board.RemoveAt(i - 1);
                    i--;
                }
            }

            count++;
        }

        this.FillBoard(board);
    }

    private void FillList(List<Image> board, string image, string oddImage, decimal limit)
    {
        for (decimal i = limit; i > 0; i -= UC)
        {
            if (i < UC)
                board.Add(GetImage(oddImage, i.ToString("0.###")));
            else
                board.Add(GetImage(image, UC.ToString("0.###")));
        }
    }

    private Image GetImage(string fileName)
    {
        return GetImage(fileName, null);
    }
    private Image GetImage(string fileName, string toolTip)
    {
        Image image = new Image();
        image.ImageUrl = fileName;
        if (toolTip != null && toolTip != string.Empty)
            image.ToolTip = toolTip;
        return image;
    }

    private void FillBoard(List<Image> board)
    {
        foreach (Image image in board)
        {
            phKanban.Controls.Add(image);
        }
    }
}
