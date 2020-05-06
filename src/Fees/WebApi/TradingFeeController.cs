using System;
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
    public class TradingFeeController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TradingFeeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(new TradingFeeModel());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] TradingFeeAddModel model)
        {
            return Ok(new TradingFeeModel());
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseModel<TradingFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] TradingFeeEditModel model)
        {
            return Ok(new TradingFeeModel());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok();
        }
    }
}
