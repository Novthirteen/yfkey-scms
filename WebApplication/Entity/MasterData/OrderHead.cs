using System;
using System.Collections.Generic;
using com.Sconit.Entity.Exception;
using System.Collections;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class OrderHead : OrderHeadBase
    {
        #region Non O/R Mapping Properties

        /// <remarks/>
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (this.OrderDetails == null)
            {
                this.OrderDetails = new List<OrderDetail>();
                this.OrderDetails.Add(orderDetail);
            }
            else
            {
                //查找Sequence，插入到合适位置
                int position = this.OrderDetails.Count;
                for (int i = 0; i < this.OrderDetails.Count; i++)
                {
                    OrderDetail od = this.OrderDetails[i];
                    if (od.Sequence > orderDetail.Sequence)
                    {
                        position = i;
                        break;
                    }

                    //检查Sequence是否重复
                    if (od.Sequence == orderDetail.Sequence)
                    {
                        throw new BusinessErrorException("OrderDetail.Error.SequenceOverlap", orderDetail.Sequence.ToString());
                    }
                }

                this.OrderDetails.Insert(position, orderDetail);
            }
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            if (this.OrderDetails != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderDetails.Count; i++)
                {
                    if (this.OrderDetails[i].Id == id)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    return this.OrderDetails[rowIndex];
                }
            }

            return null;
        }

        public OrderDetail GetOrderDetailByRowIndex(int rowIndex)
        {
            if (this.OrderDetails != null && rowIndex < this.OrderDetails.Count)
            {
                return this.OrderDetails[rowIndex];
            }

            return null;
        }

        public OrderDetail GetOrderDetailBySequence(int sequence)
        {
            if (this.OrderDetails != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderDetails.Count; i++)
                {
                    if (this.OrderDetails[i].Sequence == sequence)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    return this.OrderDetails[rowIndex];
                }
            }

            return null;
        }

        public OrderDetail GetOrderDetailByFlowDetailIdAndItemCode(int flowDetailId, string itemCode)
        {
            if (this.OrderDetails != null && this.OrderDetails.Count > 0)
            {
                foreach (OrderDetail orderDetail in this.OrderDetails)
                {
                    if (orderDetail.FlowDetail != null && orderDetail.FlowDetail.Id == flowDetailId
                        && orderDetail.FlowDetail.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper())
                    {
                        return orderDetail;
                    }
                }
            }

            return null;
        }

        public void RemoveOrderDetailById(int id)
        {
            if (this.OrderDetails != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderDetails.Count; i++)
                {
                    if (this.OrderDetails[i].Id == id)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    this.OrderDetails.RemoveAt(rowIndex);
                }
            }
        }

        public void RemoveOrderDetailByRowIndex(int rowIndex)
        {
            if (this.OrderDetails != null)
            {
                this.OrderDetails.RemoveAt(rowIndex);
            }
        }

        public void RemoveOrderDetailBySequence(int sequence)
        {
            if (this.OrderDetails != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderDetails.Count; i++)
                {
                    if (this.OrderDetails[i].Sequence == sequence)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    this.OrderDetails.RemoveAt(rowIndex);
                }
            }
        }

        public void AddOrderOperation(OrderOperation orderOperation)
        {
            if (this.OrderOperations == null)
            {
                this.OrderOperations = new List<OrderOperation>();
                this.OrderOperations.Add(orderOperation);
            }
            else
            {
                //查找Op，插入到合适位置
                int position = this.OrderOperations.Count;
                for (int i = 0; i < this.OrderOperations.Count; i++)
                {
                    OrderOperation oo = this.OrderOperations[i];
                    if (oo.Operation > orderOperation.Operation)
                    {
                        position = i;
                        break;
                    }

                    //判断是否已经有相同工序号的orderOperation，如果没有才添加
                    if (oo.Operation == orderOperation.Operation)
                    {
                        return;
                    }
                }

                this.OrderOperations.Insert(position, orderOperation);
            }
        }

        public OrderOperation GetOrderOperationById(int id)
        {
            if (this.OrderOperations != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderOperations.Count; i++)
                {
                    if (this.OrderOperations[i].Id == id)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    return this.OrderOperations[rowIndex];
                }
            }

            return null;
        }

        public OrderOperation GetOrderOperationByRowIndex(int rowIndex)
        {
            if (this.OrderOperations != null && rowIndex < this.OrderOperations.Count)
            {
                return this.OrderOperations[rowIndex];
            }

            return null;
        }

        public OrderOperation GetOrderOperationByOperation(int operation)
        {
            if (this.OrderOperations != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderOperations.Count; i++)
                {
                    if (this.OrderOperations[i].Operation == operation)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    return this.OrderOperations[rowIndex];
                }
            }

            return null;
        }

        public void RemoveOrderOperationById(int id)
        {
            if (this.OrderOperations != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderOperations.Count; i++)
                {
                    if (this.OrderOperations[i].Id == id)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    this.OrderOperations.RemoveAt(rowIndex);
                }
            }
        }

        public void RemoveOrderOperationByRowIndex(int rowIndex)
        {
            if (this.OrderOperations != null)
            {
                this.OrderOperations.RemoveAt(rowIndex);
            }
        }

        public void RemoveOrderOperationByOperation(int operation)
        {
            if (this.OrderOperations != null)
            {
                int rowIndex = -1;
                for (int i = 0; i < this.OrderOperations.Count; i++)
                {
                    if (this.OrderOperations[i].Operation == operation)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex > -1)
                {
                    this.OrderOperations.RemoveAt(rowIndex);
                }
            }
        }

        public decimal OrderAmountFrom
        {
            get
            {
                decimal discountFrom = this.DiscountFrom.HasValue ? this.DiscountFrom.Value : 0;
                return this.TotalOrderDetailAmountFrom - discountFrom;
            }
        }

        public decimal OrderAmountTo
        {
            get
            {
                decimal discountTo = this.DiscountTo.HasValue ? this.DiscountTo.Value : 0;
                return this.TotalOrderDetailAmountTo - discountTo;
            }
        }

        public decimal DiscountRateFrom
        {
            get
            {
                decimal discountRateFrom = 0;
                decimal discountFrom = this.DiscountFrom == null ? 0 : (decimal)this.DiscountFrom;
                if (this.OrderAmountFrom != 0)
                {
                    discountRateFrom = 100 * discountFrom / this.TotalOrderDetailAmountFrom;
                }
                return discountRateFrom;
            }
        }

        public decimal DiscountRateTo
        {
            get
            {
                decimal discountRateTo = 0;
                decimal discountTo = this.DiscountTo == null ? 0 : (decimal)this.DiscountTo;
                if (this.OrderAmountTo != 0)
                {
                    discountRateTo = 100 * discountTo / this.TotalOrderDetailAmountTo;
                }
                return discountRateTo;
            }
        }

        public decimal TotalOrderDetailAmountFrom
        {
            get
            {
                decimal orderDetailAmountFrom = 0;
                if (this.OrderDetails != null)
                {
                    foreach (OrderDetail orderDetail in OrderDetails)
                    {
                        orderDetailAmountFrom += orderDetail.AmountFrom;
                    }
                }
                return orderDetailAmountFrom;
            }
        }

        public decimal TotalOrderDetailAmountTo
        {
            get
            {
                decimal orderDetailAmountTo = 0;
                if (this.OrderDetails != null)
                {
                    foreach (OrderDetail orderDetail in OrderDetails)
                    {
                        orderDetailAmountTo += orderDetail.AmountTo;
                    }
                }
                return orderDetailAmountTo;
            }
        }

        //给portal加的
        public string WinDate
        {
            get
            {
                if (this.WindowTime != null)
                {
                    return this.WindowTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    return null;
                }
            }
        }

        public string WinTime
        {
            get
            {
                if (this.WindowTime != null)
                {
                    return this.WindowTime.ToString("hh:mm:ss");
                }
                else
                {
                    return null;
                }
            }
        }

        public string DeliverySite
        {
            get
            {
                string shipToAddress = this.ShipTo.Address;
                if (this.DockDescription != string.Empty)
                {
                    shipToAddress = shipToAddress + "@" + this.DockDescription;
                }
                return shipToAddress;
            }
        }

        public bool IsConfirmed
        {
            get
            {
                bool isConfirmed = false;
                if (this.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                {
                    isConfirmed = true;
                }
                return isConfirmed;
            }
        }

        //private string _subType;
        //public string SubType
        //{
        //    get
        //    {
        //        return this._subType;
        //    }
        //    set
        //    {
        //        this._subType = value;
        //    }
        //}

        #endregion
    }
}