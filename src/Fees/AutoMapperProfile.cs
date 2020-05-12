using AutoMapper;
using Fees.Domain.Entities;
using Fees.WebApi.Models.Audit.CashOperationsFee;
using Fees.WebApi.Models.CashOperationsFee;
using Fees.WebApi.Models.Settings;
using Fees.WebApi.Models.TradingFee;
using CashOperationsFeeModel = Fees.WebApi.Models.CashOperationsFee.CashOperationsFeeModel;

namespace Fees
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            WebApi();
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

            CreateMap<Settings, SettingsModel>(MemberList.Destination);

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryModel>(MemberList.Source);
        }
    }
}
