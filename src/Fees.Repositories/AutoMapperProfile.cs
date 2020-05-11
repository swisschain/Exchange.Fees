using System;
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
            CreateMap<CashOperationsFeeData, CashOperationsFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified)));

            CreateMap<TradingFee, TradingFeeData>(MemberList.Destination);
            CreateMap<TradingFeeData, TradingFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified)));

            CreateMap<TradingFeeLevel, TradingFeeLevelData>(MemberList.Destination);
            CreateMap<TradingFeeLevelData, TradingFeeLevel>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert(src.Created)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => Convert(src.Modified)));

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryData>(MemberList.Destination);
            CreateMap<CashOperationsFeeHistoryData, CashOperationsFeeHistory>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => Convert(src.Timestamp)));
        }

        private static DateTime Convert(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            if (dateTime.Kind == DateTimeKind.Local)
                dateTime = dateTime.ToUniversalTime();

            return dateTime;
        }
    }
}
