using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Services;
using Fees.Exceptions;
using Fees.WebApi.Models.Audit.CashOperationsFee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/audit")]
    public class AuditController : ControllerBase
    {
        private readonly ICashOperationsFeeService _cashOperationsFeeService;
        private readonly IMapper _mapper;

        public AuditController(ICashOperationsFeeService cashOperationsFeeService, IMapper mapper)
        {
            _cashOperationsFeeService = cashOperationsFeeService;
            _mapper = mapper;
        }

        [HttpGet("cash-operations")]
        [ProducesResponseType(typeof(ResponseModel<Paginated<CashOperationsFeeHistoryModel, long>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCashOperationsFeeHistoryManyAsync([FromQuery] CashOperationsFeeHistoryRequestManyModel request)
        {
            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = User.GetTenantId();

            var cashOperationsFeeHistories = await _cashOperationsFeeService.GetAllHistoriesAsync(request.CashOperationFeeId, brokerId, request.UserId, request.Asset, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<List<CashOperationsFeeHistoryModel>>(cashOperationsFeeHistories);

            var payload = result.Paginate(request, Url, x => x.Id);

            return Ok(ResponseModel<Paginated<CashOperationsFeeHistoryModel, long>>.Ok(payload));
        }
    }
}
