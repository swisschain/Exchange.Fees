using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Exceptions;
using Fees.WebApi.Models.TradingFee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/trading")]
    public class TradingFeeLevelController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TradingFeeLevelController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{tradingFeeId}/levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLevelsAsync(Guid tradingFeeId)
        {
            return Ok(new List<TradingFeeLevelModel>());
        }

        [HttpPost("{tradingFeeId}/levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] TradingFeeLevelAddModel model)
        {
            return Ok(new TradingFeeLevelModel());
        }

        [HttpPut("levels")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeLevelModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] TradingFeeLevelEditModel model)
        {
            return Ok(new TradingFeeLevelModel());
        }

        [HttpDelete("levels/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok();
        }
    }
}
