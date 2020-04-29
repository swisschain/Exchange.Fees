using AutoMapper;
using Fees.Domain.Entities;
using Fees.WebApi.Models.CashOperationsFee;

namespace Fees
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CashOperationsFee, CashOperationsFeeModel>(MemberList.Destination);
            CreateMap<CashOperationsFeeAddModel, CashOperationsFee>(MemberList.Source);
            CreateMap<CashOperationsFeeEditModel, CashOperationsFee>(MemberList.Source);
        }
    }
}
