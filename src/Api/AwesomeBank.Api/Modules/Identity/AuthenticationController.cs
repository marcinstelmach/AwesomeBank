namespace AwesomeBank.Api.Modules.Identity
{
    using System.Net;
    using System.Threading.Tasks;
    using AwesomeBank.Api.Filters;
    using AwesomeBank.Api.Models;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Commands;
    using AwesomeBank.Identity.Application.Dtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AuthenticationController(IMapper mapper, IBus bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        [HttpPost]
        [ValidateModelFilter]
        [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignInUserAsync([FromBody] SignInUserViewModel viewModel)
        {
            var signInUserCommand = _mapper.Map<SignInUserViewModel, SignInUserCommand>(viewModel);
            var tokenDto = await _bus.ExecuteCommandAsync(signInUserCommand);
            var tokenViewModel = _mapper.Map<TokenDto, TokenViewModel>(tokenDto);

            return Ok(tokenViewModel);
        }
    }
}