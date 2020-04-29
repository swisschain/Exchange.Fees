﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Services;
using Fees.Exceptions;
using Fees.WebApi.Models.CashOperationsFee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/cash-operations-fee")]
    public class CashOperationsFeeController : ControllerBase
    {
        private readonly ICashOperationsFeeService _cashOperationsFeeService;
        private readonly IMapper _mapper;

        public CashOperationsFeeController(ICashOperationsFeeService cashOperationsFeeService, IMapper mapper)
        {
            _cashOperationsFeeService = cashOperationsFeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<Paginated<CashOperationsFeeModel, Guid>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManyAsync([FromQuery] CashOperationsFeeRequestManyModel request)
        {
            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = Guid.Parse(User.GetTenantId());

            var assets = await _cashOperationsFeeService.GetAllAsync(brokerId, request.Asset, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<List<CashOperationsFeeModel>>(assets);

            var payload = result.Paginate(request, Url, x => x.Id);

            return Ok(ResponseModel<Paginated<CashOperationsFeeModel, Guid>>.Ok(payload));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseModel<CashOperationsFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var brokerId = Guid.Parse(User.GetTenantId());

            var asset = await _cashOperationsFeeService.GetAsync(id, brokerId);

            if (asset == null)
                return NotFound();

            var model = _mapper.Map<CashOperationsFeeModel>(asset);

            return Ok(ResponseModel<CashOperationsFeeModel>.Ok(model));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<CashOperationsFeeModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] CashOperationsFeeAddModel model)
        {
            var brokerId = Guid.Parse(User.GetTenantId());

            var domain = _mapper.Map<CashOperationsFee>(model);
            domain.BrokerId = brokerId;

            var newDomain = await _cashOperationsFeeService.AddAsync(domain);

            var newModel = _mapper.Map<CashOperationsFeeModel>(newDomain);

            return Ok(ResponseModel<CashOperationsFeeModel>.Ok(newModel));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseModel<CashOperationsFeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] CashOperationsFeeEditModel model)
        {
            var brokerId = Guid.Parse(User.GetTenantId());

            var domain = _mapper.Map<CashOperationsFee>(model);
            domain.BrokerId = brokerId;

            var newDmain = await _cashOperationsFeeService.UpdateAsync(domain);

            var newModel = _mapper.Map<CashOperationsFeeModel>(newDmain);

            return Ok(ResponseModel<CashOperationsFeeModel>.Ok(newModel));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var brokerId = Guid.Parse(User.GetTenantId());

            await _cashOperationsFeeService.DeleteAsync(id, brokerId);

            return Ok();
        }
    }
}
