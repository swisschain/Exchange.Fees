using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Services;
using Fees.Exceptions;
using Fees.WebApi.Models.TradingFee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/trading")]
    public class TradingFeeController : ControllerBase
    {
        private readonly ITradingFeeService _tradingFeeService;
        private readonly IMapper _mapper;

        public TradingFeeController(ITradingFeeService tradingFeeService, IMapper mapper)
        {
            _tradingFeeService = tradingFeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<Paginated<TradingFeeModel, Guid>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManyAsync([FromQuery] TradingFeeRequestManyModel request)
        {
            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = User.GetTenantId();

            var tradingFees = await _tradingFeeService.GetAllAsync(brokerId, request.AssetPair, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<TradingFeeModel[]>(tradingFees);

            var payload = result.Paginate(request, Url, x => x.Id);

            return Ok(ResponseModel<Paginated<TradingFeeModel, Guid>>.Ok(payload));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var brokerId = User.GetTenantId();

            var domain = await _tradingFeeService.GetAsync(id, brokerId);

            if (domain == null)
                return NotFound();

            var model = _mapper.Map<TradingFeeModel>(domain);

            return Ok(ResponseModel<TradingFeeModel>.Ok(model));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] TradingFeeAddModel model)
        {
            var brokerId = User.GetTenantId();

            var domain = _mapper.Map<TradingFee>(model);
            domain.BrokerId = brokerId;

            var newDomain = await _tradingFeeService.AddAsync(domain);

            var newModel = _mapper.Map<TradingFeeModel>(newDomain);

            return Ok(ResponseModel<TradingFeeModel>.Ok(newModel));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] TradingFeeEditModel model)
        {
            var brokerId = User.GetTenantId();

            var domain = _mapper.Map<TradingFee>(model);
            domain.BrokerId = brokerId;

            var newDmain = await _tradingFeeService.UpdateAsync(domain);

            var newModel = _mapper.Map<TradingFeeModel>(newDmain);

            return Ok(ResponseModel<TradingFeeModel>.Ok(newModel));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var brokerId = User.GetTenantId();

            await _tradingFeeService.DeleteAsync(id, brokerId);

            return Ok();
        }
    }
}
