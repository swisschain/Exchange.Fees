using System;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.WebApi.Models.Audit.CashOperationsFee;
using Fees.WebApi.Models.CashOperationsFee;
using Fees.WebApi.Models.TradingFee;
using Google.Protobuf.WellKnownTypes;
using CashOperationsFeeModel = Fees.WebApi.Models.CashOperationsFee.CashOperationsFeeModel;

namespace Fees
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            WebApi();

            Client();
        }

        private void WebApi()
        {
            CreateMap<CashOperationsFee, CashOperationsFeeModel>(MemberList.Destination);
            CreateMap<CashOperationsFeeAddModel, CashOperationsFee>(MemberList.Source);
            CreateMap<CashOperationsFeeEditModel, CashOperationsFee>(MemberList.Source);

            CreateMap<TradingFee, TradingFeeModel>(MemberList.Destination);
            CreateMap<TradingFeeAddModel, TradingFee>(MemberList.Source);
            CreateMap<TradingFeeEditModel, TradingFee>(MemberList.Source);

            CreateMap<TradingFeeLevel, TradingFeeLevelModel>(MemberList.Destination);
            CreateMap<TradingFeeLevelAddModel, TradingFeeLevel>(MemberList.Source);
            CreateMap<TradingFeeLevelEditModel, TradingFeeLevel>(MemberList.Source);

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryModel>(MemberList.Source);
        }

        private void Client()
        {
            CreateMap<CashOperationsFee,
                Swisschain.Exchange.Fees.Client.Models.CashOperationsFees.CashOperationsFeeModel>(MemberList.Destination);

            CreateMap<TradingFee,
                Swisschain.Exchange.Fees.Client.Models.TradingFees.TradingFeeModel>(MemberList.Destination);

            CreateMap<TradingFeeLevel,
                Swisschain.Exchange.Fees.Client.Models.TradingFees.TradingFeeLevelModel>(MemberList.Destination);
        }
    }
}
