using AutoMapper;
using Fees.Domain.Entities;

namespace Fees.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CashOperationsFee, CashOperationsFeeHistory>(MemberList.Destination)
                .ForMember(dest => dest.CashOperationsFeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.OperationType, opt => opt.Ignore())
                .ForMember(x => x.Timestamp, opt => opt.Ignore());
        }
    }
}
