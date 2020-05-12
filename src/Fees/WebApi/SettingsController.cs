using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Services;
using Fees.Exceptions;
using Fees.WebApi.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;

namespace Fees.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService settingsService, IMapper mapper)
        {
            _settingsService = settingsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<SettingsModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var brokerId = User.GetTenantId();

            var domain = await _settingsService.GetAsync(brokerId);

            if (domain == null)
                return NotFound();

            var model = _mapper.Map<SettingsModel>(domain);

            return Ok(ResponseModel<SettingsModel>.Ok(model));
        }
    }
}
