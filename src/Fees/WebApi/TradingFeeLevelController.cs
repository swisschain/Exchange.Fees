using System;
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

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/trading")]
    public class TradingFeeLevelController : ControllerBase
    {
        private readonly ITradingFeeLevelService _tradingFeeLevelService;
        private readonly IMapper _mapper;

        public TradingFeeLevelController(ITradingFeeLevelService tradingFeeLevelService, IMapper mapper)
        {
            _tradingFeeLevelService = tradingFeeLevelService;
            _mapper = mapper;
        }

        [HttpGet("{tradingFeeId}/levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLevelsAsync(Guid tradingFeeId)
        {
            var brokerId = User.GetTenantId();

            var domain = await _tradingFeeLevelService.GetAllAsync(tradingFeeId, brokerId);

            if (domain == null)
                return NotFound();

            var model = _mapper.Map<TradingFeeLevelModel[]>(domain);

            return Ok(ResponseModel<TradingFeeLevelModel[]>.Ok(model));
        }

        [HttpPost("levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] TradingFeeLevelAddModel model)
        {
            var brokerId = User.GetTenantId();

            var domain = _mapper.Map<TradingFeeLevel>(model);

            var newDomain = await _tradingFeeLevelService.AddAsync(domain, brokerId);

            var newModel = _mapper.Map<TradingFeeLevelModel>(newDomain);

            return Ok(ResponseModel<TradingFeeLevelModel>.Ok(newModel));
        }

        [HttpPut("levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] TradingFeeLevelEditModel model)
        {
            var brokerId = User.GetTenantId();

            var domain = _mapper.Map<TradingFeeLevel>(model);

            var newDmain = await _tradingFeeLevelService.UpdateAsync(domain, brokerId);

            var newModel = _mapper.Map<TradingFeeLevelModel>(newDmain);

            return Ok(ResponseModel<TradingFeeLevelModel>.Ok(newModel));
        }

        [HttpDelete("levels/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var brokerId = User.GetTenantId();

            await _tradingFeeLevelService.DeleteAsync(id, brokerId);

            return Ok();
        }
    }
}
