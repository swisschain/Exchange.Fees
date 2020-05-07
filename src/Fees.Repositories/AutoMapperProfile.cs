using AutoMapper;
using Fees.Domain.Entities;
using Fees.Repositories.DTOs;

namespace Fees.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CashOperationsFee, CashOperationsFeeData>(MemberList.Destination);
            CreateMap<CashOperationsFeeData, CashOperationsFee>(MemberList.Destination);

            CreateMap<TradingFee, TradingFeeData>(MemberList.Destination);
            CreateMap<TradingFeeData, TradingFee>(MemberList.Destination);

            CreateMap<TradingFeeLevel, TradingFeeLevelData>(MemberList.Destination);
            CreateMap<TradingFeeLevelData, TradingFeeLevel>(MemberList.Destination);

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryData>(MemberList.Destination);
            CreateMap<CashOperationsFeeHistoryData, CashOperationsFeeHistory>(MemberList.Destination);
        }
    }
}
