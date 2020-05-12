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
            CreateMap<CashOperationsFee, CashOperationsFeeEntity>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Modified, DateTimeKind.Utc)));
            CreateMap<CashOperationsFeeEntity, CashOperationsFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.UtcDateTime))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified.UtcDateTime));

            CreateMap<TradingFee, TradingFeeEntity>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Modified, DateTimeKind.Utc)));
            CreateMap<TradingFeeEntity, TradingFee>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.UtcDateTime))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified.UtcDateTime));

            CreateMap<TradingFeeLevel, TradingFeeLevelEntity>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Modified, DateTimeKind.Utc)));
            CreateMap<TradingFeeLevelEntity, TradingFeeLevel>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.UtcDateTime))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified.UtcDateTime));

            CreateMap<Settings, SettingsEntity>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc)))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Modified, DateTimeKind.Utc)));
            CreateMap<SettingsEntity, Settings>(MemberList.Destination)
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.UtcDateTime))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified.UtcDateTime));

            CreateMap<CashOperationsFeeHistory, CashOperationsFeeHistoryEntity>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Timestamp, DateTimeKind.Utc)));
            CreateMap<CashOperationsFeeHistoryEntity, CashOperationsFeeHistory>(MemberList.Destination)
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.UtcDateTime));
        }
    }
}
