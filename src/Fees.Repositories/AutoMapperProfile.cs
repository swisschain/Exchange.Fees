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
        }
    }
}
