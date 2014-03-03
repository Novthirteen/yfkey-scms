using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Production;

namespace com.Sconit.Service.Business
{
    public interface IExecuteMgr
    {
        void CancelOperation(Resolver resolver);

        void CancelRepackOperation(Resolver resolver);

        IList<LocationLotDetail> ConvertTransformersToLocationLotDetails(List<Transformer> transformerList, bool isPutAway);

        LocationLotDetail ConvertTransformerDetailToLocationLotDetail(TransformerDetail transformerDetail, bool isPutAway);

        IList<InspectOrderDetail> ConvertResolverToInspectOrderDetails(Resolver resolver);

        IList<RepackDetail> ConvertTransformerListToRepackDetail(IList<Transformer> transformerList);

        IList<OrderDetail> ConvertResolverToOrderDetails(Resolver resolver, Flow flow);

        IList<OrderDetail> ConvertResolverToOrderDetails(Resolver resolver);

        IList<MaterialIn> ConvertTransformersToMaterialIns(List<Transformer> transformers);

        void OrderReturn(Resolver resolver);

        Resolver GetReceiptNotes(Resolver resolver, params string[] orderTypes);

        void PrintReceipt(Resolver resolver);
    }
}
