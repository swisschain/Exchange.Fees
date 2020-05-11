using System;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Repositories.Entities;

namespace Fees.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CashOperationsFee, CashOperationsFeeEntity>(MemberList.Destination);
            CreateMap<CashOperationsFeeEntity, CashOperationsFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created.UtcDateTime)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified.UtcDateTime)));

            CreateMap<TradingFee, TradingFeeEntity>(MemberList.Destination);
            CreateMap<TradingFeeEntity, TradingFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified)));

            CreateMap<TradingFeeLevel, TradingFeeLevelEntity>(MemberList.Destination);
            CreateMap<TradingFeeLevelEntity, TradingFeeLevel>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified)));

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryEntity>(MemberList.Destination);
            CreateMap<CashOperationsFeeHistoryEntity, CashOperationsFeeHistory>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => Convert(src.Timestamp)));
        }

        private static DateTime Convert(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.UtcDateTime;
        }
    }
}
